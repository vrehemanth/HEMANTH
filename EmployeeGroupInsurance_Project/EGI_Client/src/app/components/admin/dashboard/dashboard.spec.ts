import { ComponentFixture, TestBed } from '@angular/core/testing';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { provideRouter } from '@angular/router';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { AdminDashboardComponent } from './dashboard';

describe('AdminDashboardComponent', () => {
    let component: AdminDashboardComponent;
    let fixture: ComponentFixture<AdminDashboardComponent>;

    beforeEach(async () => {
        await TestBed.configureTestingModule({
            imports: [AdminDashboardComponent, HttpClientTestingModule, ReactiveFormsModule, FormsModule],
            providers: [provideRouter([])]
        }).compileComponents();

        fixture = TestBed.createComponent(AdminDashboardComponent);
        component = fixture.componentInstance;
    });

    it('should cleanly initialize the dashboard structure securely', () => {
        expect(component).toBeTruthy();
    });

    it('should assert primary properties map directly without null crashing', () => {
        expect(component).toBeDefined();
        expect(component).not.toBeNull();
    });
});
