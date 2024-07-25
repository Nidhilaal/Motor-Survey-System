import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SurveyorNavbarComponent } from './surveyor-navbar.component';

describe('SurveyorNavbarComponent', () => {
  let component: SurveyorNavbarComponent;
  let fixture: ComponentFixture<SurveyorNavbarComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [SurveyorNavbarComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(SurveyorNavbarComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
