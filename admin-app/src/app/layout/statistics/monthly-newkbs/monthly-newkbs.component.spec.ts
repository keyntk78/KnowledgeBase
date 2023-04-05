import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MonthlyNewkbsComponent } from './monthly-newkbs.component';

describe('MonthlyNewkbsComponent', () => {
  let component: MonthlyNewkbsComponent;
  let fixture: ComponentFixture<MonthlyNewkbsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ MonthlyNewkbsComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(MonthlyNewkbsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
