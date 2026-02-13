# Contoso Medical Clinic — Product Requirements Document (PRD)

**Version:** 1.0  
**Status:** Draft  
**Author:** Copilot / Project Team  

---

## 1. Executive Summary

Contoso Medical Clinic is a full-featured, modern web application built for a fictitious multi-specialty medical clinic. The application serves **two primary audiences**: patients who use the clinic's services, and clinic staff (front-office representatives, doctors, nurses, hygienists, and administrators). The system covers appointment scheduling, patient records, billing & invoicing, insurance management, staff rostering, and service catalog management.

The application is built on **.NET Aspire** with a **Blazor** front-end, uses **Data API Builder (DAB)** to expose a REST/GraphQL API over a **SQL Server** database (LocalDB for development, Azure SQL free tier for production), and follows **SOLID design principles** throughout the C# codebase.

---

## 2. Goals & Objectives

| # | Goal | Success Metric |
|---|------|----------------|
| 1 | Provide patients a self-service portal for appointments, records, and billing | Patients can book, reschedule, cancel appointments and pay invoices online |
| 2 | Give clinic staff an efficient back-office for daily operations | Staff can manage calendar, patient records, payments, and rosters from a single app |
| 3 | Deliver a modern, aesthetically polished UI | Responsive design with a professional medical-grade look and feel |
| 4 | Use .NET Aspire for orchestration and observability | All services discoverable in Aspire dashboard with health checks and telemetry |
| 5 | Use Data API Builder for database access | REST and GraphQL endpoints auto-generated from SQL schema |
| 6 | Optimize for cost | Use SQL Server free tier (LocalDB → Azure SQL free tier) |
| 7 | Enable future Azure Container Apps deployment | Dockerized services ready for ACA deployment |

---

## 3. User Roles & Personas

### 3.1 Patient
- **Description:** An individual seeking medical services at the clinic.
- **Access:** Patient portal (public-facing).
- **Key needs:** Book appointments, view past visit reports, view/pay invoices.

### 3.2 Front Office Representative
- **Description:** Clinic staff responsible for scheduling, reception, and billing.
- **Access:** Staff portal (authenticated).
- **Key needs:** Manage appointment calendar, manage patient payment information.

### 3.3 Doctor / Medical Professional
- **Description:** Licensed medical practitioner (physician, specialist, dentist, hygienist, nurse).
- **Access:** Staff portal (authenticated, elevated permissions).
- **Key needs:** View and manage patient medical records, document visit notes.

### 3.4 Clinic Administrator
- **Description:** Manages overall clinic operations.
- **Access:** Admin portal (authenticated, full permissions).
- **Key needs:** Staff roster & shift management, insurance management, service catalog management, reporting & analytics.

---

## 4. Functional Requirements

### 4.1 Patient-Facing Features

#### FR-P01: Appointment Scheduling
- **As a patient**, I can schedule a new appointment by selecting a service, a preferred doctor/provider, a date, and a time slot.
- **As a patient**, I can reschedule an existing appointment to a different date/time.
- **As a patient**, I can cancel an upcoming appointment.
- Appointments show available time slots based on provider schedules.
- Confirmation is displayed on-screen after booking.

#### FR-P02: Past Visit Reports
- **As a patient**, I can view a chronological list of my past visits.
- Each visit entry includes: date, provider seen, services performed, and clinical notes (summary).
- Patient can filter visits by date range or provider.

#### FR-P03: Invoices & Payments
- **As a patient**, I can view all my invoices (pending, paid, overdue).
- **As a patient**, I can pay an invoice from the website (simulated payment gateway).
- Invoice details include: date of service, services rendered, amounts, insurance adjustments, balance due.

### 4.2 Clinic Staff Features (Front Office)

#### FR-S01: Appointment Calendar Management
- **As a front office representative**, I can view the appointment calendar for all providers.
- Calendar supports day, week, and month views.
- I can add, edit, and cancel appointments on behalf of patients.
- I can filter the calendar by provider, service type, or patient.

#### FR-S02: Patient Payment Management
- **As a front office representative**, I can manage payment information for patients.
- I can record payments received (cash, card, insurance).
- I can generate and send invoices.
- I can view payment history for a patient.

### 4.3 Doctor / Medical Professional Features

#### FR-D01: Patient Medical Records
- **As a doctor or medical professional**, I can view patient medical records.
- Records include: demographics, medical history, allergies, medications, visit notes.
- I can add new visit notes and update medical records.
- I can upload/attach documents or images to a patient's record.

### 4.4 Administrator Features

