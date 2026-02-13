-- =============================================
-- Contoso Medical Clinic - Database Schema
-- =============================================

-- Service Categories
CREATE TABLE dbo.ServiceCategories (
    CategoryId INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(100) NOT NULL UNIQUE,
    Description NVARCHAR(500) NULL,
    IconCssClass NVARCHAR(100) NULL,
    SortOrder INT NOT NULL DEFAULT 0,
    IsActive BIT NOT NULL DEFAULT 1
);

-- Services
CREATE TABLE dbo.Services (
    ServiceId INT IDENTITY(1,1) PRIMARY KEY,
    CategoryId INT NOT NULL REFERENCES dbo.ServiceCategories(CategoryId),
    Name NVARCHAR(200) NOT NULL,
    Description NVARCHAR(1000) NULL,
    DefaultDurationMinutes INT NOT NULL,
    DefaultPrice DECIMAL(10,2) NOT NULL,
    ProcedureCode NVARCHAR(20) NULL,
    IsActive BIT NOT NULL DEFAULT 1,
    CreatedAt DATETIME2 NOT NULL DEFAULT GETUTCDATE()
);

-- Staff
CREATE TABLE dbo.Staff (
    StaffId INT IDENTITY(1,1) PRIMARY KEY,
    FirstName NVARCHAR(100) NOT NULL,
    LastName NVARCHAR(100) NOT NULL,
    Email NVARCHAR(256) NOT NULL UNIQUE,
    Phone NVARCHAR(20) NULL,
    Role NVARCHAR(50) NOT NULL,
    Specialization NVARCHAR(100) NULL,
    LicenseNumber NVARCHAR(50) NULL,
    HireDate DATE NOT NULL,
    IsActive BIT NOT NULL DEFAULT 1,
    CreatedAt DATETIME2 NOT NULL DEFAULT GETUTCDATE()
);

-- Providers
CREATE TABLE dbo.Providers (
    ProviderId INT IDENTITY(1,1) PRIMARY KEY,
    StaffId INT NOT NULL UNIQUE REFERENCES dbo.Staff(StaffId),
    Title NVARCHAR(20) NULL,
    Bio NVARCHAR(2000) NULL,
    PhotoUrl NVARCHAR(500) NULL,
    AcceptsNewPatients BIT NOT NULL DEFAULT 1,
    IsActive BIT NOT NULL DEFAULT 1
);

-- Provider-Service mapping
CREATE TABLE dbo.ProviderServices (
    ProviderId INT NOT NULL REFERENCES dbo.Providers(ProviderId),
    ServiceId INT NOT NULL REFERENCES dbo.Services(ServiceId),
    PRIMARY KEY (ProviderId, ServiceId)
);

-- Patients
CREATE TABLE dbo.Patients (
    PatientId INT IDENTITY(1,1) PRIMARY KEY,
    FirstName NVARCHAR(100) NOT NULL,
    LastName NVARCHAR(100) NOT NULL,
    DateOfBirth DATE NOT NULL,
    Gender NVARCHAR(20) NULL,
    Email NVARCHAR(256) NOT NULL UNIQUE,
    Phone NVARCHAR(20) NULL,
    Address NVARCHAR(500) NULL,
    City NVARCHAR(100) NULL,
    State NVARCHAR(50) NULL,
    ZipCode NVARCHAR(10) NULL,
    EmergencyContactName NVARCHAR(200) NULL,
    EmergencyContactPhone NVARCHAR(20) NULL,
    CreatedAt DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
    UpdatedAt DATETIME2 NULL,
    IsActive BIT NOT NULL DEFAULT 1
);

-- Appointments
CREATE TABLE dbo.Appointments (
    AppointmentId INT IDENTITY(1,1) PRIMARY KEY,
    PatientId INT NOT NULL REFERENCES dbo.Patients(PatientId),
    ProviderId INT NOT NULL REFERENCES dbo.Providers(ProviderId),
    ServiceId INT NOT NULL REFERENCES dbo.Services(ServiceId),
    AppointmentDate DATE NOT NULL,
    StartTime TIME NOT NULL,
    EndTime TIME NOT NULL,
    Status NVARCHAR(30) NOT NULL DEFAULT 'Scheduled',
    Notes NVARCHAR(1000) NULL,
    CancellationReason NVARCHAR(500) NULL,
    CreatedAt DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
    UpdatedAt DATETIME2 NULL
);

