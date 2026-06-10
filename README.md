# Mini-Prüfprotokoll-App

Kleine Fullstack-Demo für Prüfprotokolle mit Angular, ASP.NET Core Web API, Entity Framework Core und SQLite.

Die Anwendung kann lokal mit .NET/npm oder vollständig über Docker gestartet werden.

## Funktionen

* Prüfprotokolle erstellen, anzeigen, bearbeiten und löschen
* Detailansicht pro Prüfprotokoll
* Statusauswahl: `Bestanden`, `Mängel`, `Nicht bestanden`
* Responsive UI für Desktop und Smartphone
* SQLite-Datenbank mit 2 Seed-Datensätzen
* Einfache Unit-Tests für Backend-Service und Angular-Formularvalidierung
* Docker-Setup mit Angular-Frontend, Nginx und ASP.NET-Core-Backend

## Technologien

* Angular
* ASP.NET Core Web API
* Entity Framework Core
* SQLite
* Docker
* Nginx
* .NET 8
* Node.js 20

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
│   ├── MiniInspectionReports.Tests/
│   └── Dockerfile
├── frontend/
│   ├── src/app/components/
│   ├── src/app/models/
│   ├── src/app/services/
│   ├── proxy.conf.json
│   ├── nginx.conf
│   └── Dockerfile
├── docker-compose.yml
└── README.md
```

## Voraussetzungen

Für lokalen Start ohne Docker:

* .NET 8 SDK
* Node.js 20 LTS oder kompatibel
* npm

Für Start mit Docker:

* Docker Desktop
* Docker Compose

## Mit Docker starten

Im Hauptordner des Projekts ausführen:

```bash
docker compose up --build
```

Die Anwendung ist danach erreichbar unter:

```text
http://localhost:4200
```

Die API ist erreichbar unter:

```text
http://localhost:5000/api/inspectionreports
```

Docker startet zwei Services:

```text
frontend   Angular-Build mit Nginx auf Port 4200
backend    ASP.NET Core Web API auf Port 5000
```

Container stoppen:

```bash
docker compose down
```

Container neu bauen:

```bash
docker compose build --no-cache
docker compose up
```

## Backend lokal starten

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

## Frontend lokal starten

In einem zweiten Terminal:

```bash
cd frontend
npm.cmd install
npm.cmd start
```

Die Angular-App läuft danach unter:

```text
http://localhost:4200
```

Das Frontend nutzt lokal `proxy.conf.json`, damit API-Aufrufe an `/api` automatisch an `http://localhost:5000` weitergeleitet werden.

Im Docker-Betrieb übernimmt `nginx.conf` die Weiterleitung von `/api` an den Backend-Container.

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

## Git-Workflow

Für neue Änderungen sollte ein eigener Branch erstellt werden:

```bash
git switch -c feature/docker-setup
```

Änderungen committen:

```bash
git add .
git commit -m "Add Docker setup"
```

Branch zu GitHub pushen:

```bash
git push -u origin feature/docker-setup
```

Danach kann auf GitHub ein Pull Request nach `main` erstellt werden.
