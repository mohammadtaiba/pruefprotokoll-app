import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { RouterLink } from '@angular/router';
import { InspectionReport } from '../../models/inspection-report';
import { InspectionReportService } from '../../services/inspection-report.service';

@Component({
  selector: 'app-report-list',
  standalone: true,
  imports: [CommonModule, RouterLink],
  templateUrl: './report-list.component.html',
  styleUrl: './report-list.component.css'
})
export class ReportListComponent implements OnInit {
  reports: InspectionReport[] = [];
  isLoading = false;
  errorMessage = '';

  constructor(private readonly reportService: InspectionReportService) {}

  ngOnInit(): void {
    this.loadReports();
  }

  loadReports(): void {
    this.isLoading = true;
    this.errorMessage = '';

    this.reportService.getAll().subscribe({
      next: reports => {
        this.reports = reports;
        this.isLoading = false;
      },
      error: () => {
        this.errorMessage = 'Prüfprotokolle konnten nicht geladen werden.';
        this.isLoading = false;
      }
    });
  }

  deleteReport(id: number): void {
    if (!confirm('Prüfprotokoll wirklich löschen?')) {
      return;
    }

    this.reportService.delete(id).subscribe({
      next: () => this.reports = this.reports.filter(report => report.id !== id),
      error: () => this.errorMessage = 'Prüfprotokoll konnte nicht gelöscht werden.'
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