#### FR-A01: Staff Roster & Shift Management
- **As a clinic administrator**, I can manage the roster of all staff members.
- I can create, edit, and deactivate staff profiles.
- I can assign shifts to staff (day, time, role).
- I can view shift schedules for any given week.

#### FR-A02: Insurance Management
- **As a clinic administrator**, I can view and manage insurance requests/claims.
- I can add supported insurance providers to the system.
- I can link patients to their insurance plans.
- I can track claim statuses (submitted, approved, denied, pending).

#### FR-A03: Service Catalog Management
- **As a clinic administrator**, I can manage the list of medical services offered by the clinic.
- I can add, edit, activate, and deactivate services.
- Each service has: name, description, category, default duration, default price, and applicable CPT/procedure code.

---

## 5. Service Catalog (Initial Seed Data)

The clinic is a **multi-specialty medical clinic** offering the following categories and services:

### 5.1 General Medicine
| Service | Default Duration | Default Price |
|---------|-----------------|---------------|
| General Consultation | 30 min | $150.00 |
| Annual Physical Exam | 60 min | $250.00 |
| Follow-up Visit | 20 min | $100.00 |
| Urgent Care Visit | 30 min | $200.00 |

### 5.2 Dental Services
| Service | Default Duration | Default Price |
|---------|-----------------|---------------|
| Doctor Exam w/ Full-mouth X-rays (FMX) | 60 min | $300.00 |
| Adult Prophylaxis / Deep Cleaning | 45 min | $200.00 |
| Child Prophylaxis | 30 min | $120.00 |
| Removable Partial – Metal Frame | 90 min | $1,500.00 |
| Porcelain Crown | 90 min | $1,200.00 |
| Amalgam / 1 Surface | 30 min | $180.00 |
| Anterior Composite / 1 Surface | 30 min | $220.00 |
| Posterior Composite / 1 Surface | 30 min | $250.00 |
| Night Guard | 30 min | $400.00 |

### 5.3 Cardiology
| Service | Default Duration | Default Price |
|---------|-----------------|---------------|
| Cardiology Consultation | 45 min | $300.00 |
| ECG / EKG | 20 min | $150.00 |
| Echocardiogram | 45 min | $500.00 |
| Stress Test | 60 min | $600.00 |

### 5.4 Dermatology
| Service | Default Duration | Default Price |
|---------|-----------------|---------------|
| Dermatology Consultation | 30 min | $200.00 |
| Skin Biopsy | 30 min | $350.00 |
| Mole Removal | 30 min | $300.00 |
| Acne Treatment | 30 min | $175.00 |

### 5.5 Orthopedics
| Service | Default Duration | Default Price |
|---------|-----------------|---------------|
| Orthopedic Consultation | 30 min | $250.00 |
| X-ray (Single View) | 15 min | $100.00 |
| Physical Therapy Session | 45 min | $150.00 |
| Joint Injection | 30 min | $350.00 |

### 5.6 Pediatrics
| Service | Default Duration | Default Price |
|---------|-----------------|---------------|
| Pediatric Well-Child Visit | 30 min | $175.00 |
| Pediatric Sick Visit | 20 min | $150.00 |
| Immunizations | 15 min | $75.00 |

### 5.7 Laboratory & Diagnostics
| Service | Default Duration | Default Price |
|---------|-----------------|---------------|
| Blood Work (CBC) | 10 min | $50.00 |
| Comprehensive Metabolic Panel | 10 min | $75.00 |
| Urinalysis | 10 min | $30.00 |
| COVID-19 Test | 15 min | $100.00 |

---

## 6. Non-Functional Requirements

### 6.1 Performance
- Page load time < 2 seconds for all views.
- Appointment calendar renders smoothly with 500+ appointments visible.
- Data API Builder responses < 200ms for standard queries.

### 6.2 Security
- Role-based access control (RBAC) for all four user roles.
- Patient data protected per HIPAA guidelines (simulated).
- Authentication via ASP.NET Core Identity (local accounts).
- All API endpoints secured with JWT bearer tokens.

### 6.3 Accessibility
- WCAG 2.1 AA compliance target.
- Keyboard navigation support for all interactive elements.
- Screen reader compatible labels and ARIA attributes.

### 6.4 Responsiveness
- Fully responsive design (mobile, tablet, desktop).
- Minimum supported viewport: 360px width.

### 6.5 Observability
- .NET Aspire dashboard for service health and metrics.
- Structured logging with OpenTelemetry.
- Health check endpoints for all services.

---

## 7. Technical Architecture

### 7.1 High-Level Architecture

