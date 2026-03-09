import { Component, inject, signal } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { AuthService } from '../../../core/services/auth.service';
import { ToastService } from '../../../core/services/toast.service';

@Component({
    selector: 'app-change-password',
    standalone: true,
    imports: [CommonModule, ReactiveFormsModule],
    templateUrl: './change-password.html'
})
export class ChangePasswordComponent {
    private fb = inject(FormBuilder);
    private authService = inject(AuthService);
    private toastService = inject(ToastService);

    isLoading = signal(false);

    onPasswordPaste(event: ClipboardEvent) {
        event.preventDefault();
        this.toastService.warning('Password pasting is not allowed for security reasons.');
    }

    changePasswordForm = this.fb.group({
        newPassword: ['', [Validators.required, Validators.minLength(6)]],
        confirmPassword: ['', Validators.required]
    }, { validators: this.passwordMatchValidator });

    passwordMatchValidator(g: any) {
        return g.get('newPassword').value === g.get('confirmPassword').value
            ? null : { mismatch: true };
    }

    onSubmit() {
        if (this.changePasswordForm.invalid) {
            this.changePasswordForm.markAllAsTouched();
            return;
        }

        this.isLoading.set(true);

        const newPassword = this.changePasswordForm.value.newPassword!;

        this.authService.changePassword(newPassword).subscribe({
            next: (res: any) => {
                this.isLoading.set(false);
                this.toastService.success(res?.message || res || "Password updated successfully.");
                this.changePasswordForm.reset();

                // Navigate back to dashboard now that password is changed
                const user = this.authService.currentUser();
                if (user) {
                    // This uses the existing private navigateByRole logic indirectly by forcing a reload or just calling router
                    // For simplicity, let's just trigger a navigation to home which will re-evaluate guards or dashboard
                    window.location.href = '/'; // Simple way to reset state and re-trigger layout logic
                }
            },
            error: (err) => {
                this.isLoading.set(false);
                this.toastService.error(err.error?.message || "Failed to change password.");
            }
        });
    }
}
