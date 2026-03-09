import { Component, inject, signal, OnInit } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { Router, RouterLink } from '@angular/router';
import { AuthService } from '../../../core/services/auth.service';
import { ToastService } from '../../../core/services/toast.service';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, RouterLink],
  templateUrl: './register.html',
  styleUrls: ['./register.css']
})
export class RegisterComponent implements OnInit {
  private fb = inject(FormBuilder);
  private authService = inject(AuthService);
  private toastService = inject(ToastService);
  private router = inject(Router);

  ngOnInit(): void {
    // Clear session if user navigates back to register from active dashboard
    this.authService.clearSession();
  }

  isLoading = signal(false);

  onPasswordPaste(event: ClipboardEvent) {
    event.preventDefault();
    this.toastService.warning('Password pasting is not allowed for security reasons.');
  }

  registerForm = this.fb.group({
    name: ['', Validators.required],
    email: ['', [Validators.required, Validators.email, Validators.pattern(/^[a-zA-Z0-9._%+\-]+@gmail\.com$/)]],
    password: ['', [Validators.required, Validators.minLength(6)]]
  });

  onSubmit() {
    if (this.registerForm.invalid) {
      this.registerForm.markAllAsTouched();
      return;
    }

    this.isLoading.set(true);
    const data = this.registerForm.value as any;

    this.authService.register(data).subscribe({
      next: () => {
        this.isLoading.set(false);
        this.toastService.success("Registration successful! Please login to complete your profile.");
        this.router.navigate(['/auth/login']);
      },
      error: (err) => {
        this.isLoading.set(false);
        this.toastService.error(err.error?.message || "Registration failed. Please try again.");
      }
    });
  }
}
