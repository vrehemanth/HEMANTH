import { Injectable, signal } from '@angular/core';

export interface ToastMessage {
    id: number;
    message: string;
    type: 'success' | 'error' | 'info' | 'warning';
    duration?: number;
}

@Injectable({
    providedIn: 'root'
})
export class ToastService {
    toasts = signal<ToastMessage[]>([]);
    private nextId = 0;

    show(message: string, type: 'success' | 'error' | 'info' | 'warning' = 'info', duration: number = 4000) {
        const id = this.nextId++;
        this.toasts.update(t => [...t, { id, message, type, duration }]);

        if (duration > 0) {
            setTimeout(() => this.remove(id), duration);
        }
    }

    success(message: string, duration?: number) {
        this.show(message, 'success', duration);
    }

    error(message: string, duration?: number) {
        this.show(message, 'error', duration);
    }

    info(message: string, duration?: number) {
        this.show(message, 'info', duration);
    }

    warning(message: string, duration?: number) {
        this.show(message, 'warning', duration);
    }

    remove(id: number) {
        this.toasts.update(t => t.filter(toast => toast.id !== id));
    }
}
