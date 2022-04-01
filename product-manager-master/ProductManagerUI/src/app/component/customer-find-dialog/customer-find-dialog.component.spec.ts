import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CustomerFindDialogComponent } from './customer-find-dialog.component';

describe('CustomerFindDialogComponent', () => {
  let component: CustomerFindDialogComponent;
  let fixture: ComponentFixture<CustomerFindDialogComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CustomerFindDialogComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(CustomerFindDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
