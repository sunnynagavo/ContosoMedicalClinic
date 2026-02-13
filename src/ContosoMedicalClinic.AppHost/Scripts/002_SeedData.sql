-- =============================================
-- Contoso Medical Clinic - Seed Data
-- =============================================

-- Service Categories
SET IDENTITY_INSERT dbo.ServiceCategories ON;
INSERT INTO dbo.ServiceCategories (CategoryId, Name, Description, IconCssClass, SortOrder)
VALUES
    (1, 'General Medicine', 'Comprehensive primary care and checkups', 'bi-heart-pulse', 1),
    (2, 'Dental Services', 'Complete dental care and oral health', 'bi-emoji-smile', 2),
    (3, 'Cardiology', 'Heart health and cardiovascular care', 'bi-activity', 3),
    (4, 'Dermatology', 'Skin care and dermatological treatments', 'bi-shield-plus', 4),
    (5, 'Orthopedics', 'Bone, joint, and muscle care', 'bi-bandaid', 5),
    (6, 'Pediatrics', 'Specialized care for children', 'bi-balloon', 6),
    (7, 'Laboratory & Diagnostics', 'Diagnostics and lab testing', 'bi-droplet', 7);
SET IDENTITY_INSERT dbo.ServiceCategories OFF;

-- Services
SET IDENTITY_INSERT dbo.Services ON;
INSERT INTO dbo.Services (ServiceId, CategoryId, Name, Description, DefaultDurationMinutes, DefaultPrice, ProcedureCode)
VALUES
    -- General Medicine
    (1,  1, 'General Consultation', 'Standard medical consultation', 30, 150.00, '99213'),
    (2,  1, 'Annual Physical Exam', 'Comprehensive yearly health assessment', 60, 250.00, '99395'),
    (3,  1, 'Follow-up Visit', 'Follow-up on previous treatment', 20, 100.00, '99214'),
    (4,  1, 'Urgent Care Visit', 'Walk-in urgent medical care', 30, 200.00, '99215'),
    -- Dental
    (5,  2, 'Doctor Exam w/ Full-mouth X-rays (FMX)', 'Complete dental exam with X-rays', 60, 300.00, 'D0210'),
    (6,  2, 'Adult Prophylaxis / Deep Cleaning', 'Professional teeth cleaning for adults', 45, 200.00, 'D1110'),
    (7,  2, 'Child Prophylaxis', 'Professional teeth cleaning for children', 30, 120.00, 'D1120'),
    (8,  2, 'Removable Partial - Metal Frame', 'Metal frame partial denture', 90, 1500.00, 'D5213'),
    (9,  2, 'Porcelain Crown', 'Porcelain dental crown', 90, 1200.00, 'D2740'),
    (10, 2, 'Amalgam / 1 Surface', 'Single surface amalgam filling', 30, 180.00, 'D2140'),
    (11, 2, 'Anterior Composite / 1 Surface', 'Single surface anterior composite filling', 30, 220.00, 'D2330'),
    (12, 2, 'Posterior Composite / 1 Surface', 'Single surface posterior composite filling', 30, 250.00, 'D2391'),
    (13, 2, 'Night Guard', 'Custom night guard for bruxism', 30, 400.00, 'D9944'),
    -- Cardiology
    (14, 3, 'Cardiology Consultation', 'Heart health evaluation', 45, 300.00, '99243'),
    (15, 3, 'ECG / EKG', 'Electrocardiogram', 20, 150.00, '93000'),
    (16, 3, 'Echocardiogram', 'Heart ultrasound imaging', 45, 500.00, '93306'),
    (17, 3, 'Stress Test', 'Cardiac stress test', 60, 600.00, '93015'),
    -- Dermatology
    (18, 4, 'Dermatology Consultation', 'Skin health evaluation', 30, 200.00, '99243'),
    (19, 4, 'Skin Biopsy', 'Skin tissue sampling', 30, 350.00, '11102'),
    (20, 4, 'Mole Removal', 'Excision of skin mole', 30, 300.00, '11400'),
    (21, 4, 'Acne Treatment', 'Acne evaluation and treatment plan', 30, 175.00, '99213'),
    -- Orthopedics
    (22, 5, 'Orthopedic Consultation', 'Musculoskeletal evaluation', 30, 250.00, '99243'),
    (23, 5, 'X-ray (Single View)', 'Single view diagnostic X-ray', 15, 100.00, '73030'),
    (24, 5, 'Physical Therapy Session', 'Guided physical therapy', 45, 150.00, '97110'),
    (25, 5, 'Joint Injection', 'Therapeutic joint injection', 30, 350.00, '20610'),
    -- Pediatrics
    (26, 6, 'Pediatric Well-Child Visit', 'Routine pediatric checkup', 30, 175.00, '99392'),
    (27, 6, 'Pediatric Sick Visit', 'Acute pediatric illness evaluation', 20, 150.00, '99213'),
    (28, 6, 'Immunizations', 'Childhood vaccination administration', 15, 75.00, '90460'),
    -- Lab
    (29, 7, 'Blood Work (CBC)', 'Complete blood count', 10, 50.00, '85025'),
    (30, 7, 'Comprehensive Metabolic Panel', 'Blood chemistry panel', 10, 75.00, '80053'),
    (31, 7, 'Urinalysis', 'Urine analysis', 10, 30.00, '81001'),
    (32, 7, 'COVID-19 Test', 'COVID-19 diagnostic test', 15, 100.00, '87635');
