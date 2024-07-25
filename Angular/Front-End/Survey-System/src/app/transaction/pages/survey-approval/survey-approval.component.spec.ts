import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SurveyApprovalComponent } from './survey-approval.component';

describe('SurveyApprovalComponent', () => {
  let component: SurveyApprovalComponent;
  let fixture: ComponentFixture<SurveyApprovalComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [SurveyApprovalComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(SurveyApprovalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
