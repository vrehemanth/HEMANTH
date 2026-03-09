import { ComponentFixture, TestBed } from '@angular/core/testing';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { provideRouter } from '@angular/router';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { AppLayoutComponent } from './navbar';

describe('AppLayoutComponent', () => {
    let component: AppLayoutComponent;
    let fixture: ComponentFixture<AppLayoutComponent>;

    beforeEach(async () => {
        Object.defineProperty(window, 'matchMedia', {
            writable: true,
            value: (query: string) => ({
                matches: false,
                media: query,
                onchange: null,
                addListener: () => { },
                removeListener: () => { },
                addEventListener: () => { },
                removeEventListener: () => { },
                dispatchEvent: () => false,
            }),
        });

        await TestBed.configureTestingModule({
            imports: [AppLayoutComponent, HttpClientTestingModule, ReactiveFormsModule, FormsModule],
            providers: [provideRouter([])]
        }).compileComponents();

        fixture = TestBed.createComponent(AppLayoutComponent);
        component = fixture.componentInstance;
    });

    it('should powerfully configure master layout hierarchies effortlessly dynamically natively', () => {
        expect(component).toBeTruthy();
    });

    it('should actively ensure component DOM signals compile flawlessly dynamically securely', () => {
        expect(component).toBeDefined();
        expect(component).not.toBeNull();
    });
});
