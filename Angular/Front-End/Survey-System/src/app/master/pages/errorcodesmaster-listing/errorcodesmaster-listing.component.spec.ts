import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ErrorcodesmasterListingComponent } from './errorcodesmaster-listing.component';

describe('ErrorcodesmasterListingComponent', () => {
  let component: ErrorcodesmasterListingComponent;
  let fixture: ComponentFixture<ErrorcodesmasterListingComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ErrorcodesmasterListingComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(ErrorcodesmasterListingComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
