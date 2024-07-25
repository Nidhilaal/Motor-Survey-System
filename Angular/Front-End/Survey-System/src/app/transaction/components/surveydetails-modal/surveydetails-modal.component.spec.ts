import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SurveydetailsModalComponent } from './surveydetails-modal.component';

describe('SurveydetailsModalComponent', () => {
  let component: SurveydetailsModalComponent;
  let fixture: ComponentFixture<SurveydetailsModalComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [SurveydetailsModalComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(SurveydetailsModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