```
┌──────────────────────────────────────────────────────────────────┐
│                      .NET Aspire AppHost                         │
│                  (Orchestration & Service Discovery)             │
├──────────────┬───────────────────┬───────────────────────────────┤
│              │                   │                               │
│  Blazor Web  │  Data API Builder │   SQL Server (LocalDB)        │
│  Application │  (REST/GraphQL)   │   ──────────────────          │
│  (Frontend)  │  (API Layer)      │   ContosoMedicalClinicDb      │
│              │                   │                               │
│  - Patient   │  Auto-generated   │   Tables:                     │
│    Portal    │  endpoints from   │   - Patients                  │
│  - Staff     │  SQL schema via   │   - Providers                 │
│    Portal    │  dab-config.json  │   - Appointments              │
│  - Admin     │                   │   - Services                  │
│    Portal    │                   │   - Invoices                  │
│              │                   │   - MedicalRecords            │
│              │                   │   - Staff / Shifts            │
│              │                   │   - Insurance                 │
└──────────────┴───────────────────┴───────────────────────────────┘
```

### 7.2 Solution Structure

```
ContosoMedicalClinic/
├── ContosoMedicalClinic.sln
├── src/
│   ├── ContosoMedicalClinic.AppHost/          # .NET Aspire AppHost
│   │   ├── Program.cs                         # Service orchestration
│   │   └── appsettings.json
│   │
│   ├── ContosoMedicalClinic.ServiceDefaults/  # Shared Aspire defaults
│   │   ├── Extensions.cs                      # OpenTelemetry, health checks
│   │   └── ContosoMedicalClinic.ServiceDefaults.csproj
│   │
│   ├── ContosoMedicalClinic.Web/              # Blazor Web App (SSR + Interactive)
│   │   ├── Components/
│   │   │   ├── Layout/                        # MainLayout, NavMenu
│   │   │   ├── Pages/
│   │   │   │   ├── Patient/                   # Patient portal pages
│   │   │   │   │   ├── Appointments.razor
│   │   │   │   │   ├── BookAppointment.razor
│   │   │   │   │   ├── VisitHistory.razor
│   │   │   │   │   ├── Invoices.razor
│   │   │   │   │   └── PayInvoice.razor
│   │   │   │   ├── Staff/                     # Staff portal pages
│   │   │   │   │   ├── Calendar.razor
│   │   │   │   │   ├── PatientRecords.razor
│   │   │   │   │   └── Payments.razor
│   │   │   │   ├── Admin/                     # Admin portal pages
│   │   │   │   │   ├── StaffRoster.razor
│   │   │   │   │   ├── ShiftManagement.razor
│   │   │   │   │   ├── InsuranceManagement.razor
│   │   │   │   │   └── ServiceCatalog.razor
│   │   │   │   ├── Home.razor
│   │   │   │   └── Login.razor
│   │   │   └── Shared/                        # Reusable components
│   │   ├── Services/                          # HTTP clients for DAB API
│   │   ├── Models/                            # C# DTOs / view models
│   │   ├── wwwroot/                           # Static assets, CSS
│   │   └── Program.cs
│   │
│   └── ContosoMedicalClinic.DataApi/          # Data API Builder config
│       ├── dab-config.json                    # DAB entity/endpoint config
│       └── Dockerfile                         # For containerized DAB
│
├── database/
│   ├── migrations/
│   │   ├── 001_CreateSchema.sql               # Initial schema
│   │   └── 002_SeedData.sql                   # Seed services, sample data
│   └── localdb-setup.ps1                      # LocalDB setup script
│
├── docs/
│   └── PRD.md                                 # This document
│
├── docker/
│   ├── Dockerfile.web                         # Blazor app Dockerfile
│   └── docker-compose.yml                     # Local Docker Compose (future)
│
└── tests/
    ├── ContosoMedicalClinic.Web.Tests/        # Blazor component tests
    └── ContosoMedicalClinic.Integration.Tests/ # Integration tests
```

### 7.3 Technology Stack

| Layer | Technology | Notes |
|-------|-----------|-------|
| **Orchestration** | .NET Aspire 9.x | Service discovery, health checks, telemetry |
| **Frontend** | Blazor Web App (.NET 9) | Server-side rendering + Interactive Server mode |
| **UI Framework** | MudBlazor | Modern Material Design components |
| **API Layer** | Data API Builder (DAB) | Auto-generates REST & GraphQL from SQL schema |
| **Database** | SQL Server LocalDB (dev) → Azure SQL Free Tier (prod) | Cost-optimized |
| **Authentication** | ASP.NET Core Identity | Cookie-based auth with role claims |
| **ORM (migrations only)** | EF Core (for schema migrations) | DAB handles runtime data access |
| **Observability** | OpenTelemetry + Aspire Dashboard | Traces, metrics, logs |
| **Containerization** | Docker | For future ACA deployment |

