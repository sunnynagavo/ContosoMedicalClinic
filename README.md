<div align="center">

# 🏥 Contoso Medical Clinic

### A Modern Multi-Specialty Medical Clinic Web Application

[![.NET](https://img.shields.io/badge/.NET_10-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)](https://dotnet.microsoft.com/)
[![Aspire](https://img.shields.io/badge/Aspire_13-6C3FC5?style=for-the-badge&logo=dotnet&logoColor=white)](https://learn.microsoft.com/en-us/dotnet/aspire/)
[![Blazor](https://img.shields.io/badge/Blazor_Server-512BD4?style=for-the-badge&logo=blazor&logoColor=white)](https://blazor.net/)
[![SQL Server](https://img.shields.io/badge/SQL_Server-CC2927?style=for-the-badge&logo=microsoftsqlserver&logoColor=white)](https://www.microsoft.com/en-us/sql-server)
[![DAB](https://img.shields.io/badge/Data_API_Builder-0078D4?style=for-the-badge&logo=microsoft&logoColor=white)](https://learn.microsoft.com/en-us/azure/data-api-builder/)
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg?style=for-the-badge)](LICENSE)

*A full-featured, fictitious medical clinic management system built with .NET Aspire, Blazor, Data API Builder, and SQL Server — following Clean Architecture and SOLID principles.*

---

[**Getting Started**](#-getting-started) · [**Architecture**](#-architecture) · [**Features**](#-features) · [**Demo Accounts**](#-demo-accounts) · [**Tech Stack**](#-tech-stack) · [**Roadmap**](#-roadmap)

</div>

---

## 📋 Overview

Contoso Medical Clinic is a **multi-specialty healthcare management platform** that serves patients, doctors, staff, and administrators. The application covers the full clinic workflow — from appointment booking and patient records to billing, insurance claims, and staff management.

Built as a showcase for modern .NET development patterns, this project demonstrates:

- 🔷 **.NET Aspire** orchestration with service discovery & health checks
- 🎨 **Blazor Server** with a custom medical-themed UI (Bootstrap 5)
- 🗄️ **Data API Builder (DAB)** for zero-code REST API generation from SQL
- 🏗️ **Clean Architecture** with SOLID principles in C#
- 🔐 **Role-based access control** with cookie authentication
- 📊 **7 medical specialties**, 32+ services, 15 database tables

---

## ✨ Features

### 🩺 Patient Portal
| Feature | Description |
|---------|-------------|
| **Dashboard** | At-a-glance view of upcoming appointments, recent invoices, and quick actions |
| **Book Appointment** | Step-by-step wizard: select service → choose provider → pick date/time → confirm |
| **My Appointments** | View, filter, and manage all scheduled and past appointments |
| **Visit History** | Browse past visits with clinical notes, diagnoses, and treatment plans |
| **Invoices & Payments** | View invoices, check balances, and make payments |

### 🏥 Staff Portal *(Doctors & Front Office)*
| Feature | Description |
|---------|-------------|
| **Staff Dashboard** | Daily schedule, patient queue, and operational metrics |
| **Appointment Calendar** | Full calendar view with filtering by provider, date, and status |
| **Patient Records** | Search and view patient demographics, medical history, allergies, and visit notes |
| **Payment Management** | Record payments, generate invoices, and track payment history |

### ⚙️ Admin Portal
| Feature | Description |
|---------|-------------|
| **Analytics Dashboard** | Clinic-wide KPIs: appointments, revenue, patient stats |
| **Staff Roster** | Manage staff profiles, roles, and employment status |
| **Shift Management** | Create and assign shifts across the week |
| **Insurance Management** | Track insurance providers, patient policies, and claims |
| **Service Catalog** | Add, edit, and manage the full catalog of medical services |

### 🔐 Authentication & Authorization
| Feature | Description |
|---------|-------------|
| **Login / Register** | Email-based authentication with demo account quick-fill |
| **Role-Based Access** | Four roles: Patient, Doctor, Staff, Admin |
| **Protected Routes** | Portal sections visible only to authorized roles |
| **Navbar Adaptation** | Navigation dynamically adjusts based on logged-in role |

---

## 🏗️ Architecture

This project follows **Clean Architecture** with clear separation of concerns:

```
┌─────────────────────────────────────────────────────────────────────┐
│                       .NET Aspire AppHost                           │
│                   (Orchestration & Service Discovery)               │
├─────────────┬──────────────────┬──────────────┬─────────────────────┤
│             │                  │              │                     │
│  SQL Server │  DB Initializer  │  Data API    │  Blazor Web App     │
│  (Docker)   │  (Schema + Seed) │  Builder     │  (Frontend)         │
│             │                  │  (REST API)  │                     │
│  15 Tables  │  Runs on startup │  18 Entities │  14+ Pages          │
│  + Views    │  then exits      │  Auto-CRUD   │  3 Portals          │
│             │                  │              │  Role-based Auth    │
└─────────────┴──────────────────┴──────────────┴─────────────────────┘

Startup Chain:  SQL Server ──► DB Initializer ──► Data API Builder ──► Web App
                (container)    (runs & exits)     (container)          (project)
```

### Solution Structure

```
ContosoMedicalClinic/
├── 📄 ContosoMedicalClinic.slnx          # .NET 10 solution file
├── 📁 docs/
│   └── PRD.md                             # Product Requirements Document
│
└── 📁 src/
    ├── 🎯 ContosoMedicalClinic.AppHost/          # Aspire orchestration
    │   ├── AppHost.cs                             # Service wiring & startup chain
    │   ├── dab-config.json                        # DAB entity configuration
    │   └── Scripts/
    │       ├── 001_CreateSchema.sql               # Database schema (15 tables + views)
    │       └── 002_SeedData.sql                   # Seed data (demo patients, providers, etc.)
    │
    ├── 🟦 ContosoMedicalClinic.Domain/            # Core domain entities & enums
    │   ├── Entities/                               # Patient, Provider, Appointment, Invoice...
    │   └── Enums/                                  # AppointmentStatus, StaffRole, PaymentMethod...
    │
    ├── 📐 ContosoMedicalClinic.Application/       # DTOs & service interfaces
    │   ├── DTOs/Dtos.cs                            # Record types for all data transfer
    │   └── Interfaces/IServices.cs                 # 10 service contracts
    │
    ├── 🔧 ContosoMedicalClinic.Infrastructure/    # DAB HTTP client & service implementations
    │   ├── DataApi/DabHttpClient.cs                # Generic REST client for DAB
    │   ├── DataApi/DabServices.cs                  # All service implementations
    │   └── DependencyInjection/                    # DI registration
    │
    ├── 🌐 ContosoMedicalClinic.Web/               # Blazor Server frontend
    │   ├── Components/
    │   │   ├── Layout/MainLayout.razor             # Navbar + footer + role-based menus
    │   │   └── Pages/
    │   │       ├── Home.razor                      # Landing page with hero section
    │   │       ├── Login.razor / Register.razor    # Authentication pages
    │   │       ├── Services.razor                  # Expandable service catalog
    │   │       ├── Providers.razor                 # Provider directory
    │   │       ├── Patient/                        # 5 patient portal pages
    │   │       ├── Staff/                          # 4 staff portal pages
    │   │       └── Admin/                          # 5 admin portal pages
    │   ├── Program.cs                              # Cookie auth + Aspire + DI setup
    │   └── wwwroot/app.css                         # Custom medical theme (teal & coral)
    │
    ├── 🗃️ ContosoMedicalClinic.DbInitializer/     # Database initialization (console app)
    │   └── Program.cs                              # Creates DB, runs schema & seed scripts
    │
    └── 🔗 ContosoMedicalClinic.ServiceDefaults/   # Shared Aspire configuration
        └── Extensions.cs                           # OpenTelemetry, health checks, resilience
```

---

## 🚀 Getting Started

### Prerequisites

| Tool | Version | Purpose |
|------|---------|---------|
| [.NET SDK](https://dotnet.microsoft.com/download) | 10.0+ | Runtime & build |
| [.NET Aspire Workload](https://learn.microsoft.com/en-us/dotnet/aspire/) | 13.x | `dotnet workload install aspire` |
| [Docker Desktop](https://www.docker.com/products/docker-desktop/) | Latest | SQL Server & DAB containers |

### Quick Start

```bash
# 1. Clone the repository
git clone https://github.com/sunnynagavo/ContosoMedicalClinic.git
cd ContosoMedicalClinic

# 2. Restore & build
dotnet build ContosoMedicalClinic.slnx

# 3. Run with Aspire (starts SQL, initializes DB, launches DAB & web app)
cd src/ContosoMedicalClinic.AppHost
dotnet run
```

> 💡 Open the **Aspire Dashboard** URL printed in the console to monitor all resources.

### What Happens on Startup

1. **SQL Server** container starts (Docker)
2. **DB Initializer** waits for SQL, creates the database, runs schema & seed scripts, then exits
3. **Data API Builder** starts after DB init completes — serves REST API at `/api/*`
4. **Blazor Web App** starts after DAB is healthy — serves the frontend

---

## 👤 Demo Accounts

All demo accounts use the password: **`Password1!`**

| Role | Email | Portal Access |
|------|-------|---------------|
| 🟢 **Patient** | `john.smith@email.com` | Patient Portal |
| 🟢 **Patient** | `alice.johnson@email.com` | Patient Portal |
| 🟢 **Patient** | `carlos.garcia@email.com` | Patient Portal |
| 🔵 **Doctor** | `sarah.chen@contoso.com` | Staff Portal |
| 🔵 **Doctor** | `michael.r@contoso.com` | Staff Portal |
| 🔵 **Doctor** | `priya.patel@contoso.com` | Staff Portal |
| 🟡 **Staff** | `jennifer.l@contoso.com` | Staff Portal |
| 🟡 **Staff** | `robert.w@contoso.com` | Staff Portal |
| 🔴 **Admin** | `admin@contoso.com` | Admin Portal |

---

## 🩺 Medical Services

The clinic offers **32 services** across **7 specialties**:

<table>
<tr>
<td width="33%" valign="top">

### 💊 General Medicine
- General Consultation
- Annual Physical Exam
- Follow-up Visit
- Urgent Care Visit

### 🦷 Dental Services
- Full-mouth X-rays
- Adult/Child Prophylaxis
- Porcelain Crown
- Night Guard
- Composite Fillings

</td>
<td width="33%" valign="top">

### ❤️ Cardiology
- Cardiology Consultation
- ECG / EKG
- Echocardiogram
- Stress Test

### 🔬 Dermatology
- Dermatology Consultation
- Skin Biopsy
- Mole Removal
- Acne Treatment

</td>
<td width="33%" valign="top">

### 🦴 Orthopedics
- Orthopedic Consultation
- X-ray (Single View)
- Physical Therapy
- Joint Injection

### 👶 Pediatrics
- Well-Child Visit
- Sick Visit
- Immunizations

### 🧪 Lab & Diagnostics
- Blood Work (CBC)
- Metabolic Panel
- Urinalysis
- COVID-19 Test

</td>
</tr>
</table>

---

## 🛠️ Tech Stack

| Layer | Technology | Why |
|-------|-----------|-----|
| **Orchestration** | .NET Aspire 13 | Service discovery, health checks, telemetry, container management |
| **Frontend** | Blazor Server (.NET 10) | Interactive server-rendered pages with real-time updates |
| **UI** | Bootstrap 5 + Bootstrap Icons | Responsive design with custom medical theme |
| **API** | Data API Builder 1.6 | Zero-code REST API auto-generated from SQL schema |
| **Database** | SQL Server (Docker) | 15 tables, views, indexes — containerized for portability |
| **Auth** | ASP.NET Cookie Authentication | Simple, secure role-based access without EF Core Identity |
| **Architecture** | Clean Architecture / SOLID | Domain → Application → Infrastructure → Presentation |

---

## 🗄️ Database Schema

The database contains **15 tables**, **1 view**, and **11 indexes**:

```
┌──────────────────┐     ┌──────────────────┐     ┌──────────────────┐
│ ServiceCategories │────<│    Services      │     │     Staff        │
└──────────────────┘     └──────────────────┘     └────────┬─────────┘
                               │                           │
                               │                    ┌──────┴─────────┐
┌──────────────────┐     ┌─────┴────────────┐      │   Providers     │
│    Patients      │────<│  Appointments    │>─────│  (vw_Details)   │
└────────┬─────────┘     └──────────────────┘      └──────┬──────────┘
         │                                                │
    ┌────┴───────────┐    ┌──────────────────┐    ┌───────┴──────────┐
    │ MedicalRecords │    │    Invoices      │    │ProviderServices  │
    │  VisitNotes    │    │  InvoiceLines    │    └──────────────────┘
    │ PatientInsur.  │    │    Payments      │
    └────────────────┘    └──────────────────┘
                          
    ┌────────────────┐    ┌──────────────────┐
    │    Shifts      │    │InsuranceProviders│
    │ UserAccounts   │    │InsuranceClaims   │
    └────────────────┘    └──────────────────┘
```

---

## 📡 Data API Builder Endpoints

DAB auto-generates REST endpoints for all entities:

| Endpoint | HTTP Methods | Description |
|----------|-------------|-------------|
| `/api/Patient` | GET, POST, PATCH | Patient records |
| `/api/Provider` | GET, POST, PATCH | Provider records |
| `/api/ProviderDetails` | GET | Provider + Staff joined view |
| `/api/Appointment` | GET, POST, PATCH, DELETE | Appointments |
| `/api/Service` | GET, POST, PATCH, DELETE | Medical services |
| `/api/ServiceCategory` | GET, POST, PATCH, DELETE | Service categories |
| `/api/Invoice` | GET, POST, PATCH | Invoices |
| `/api/Payment` | GET, POST | Payments |
| `/api/Staff` | GET, POST, PATCH | Staff members |
| `/api/Shift` | GET, POST, PATCH, DELETE | Staff shifts |
| `/api/MedicalRecord` | GET, POST, PATCH | Medical records |
| `/api/VisitNote` | GET, POST, PATCH | Visit notes |
| `/api/InsuranceProvider` | GET, POST, PATCH | Insurance companies |
| `/api/InsuranceClaim` | GET, POST, PATCH | Insurance claims |
| `/api/PatientInsurance` | GET, POST, PATCH | Patient policies |
| `/api/UserAccount` | GET, POST, PATCH | User authentication |

> 📖 DAB also generates a **GraphQL** endpoint at `/graphql`

---

## 🎨 Design System

The application uses a custom medical theme built on Bootstrap 5:

| Element | Value | Usage |
|---------|-------|-------|
| **Primary** | `#0D7377` (Teal) | Navbar, buttons, headings, accents |
| **Secondary** | `#FF6B6B` (Coral) | CTAs, highlights, alerts |
| **Accent** | `#4ECDC4` (Mint) | Gradients, hover states |
| **Background** | `#F5F7FA` (Light Gray) | Page backgrounds |
| **Dark** | `#2D3748` (Charcoal) | Text, footer |

**UI Components:**
- 🎯 Hero section with gradient backgrounds
- 📊 KPI cards with colored left borders
- 🃏 Elevated cards with hover lift animations
- 🏷️ Color-coded status badges (Scheduled, Confirmed, Completed, etc.)
- ✅ Selectable cards with checkmark indicators (booking wizard)
- 📂 Expandable accordion sections (service catalog)

---

## 🗺️ Roadmap

- [x] Clean Architecture solution structure
- [x] Full database schema (15 tables + views)
- [x] Data API Builder integration
- [x] Blazor UI with 14+ pages
- [x] Role-based authentication (4 roles)
- [x] .NET Aspire orchestration
- [ ] Azure Container Apps deployment scripts
- [ ] Azure SQL Free Tier migration
- [ ] Docker Compose for local dev (non-Aspire)
- [ ] Unit & integration tests
- [ ] Real payment gateway integration (Stripe)
- [ ] Email notifications (appointment reminders)
- [ ] File upload for medical documents
- [ ] Dark mode theme toggle

---

## 📄 Documentation

- [Product Requirements Document (PRD)](docs/PRD.md) — Full requirements, user stories, and specifications

---

## 🤝 Contributing

1. Fork the repository
2. Create a feature branch (`git checkout -b feature/amazing-feature`)
3. Commit your changes (`git commit -m 'Add amazing feature'`)
4. Push to the branch (`git push origin feature/amazing-feature`)
5. Open a Pull Request

---

## 📝 License

This project is licensed under the MIT License — see the [LICENSE](LICENSE) file for details.

---

<div align="center">

**Built with ❤️ using .NET Aspire, Blazor, Data API Builder & SQL Server**

*A fictitious application for educational and demonstration purposes.*

</div>