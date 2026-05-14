import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { Observable } from 'rxjs';
import { InspectionReport, InspectionReportPayload } from '../../models/inspection-report';
import { InspectionReportService } from '../../services/inspection-report.service';

@Component({
  selector: 'app-report-form',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, RouterLink],
  templateUrl: './report-form.component.html',
  styleUrl: './report-form.component.css'
})
export class ReportFormComponent implements OnInit {
  readonly statuses = ['Bestanden', 'Mängel', 'Nicht bestanden'] as const;

  reportId?: number;
  isEditMode = false;
  isSaving = false;
  errorMessage = '';

  // Pflichtfelder werden direkt im Formular validiert.
  form = this.formBuilder.nonNullable.group({
    productName: ['', Validators.required],
    inspectorName: ['', Validators.required],
    inspectionDate: ['', Validators.required],
    resultStatus: ['Bestanden', Validators.required],
    comment: ['']
  });

  constructor(
    private readonly formBuilder: FormBuilder,
    private readonly route: ActivatedRoute,
    private readonly router: Router,
    private readonly reportService: InspectionReportService
  ) {}

  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('id');
    if (!id) {
      return;
    }

    this.reportId = Number(id);
    this.isEditMode = true;

    this.reportService.getById(this.reportId).subscribe({
      next: report => this.form.patchValue({
        productName: report.productName,
        inspectorName: report.inspectorName,
        inspectionDate: report.inspectionDate.substring(0, 10),
        resultStatus: report.resultStatus,
        comment: report.comment ?? ''
      }),
      error: () => this.errorMessage = 'Prüfprotokoll konnte nicht geladen werden.'
    });
  }

    submit(): void {
        this.errorMessage = '';

        if (this.form.invalid) {
            this.form.markAllAsTouched();
            return;
        }

        const payload = this.form.getRawValue() as InspectionReportPayload;
        this.isSaving = true;

        if (this.isEditMode && this.reportId) {
            this.reportService.update(this.reportId, payload).subscribe({
                next: () => this.router.navigate(['/']),
                error: () => {
                    this.errorMessage = 'Prüfprotokoll konnte nicht gespeichert werden.';
                    this.isSaving = false;
                }
            });
        } else {
            this.reportService.create(payload).subscribe({
                next: () => this.router.navigate(['/']),
                error: () => {
                    this.errorMessage = 'Prüfprotokoll konnte nicht gespeichert werden.';
                    this.isSaving = false;
                }
            });
        }
    }

  isInvalid(fieldName: 'productName' | 'inspectorName' | 'inspectionDate' | 'resultStatus' | 'comment'): boolean {
    const field = this.form.controls[fieldName];
    return field.invalid && (field.dirty || field.touched);
  }
}
