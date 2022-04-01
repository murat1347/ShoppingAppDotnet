import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SellerFindDialogComponent } from './seller-find-dialog.component';

describe('SellerFindDialogComponent', () => {
  let component: SellerFindDialogComponent;
  let fixture: ComponentFixture<SellerFindDialogComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SellerFindDialogComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SellerFindDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