SET IDENTITY_INSERT dbo.Services OFF;

-- Staff
SET IDENTITY_INSERT dbo.Staff ON;
INSERT INTO dbo.Staff (StaffId, FirstName, LastName, Email, Phone, Role, Specialization, LicenseNumber, HireDate)
VALUES
    (1, 'Sarah', 'Chen', 'sarah.chen@contosomedical.com', '555-0101', 'Doctor', 'General Medicine', 'MD-2019-001', '2019-03-15'),
    (2, 'James', 'Wilson', 'james.wilson@contosomedical.com', '555-0102', 'Doctor', 'Cardiology', 'MD-2018-042', '2018-07-01'),
    (3, 'Maria', 'Rodriguez', 'maria.rodriguez@contosomedical.com', '555-0103', 'Doctor', 'Dermatology', 'MD-2020-015', '2020-01-10'),
    (4, 'Michael', 'Park', 'michael.park@contosomedical.com', '555-0104', 'Doctor', 'Orthopedics', 'MD-2017-088', '2017-09-20'),
    (5, 'Lisa', 'Thompson', 'lisa.thompson@contosomedical.com', '555-0105', 'Doctor', 'Dental', 'DDS-2019-023', '2019-06-01'),
    (6, 'Emily', 'Davis', 'emily.davis@contosomedical.com', '555-0106', 'Nurse', 'Pediatrics', 'RN-2021-044', '2021-02-15'),
    (7, 'Robert', 'Johnson', 'robert.johnson@contosomedical.com', '555-0107', 'FrontOffice', NULL, NULL, '2020-08-01'),
    (8, 'Amanda', 'White', 'amanda.white@contosomedical.com', '555-0108', 'Admin', NULL, NULL, '2018-01-10');
SET IDENTITY_INSERT dbo.Staff OFF;

-- Providers
SET IDENTITY_INSERT dbo.Providers ON;
INSERT INTO dbo.Providers (ProviderId, StaffId, Title, Bio, AcceptsNewPatients)
VALUES
    (1, 1, 'Dr.', 'Dr. Sarah Chen is a board-certified internist with over 10 years of experience in primary care.', 1),
    (2, 2, 'Dr.', 'Dr. James Wilson is a cardiologist specializing in preventive cardiology and heart failure management.', 1),
    (3, 3, 'Dr.', 'Dr. Maria Rodriguez is a dermatologist with expertise in medical and cosmetic dermatology.', 1),
    (4, 4, 'Dr.', 'Dr. Michael Park is an orthopedic surgeon specializing in sports medicine and joint replacement.', 1),
    (5, 5, 'Dr.', 'Dr. Lisa Thompson is a general dentist providing comprehensive dental care for patients of all ages.', 1);
