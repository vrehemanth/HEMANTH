import { Component, inject, signal } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { Router, RouterLink, ActivatedRoute } from '@angular/router';
import { AuthService } from '../../../core/services/auth.service';
import { ToastService } from '../../../core/services/toast.service';

@Component({
  selector: 'app-reset-password',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, RouterLink],
  templateUrl: './reset-password.html'
})
export class ResetPasswordComponent {
  private fb = inject(FormBuilder);
  private authService = inject(AuthService);
  private toastService = inject(ToastService);
  private router = inject(Router);
  private route = inject(ActivatedRoute);

  isLoading = signal(false);

  onPasswordPaste(event: ClipboardEvent) {
    event.preventDefault();
    this.toastService.warning('Password pasting is not allowed for security reasons.');
  }

  resetForm = this.fb.group({
    email: ['', [Validators.required, Validators.email]],
    token: ['', Validators.required],
    newPassword: ['', [Validators.required, Validators.minLength(6)]],
    confirmPassword: ['', Validators.required]
  }, { validators: this.passwordMatchValidator });

  constructor() {
    this.route.queryParams.subscribe(params => {
      if (params['email']) {
        this.resetForm.patchValue({ email: params['email'] });
      }
      if (params['token']) {
        this.resetForm.patchValue({ token: params['token'] });
      }
    });
  }

  passwordMatchValidator(g: any) {
    return g.get('newPassword').value === g.get('confirmPassword').value
      ? null : { mismatch: true };
  }

  onSubmit() {
    if (this.resetForm.invalid) {
      this.resetForm.markAllAsTouched();
      return;
    }

    this.isLoading.set(true);

    const data = {
      email: this.resetForm.value.email!,
      token: this.resetForm.value.token!,
      newPassword: this.resetForm.value.newPassword!
    };

    this.authService.resetPassword(data).subscribe({
      next: (res: any) => {
        this.isLoading.set(false);
        this.toastService.success(res?.message || res || "Password reset successfully. You can now log in.");
        this.router.navigate(['/auth/login']);
      },
      error: (err) => {
        this.isLoading.set(false);
        this.toastService.error(err.error?.message || "Invalid or expired token.");
      }
    });
  }
}