### 7.4 Data API Builder Configuration

Data API Builder will expose the following entities as REST and GraphQL endpoints:

| Entity | Table | Allowed Roles | Operations |
|--------|-------|---------------|------------|
| Patient | `dbo.Patients` | patient, staff, admin | CRUD |
| Provider | `dbo.Providers` | staff, admin | CRUD |
| Appointment | `dbo.Appointments` | patient, staff, admin | CRUD |
| Service | `dbo.Services` | patient (R), staff (R), admin (CRUD) |
| ServiceCategory | `dbo.ServiceCategories` | patient (R), staff (R), admin (CRUD) |
| Invoice | `dbo.Invoices` | patient (R), staff (CRUD), admin (CRUD) |
| InvoiceLineItem | `dbo.InvoiceLineItems` | patient (R), staff (CRUD), admin (CRUD) |
| Payment | `dbo.Payments` | patient (R,C), staff (CRUD), admin (CRUD) |
| MedicalRecord | `dbo.MedicalRecords` | patient (R), doctor (CRUD), admin (R) |
| VisitNote | `dbo.VisitNotes` | patient (R), doctor (CRUD), admin (R) |
| Staff | `dbo.Staff` | admin (CRUD), staff (R) |
| Shift | `dbo.Shifts` | admin (CRUD), staff (R) |
| InsuranceProvider | `dbo.InsuranceProviders` | staff (R), admin (CRUD) |
| InsuranceClaim | `dbo.InsuranceClaims` | staff (CRUD), admin (CRUD) |
| PatientInsurance | `dbo.PatientInsurance` | patient (R), staff (CRUD), admin (CRUD) |

---

## 8. Database Schema

### 8.1 Entity-Relationship Overview

```
Patients ──────< Appointments >────── Providers
    │                  │
    │                  │
    ├──< MedicalRecords (has many VisitNotes)
    │
    ├──< PatientInsurance >──── InsuranceProviders
    │
    ├──< Invoices ──< InvoiceLineItems >── Services
    │       │
    │       └──< Payments
    │
    └──< InsuranceClaims

Services >──── ServiceCategories

Staff ──< Shifts
Providers ──> Staff (1:1, provider is a staff member)
```

### 8.2 Core Tables

#### Patients
| Column | Type | Constraints |
|--------|------|-------------|
| PatientId | INT | PK, IDENTITY |
| FirstName | NVARCHAR(100) | NOT NULL |
| LastName | NVARCHAR(100) | NOT NULL |
| DateOfBirth | DATE | NOT NULL |
| Gender | NVARCHAR(20) | |
| Email | NVARCHAR(256) | UNIQUE, NOT NULL |
| Phone | NVARCHAR(20) | |
| Address | NVARCHAR(500) | |
| City | NVARCHAR(100) | |
| State | NVARCHAR(50) | |
| ZipCode | NVARCHAR(10) | |
| EmergencyContactName | NVARCHAR(200) | |
| EmergencyContactPhone | NVARCHAR(20) | |
| CreatedAt | DATETIME2 | DEFAULT GETUTCDATE() |
| UpdatedAt | DATETIME2 | |
| IsActive | BIT | DEFAULT 1 |

#### ServiceCategories
| Column | Type | Constraints |
|--------|------|-------------|
| CategoryId | INT | PK, IDENTITY |
| Name | NVARCHAR(100) | NOT NULL, UNIQUE |
| Description | NVARCHAR(500) | |
| IconCssClass | NVARCHAR(100) | |
| SortOrder | INT | DEFAULT 0 |
| IsActive | BIT | DEFAULT 1 |

#### Services
| Column | Type | Constraints |
|--------|------|-------------|
| ServiceId | INT | PK, IDENTITY |
| CategoryId | INT | FK → ServiceCategories |
| Name | NVARCHAR(200) | NOT NULL |
| Description | NVARCHAR(1000) | |
| DefaultDurationMinutes | INT | NOT NULL |
| DefaultPrice | DECIMAL(10,2) | NOT NULL |
| ProcedureCode | NVARCHAR(20) | |
| IsActive | BIT | DEFAULT 1 |
| CreatedAt | DATETIME2 | DEFAULT GETUTCDATE() |

