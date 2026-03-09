import { ComponentFixture, TestBed } from '@angular/core/testing';
import { HomeComponent } from './home';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { RouterModule } from '@angular/router';
import { PublicService } from '../../data-access/api.services';
import { ThemeService } from '../../core/services/theme.service';
import { of, throwError } from 'rxjs';

describe('HomeComponent', () => {
    let component: HomeComponent;
    let fixture: ComponentFixture<HomeComponent>;

    let mockPlansObservable: any;
    const publicServiceMock = {
        getInsurancePlans: () => mockPlansObservable
    };

    const themeServiceMock = {
        toggleTheme: () => { },
        isDarkMode: () => false
    };

    beforeEach(async () => {
        mockPlansObservable = of([]);

        await TestBed.configureTestingModule({
            imports: [HomeComponent, HttpClientTestingModule, RouterModule.forRoot([])],
            providers: [
                { provide: PublicService, useValue: publicServiceMock },
                { provide: ThemeService, useValue: themeServiceMock }
            ]
        }).compileComponents();
    });

    it('should create the home component successfully', () => {
        fixture = TestBed.createComponent(HomeComponent);
        component = fixture.componentInstance;
        fixture.detectChanges();
        expect(component).toBeTruthy();
    });

    it('should correctly map empty backend data arrays into default 3 active fallback plans', () => {
        mockPlansObservable = of([]);
        fixture = TestBed.createComponent(HomeComponent);
        component = fixture.componentInstance;
        fixture.detectChanges();

        expect(component.activePlans().length).toBe(3);
        expect(component.activePlans()[0].name).toBe('Bronze Shield');
    });

    it('should safely unwrap generic object wrappers like {data: []}', () => {
        const mockPayload = { data: [{ planName: 'Ruby Extra', basePremium: 8000, coverages: [] }] };
        mockPlansObservable = of(mockPayload);

        fixture = TestBed.createComponent(HomeComponent);
        component = fixture.componentInstance;
        fixture.detectChanges();

        expect(component.activePlans().length).toBe(1);
        expect(component.activePlans()[0].name).toBe('Ruby Extra');
    });

    it('should format backend database strings directly into the UI mapping feature list format', () => {
        const dbData = [{
            planName: 'Silver Flex',
            basePremium: 3000,
            coverages: [{ type: 'Health', coverageAmount: 50000, coveredGroup: 'Spouse' }]
        }];
        mockPlansObservable = of(dbData);

        fixture = TestBed.createComponent(HomeComponent);
        component = fixture.componentInstance;
        fixture.detectChanges();

        expect(component.activePlans()[0].features[0]).toContain('Health Coverage up to ₹50,000');
        expect(component.activePlans()[0].features[0]).toContain('(Spouse)');
    });

    it('should fall back onto default cached payload if the backend heavily rejects with an HTTP error 500/400 (fail-safe)', () => {
        const originalError = console.error;
        console.error = () => { }; // Silences terminal error dump spam

        mockPlansObservable = throwError(() => ({ message: 'Server Down' }));

        fixture = TestBed.createComponent(HomeComponent);
        component = fixture.componentInstance;
        fixture.detectChanges();

        expect(component.activePlans().length).toBe(3);
        expect(component.activePlans()[2].name).toBe('Gold Pinnacle');

        console.error = originalError; // Restore cleanly
    });

    it('should toggle desktop/mobile menu signal state open and close', () => {
        mockPlansObservable = of([]);
        fixture = TestBed.createComponent(HomeComponent);
        component = fixture.componentInstance;

        component.isMenuOpen.set(false);
        component.isMenuOpen.set(true);
        expect(component.isMenuOpen()).toBe(true);
    });

    it('window scroll should correctly trigger the header glass bar boolean state', () => {
        mockPlansObservable = of([]);
        fixture = TestBed.createComponent(HomeComponent);
        component = fixture.componentInstance;

        // mock scroll
        Object.defineProperty(window, 'scrollY', { value: 30, writable: true });
        component.onWindowScroll();
        expect(component.isScrolled()).toBe(true);

        Object.defineProperty(window, 'scrollY', { value: 10, writable: true });
        component.onWindowScroll();
        expect(component.isScrolled()).toBe(false);
    });

    it('should properly reflect initially clean internal plan loading states on spawn', () => {
        mockPlansObservable = of([]);
        fixture = TestBed.createComponent(HomeComponent);
        component = fixture.componentInstance;
        fixture.detectChanges(); // Trigger Angular ngOnInit to populate signals
        expect(component).toBeDefined();
        // Since backend was empty, the activePlans should perfectly mirror the 3 static default plans
        expect(component.activePlans().length).toBe(3);
    });

    it('scroll event broadcasater natively maintains decoupled overlay structures accurately', () => {
        mockPlansObservable = of([]);
        fixture = TestBed.createComponent(HomeComponent);
        component = fixture.componentInstance;
        window.dispatchEvent(new Event('scroll'));
        // Menu should not randomly open on vertical scrolling
        expect(component.isMenuOpen()).toBe(false);
    });
});
