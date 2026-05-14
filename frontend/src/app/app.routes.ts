import { Routes } from '@angular/router';
import { ReportListComponent } from './components/report-list/report-list.component';
import { ReportFormComponent } from './components/report-form/report-form.component';
import { ReportDetailComponent } from './components/report-detail/report-detail.component';

export const routes: Routes = [
  { path: '', component: ReportListComponent },
  { path: 'reports/new', component: ReportFormComponent },
  { path: 'reports/:id', component: ReportDetailComponent },
  { path: 'reports/:id/edit', component: ReportFormComponent },
  { path: '**', redirectTo: '' }
];
