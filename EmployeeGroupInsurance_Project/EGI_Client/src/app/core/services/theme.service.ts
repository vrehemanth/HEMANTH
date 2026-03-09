import { Injectable, signal, effect } from '@angular/core';

@Injectable({
    providedIn: 'root'
})
export class ThemeService {
    isDarkMode = signal<boolean>(false);

    constructor() {
        this.initTheme();
        effect(() => {
            const isDark = this.isDarkMode();
            if (typeof window !== 'undefined' && typeof document !== 'undefined') {
                const root = document.documentElement;
                if (isDark) {
                    root.classList.add('dark');
                } else {
                    root.classList.remove('dark');
                }
                localStorage.setItem('theme', isDark ? 'dark' : 'light');
            }
        }); 
    }

    private initTheme() {
        if (typeof window !== 'undefined' && typeof document !== 'undefined') {
            const storedTheme = localStorage.getItem('theme');
            if (storedTheme === 'dark' || (!storedTheme && window.matchMedia('(prefers-color-scheme: dark)').matches)) {
                this.isDarkMode.set(true);
            }
        }
    }

    toggleTheme() {
        this.isDarkMode.update(value => !value);
    }
}