SET IDENTITY_INSERT dbo.Providers OFF;

-- Patients
SET IDENTITY_INSERT dbo.Patients ON;
INSERT INTO dbo.Patients (PatientId, FirstName, LastName, DateOfBirth, Gender, Email, Phone, Address, City, State, ZipCode, EmergencyContactName, EmergencyContactPhone)
VALUES
    (1,  'John', 'Smith', '1985-04-12', 'Male', 'john.smith@email.com', '555-1001', '100 Main St', 'Medical City', 'MC', '12345', 'Jane Smith', '555-1002'),
    (2,  'Alice', 'Johnson', '1990-08-22', 'Female', 'alice.j@email.com', '555-1003', '200 Oak Ave', 'Medical City', 'MC', '12345', 'Bob Johnson', '555-1004'),
    (3,  'Carlos', 'Garcia', '1978-12-05', 'Male', 'carlos.g@email.com', '555-1005', '300 Pine Rd', 'Medical City', 'MC', '12346', 'Maria Garcia', '555-1006'),
    (4,  'Priya', 'Patel', '1995-03-18', 'Female', 'priya.p@email.com', '555-1007', '400 Elm St', 'Medical City', 'MC', '12346', 'Raj Patel', '555-1008'),
    (5,  'David', 'Lee', '1982-07-30', 'Male', 'david.lee@email.com', '555-1009', '500 Maple Dr', 'Medical City', 'MC', '12347', 'Susan Lee', '555-1010'),
    (6,  'Emma', 'Brown', '2015-11-14', 'Female', 'emma.parent@email.com', '555-1011', '600 Birch Ln', 'Medical City', 'MC', '12347', 'Tom Brown', '555-1012'),
    (7,  'Michael', 'Taylor', '1970-01-25', 'Male', 'mtaylor@email.com', '555-1013', '700 Cedar Ct', 'Medical City', 'MC', '12348', 'Linda Taylor', '555-1014'),
    (8,  'Sofia', 'Martinez', '1988-06-09', 'Female', 'sofia.m@email.com', '555-1015', '800 Walnut Blvd', 'Medical City', 'MC', '12348', 'Diego Martinez', '555-1016'),
    (9,  'William', 'Anderson', '1965-09-03', 'Male', 'wanderson@email.com', '555-1017', '900 Spruce Ave', 'Medical City', 'MC', '12349', 'Mary Anderson', '555-1018'),
    (10, 'Olivia', 'Thomas', '2000-02-28', 'Female', 'olivia.t@email.com', '555-1019', '1000 Ash Way', 'Medical City', 'MC', '12349', 'James Thomas', '555-1020');
SET IDENTITY_INSERT dbo.Patients OFF;

-- Medical Records
INSERT INTO dbo.MedicalRecords (PatientId, BloodType, Allergies, CurrentMedications, ChronicConditions, FamilyHistory)
VALUES
    (1, 'A+', 'Penicillin', 'Lisinopril 10mg daily', 'Hypertension', 'Father: heart disease'),
    (2, 'O-', 'None known', 'None', 'None', 'Mother: diabetes'),
    (3, 'B+', 'Sulfa drugs', 'Metformin 500mg BID, Atorvastatin 20mg', 'Type 2 Diabetes, High cholesterol', 'Both parents: diabetes'),
    (5, 'AB+', 'Latex', 'Ibuprofen PRN', 'None', 'No significant family history'),
    (7, 'A-', 'Aspirin', 'Amlodipine 5mg, Metoprolol 25mg', 'Hypertension, AFib', 'Father: stroke, Mother: hypertension'),
    (9, 'O+', 'Codeine', 'Omeprazole 20mg, Aspirin 81mg', 'GERD, CAD', 'Father: MI at 55, Mother: breast cancer');

