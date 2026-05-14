import { ComponentFixture, TestBed } from '@angular/core/testing';
import { convertToParamMap, ActivatedRoute, Router } from '@angular/router';
import { of } from 'rxjs';
import { ReportFormComponent } from './report-form.component';
import { InspectionReportService } from '../../services/inspection-report.service';

describe('ReportFormComponent', () => {
  let component: ReportFormComponent;
  let fixture: ComponentFixture<ReportFormComponent>;

  const reportServiceMock = {
    getById: jasmine.createSpy('getById').and.returnValue(of(null)),
    create: jasmine.createSpy('create').and.returnValue(of({ id: 1 })),
    update: jasmine.createSpy('update').and.returnValue(of(void 0))
  };

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ReportFormComponent],
      providers: [
        { provide: InspectionReportService, useValue: reportServiceMock },
        { provide: ActivatedRoute, useValue: { snapshot: { paramMap: convertToParamMap({}) } } },
        { provide: Router, useValue: { navigate: jasmine.createSpy('navigate') } }
      ]
    }).compileComponents();

    fixture = TestBed.createComponent(ReportFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('marks required fields as invalid until valid values are entered', () => {
    component.form.patchValue({
      productName: '',
      inspectorName: '',
      inspectionDate: '',
      resultStatus: 'Bestanden',
      comment: ''
    });

    expect(component.form.valid).toBeFalse();
    expect(component.form.controls.productName.hasError('required')).toBeTrue();
    expect(component.form.controls.inspectorName.hasError('required')).toBeTrue();
    expect(component.form.controls.inspectionDate.hasError('required')).toBeTrue();

    component.form.patchValue({
      productName: 'Hydraulikpumpe HP-200',
      inspectorName: 'Anna Müller',
      inspectionDate: '2026-05-01',
      resultStatus: 'Bestanden'
    });

    expect(component.form.valid).toBeTrue();
  });
});
