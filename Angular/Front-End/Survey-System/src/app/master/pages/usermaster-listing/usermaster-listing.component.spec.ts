import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UsermasterListingComponent } from './usermaster-listing.component';

describe('UsermasterListingComponent', () => {
  let component: UsermasterListingComponent;
  let fixture: ComponentFixture<UsermasterListingComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [UsermasterListingComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(UsermasterListingComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
