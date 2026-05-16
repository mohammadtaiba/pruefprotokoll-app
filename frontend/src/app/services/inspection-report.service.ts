import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { InspectionReport, InspectionReportPayload } from '../models/inspection-report';

@Injectable({
  providedIn: 'root'
})
export class InspectionReportService {
  private readonly apiUrl = '/api/inspectionreports';

  constructor(private readonly http: HttpClient) {}

  getAll(): Observable<InspectionReport[]> {
    return this.http.get<InspectionReport[]>(this.apiUrl);
  }

  getById(id: number): Observable<InspectionReport> {
    return this.http.get<InspectionReport>(`${this.apiUrl}/${id}`);
  }

  create(report: InspectionReportPayload): Observable<InspectionReport> {
    return this.http.post<InspectionReport>(this.apiUrl, report);
  }

  update(id: number, report: InspectionReportPayload): Observable<void> {
    return this.http.put<void>(`${this.apiUrl}/${id}`, report);
  }

  delete(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }
}
