import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CodesmasterListingComponent } from './codesmaster-listing.component';

describe('CodesmasterListingComponent', () => {
  let component: CodesmasterListingComponent;
  let fixture: ComponentFixture<CodesmasterListingComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [CodesmasterListingComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(CodesmasterListingComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
