import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CodesmasterModalComponent } from './codesmaster-modal.component';

describe('CodesmasterModalComponent', () => {
  let component: CodesmasterModalComponent;
  let fixture: ComponentFixture<CodesmasterModalComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [CodesmasterModalComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(CodesmasterModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