-- Appointments (mix of past and future)
INSERT INTO dbo.Appointments (PatientId, ProviderId, ServiceId, AppointmentDate, StartTime, EndTime, Status, Notes)
VALUES
    -- Past appointments (completed)
    (1, 1, 1, '2026-01-10', '09:00', '09:30', 'Completed', 'Annual checkup'),
    (2, 1, 2, '2026-01-12', '10:00', '11:00', 'Completed', 'Physical exam'),
    (3, 2, 14, '2026-01-15', '08:30', '09:15', 'Completed', 'Cardiology follow-up'),
    (4, 3, 18, '2026-01-18', '14:00', '14:30', 'Completed', 'Skin rash evaluation'),
    (5, 4, 22, '2026-01-20', '11:00', '11:30', 'Completed', 'Knee pain evaluation'),
    (1, 5, 5, '2026-01-22', '09:00', '10:00', 'Completed', 'Dental exam with X-rays'),
    (7, 2, 15, '2026-01-25', '08:00', '08:20', 'Completed', 'EKG'),
    (8, 3, 21, '2026-01-28', '13:00', '13:30', 'Completed', 'Acne treatment'),
    (9, 1, 3, '2026-02-01', '10:00', '10:20', 'Completed', 'Follow-up visit'),
    (6, 1, 26, '2026-02-03', '09:00', '09:30', 'Completed', 'Well-child visit'),
    -- Past cancelled
    (10, 1, 1, '2026-02-05', '14:00', '14:30', 'Cancelled', NULL),
    -- Future appointments (scheduled)
    (1, 1, 3, '2026-02-20', '09:00', '09:20', 'Scheduled', 'Follow-up on hypertension'),
    (2, 5, 6, '2026-02-22', '10:00', '10:45', 'Scheduled', 'Dental cleaning'),
    (3, 2, 16, '2026-02-25', '08:30', '09:15', 'Confirmed', 'Echocardiogram'),
    (4, 3, 19, '2026-02-27', '14:00', '14:30', 'Scheduled', 'Skin biopsy'),
    (5, 4, 25, '2026-03-01', '11:00', '11:30', 'Scheduled', 'Knee injection'),
    (7, 2, 17, '2026-03-03', '08:00', '09:00', 'Scheduled', 'Stress test'),
    (8, 1, 1, '2026-03-05', '13:00', '13:30', 'Scheduled', 'General consultation'),
    (9, 1, 29, '2026-03-07', '10:00', '10:10', 'Confirmed', 'Blood work'),
    (10, 5, 7, '2026-03-10', '09:00', '09:30', 'Scheduled', 'Dental cleaning');

-- Visit Notes for completed appointments
INSERT INTO dbo.VisitNotes (AppointmentId, PatientId, ProviderId, ChiefComplaint, Diagnosis, TreatmentPlan, Prescriptions, FollowUpNotes)
VALUES
    (1, 1, 1, 'Routine checkup, mild headaches', 'Tension headache, hypertension well-controlled', 'Continue current medications, stress management', 'Lisinopril 10mg daily', 'Follow up in 6 weeks'),
    (2, 2, 1, 'Annual physical', 'Healthy, no acute issues', 'Maintain healthy diet and exercise', 'None', 'Next annual in 12 months'),
    (3, 3, 2, 'Chest tightness with exertion', 'Stable angina', 'Cardiac catheterization if symptoms worsen, increase nitrates', 'Nitroglycerin SL PRN', 'Echocardiogram scheduled'),
    (4, 4, 3, 'Persistent rash on forearms', 'Contact dermatitis', 'Avoid irritants, topical steroids', 'Triamcinolone cream 0.1% BID', 'Follow up in 2 weeks'),
    (5, 5, 4, 'Right knee pain, swelling', 'Mild osteoarthritis, right knee', 'Physical therapy 2x/week, NSAID for pain', 'Naproxen 500mg BID with food', 'Re-evaluate in 4 weeks, consider injection'),
    (6, 1, 5, 'Routine dental exam', 'Mild gingivitis, no cavities', 'Improved flossing routine, prescription rinse', 'Chlorhexidine rinse', 'Cleaning in 6 months'),
    (7, 7, 2, 'Palpitations', 'Atrial fibrillation, rate controlled', 'Continue rate control, anticoagulation', 'Metoprolol 25mg BID', 'Follow up in 3 months'),
    (8, 8, 3, 'Acne flare-up', 'Moderate acne vulgaris', 'Topical retinoid and antibiotic', 'Tretinoin 0.025% cream, Clindamycin gel', 'Follow up in 6 weeks');

