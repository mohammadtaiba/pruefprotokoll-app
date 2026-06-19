# CI/CD

Die Pipeline liegt in `.github/workflows/ci-cd.yml`.

## Was automatisch läuft

Bei jedem Push auf `main` und bei jedem Pull Request nach `main` läuft:

- Backend-Dependencies installieren
- Backend-Tests ausführen
- Backend bauen
- Frontend-Dependencies installieren
- Frontend-Linting ausführen
- Frontend-Tests ausführen
- Frontend bauen
- Docker-Images für Backend und Frontend bauen

Bei Push auf `main` werden die Docker-Images zusätzlich in die GitHub Container Registry gepusht:

- `ghcr.io/mohammadtaiba/pruefprotokoll-app-backend:latest`
- `ghcr.io/mohammadtaiba/pruefprotokoll-app-frontend:latest`
- `ghcr.io/mohammadtaiba/pruefprotokoll-app-backend:<commit-sha>`
- `ghcr.io/mohammadtaiba/pruefprotokoll-app-frontend:<commit-sha>`

## Secrets

Für GHCR wird kein eigenes Secret benötigt. Die Pipeline nutzt das automatisch verfügbare GitHub-Secret:

```text
secrets.GITHUB_TOKEN
```

Der Token wird nicht im Repository gespeichert und nur im Workflow verwendet.

Für eine externe Registry wie Docker Hub müssen Secrets unter GitHub gespeichert werden:

```text
DOCKERHUB_USERNAME
DOCKERHUB_TOKEN
```

Nicht erlaubt:

```text
password: mein-passwort
apiKey: abc123
```

## Merge blockieren bei Fehlern

Damit fehlgeschlagene Tests oder Builds den Merge blockieren, muss Branch Protection für `main` aktiviert werden:

1. GitHub Repository öffnen
2. `Settings` → `Branches`
3. `Add branch ruleset` oder `Add branch protection rule`
4. Branch pattern: `main`
5. Aktivieren: `Require status checks to pass`
6. Diese Checks auswählen:
   - `Install, lint, test and build`
   - `Build and push Docker images`
7. Regel speichern

Danach können Pull Requests nur gemerged werden, wenn CI, Tests, Build, Linting und Docker-Build erfolgreich sind.

## Linting

Das Linting läuft ohne automatische Formatierung.

Die Pipeline nutzt kein Prettier und führt keine automatische Code-Formatierung aus.
