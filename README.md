# Prüfprotokoll-App

![CI/CD](https://github.com/mohammadtaiba/pruefprotokoll-app/actions/workflows/ci-cd.yml/badge.svg)
![.NET](https://img.shields.io/badge/.NET-8.0-512BD4?logo=dotnet)
![Angular](https://img.shields.io/badge/Angular-17-DD0031?logo=angular)
![Docker](https://img.shields.io/badge/Docker-ready-2496ED?logo=docker)
![License](https://img.shields.io/badge/Project-Portfolio-blue)

Eine Fullstack-Webanwendung zur Verwaltung von Prüfprotokollen. Das Projekt zeigt eine saubere Umsetzung einer kleinen Business-Anwendung mit Angular-Frontend, ASP.NET-Core-Web-API, Entity Framework Core, SQLite, Docker und automatisierter CI/CD-Pipeline.

Die Anwendung wurde als kompaktes Portfolio-Projekt entwickelt und demonstriert typische Aufgaben aus der modernen Webentwicklung: Formularvalidierung, REST-Kommunikation, Datenpersistenz, serviceorientierte Backend-Struktur, Containerisierung und automatisierte Qualitätsprüfungen.

## Projektziel

Ziel des Projekts ist eine einfache, nachvollziehbare Prüfprotokoll-Verwaltung, mit der Prüfberichte erstellt, angezeigt, bearbeitet und gelöscht werden können. Der Fokus liegt nicht auf Funktionsumfang, sondern auf einer klaren technischen Struktur, reproduzierbarem Setup und nachvollziehbarer Codequalität.

## Funktionen

* Prüfprotokolle erstellen, anzeigen, bearbeiten und löschen
* Detailansicht für einzelne Prüfprotokolle
* Statusauswahl mit `Bestanden`, `Mängel` und `Nicht bestanden`
* Formularvalidierung im Frontend
* Serverseitige Validierung im Backend
* SQLite-Datenbank mit Seed-Daten für Demo-Zwecke
* Responsive Oberfläche für Desktop und Smartphone
* REST-API mit klaren Endpunkten
* Docker-Setup für Frontend und Backend
* GitHub-Actions-Pipeline für Installation, Tests, Linting, Build und Docker-Image-Erstellung


## Screenshots

### Übersicht der Prüfprotokolle

<img src="docs/screenshots/overview.png" alt="Übersicht der Prüfprotokolle" width="800">

### Prüfprotokoll erstellen

<img src="docs/screenshots/create-report.png" alt="Prüfprotokoll erstellen" width="800">

### Responsive Ansicht

<img src="docs/screenshots/mobile-view.png" alt="Responsive Smartphone-Ansicht" width="350">

## Tech Stack

| Bereich | Technologie |
|---|---|
| Frontend | Angular 17, TypeScript, HTML, CSS |
| Backend | ASP.NET Core Web API, C#, .NET 8 |
| Datenbank | SQLite, Entity Framework Core 8 |
| Tests | xUnit/.NET Tests, Angular/Karma/Jasmine |
| Webserver | Nginx für das Angular-Frontend im Docker-Container |
| Containerisierung | Docker, Docker Compose |
| CI/CD | GitHub Actions, GitHub Container Registry |

## Architektur

```text
Browser
  |
  | HTTP
  v
Angular Frontend
  |
  | /api/inspectionreports
  v
ASP.NET Core Web API
  |
  | Entity Framework Core
  v
SQLite-Datenbank
```

Im lokalen Entwicklungsmodus leitet Angular API-Aufrufe über `proxy.conf.json` an das Backend weiter. Im Docker-Betrieb übernimmt Nginx die Weiterleitung von `/api` an den Backend-Container.

## Projektstruktur

```text
pruefprotokoll-app/
├── .github/
│   └── workflows/
│       └── ci-cd.yml
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

Für den Start mit Docker:

* Docker Desktop
* Docker Compose

Für den lokalen Start ohne Docker:

* .NET 8 SDK
* Node.js 20 oder kompatibel
* npm

## Start mit Docker

Im Hauptordner des Projekts ausführen:

```bash
docker compose up --build
```

Danach ist die Anwendung erreichbar unter:

```text
http://localhost:4200
```

Die API ist erreichbar unter:

```text
http://localhost:5000/api/inspectionreports
```

Docker Compose startet zwei Services:

```text
frontend   Angular-Build mit Nginx auf Port 4200
backend    ASP.NET-Core-Web-API auf Port 5000
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

Beim ersten Start wird automatisch eine SQLite-Datenbank erstellt und mit zwei Demo-Datensätzen befüllt.

## Frontend lokal starten

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

Das Frontend nutzt im lokalen Entwicklungsmodus `proxy.conf.json`, damit API-Aufrufe an `/api` automatisch an das Backend weitergeleitet werden.

## Tests und Qualitätssicherung

Backend-Tests ausführen:

```bash
cd backend/MiniInspectionReports.Tests
dotnet test
```

Frontend-Tests lokal ausführen:

```bash
cd frontend
npm test
```

Frontend-Tests im Headless-Modus ausführen, wie in der CI-Pipeline:

```bash
cd frontend
npm test -- --watch=false --browsers=ChromeHeadless
```

Frontend builden:

```bash
cd frontend
npm run build
```

Backend builden:

```bash
dotnet build backend/MiniInspectionReports.Api/MiniInspectionReports.Api.csproj --configuration Release
```

## CI/CD-Pipeline

Das Repository enthält eine GitHub-Actions-Pipeline unter:

```text
.github/workflows/ci-cd.yml
```

Die Pipeline läuft automatisch bei:

* jedem Push auf beliebige Branches
* jedem Pull Request nach `main`

Die CI-Pipeline führt aus:

* Checkout des Repositorys
* Setup von .NET 8
* Wiederherstellung der Backend-Abhängigkeiten
* Backend-Tests
* Backend-Build im Release-Modus
* Setup von Node.js 20
* Installation der Frontend-Abhängigkeiten mit `npm ci`
* Frontend-Linting ohne automatische Formatierung
* Frontend-Tests im Chrome-Headless-Modus
* Frontend-Build

Zusätzlich gibt es einen Docker-Job, der erst nach erfolgreicher CI läuft. Dabei werden Docker-Images für Backend und Frontend gebaut. Bei Push auf `main` werden die Images in die GitHub Container Registry gepusht.

```text
ghcr.io/<github-user>/pruefprotokoll-app-backend:<commit-sha>
ghcr.io/<github-user>/pruefprotokoll-app-backend:latest
ghcr.io/<github-user>/pruefprotokoll-app-frontend:<commit-sha>
ghcr.io/<github-user>/pruefprotokoll-app-frontend:latest
```

Für produktives Arbeiten sollte zusätzlich in GitHub ein Branch Protection Rule oder Ruleset für `main` aktiviert werden, damit Pull Requests nur gemerged werden können, wenn die erforderlichen Status Checks erfolgreich sind.

## REST-API

Basisroute:

```text
/api/inspectionreports
```

| Methode | Route | Beschreibung |
|---|---|---|
| GET | `/api/inspectionreports` | Alle Prüfprotokolle abrufen |
| GET | `/api/inspectionreports/{id}` | Einzelnes Prüfprotokoll abrufen |
| POST | `/api/inspectionreports` | Neues Prüfprotokoll erstellen |
| PUT | `/api/inspectionreports/{id}` | Bestehendes Prüfprotokoll aktualisieren |
| DELETE | `/api/inspectionreports/{id}` | Prüfprotokoll löschen |

## Datenmodell

```csharp
public class InspectionReport
{
    public int Id { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public string InspectorName { get; set; } = string.Empty;
    public DateTime InspectionDate { get; set; }
    public string ResultStatus { get; set; } = string.Empty;
    public string? Comment { get; set; }
}
```

Validierungsregeln:

| Feld | Regel |
|---|---|
| `ProductName` | Pflichtfeld, maximal 120 Zeichen |
| `InspectorName` | Pflichtfeld, maximal 120 Zeichen |
| `InspectionDate` | Pflichtfeld |
| `ResultStatus` | Pflichtfeld, maximal 30 Zeichen |
| `Comment` | Optional, maximal 1000 Zeichen |

## Beispiel-Datensätze

Beim Start werden zwei Demo-Datensätze angelegt:

| Produkt | Prüfer/in | Status |
|---|---|---|
| Hydraulikpumpe HP-200 | Anna Müller | Bestanden |
| Steuergerät SG-42 | Max Schneider | Mängel |

## Git-Workflow

Für neue Änderungen sollte ein eigener Branch erstellt werden:

```bash
git switch -c feature/meine-aenderung
```

Änderungen prüfen:

```bash
git status
```

Änderungen committen:

```bash
git add .
git commit -m "feat: add meaningful feature description"
```

Branch zu GitHub pushen:

```bash
git push -u origin feature/meine-aenderung
```

Danach kann auf GitHub ein Pull Request nach `main` erstellt werden.

## Sicherheit und Konfiguration

* Es werden keine Zugangsdaten im Repository gespeichert.
* Die CI/CD-Pipeline nutzt `GITHUB_TOKEN` für den Zugriff auf die GitHub Container Registry.
* Lokale Datenbankdateien und Build-Artefakte sollten nicht versioniert werden.
* Für echte Produktivumgebungen sollten Datenbankverbindung, CORS-Regeln und Secrets über Umgebungsvariablen konfiguriert werden.

## Geplante Erweiterungen

* Authentifizierung und Rollenmodell
* Erweiterte Filter- und Suchfunktion
* Pagination für größere Datenmengen
* Export von Prüfprotokollen als PDF
* PostgreSQL statt SQLite für produktionsnahe Umgebungen
* Deployment auf Cloud-Plattform oder VPS
* E2E-Tests für vollständige Nutzerabläufe