-- Invoices
SET IDENTITY_INSERT dbo.Invoices ON;
INSERT INTO dbo.Invoices (InvoiceId, PatientId, AppointmentId, InvoiceNumber, InvoiceDate, DueDate, SubTotal, InsuranceAdjustment, TotalAmount, BalanceDue, Status)
VALUES
    (1,  1, 1,  'INV-2026-001', '2026-01-10', '2026-02-10', 150.00, 50.00, 100.00, 0.00, 'Paid'),
    (2,  2, 2,  'INV-2026-002', '2026-01-12', '2026-02-12', 250.00, 100.00, 150.00, 0.00, 'Paid'),
    (3,  3, 3,  'INV-2026-003', '2026-01-15', '2026-02-15', 300.00, 120.00, 180.00, 180.00, 'Overdue'),
    (4,  4, 4,  'INV-2026-004', '2026-01-18', '2026-02-18', 200.00, 80.00, 120.00, 120.00, 'Sent'),
    (5,  5, 5,  'INV-2026-005', '2026-01-20', '2026-02-20', 250.00, 0.00, 250.00, 250.00, 'Sent'),
    (6,  1, 6,  'INV-2026-006', '2026-01-22', '2026-02-22', 300.00, 150.00, 150.00, 75.00, 'PartiallyPaid'),
    (7,  7, 7,  'INV-2026-007', '2026-01-25', '2026-02-25', 150.00, 60.00, 90.00, 0.00, 'Paid'),
    (8,  8, 8,  'INV-2026-008', '2026-01-28', '2026-02-28', 175.00, 70.00, 105.00, 105.00, 'Sent'),
    (9,  9, 9,  'INV-2026-009', '2026-02-01', '2026-03-01', 100.00, 40.00, 60.00, 0.00, 'Paid'),
    (10, 6, 10, 'INV-2026-010', '2026-02-03', '2026-03-03', 175.00, 75.00, 100.00, 100.00, 'Sent');
SET IDENTITY_INSERT dbo.Invoices OFF;

-- Invoice Line Items
INSERT INTO dbo.InvoiceLineItems (InvoiceId, ServiceId, Description, Quantity, UnitPrice, Amount)
VALUES
    (1, 1, 'General Consultation', 1, 150.00, 150.00),
    (2, 2, 'Annual Physical Exam', 1, 250.00, 250.00),
    (3, 14, 'Cardiology Consultation', 1, 300.00, 300.00),
    (4, 18, 'Dermatology Consultation', 1, 200.00, 200.00),
    (5, 22, 'Orthopedic Consultation', 1, 250.00, 250.00),
    (6, 5, 'Dental Exam w/ Full-mouth X-rays', 1, 300.00, 300.00),
    (7, 15, 'ECG / EKG', 1, 150.00, 150.00),
    (8, 21, 'Acne Treatment', 1, 175.00, 175.00),
    (9, 3, 'Follow-up Visit', 1, 100.00, 100.00),
    (10, 26, 'Pediatric Well-Child Visit', 1, 175.00, 175.00);

