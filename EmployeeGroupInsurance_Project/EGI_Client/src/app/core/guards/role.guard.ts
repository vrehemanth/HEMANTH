import { inject } from '@angular/core';
import { Router, type CanActivateFn } from '@angular/router';

export const roleGuard: CanActivateFn = (route, state) => {
    const router = inject(Router);

    if (typeof window !== 'undefined' && localStorage) {
        const role = localStorage.getItem('role');
        const token = localStorage.getItem('jwt');

        if (!token) {
            router.navigate(['/auth/login']);
            return false;
        }

        const expectedRoles = route.data?.['roles'] as Array<string>;

        if (expectedRoles && role && expectedRoles.includes(role)) {
            return true;
        }
    }

    router.navigate(['/auth/login']);
    return false;
};
