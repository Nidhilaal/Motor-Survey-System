import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MotorclaimListingComponent } from './motorclaim-listing.component';

describe('MotorclaimListingComponent', () => {
  let component: MotorclaimListingComponent;
  let fixture: ComponentFixture<MotorclaimListingComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [MotorclaimListingComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(MotorclaimListingComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
