import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FuntionsComponent } from './funtions.component';

describe('FuntionsComponent', () => {
  let component: FuntionsComponent;
  let fixture: ComponentFixture<FuntionsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FuntionsComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(FuntionsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