#### Staff
| Column | Type | Constraints |
|--------|------|-------------|
| StaffId | INT | PK, IDENTITY |
| FirstName | NVARCHAR(100) | NOT NULL |
| LastName | NVARCHAR(100) | NOT NULL |
| Email | NVARCHAR(256) | UNIQUE, NOT NULL |
| Phone | NVARCHAR(20) | |
| Role | NVARCHAR(50) | NOT NULL (Doctor, Nurse, Hygienist, FrontOffice, Admin) |
| Specialization | NVARCHAR(100) | |
| LicenseNumber | NVARCHAR(50) | |
| HireDate | DATE | NOT NULL |
| IsActive | BIT | DEFAULT 1 |
| CreatedAt | DATETIME2 | DEFAULT GETUTCDATE() |

#### Providers
| Column | Type | Constraints |
|--------|------|-------------|
| ProviderId | INT | PK, IDENTITY |
| StaffId | INT | FK → Staff, UNIQUE |
| Title | NVARCHAR(20) | (Dr., NP, PA, etc.) |
| Bio | NVARCHAR(2000) | |
| PhotoUrl | NVARCHAR(500) | |
| AcceptsNewPatients | BIT | DEFAULT 1 |
| IsActive | BIT | DEFAULT 1 |

#### ProviderServices (Many-to-Many: Provider ↔ Service)
| Column | Type | Constraints |
|--------|------|-------------|
| ProviderId | INT | PK, FK → Providers |
| ServiceId | INT | PK, FK → Services |

#### Appointments
| Column | Type | Constraints |
|--------|------|-------------|
| AppointmentId | INT | PK, IDENTITY |
| PatientId | INT | FK → Patients |
| ProviderId | INT | FK → Providers |
| ServiceId | INT | FK → Services |
| AppointmentDate | DATE | NOT NULL |
| StartTime | TIME | NOT NULL |
| EndTime | TIME | NOT NULL |
| Status | NVARCHAR(30) | NOT NULL (Scheduled, Confirmed, InProgress, Completed, Cancelled, NoShow) |
| Notes | NVARCHAR(1000) | |
| CancellationReason | NVARCHAR(500) | |
| CreatedAt | DATETIME2 | DEFAULT GETUTCDATE() |
| UpdatedAt | DATETIME2 | |

#### MedicalRecords
| Column | Type | Constraints |
|--------|------|-------------|
| RecordId | INT | PK, IDENTITY |
| PatientId | INT | FK → Patients |
| BloodType | NVARCHAR(5) | |
| Allergies | NVARCHAR(2000) | |
| CurrentMedications | NVARCHAR(2000) | |
| ChronicConditions | NVARCHAR(2000) | |
| FamilyHistory | NVARCHAR(2000) | |
| CreatedAt | DATETIME2 | DEFAULT GETUTCDATE() |
| UpdatedAt | DATETIME2 | |

#### VisitNotes
| Column | Type | Constraints |
|--------|------|-------------|
| VisitNoteId | INT | PK, IDENTITY |
| AppointmentId | INT | FK → Appointments |
| PatientId | INT | FK → Patients |
| ProviderId | INT | FK → Providers |
| ChiefComplaint | NVARCHAR(500) | |
| Diagnosis | NVARCHAR(1000) | |
| TreatmentPlan | NVARCHAR(2000) | |
| Prescriptions | NVARCHAR(2000) | |
| FollowUpNotes | NVARCHAR(1000) | |
| CreatedAt | DATETIME2 | DEFAULT GETUTCDATE() |

#### Invoices
| Column | Type | Constraints |
|--------|------|-------------|
| InvoiceId | INT | PK, IDENTITY |
| PatientId | INT | FK → Patients |
| AppointmentId | INT | FK → Appointments (nullable) |
| InvoiceNumber | NVARCHAR(20) | UNIQUE, NOT NULL |
| InvoiceDate | DATE | NOT NULL |
| DueDate | DATE | NOT NULL |
| SubTotal | DECIMAL(10,2) | NOT NULL |
| InsuranceAdjustment | DECIMAL(10,2) | DEFAULT 0 |
| TotalAmount | DECIMAL(10,2) | NOT NULL |
| BalanceDue | DECIMAL(10,2) | NOT NULL |
| Status | NVARCHAR(20) | NOT NULL (Draft, Sent, Paid, PartiallyPaid, Overdue, Cancelled) |
| CreatedAt | DATETIME2 | DEFAULT GETUTCDATE() |

#### InvoiceLineItems
| Column | Type | Constraints |
|--------|------|-------------|
| LineItemId | INT | PK, IDENTITY |
| InvoiceId | INT | FK → Invoices |
| ServiceId | INT | FK → Services |
| Description | NVARCHAR(500) | NOT NULL |
| Quantity | INT | DEFAULT 1 |
| UnitPrice | DECIMAL(10,2) | NOT NULL |
| Amount | DECIMAL(10,2) | NOT NULL |

