import { Component, inject, signal, OnInit } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { AuthService } from '../../../core/services/auth.service';
import { Router, RouterLink } from '@angular/router';
import { ToastService } from '../../../core/services/toast.service';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, RouterLink],
  templateUrl: './login.html',
  styleUrls: ['./login.css']
})
export class LoginComponent implements OnInit {
  private fb = inject(FormBuilder);
  private authService = inject(AuthService);
  private toastService = inject(ToastService);
  private router = inject(Router);

  ngOnInit(): void {
    // Clear session if user navigates back to login from active dashboard
    this.authService.clearSession();
  }

  isLoading = signal(false);
  errorMessage = signal<string | null>(null);
  isAccountBlocked = signal(false);
  showPassword = signal(false);

  onPasswordPaste(event: ClipboardEvent) {
    event.preventDefault();
    this.toastService.warning('Password pasting is not allowed for security reasons.');
  }

  // --- CAPTCHA STATE ---
  isCaptchaVerified = signal(false);
  showCaptchaModal = signal(false);
  captchaError = signal(false);
  selectedCaptchaIndices = signal<number[]>([]);
  captchaTarget = 'Star';

  captchaCorrectIndices: number[] = [];
  captchaImages: string[] = [];

  constructor() {
    this.refreshCaptcha();
  }

  loginForm = this.fb.group({
    email: ['', [Validators.required, Validators.email, Validators.pattern(/^[a-zA-Z0-9._%+\-]+@gmail\.com$/)]],
    password: ['', Validators.required]
  });

  // --- CAPTCHA LOGIC ---
  refreshCaptcha() {
    this.selectedCaptchaIndices.set([]);
    this.captchaError.set(false);

    // Pick 3 or 4 random indices to be stars
    const targetCount = Math.floor(Math.random() * 2) + 3;
    const shuffled = [0, 1, 2, 3, 4, 5, 6, 7, 8].sort(() => 0.5 - Math.random());
    this.captchaCorrectIndices = shuffled.slice(0, targetCount);

    // Dynamic locally-generated SVG generator for 100% network reliability
    const generateShapeSVG = (isTarget: boolean) => {
      const colors = ['%23EF4444', '%233B82F6', '%2310B981', '%23F59E0B', '%238B5CF6', '%23EC4899', '%2314B8A6'];
      const bg = colors[Math.floor(Math.random() * colors.length)];

      let shape = '';
      if (isTarget) {
        // A recognizable Star
        shape = `<polygon points="50,15 61,35 82,38 67,54 70,76 50,65 30,76 33,54 18,38 39,35" fill="white"/>`;
      } else {
        // Decoys
        const rand = Math.random();
        if (rand < 0.33) {
          // Circle
          shape = `<circle cx="50" cy="50" r="25" fill="white"/>`;
        } else if (rand < 0.66) {
          // Square
          shape = `<rect x="25" y="25" width="50" height="50" fill="white" rx="5"/>`;
        } else {
          // Triangle
          shape = `<polygon points="50,25 75,75 25,75" fill="white" stroke-linejoin="round"/>`;
        }
      }

      const svgStr = `<svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 100 100"><rect width="100" height="100" fill="${bg}"/>${shape}</svg>`;
      return 'data:image/svg+xml;charset=UTF-8,' + svgStr;
    };

    // Build the grid
    const newImages = [];
    for (let i = 0; i < 9; i++) {
      const isTargetShape = this.captchaCorrectIndices.includes(i);
      newImages.push(generateShapeSVG(isTargetShape));
    }
    this.captchaImages = newImages;
  }

  openCaptcha() {
    if (this.isCaptchaVerified()) return;
    this.showCaptchaModal.set(true);
    // Auto refresh if they failed previously
    if (this.captchaError()) {
      this.refreshCaptcha();
    }
  }

  toggleCaptchaImage(index: number) {
    const current = this.selectedCaptchaIndices();
    if (current.includes(index)) {
      this.selectedCaptchaIndices.set(current.filter(i => i !== index));
    } else {
      this.selectedCaptchaIndices.set([...current, index]);
    }
  }

  verifyCaptcha() {
    const selected = this.selectedCaptchaIndices().sort();
    const correct = [...this.captchaCorrectIndices].sort();

    const isMatch = selected.length === correct.length && selected.every((val, i) => val === correct[i]);

    if (isMatch) {
      this.isCaptchaVerified.set(true);
      this.showCaptchaModal.set(false);
      this.captchaError.set(false);
    } else {
      this.captchaError.set(true);
      this.selectedCaptchaIndices.set([]);
    }
  }

  closeCaptcha() {
    this.showCaptchaModal.set(false);
  }

  onSubmit() {
    if (this.loginForm.invalid || !this.isCaptchaVerified()) {
      this.loginForm.markAllAsTouched();
      return;
    }

    this.isLoading.set(true);
    this.errorMessage.set(null);

    const creds = this.loginForm.value as { email: string; password: string };

    this.authService.login(creds).subscribe({
      next: () => {
        this.isLoading.set(false);
      },
      error: (err) => {
        this.isLoading.set(false);
        const errorMsg = err.error?.message || 'Invalid email or password provided.';
        this.errorMessage.set(errorMsg);

        // Detect permanent block to show custom error page
        if (errorMsg.toLowerCase().includes('blocked') || errorMsg.toLowerCase().includes('exceeded')) {
          this.router.navigate(['/account-blocked']);
        }
      }
    });
  }
}
