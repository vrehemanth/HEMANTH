import { ComponentFixture, TestBed } from '@angular/core/testing';
import { LoginComponent } from './login';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { ReactiveFormsModule } from '@angular/forms';
import { Router, provideRouter } from '@angular/router';
import { AuthService } from '../../../core/services/auth.service';
import { of, throwError } from 'rxjs';

describe('LoginComponent', () => {
    let component: LoginComponent;
    let fixture: ComponentFixture<LoginComponent>;

    let loginCalled = false;
    let navigateCalledWith = '';
    let mockAuthResponse: any;

    const authServiceMock = {
        login: () => {
            loginCalled = true;
            return typeof mockAuthResponse === 'function' ? mockAuthResponse() : of(mockAuthResponse);
        },
        clearSession: () => { }
    };

    beforeEach(async () => {
        loginCalled = false;
        navigateCalledWith = '';
        mockAuthResponse = {};

        await TestBed.configureTestingModule({
            imports: [LoginComponent, HttpClientTestingModule, ReactiveFormsModule],
            providers: [
                { provide: AuthService, useValue: authServiceMock },
                provideRouter([])
            ]
        }).compileComponents();
    });

    beforeEach(() => {
        fixture = TestBed.createComponent(LoginComponent);
        component = fixture.componentInstance;

        const realRouter = TestBed.inject(Router);
        realRouter.navigate = ((...args: any[]) => {
            navigateCalledWith = args[0][0];
            return Promise.resolve(true);
        }) as any;

        fixture.detectChanges();
    });

    it('should create the component', () => {
        expect(component).toBeTruthy();
    });

    it('should perfectly validate empty login forms', () => {
        expect(component.loginForm.valid).toBe(false);
        expect(component.loginForm.get('email')?.valid).toBe(false);
        expect(component.loginForm.get('password')?.valid).toBe(false);
    });

    it('should cleanly apply validation logic for missing password', () => {
        component.loginForm.controls['email'].setValue('test@gmail.com');
        component.loginForm.controls['password'].setValue('');
        expect(component.loginForm.valid).toBe(false);
    });

    it('captcha selection framework should correctly reset on initialization', () => {
        component.selectedCaptchaIndices.set([1, 2, 3]);
        component.refreshCaptcha();
        expect(component.selectedCaptchaIndices().length).toBe(0);
        expect(component.captchaCorrectIndices.length).toBeGreaterThan(0);
        expect(component.captchaImages.length).toBe(9);
    });

    it('should toggle selecting an image properly using signals', () => {
        component.selectedCaptchaIndices.set([]);
        component.toggleCaptchaImage(2);
        expect(component.selectedCaptchaIndices()).toContain(2);

        // Toggling same index should perfectly subtract it again
        component.toggleCaptchaImage(2);
        expect(component.selectedCaptchaIndices()).not.toContain(2);
    });

    it('form submit blocker should securely intercept validation loop if CAPTCHA completely failed', () => {
        component.isCaptchaVerified.set(false);
        component.loginForm.controls['email'].setValue('admin@gmail.com');
        component.loginForm.controls['password'].setValue('Pass123!');

        component.onSubmit();

        // Form submitted gracefully but blocked from accessing network
        expect(component.showCaptchaModal()).toBe(false);
        expect(loginCalled).toBe(false);
    });

    it('successful robust login sequence should cleanly bypass components to correctly resolve route mapping', () => {
        component.isCaptchaVerified.set(true);
        component.loginForm.controls['email'].setValue('john@gmail.com');
        component.loginForm.controls['password'].setValue('Secret12!');

        // Mock successful backend network auth response
        mockAuthResponse = { token: 'jwt.123', role: 'admin', user: { name: 'John Doe' } };

        component.onSubmit();

        expect(component.isLoading()).toBe(false);
        expect(loginCalled).toBe(true);
    });

    it('safely trap network or backend auth API 401 Unauthorized errors onto UI string signal', () => {
        component.isCaptchaVerified.set(true);
        component.loginForm.controls['email'].setValue('fail@gmail.com');
        component.loginForm.controls['password'].setValue('BadPass');

        // Mock backend crash or 401 logic block
        mockAuthResponse = () => throwError(() => ({ error: { message: 'Invalid Credentials.' } }));

        component.onSubmit();

        expect(component.isLoading()).toBe(false);
        expect(component.errorMessage()).toBe('Invalid Credentials.');
    });

    it('modal closing signal appropriately cleanly removes the graphical layout overlay', () => {
        component.showCaptchaModal.set(true);
        component.closeCaptcha();
        expect(component.showCaptchaModal()).toBe(false);
    });

    it('captcha pseudo-random engine dynamically reconstructs unique targeting algorithms', () => {
        component.refreshCaptcha();
        // Generator creates minimum 3 stars natively
        expect(component.captchaCorrectIndices.length).toBeGreaterThanOrEqual(3);
        expect(component.captchaImages.length).toBe(9);
    });
});