-- Payments
INSERT INTO dbo.Payments (InvoiceId, PatientId, PaymentDate, Amount, PaymentMethod, ReferenceNumber)
VALUES
    (1, 1, '2026-01-15', 100.00, 'CreditCard', 'PAY-CC-001'),
    (2, 2, '2026-01-20', 150.00, 'Insurance', 'CLM-INS-001'),
    (6, 1, '2026-02-01', 75.00, 'DebitCard', 'PAY-DC-001'),
    (7, 7, '2026-02-05', 90.00, 'CreditCard', 'PAY-CC-002'),
    (9, 9, '2026-02-10', 60.00, 'Cash', NULL);

-- Insurance Providers
SET IDENTITY_INSERT dbo.InsuranceProviders ON;
INSERT INTO dbo.InsuranceProviders (InsuranceProviderId, Name, ContactPhone, ContactEmail, Website)
VALUES
    (1, 'Contoso Health Insurance', '800-555-0001', 'claims@contosohealth.com', 'https://contosohealth.com'),
    (2, 'Northwind Insurance', '800-555-0002', 'claims@northwindins.com', 'https://northwindinsurance.com'),
    (3, 'AdventureWorks Medical', '800-555-0003', 'claims@awmedical.com', 'https://adventureworksmedical.com');
SET IDENTITY_INSERT dbo.InsuranceProviders OFF;

-- Patient Insurance
INSERT INTO dbo.PatientInsurance (PatientId, InsuranceProviderId, PolicyNumber, GroupNumber, SubscriberName, EffectiveDate, ExpirationDate, IsPrimary)
VALUES
    (1, 1, 'CH-100001', 'GRP-A100', 'John Smith', '2025-01-01', '2026-12-31', 1),
    (2, 1, 'CH-100002', 'GRP-A100', 'Alice Johnson', '2025-01-01', '2026-12-31', 1),
    (3, 2, 'NW-200001', 'GRP-B200', 'Carlos Garcia', '2025-06-01', '2026-05-31', 1),
    (5, 3, 'AW-300001', 'GRP-C300', 'David Lee', '2025-01-01', '2026-12-31', 1),
    (7, 1, 'CH-100003', 'GRP-A100', 'Michael Taylor', '2025-01-01', '2026-12-31', 1),
    (8, 2, 'NW-200002', 'GRP-B200', 'Sofia Martinez', '2025-06-01', '2026-05-31', 1),
    (9, 1, 'CH-100004', 'GRP-A100', 'William Anderson', '2025-01-01', '2026-12-31', 1);

-- Insurance Claims
INSERT INTO dbo.InsuranceClaims (PatientInsuranceId, InvoiceId, ClaimNumber, SubmissionDate, ClaimedAmount, ApprovedAmount, Status, ProcessedDate)
VALUES
    (1, 1, 'CLM-2026-001', '2026-01-11', 150.00, 50.00, 'Approved', '2026-01-20'),
    (2, 2, 'CLM-2026-002', '2026-01-13', 250.00, 100.00, 'Approved', '2026-01-25'),
    (3, 3, 'CLM-2026-003', '2026-01-16', 300.00, NULL, 'UnderReview', NULL),
    (5, 6, 'CLM-2026-004', '2026-01-23', 300.00, 150.00, 'Approved', '2026-02-01'),
    (6, 8, 'CLM-2026-005', '2026-01-29', 175.00, NULL, 'Submitted', NULL),
    (7, 9, 'CLM-2026-006', '2026-02-02', 100.00, 40.00, 'Approved', '2026-02-08');