-- Medical Records
CREATE TABLE dbo.MedicalRecords (
    RecordId INT IDENTITY(1,1) PRIMARY KEY,
    PatientId INT NOT NULL REFERENCES dbo.Patients(PatientId),
    BloodType NVARCHAR(5) NULL,
    Allergies NVARCHAR(2000) NULL,
    CurrentMedications NVARCHAR(2000) NULL,
    ChronicConditions NVARCHAR(2000) NULL,
    FamilyHistory NVARCHAR(2000) NULL,
    CreatedAt DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
    UpdatedAt DATETIME2 NULL
);

-- Visit Notes
CREATE TABLE dbo.VisitNotes (
    VisitNoteId INT IDENTITY(1,1) PRIMARY KEY,
    AppointmentId INT NOT NULL REFERENCES dbo.Appointments(AppointmentId),
    PatientId INT NOT NULL REFERENCES dbo.Patients(PatientId),
    ProviderId INT NOT NULL REFERENCES dbo.Providers(ProviderId),
    ChiefComplaint NVARCHAR(500) NULL,
    Diagnosis NVARCHAR(1000) NULL,
    TreatmentPlan NVARCHAR(2000) NULL,
    Prescriptions NVARCHAR(2000) NULL,
    FollowUpNotes NVARCHAR(1000) NULL,
    CreatedAt DATETIME2 NOT NULL DEFAULT GETUTCDATE()
);

-- Invoices
CREATE TABLE dbo.Invoices (
    InvoiceId INT IDENTITY(1,1) PRIMARY KEY,
    PatientId INT NOT NULL REFERENCES dbo.Patients(PatientId),
    AppointmentId INT NULL REFERENCES dbo.Appointments(AppointmentId),
    InvoiceNumber NVARCHAR(20) NOT NULL UNIQUE,
    InvoiceDate DATE NOT NULL,
    DueDate DATE NOT NULL,
    SubTotal DECIMAL(10,2) NOT NULL,
    InsuranceAdjustment DECIMAL(10,2) NOT NULL DEFAULT 0,
    TotalAmount DECIMAL(10,2) NOT NULL,
    BalanceDue DECIMAL(10,2) NOT NULL,
    Status NVARCHAR(20) NOT NULL DEFAULT 'Draft',
    CreatedAt DATETIME2 NOT NULL DEFAULT GETUTCDATE()
);

-- Invoice Line Items
CREATE TABLE dbo.InvoiceLineItems (
    LineItemId INT IDENTITY(1,1) PRIMARY KEY,
    InvoiceId INT NOT NULL REFERENCES dbo.Invoices(InvoiceId),
    ServiceId INT NOT NULL REFERENCES dbo.Services(ServiceId),
    Description NVARCHAR(500) NOT NULL,
    Quantity INT NOT NULL DEFAULT 1,
    UnitPrice DECIMAL(10,2) NOT NULL,
    Amount DECIMAL(10,2) NOT NULL
);

-- Payments
CREATE TABLE dbo.Payments (
    PaymentId INT IDENTITY(1,1) PRIMARY KEY,
    InvoiceId INT NOT NULL REFERENCES dbo.Invoices(InvoiceId),
    PatientId INT NOT NULL REFERENCES dbo.Patients(PatientId),
    PaymentDate DATETIME2 NOT NULL,
    Amount DECIMAL(10,2) NOT NULL,
    PaymentMethod NVARCHAR(30) NOT NULL,
    ReferenceNumber NVARCHAR(50) NULL,
    Notes NVARCHAR(500) NULL,
    CreatedAt DATETIME2 NOT NULL DEFAULT GETUTCDATE()
);

-- Insurance Providers
CREATE TABLE dbo.InsuranceProviders (
    InsuranceProviderId INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(200) NOT NULL,
    ContactPhone NVARCHAR(20) NULL,
    ContactEmail NVARCHAR(256) NULL,
    Website NVARCHAR(500) NULL,
    IsActive BIT NOT NULL DEFAULT 1
);

