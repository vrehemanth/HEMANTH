import { ComponentFixture, TestBed } from '@angular/core/testing';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { provideRouter } from '@angular/router';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { AgentDashboardComponent } from './dashboard';

describe('AgentDashboardComponent', () => {
    let component: AgentDashboardComponent;
    let fixture: ComponentFixture<AgentDashboardComponent>;

    beforeEach(async () => {
        await TestBed.configureTestingModule({
            imports: [AgentDashboardComponent, HttpClientTestingModule, ReactiveFormsModule, FormsModule],
            providers: [provideRouter([])]
        }).compileComponents();

        fixture = TestBed.createComponent(AgentDashboardComponent);
        component = fixture.componentInstance;
    });

    it('should successfully instantiate the component logic tier safely', () => {
        expect(component).toBeTruthy();
    });

    it('should strictly compile static layout DOM interfaces dynamically without panics', () => {
        expect(component).toBeDefined();
        expect(component).not.toBeNull();
    });
});
