import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { MonthlyNewkbsComponent } from './monthly-newkbs/monthly-newkbs.component';
import { MonthlyCommentsComponent } from './monthly-comments/monthly-comments.component';
import { MonthlyNewmembersComponent } from './monthly-newmembers/monthly-newmembers.component';

const routes: Routes = [
    {
        path: '',
        component: MonthlyNewkbsComponent
    },
    {
        path: 'monthly-new-kb',
        component: MonthlyNewkbsComponent
    },
    {
        path: 'monthly-new-comment',
        component: MonthlyCommentsComponent
    },
    {
        path: 'monthly-new-member',
        component: MonthlyNewmembersComponent
    }
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class StatisticsRoutingModule {}