#### Payments
| Column | Type | Constraints |
|--------|------|-------------|
| PaymentId | INT | PK, IDENTITY |
| InvoiceId | INT | FK → Invoices |
| PatientId | INT | FK → Patients |
| PaymentDate | DATETIME2 | NOT NULL |
| Amount | DECIMAL(10,2) | NOT NULL |
| PaymentMethod | NVARCHAR(30) | NOT NULL (Cash, CreditCard, DebitCard, Insurance, Check) |
| ReferenceNumber | NVARCHAR(50) | |
| Notes | NVARCHAR(500) | |
| CreatedAt | DATETIME2 | DEFAULT GETUTCDATE() |

#### InsuranceProviders
| Column | Type | Constraints |
|--------|------|-------------|
| InsuranceProviderId | INT | PK, IDENTITY |
| Name | NVARCHAR(200) | NOT NULL |
| ContactPhone | NVARCHAR(20) | |
| ContactEmail | NVARCHAR(256) | |
| Website | NVARCHAR(500) | |
| IsActive | BIT | DEFAULT 1 |

#### PatientInsurance
| Column | Type | Constraints |
|--------|------|-------------|
| PatientInsuranceId | INT | PK, IDENTITY |
| PatientId | INT | FK → Patients |
| InsuranceProviderId | INT | FK → InsuranceProviders |
| PolicyNumber | NVARCHAR(50) | NOT NULL |
| GroupNumber | NVARCHAR(50) | |
| SubscriberName | NVARCHAR(200) | |
| EffectiveDate | DATE | NOT NULL |
| ExpirationDate | DATE | |
| IsPrimary | BIT | DEFAULT 1 |
| IsActive | BIT | DEFAULT 1 |

#### InsuranceClaims
| Column | Type | Constraints |
|--------|------|-------------|
| ClaimId | INT | PK, IDENTITY |
| PatientInsuranceId | INT | FK → PatientInsurance |
| InvoiceId | INT | FK → Invoices |
| ClaimNumber | NVARCHAR(50) | UNIQUE |
| SubmissionDate | DATE | NOT NULL |
| ClaimedAmount | DECIMAL(10,2) | NOT NULL |
| ApprovedAmount | DECIMAL(10,2) | |
| Status | NVARCHAR(30) | NOT NULL (Submitted, UnderReview, Approved, Denied, Appealed) |
| DenialReason | NVARCHAR(500) | |
| ProcessedDate | DATE | |
| CreatedAt | DATETIME2 | DEFAULT GETUTCDATE() |

#### Shifts
| Column | Type | Constraints |
|--------|------|-------------|
| ShiftId | INT | PK, IDENTITY |
| StaffId | INT | FK → Staff |
| ShiftDate | DATE | NOT NULL |
| StartTime | TIME | NOT NULL |
| EndTime | TIME | NOT NULL |
| ShiftType | NVARCHAR(20) | (Morning, Afternoon, Evening, Night) |
| Notes | NVARCHAR(500) | |
| CreatedAt | DATETIME2 | DEFAULT GETUTCDATE() |

#### Users (ASP.NET Core Identity — extended)
| Column | Type | Constraints |
|--------|------|-------------|
| Id | NVARCHAR(450) | PK (ASP.NET Identity default) |
| PatientId | INT | FK → Patients (nullable) |
| StaffId | INT | FK → Staff (nullable) |
| UserName | NVARCHAR(256) | |
| Email | NVARCHAR(256) | |
| PasswordHash | NVARCHAR(MAX) | |
| Role | NVARCHAR(50) | (Patient, FrontOffice, Doctor, Admin) |
| *(+ standard Identity columns)* | | |

---

## 9. User Interface Design

