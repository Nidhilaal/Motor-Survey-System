import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UsermasterModalComponent } from './usermaster-modal.component';

describe('UsermasterModalComponent', () => {
  let component: UsermasterModalComponent;
  let fixture: ComponentFixture<UsermasterModalComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [UsermasterModalComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(UsermasterModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
