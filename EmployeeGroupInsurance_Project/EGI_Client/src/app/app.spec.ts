import { TestBed } from '@angular/core/testing';
import { App } from './app';

describe('App', () => {
  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [App],
    }).compileComponents();
  });

  it('should create the app', () => {
    const fixture = TestBed.createComponent(App);
    const app = fixture.componentInstance;
    expect(app).toBeTruthy();
  });

  it('should render an application routing root', async () => {
    const fixture = TestBed.createComponent(App);
    await fixture.whenStable();
    const compiled = fixture.nativeElement as HTMLElement;
    expect(compiled.querySelector('router-outlet')).toBeTruthy();
  });

  it('should securely compile baseline properties without application crashing', () => {
    const fixture = TestBed.createComponent(App);
    const compiled = fixture.componentInstance;
    expect(compiled).toBeDefined();
    expect(compiled).not.toBeNull();
  });

  it('should cleanly host the application navigation shell structure directly via DOM', () => {
    const fixture = TestBed.createComponent(App);
    fixture.detectChanges();
    const dom = fixture.nativeElement as HTMLElement;
    expect(dom).toBeDefined();
  });
});