### 9.1 Design System
- **Theme:** Clean, professional medical aesthetic with a calming color palette.
- **Primary Color:** Deep Teal (#0D7377) — trust, health, professionalism.
- **Secondary Color:** Warm Coral (#FF6B6B) — approachable, action-oriented.
- **Neutral Palette:** White (#FFFFFF), Light Gray (#F5F7FA), Dark Gray (#2D3748).
- **Typography:** Inter or Segoe UI for body text; bold headings for hierarchy.
- **Icons:** Medical-themed icon set (Material Icons via MudBlazor).
- **Component Library:** MudBlazor for Material Design components.

### 9.2 Page Layout

#### Public Pages
| Page | Route | Description |
|------|-------|-------------|
| Home / Landing | `/` | Hero section, services overview, provider highlights, CTA to book |
| Services | `/services` | Browse service catalog by category |
| Our Providers | `/providers` | Provider directory with bios and specialties |
| Login | `/login` | Authentication page |
| Register | `/register` | Patient registration |

#### Patient Portal (`/patient/*`)
| Page | Route | Description |
|------|-------|-------------|
| Dashboard | `/patient/dashboard` | Upcoming appointments, recent invoices, quick actions |
| Book Appointment | `/patient/appointments/book` | Step-by-step booking wizard |
| My Appointments | `/patient/appointments` | List of upcoming & past appointments |
| Visit History | `/patient/visits` | Past visit reports with clinical summaries |
| My Invoices | `/patient/invoices` | Invoice list with status indicators |
| Pay Invoice | `/patient/invoices/{id}/pay` | Payment form for a specific invoice |
| My Profile | `/patient/profile` | View/edit personal information |

#### Staff Portal (`/staff/*`)
| Page | Route | Description |
|------|-------|-------------|
| Dashboard | `/staff/dashboard` | Today's schedule, stats, alerts |
| Appointment Calendar | `/staff/calendar` | Interactive calendar (day/week/month) |
| Patient Search | `/staff/patients` | Search and select patients |
| Patient Detail | `/staff/patients/{id}` | Full patient profile, records, history |
| Patient Records | `/staff/patients/{id}/records` | Medical records editor |
| Payments | `/staff/payments` | Record and manage payments |

#### Admin Portal (`/admin/*`)
| Page | Route | Description |
|------|-------|-------------|
| Dashboard | `/admin/dashboard` | Clinic KPIs, revenue, appointment stats |
| Staff Roster | `/admin/staff` | Staff list with CRUD |
| Shift Management | `/admin/shifts` | Shift scheduling calendar |
| Insurance Management | `/admin/insurance` | Insurance providers and claims |
| Service Catalog | `/admin/services` | Service management with categories |
| Reports | `/admin/reports` | Revenue, utilization, patient demographics |

### 9.3 Key UI Components
- **Appointment Booking Wizard:** Multi-step form (Select Category → Select Service → Select Provider → Pick Date/Time → Confirm).
- **Interactive Calendar:** Drag-and-drop appointment management with color-coded service categories.
- **Patient Record Viewer:** Tabbed interface (Demographics | Medical History | Visits | Invoices | Insurance).
- **Dashboard Cards:** KPI cards with icons, trend indicators, and sparkline charts.
- **Data Tables:** Sortable, filterable, paginated tables with inline actions.
- **Toast Notifications:** Success/error/info toasts for user feedback.

---

## 10. SOLID Design Principles Implementation

### 10.1 Single Responsibility Principle (SRP)
- Each Blazor component handles one view concern.
- Service classes are split by domain: `AppointmentService`, `PatientService`, `InvoiceService`, etc.
- Data access is delegated entirely to Data API Builder.

### 10.2 Open/Closed Principle (OCP)
- Use interfaces (`IAppointmentService`, `IPatientService`) so implementations can be swapped.
- Service catalog categories are data-driven (not hardcoded).

### 10.3 Liskov Substitution Principle (LSP)
- All service interfaces have consistent contracts.
- Mock implementations can substitute real services for testing.

### 10.4 Interface Segregation Principle (ISP)
- Narrow interfaces per domain rather than a monolithic `IClinicService`.
- Role-specific service interfaces (e.g., `IPatientPortalService` vs. `IStaffPortalService`).

### 10.5 Dependency Inversion Principle (DIP)
- All services registered via DI in `Program.cs`.
- Components depend on abstractions (interfaces), not concrete classes.
- `HttpClient` instances managed via `IHttpClientFactory`.

---

## 11. Data API Builder Integration

### 11.1 Overview
Data API Builder (DAB) will be configured via `dab-config.json` to auto-generate REST and GraphQL endpoints directly from the SQL Server schema. This eliminates the need for hand-written API controllers for CRUD operations.

### 11.2 Configuration Highlights
```json
{
  "data-source": {
    "database-type": "mssql",
    "connection-string": "@env('DATABASE_CONNECTION_STRING')"
  },
  "runtime": {
    "rest": { "path": "/api", "enabled": true },
    "graphql": { "path": "/graphql", "enabled": true },
    "host": {
      "authentication": {
        "provider": "StaticWebApps"
      }
    }
  },
  "entities": {
    "Patient": { "source": "dbo.Patients", "..." : "..." },
    "Appointment": { "source": "dbo.Appointments", "..." : "..." }
  }
}
```

### 11.3 Blazor ↔ DAB Communication
- Blazor services call DAB REST endpoints via typed `HttpClient`.
- Example: `AppointmentService` calls `GET /api/Appointment?$filter=PatientId eq {id}`.
- DAB handles filtering, sorting, pagination via OData-style query parameters.

---

## 12. .NET Aspire Integration

### 12.1 AppHost Configuration
```csharp
var builder = DistributedApplication.CreateBuilder(args);

// SQL Server (LocalDB for dev)
var sqlServer = builder.AddSqlServer("sql")
    .AddDatabase("ContosoMedicalClinicDb");

// Data API Builder
var dataApi = builder.AddContainer("data-api-builder", "mcr.microsoft.com/azure-databases/data-api-builder")
    .WithReference(sqlServer);

// Blazor Web App
var web = builder.AddProject<Projects.ContosoMedicalClinic_Web>("web")
    .WithReference(dataApi)
    .WithExternalHttpEndpoints();

builder.Build().Run();
```

### 12.2 Service Defaults
- OpenTelemetry tracing and metrics for all outgoing HTTP calls.
- Health checks for SQL connectivity and DAB availability.
- Resilience policies (retry, circuit breaker) via `Microsoft.Extensions.Http.Resilience`.

---

## 13. Deployment Strategy (Future — Not In Scope for v1)

### Phase 1 (Current): Local Development
- SQL Server LocalDB
- .NET Aspire orchestration
- `dotnet run` from AppHost

### Phase 2 (Future): Azure Deployment
- **Azure Container Apps** for Blazor app and DAB containers.
- **Azure SQL Database (Free Tier)** — 32 GB storage, 100 DTUs.
- **Azure Container Registry** for Docker images.
- Deployment scripts (Bicep/ARM + GitHub Actions) to be created later.

---

## 14. Sample / Seed Data

The application will include seed data for demonstration purposes:

- **5 Providers:** Dr. Sarah Chen (General Medicine), Dr. James Wilson (Cardiology), Dr. Maria Rodriguez (Dermatology), Dr. Michael Park (Orthopedics), Dr. Lisa Thompson (Dental).
- **10 Sample Patients:** Diverse demographics with pre-populated medical records.
- **30+ Appointments:** Mix of past (completed) and future (scheduled) appointments.
- **15 Invoices:** Various statuses (paid, pending, overdue).
- **3 Insurance Providers:** Contoso Health, Northwind Insurance, AdventureWorks Medical.
- **Full Service Catalog:** All services from Section 5 pre-loaded.
- **Staff & Shifts:** 8 staff members with weekly shift assignments.

---

## 15. Acceptance Criteria

### Patient Portal
- [ ] Patient can register and log in
- [ ] Patient can browse service catalog
- [ ] Patient can book an appointment (select service → provider → date/time)
- [ ] Patient can reschedule an appointment
- [ ] Patient can cancel an appointment
- [ ] Patient can view past visit reports
- [ ] Patient can view invoices with status
- [ ] Patient can make a payment on an invoice

### Staff Portal
- [ ] Staff can log in with role-based access
- [ ] Front office can view/manage appointment calendar
- [ ] Front office can add/edit/cancel appointments
- [ ] Front office can manage patient payments
- [ ] Doctor can view patient medical records
- [ ] Doctor can add visit notes

### Admin Portal
- [ ] Admin can manage staff roster (CRUD)
- [ ] Admin can assign and manage shifts
- [ ] Admin can manage insurance providers and claims
- [ ] Admin can manage service catalog (CRUD)
- [ ] Admin dashboard shows clinic KPIs

### Technical
- [ ] Application builds and runs with `dotnet run` from AppHost
- [ ] Aspire dashboard shows all services healthy
- [ ] Data API Builder endpoints respond correctly
- [ ] All pages are responsive (mobile + desktop)
- [ ] Role-based access control enforced on all routes

---

## 16. Risks & Mitigations

| Risk | Impact | Mitigation |
|------|--------|------------|
| DAB limitations for complex queries | Medium | Fallback to stored procedures or custom API endpoints |
| LocalDB not available on all machines | Low | Provide Docker-based SQL Server alternative |
| Aspire version breaking changes | Low | Pin to specific Aspire version in global.json |
| Large seed data slowing dev startup | Low | Conditional seeding based on environment |

---

## 17. Glossary

| Term | Definition |
|------|-----------|
| **DAB** | Data API Builder — Microsoft tool that generates REST/GraphQL APIs from database schemas |
| **Aspire** | .NET Aspire — cloud-ready orchestration framework for distributed .NET apps |
| **LocalDB** | SQL Server Express LocalDB — lightweight SQL Server for development |
| **ACA** | Azure Container Apps — serverless container hosting platform |
| **RBAC** | Role-Based Access Control |
| **CPT Code** | Current Procedural Terminology — standard medical procedure codes |
| **FMX** | Full-Mouth X-rays |

---

*End of PRD — v1.0*
