export interface InspectionReport {
  id: number;
  productName: string;
  inspectorName: string;
  inspectionDate: string;
  resultStatus: 'Bestanden' | 'Mängel' | 'Nicht bestanden';
  comment?: string | null;
}

export type InspectionReportPayload = Omit<InspectionReport, 'id'>;
