import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ErrorcodesmasterModalComponent } from './errorcodesmaster-modal.component';

describe('ErrorcodesmasterModalComponent', () => {
  let component: ErrorcodesmasterModalComponent;
  let fixture: ComponentFixture<ErrorcodesmasterModalComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ErrorcodesmasterModalComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(ErrorcodesmasterModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