-- Patient Insurance
CREATE TABLE dbo.PatientInsurance (
    PatientInsuranceId INT IDENTITY(1,1) PRIMARY KEY,
    PatientId INT NOT NULL REFERENCES dbo.Patients(PatientId),
    InsuranceProviderId INT NOT NULL REFERENCES dbo.InsuranceProviders(InsuranceProviderId),
    PolicyNumber NVARCHAR(50) NOT NULL,
    GroupNumber NVARCHAR(50) NULL,
    SubscriberName NVARCHAR(200) NULL,
    EffectiveDate DATE NOT NULL,
    ExpirationDate DATE NULL,
    IsPrimary BIT NOT NULL DEFAULT 1,
    IsActive BIT NOT NULL DEFAULT 1
);

-- Insurance Claims
CREATE TABLE dbo.InsuranceClaims (
    ClaimId INT IDENTITY(1,1) PRIMARY KEY,
    PatientInsuranceId INT NOT NULL REFERENCES dbo.PatientInsurance(PatientInsuranceId),
    InvoiceId INT NOT NULL REFERENCES dbo.Invoices(InvoiceId),
    ClaimNumber NVARCHAR(50) NULL UNIQUE,
    SubmissionDate DATE NOT NULL,
    ClaimedAmount DECIMAL(10,2) NOT NULL,
    ApprovedAmount DECIMAL(10,2) NULL,
    Status NVARCHAR(30) NOT NULL DEFAULT 'Submitted',
    DenialReason NVARCHAR(500) NULL,
    ProcessedDate DATE NULL,
    CreatedAt DATETIME2 NOT NULL DEFAULT GETUTCDATE()
);

-- Shifts
CREATE TABLE dbo.Shifts (
    ShiftId INT IDENTITY(1,1) PRIMARY KEY,
    StaffId INT NOT NULL REFERENCES dbo.Staff(StaffId),
    ShiftDate DATE NOT NULL,
    StartTime TIME NOT NULL,
    EndTime TIME NOT NULL,
    ShiftType NVARCHAR(20) NULL,
    Notes NVARCHAR(500) NULL,
    CreatedAt DATETIME2 NOT NULL DEFAULT GETUTCDATE()
);

-- User Accounts (for authentication)
CREATE TABLE dbo.UserAccounts (
    UserAccountId INT IDENTITY(1,1) PRIMARY KEY,
    Email NVARCHAR(256) NOT NULL UNIQUE,
    PasswordHash NVARCHAR(500) NOT NULL,
    DisplayName NVARCHAR(200) NOT NULL,
    Role NVARCHAR(50) NOT NULL, -- Patient, Doctor, Staff, Admin
    PatientId INT NULL REFERENCES dbo.Patients(PatientId),
    StaffId INT NULL REFERENCES dbo.Staff(StaffId),
    IsActive BIT NOT NULL DEFAULT 1,
    CreatedAt DATETIME2 NOT NULL DEFAULT GETUTCDATE()
);

-- Indexes
CREATE INDEX IX_Services_CategoryId ON dbo.Services(CategoryId);
CREATE INDEX IX_Appointments_PatientId ON dbo.Appointments(PatientId);
CREATE INDEX IX_Appointments_ProviderId ON dbo.Appointments(ProviderId);
CREATE INDEX IX_Appointments_Date ON dbo.Appointments(AppointmentDate);
CREATE INDEX IX_Invoices_PatientId ON dbo.Invoices(PatientId);
CREATE INDEX IX_Payments_InvoiceId ON dbo.Payments(InvoiceId);
CREATE INDEX IX_VisitNotes_PatientId ON dbo.VisitNotes(PatientId);
CREATE INDEX IX_Shifts_StaffId ON dbo.Shifts(StaffId);
CREATE INDEX IX_MedicalRecords_PatientId ON dbo.MedicalRecords(PatientId);
CREATE INDEX IX_PatientInsurance_PatientId ON dbo.PatientInsurance(PatientId);
CREATE INDEX IX_InsuranceClaims_InvoiceId ON dbo.InsuranceClaims(InvoiceId);

GO

-- Views
CREATE OR ALTER VIEW dbo.vw_ProviderDetails AS
SELECT
    p.ProviderId, p.StaffId, p.Title, p.Bio, p.PhotoUrl,
    p.AcceptsNewPatients, p.IsActive,
    s.FirstName, s.LastName, s.Email, s.Phone,
    s.Specialization, s.LicenseNumber
FROM dbo.Providers p
INNER JOIN dbo.Staff s ON p.StaffId = s.StaffId;
