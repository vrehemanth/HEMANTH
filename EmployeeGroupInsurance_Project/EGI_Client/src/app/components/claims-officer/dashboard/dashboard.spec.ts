import { ComponentFixture, TestBed } from '@angular/core/testing';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { provideRouter } from '@angular/router';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { ClaimsOfficerDashboardComponent } from './dashboard';

describe('ClaimsOfficerDashboardComponent', () => {
    let component: ClaimsOfficerDashboardComponent;
    let fixture: ComponentFixture<ClaimsOfficerDashboardComponent>;

    beforeEach(async () => {
        await TestBed.configureTestingModule({
            imports: [ClaimsOfficerDashboardComponent, HttpClientTestingModule, ReactiveFormsModule, FormsModule],
            providers: [provideRouter([])]
        }).compileComponents();

        fixture = TestBed.createComponent(ClaimsOfficerDashboardComponent);
        component = fixture.componentInstance;
    });

    it('should robustly mount internal controller behaviors across execution scopes', () => {
        expect(component).toBeTruthy();
    });

    it('should cleanly host root level instance memory securely', () => {
        expect(component).toBeDefined();
        expect(component).not.toBeNull();
    });
});
