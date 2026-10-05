# Cleaning Service Booking System

A C# console application for managing cleaning service bookings — pricing, discounts, scheduling, and customer records — built against a formal Business Requirements Document (BRD).

> **Status:** In active development. Domain models, the full SQL-backed repository/service layer, and console UI input flows are built and wired together end-to-end. A few integration bugs remain around discount/pricing calculation being run more than once per booking — see Known Issues.

---

## Overview

The Cleaning Service Booking System lets booking administrators create and manage customer bookings for a residential cleaning service, while operations managers review bookings, summaries, and trends. Pricing is calculated from house type, service type, selected add-ons, applicable discounts, and weekend surcharges, following the formula set out in the project's BRD.

The system follows a layered architecture — **Domain → Application → Infrastructure → ConsoleUI** — applying SOLID principles throughout, with SQL Server as the persistence layer.

---

## Tech Stack

- **Language / Framework:** C# / .NET
- **Database:** SQL Server (via `Microsoft.Data.SqlClient`)
- **Console UI:** [Spectre.Console](https://spectreconsole.net/) (interactive menus and formatted output)
- **Security:** [BCrypt.Net](https://github.com/BcryptNet/bcrypt.net) for admin password hashing
- **Version Control:** Git / GitHub

---

## Architecture

```
C:\USERS\RPS3\DOCUMENTS\PROJECTS\CLEANINGSERVICEBOOKINGSYSTEM
|   .gitignore
|   CleaningServiceBookingSystem.slnx
|   ERDCleaningServicesBooking.png
|   README.md
|   
+---CleaningServiceBookingSystemMain
|   |   CleaningServiceBookingSystemMain.csproj
|   |   
|   +---Application
|   |   |   BookingStruct.cs
|   |   |   HashCryptography.cs
|   |   |   
|   |   +---Interfaces
|   |   |       IAddOnsRepository.cs
|   |   |       IAddOnsService.cs
|   |   |       IAdminRepository.cs
|   |   |       IAdminService.cs
|   |   |       IBookingAddOnService.cs
|   |   |       IBookingAddOnsRepository.cs
|   |   |       IBookingService.cs
|   |   |       IBookingsRepository.cs
|   |   |       ICustomerRepository.cs
|   |   |       ICustomerService.cs
|   |   |       IDiscountRulesRepository.cs
|   |   |       IDiscountRulesService.cs
|   |   |       IHouseTypeService.cs
|   |   |       IHouseTypesRepository.cs
|   |   |       IServiceTypesRepository.cs
|   |   |       IServiceTypesService.cs
|   |   |       
|   |   +---Services
|   |   |       AddOnsService.cs
|   |   |       AdminService.cs
|   |   |       BookingAddOnService.cs
|   |   |       BookingService.cs
|   |   |       CustomerService.cs
|   |   |       DiscountRulesService.cs
|   |   |       HouseTypeService.cs
|   |   |       ServiceTypesService.cs
|   |   |       
|   |   \---Validators
|   |           BookingValidator.cs
|   |           
|   |                   
|   +---ConsoleUI
|   |   |   AdminMenu.cs
|   |   |   ManagerMenu.cs
|   |   |   Program.cs
|   |   |   
|   |   \---InputMethods
|   |           AddonInput.cs
|   |           AdminInput.cs
|   |           BookingInput.cs
|   |           CustomerInput.cs
|   |           DateRangeInput.cs
|   |           ExistingAdmin.cs
|   |           HouseTypeInput.cs
|   |           ServiceTypeInput.cs
|   |           UpdateInput.cs
|   |           
|   +---Domain
|   |   +---Models
|   |   |       AddOns.cs
|   |   |       AddonSelection.cs
|   |   |       Admins.cs
|   |   |       BookingAddOns.cs
|   |   |       BookingByDate.cs
|   |   |       BookingByHouseType.cs
|   |   |       BookingDiscountUsage.cs
|   |   |       BookingRevenueSummary.cs
|   |   |       Bookings.cs
|   |   |       CustomerBookingHistory.cs
|   |   |       Customers.cs
|   |   |       DiscountRules.cs
|   |   |       HouseTypes.cs
|   |   |       ServiceTypes.cs
|   |   |       
|   |   \---Services
|   |           DiscountService.cs
|   |           PricingService.cs
|   |           
|   +---figlet-fonts-main
|   |       DOS Rebel.flf
|   |       
|   +---Infrastructure
|   |       DatabaseConnection.cs
|   |       RepositoryAddOns.cs
|   |       RepositoryAdmins.cs
|   |       RepositoryBookingAddOns.cs
|   |       RepositoryBookings.cs
|   |       RepositoryCustomers.cs
|   |       RepositoryDiscountRules.cs
|   |       RepositoryHouseTypes.cs
|   |       RepositoryServiceTypes.cs
|   |       
|   |                   
|   \---SQL scripts
|           Cleaning Service Booking Database.sql
|           Cleaning Service Booking Procedures.sql
|           Cleaning Service Booking Seeded Data.sql
|           

```

*An entity-relationship diagram is available in the repo: `ERDCleaningServicesBooking.png`.*

---

## User Instructions

*(To be expanded as the console UI is finalized.)*

1. Run the compiled application.
2. Choose a role at the main menu: **Booking Administrator** or **Operations Manager**.
3. Administrators log in, then can create bookings, register customers, view/update bookings, and manage admin accounts.
4. Managers can review bookings, summaries, and trends.

---

## Developer Instructions

### Prerequisites
- Visual Studio 2022 (or later) with .NET desktop development workload (project currently targets `net10.0`)
- SQL Server (local or remote instance)
- Git

### Setup
1. Clone the repository:
   ```bash
   git clone https://github.com/walters-dev/cleaning-service-booking-system.git
   ```
2. Open `CleaningServiceBookingSystem.slnx` in Visual Studio.
3. Restore NuGet packages (Spectre.Console, BCrypt.Net, Microsoft.Data.SqlClient) — Visual Studio will typically do this automatically on first build.
4. Run the provided SQL script (`Cleaning Service Booking Database.sql` then `Cleaning Service Booking Procedures.sql` and `Cleaning Service Booking Seeded Data.sql`) against your SQL Server instance to create and seed the database.
5. Configure the connection string in `Infrastructure/DatabaseConnection.cs` to point at your local database.
6. Build and run the project (`F5` in Visual Studio).
7. To sign into Admin Menu, the default username is Admin1 and the default password is Password1.

### Branching
- Each team member works on a dedicated feature branch (e.g. `ntando---calculation-+-validation`) rather than committing directly to `main`.
- Pull from your branch before pushing to avoid conflicts, especially since multiple people work on adjacent files.
- **Property names have shifted more than once during development** (e.g. `HouseTypeID` → `HouseTypeId`, `Percentage` → `DisPercentage`). Always validate a class against the *current* files on the branch you're working from before assuming it compiles.

---

## Team Workflow

- Work is split by BRD section/layer, with each contributor responsible for specific classes.
- Before opening a pull request: confirm the branch builds locally with no compile errors, and cross-check any class against the actual `Domain/Models` schema.
- Communicate schema changes (renamed properties, new fields) to the team promptly, since several classes depend on exact property names matching across files.
- Keep Git operations within a single tool per session (Git Bash *or* Visual Studio) rather than switching mid-task, to avoid repository-association issues.

---

## Known Issues / Current Status

**Working:**
- Full domain model set, including reporting models for booking history/summaries.
- Complete SQL-backed repository layer (`Infrastructure/`) with a matching `Interfaces`/`Services` pair for every entity.
- `BookingValidator` — validates customer, booking, and admin data per BRD section 15, including duplicate phone number / duplicate admin username checks against the database.
- `PricingService` / `DiscountService` — now called directly from `BookingInput.cs`, `UpdateInput.cs`, and `AdminMenu.cs` during booking creation and updates. Pricing is no longer disconnected from the UI.
- Admin login with hashed password verification (BCrypt), including duplicate-username prevention.

**Known bugs (priority to fix):**
- `DiscountService.CalculateDiscountAmount` uses hardcoded percentage constants rather than reading from the `DiscountRules` table, even though a full `IDiscountRulesRepository`/`DiscountRulesService`/`RepositoryDiscountRules` stack already exists and is unused for this purpose. `DiscountRuleId` is currently reconstructed by pattern-matching the returned discount *name* string against hardcoded IDs (`"DR001"`, `"DR002"`, `"DR003"`) in `BookingInput.cs`/`UpdateInput.cs`, rather than the discount lookup itself returning the ID.


**Planned:**
- Consolidate discount/pricing calculation to a single call site per booking (fix the duplication above).
- Wire `DiscountService` to `DiscountRules` from the database instead of hardcoded constants.
- Revisit `BookingValidator`'s repository dependencies for testability.

---

## Acknowledgments

Built as a team project against a Business Requirements Document (BRD) provided as part of the course. See `Project_1_Cleaning_Service_Booking_System_BRD.docx` for the full specification.