-- Shifts (sample week)
INSERT INTO dbo.Shifts (StaffId, ShiftDate, StartTime, EndTime, ShiftType, Notes)
VALUES
    -- Monday
    (1, '2026-02-16', '08:00', '16:00', 'Morning', NULL),
    (2, '2026-02-16', '08:00', '16:00', 'Morning', NULL),
    (5, '2026-02-16', '08:00', '16:00', 'Morning', NULL),
    (6, '2026-02-16', '08:00', '16:00', 'Morning', NULL),
    (7, '2026-02-16', '08:00', '17:00', 'Morning', 'Front desk coverage'),
    -- Tuesday
    (1, '2026-02-17', '08:00', '16:00', 'Morning', NULL),
    (3, '2026-02-17', '09:00', '17:00', 'Morning', NULL),
    (4, '2026-02-17', '08:00', '16:00', 'Morning', NULL),
    (6, '2026-02-17', '08:00', '16:00', 'Morning', NULL),
    (7, '2026-02-17', '08:00', '17:00', 'Morning', NULL),
    -- Wednesday
    (2, '2026-02-18', '08:00', '16:00', 'Morning', NULL),
    (3, '2026-02-18', '12:00', '20:00', 'Afternoon', NULL),
    (5, '2026-02-18', '08:00', '16:00', 'Morning', NULL),
    (7, '2026-02-18', '08:00', '17:00', 'Morning', NULL),
    -- Thursday
    (1, '2026-02-19', '08:00', '16:00', 'Morning', NULL),
    (4, '2026-02-19', '08:00', '16:00', 'Morning', NULL),
    (5, '2026-02-19', '12:00', '20:00', 'Afternoon', NULL),
    (6, '2026-02-19', '08:00', '16:00', 'Morning', NULL),
    (7, '2026-02-19', '08:00', '17:00', 'Morning', NULL),
    -- Friday
    (1, '2026-02-20', '08:00', '14:00', 'Morning', 'Half day'),
    (2, '2026-02-20', '08:00', '14:00', 'Morning', 'Half day'),
    (3, '2026-02-20', '08:00', '14:00', 'Morning', NULL),
    (7, '2026-02-20', '08:00', '14:00', 'Morning', NULL);

-- Provider-Service mappings
INSERT INTO dbo.ProviderServices (ProviderId, ServiceId)
VALUES
    (1, 1), (1, 2), (1, 3), (1, 4), (1, 29), (1, 30), (1, 31), (1, 32),
    (2, 14), (2, 15), (2, 16), (2, 17),
    (3, 18), (3, 19), (3, 20), (3, 21),
    (4, 22), (4, 23), (4, 24), (4, 25),
    (5, 5), (5, 6), (5, 7), (5, 8), (5, 9), (5, 10), (5, 11), (5, 12), (5, 13);

-- User Accounts (password for all demo accounts: "Password1!")
-- Hash generated via PBKDF2 placeholder — the app will use a constant for demo
INSERT INTO dbo.UserAccounts (Email, PasswordHash, DisplayName, Role, PatientId, StaffId)
VALUES
    -- Patient accounts
    ('john.smith@email.com',     'DEMO_HASH', 'John Smith',      'Patient', 1, NULL),
    ('alice.johnson@email.com',  'DEMO_HASH', 'Alice Johnson',   'Patient', 2, NULL),
    ('carlos.garcia@email.com',  'DEMO_HASH', 'Carlos Garcia',   'Patient', 3, NULL),
    -- Doctor accounts
    ('sarah.chen@contoso.com',   'DEMO_HASH', 'Dr. Sarah Chen',  'Doctor', NULL, 1),
    ('michael.r@contoso.com',    'DEMO_HASH', 'Dr. Michael R.',  'Doctor', NULL, 2),
    ('priya.patel@contoso.com',  'DEMO_HASH', 'Dr. Priya Patel', 'Doctor', NULL, 3),
    -- Staff accounts
    ('jennifer.l@contoso.com',   'DEMO_HASH', 'Jennifer Lopez',  'Staff', NULL, 7),
    ('robert.w@contoso.com',     'DEMO_HASH', 'Robert Wilson',   'Staff', NULL, 8),
    -- Admin account
    ('admin@contoso.com',        'DEMO_HASH', 'System Admin',    'Admin', NULL, NULL);
