CREATE DATABASE CleaningServiceBooking
using = CleaningServiceBooking

CREATE TABLE AdminTable (
    Admin_Id VARCHAR(7) PRIMARY KEY,
    Username VARCHAR(20) UNIQUE,
    Admin_Password VARCHAR(50),
    Email VARCHAR(50)
);


CREATE TABLE Customers (
    CustomerId VARCHAR(7) PRIMARY KEY,
    Fullname VARCHAR(MAX),
    Phonenumber VARCHAR(10),
    Email VARCHAR(255) UNIQUE,
    PhysAddress VARCHAR(255),
    CreatedAt DATE,
    UpdatedAt DATE,
    CreatedBy VARCHAR(MAX),
    UpdatedBy VARCHAR(MAX)
);


CREATE TABLE Housetypes (
    HouseTypesid VARCHAR(7) PRIMARY KEY,
    HouseName VARCHAR(MAX),
    BaseRate DECIMAL(10,2),
    RatePerRoom DECIMAL(10,2),
    MinRooms INT,
    MaxRooms INT,
    isActive BIT
);

INSERT INTO Housetypes(HouseTypesid, HouseName, BaseRate, RatePerRoom, MinRooms, MaxRooms)
VALUES
('HT001', 'Apartment/Flat', 350, 80, 1, 4),
('HT002', 'Townhouse', 500, 100, 2, 6),
('HT003', 'Standard House', 650, 120, 3, 8),
('HT004', 'Large House', 900, 150, 5, 12)

Select * from Housetypes


CREATE TABLE Servicetypes (
    ServiceTypeId VARCHAR(7) PRIMARY KEY,
    ServiceName VARCHAR(MAX),
    ServiceDescription VARCHAR(MAX),
    Multiplier DECIMAL(10,2),
    isActive BIT
);
 
Insert Into Servicetypes(ServiceTypeId, ServiceName, Multiplier, ServiceDescription)
Values
('ST001', 'Standard Clean', 1.00, 'General cleaning service calculated using base rate plus room rate.'),
('ST002', 'Deep Clean', 1.35, 'More intensive cleaning with higher labour time. '),
('ST003', 'Move-In/Move-Out Clean', 1.50, 'Higher effort clean for empty or recently occupied properties. ')

select * from Servicetypes

CREATE TABLE DiscountRules (
    DiscountRuleId VARCHAR(7) PRIMARY KEY,
    DiscountName VARCHAR(MAX),
    CriteriaDescription VARCHAR(MAX),
    isActive BIT,
    DiscPercentage DECIMAL(10,2)
);

Insert Into DiscountRules(DiscountRuleId, DiscountName, DiscPercentage, CriteriaDescription)
Values
('DR001', 'First-Time Customer Discount', 0.10,'Customer has no previous completed booking. Apply once only.'),
('DR002', 'Large Discount Booking', 0.15,'Booking has 6 or more rooms. Do not stack with first-time discount. '),
('DR003', 'Recurring Booking Discount', 0.12, 'Customer chooses weekly or bi-weekly recurring service. Do not stack with large booking discount. ')

Select * from DiscountRules

CREATE TABLE AddOns (
    AddOnId VARCHAR(10) PRIMARY KEY,
    AddOnsName VARCHAR(MAX),
    Rate DECIMAL(10,2),
    PricingType VARCHAR(MAX),
    isActive BIT
);

Insert into AddOns(AddOnId, AddOnsName, Rate, PricingType)
Values 
('AD001', 'Window Cleaning', 150, 'Flat add-on fee per booking. ' ),
('AD002', 'Carpet Cleaning', 200, 'Fee per carpeted room selected. '),
('AD003', 'Laundry Add-On', 100, 'Flat add-on fee per booking.')


Select * From AddOns

CREATE TABLE Bookings (
    BookingId VARCHAR(7) PRIMARY KEY,
    Customers_id VARCHAR(7),
    Housetypes_id VARCHAR(7),
    ServiceTypes_id VARCHAR(7),
    DiscountRule_id VARCHAR(7),
    BookingDate DATE,
    NumberOfRooms INT,
    IsRecurring BIT,
    RecurringBookingType VARCHAR(MAX),
    SubTotal DECIMAL(10,2),
    DiscountAmount DECIMAL(10,2),
    SurchargeAmount DECIMAL(10,2),
    TotalAmount DECIMAL(10,2),
    BookingStatus VARCHAR(MAX),
    CreatedAt DATE,
    UpdatedAt DATE,
    CreatedBy VARCHAR(MAX),
    UpdatedBy VARCHAR(MAX),

    FOREIGN KEY (Customers_id)
        REFERENCES Customers(CustomerId),

    FOREIGN KEY (Housetypes_id)
        REFERENCES Housetypes(HouseTypesid),

    FOREIGN KEY (ServiceTypes_id)
        REFERENCES Servicetypes(ServiceTypeId),

    FOREIGN KEY (DiscountRule_id)
        REFERENCES DiscountRules(DiscountRuleId)
);


CREATE TABLE BookingAddOns (
    BookingAddOnId VARCHAR(7) PRIMARY KEY,
    Booking_id VARCHAR(7),
    AddOn_id VARCHAR(10),
    Quantity INT,
    LineAmount VARCHAR(255),

    FOREIGN KEY (Booking_id)
        REFERENCES Bookings(BookingId),

    FOREIGN KEY (AddOn_id)
        REFERENCES AddOns(AddOnId)
);

