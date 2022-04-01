import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DeleteErrorDialogComponent } from './delete-error-dialog.component';

describe('DeleteErrorDialogComponent', () => {
  let component: DeleteErrorDialogComponent;
  let fixture: ComponentFixture<DeleteErrorDialogComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DeleteErrorDialogComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(DeleteErrorDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
