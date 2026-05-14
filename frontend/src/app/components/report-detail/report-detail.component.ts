import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, RouterLink } from '@angular/router';
import { InspectionReport } from '../../models/inspection-report';
import { InspectionReportService } from '../../services/inspection-report.service';

@Component({
  selector: 'app-report-detail',
  standalone: true,
  imports: [CommonModule, RouterLink],
  templateUrl: './report-detail.component.html',
  styleUrl: './report-detail.component.css'
})
export class ReportDetailComponent implements OnInit {
  report?: InspectionReport;
  isLoading = false;
  errorMessage = '';

  constructor(
    private readonly route: ActivatedRoute,
    private readonly reportService: InspectionReportService
  ) {}

  ngOnInit(): void {
    const id = Number(this.route.snapshot.paramMap.get('id'));
    this.isLoading = true;

    this.reportService.getById(id).subscribe({
      next: report => {
        this.report = report;
        this.isLoading = false;
      },
      error: () => {
        this.errorMessage = 'Prüfprotokoll wurde nicht gefunden.';
        this.isLoading = false;
      }
    });
  }

  getStatusClass(status: string): string {
    if (status === 'Bestanden') {
      return 'passed';
    }

    if (status === 'Mängel') {
      return 'warning';
    }

    return 'failed';
  }
}
