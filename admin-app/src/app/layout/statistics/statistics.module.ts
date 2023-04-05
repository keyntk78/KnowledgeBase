import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MonthlyNewmembersComponent } from './monthly-newmembers/monthly-newmembers.component';
import { MonthlyNewkbsComponent } from './monthly-newkbs/monthly-newkbs.component';
import { MonthlyCommentsComponent } from './monthly-comments/monthly-comments.component';
import { StatisticsRoutingModule } from './statistics-routing.module';

@NgModule({
    declarations: [MonthlyNewmembersComponent, MonthlyNewkbsComponent, MonthlyCommentsComponent],
    imports: [CommonModule, StatisticsRoutingModule]
})
export class StatisticsModule {}
