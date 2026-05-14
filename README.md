# Mini-Prüfprotokoll-App

Kleine Fullstack-Demo für Prüfprotokolle mit Angular, ASP.NET Core Web API, Entity Framework Core und SQLite.

## Funktionen

- Prüfprotokolle erstellen, anzeigen, bearbeiten und löschen
- Detailansicht pro Prüfprotokoll
- Statusauswahl: `Bestanden`, `Mängel`, `Nicht bestanden`
- Responsive UI für Desktop und Smartphone
- SQLite-Datenbank mit 2 Seed-Datensätzen
- Einfache Unit-Tests für Backend-Service und Angular-Formularvalidierung

## Projektstruktur

```text
MiniPruefprotokollApp/
├── backend/
│   ├── MiniInspectionReports.Api/
│   │   ├── Controllers/
│   │   ├── Data/
│   │   ├── Models/
│   │   ├── Repositories/
│   │   ├── Services/
│   │   └── Program.cs
│   └── MiniInspectionReports.Tests/
├── frontend/
│   ├── src/app/components/
│   ├── src/app/models/
│   ├── src/app/services/
│   └── proxy.conf.json
└── README.md
```

## Voraussetzungen

- .NET 8 SDK
- Node.js 20 LTS oder kompatibel
- npm

## Backend starten

```bash
cd backend/MiniInspectionReports.Api
dotnet restore
dotnet run
```

Die API läuft danach unter:

```text
http://localhost:5000
```

Beim ersten Start wird automatisch die SQLite-Datei `inspectionreports.db` erstellt und mit 2 Beispieldatensätzen befüllt.

## Frontend starten

In einem zweiten Terminal:

```bash
cd frontend
npm install
npm start
```

Die Angular-App läuft danach unter:

```text
http://localhost:4200
```

Das Frontend nutzt `proxy.conf.json`, damit API-Aufrufe an `/api` automatisch an `http://localhost:5000` weitergeleitet werden.

## Tests ausführen

Backend-Tests:

```bash
cd backend/MiniInspectionReports.Tests
dotnet test
```

Frontend-Test:

```bash
cd frontend
npm test
```

## REST-Endpunkte

```text
GET    /api/inspectionreports
GET    /api/inspectionreports/{id}
POST   /api/inspectionreports
PUT    /api/inspectionreports/{id}
DELETE /api/inspectionreports/{id}
```

## Datenmodell

```csharp
public class InspectionReport
{
    public int Id { get; set; }
    public string ProductName { get; set; }
    public string InspectorName { get; set; }
    public DateTime InspectionDate { get; set; }
    public string ResultStatus { get; set; }
    public string? Comment { get; set; }
}
```