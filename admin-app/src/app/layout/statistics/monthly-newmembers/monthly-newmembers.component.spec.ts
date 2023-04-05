import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MonthlyNewmembersComponent } from './monthly-newmembers.component';

describe('MonthlyNewmembersComponent', () => {
  let component: MonthlyNewmembersComponent;
  let fixture: ComponentFixture<MonthlyNewmembersComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ MonthlyNewmembersComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(MonthlyNewmembersComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
