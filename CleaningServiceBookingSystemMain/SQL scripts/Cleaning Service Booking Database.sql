CREATE DATABASE CleaningServiceBooking
using = CleaningServiceBooking

CREATE TABLE AdminTable (
    Admin_Id VARCHAR(7) PRIMARY KEY,
    Username VARCHAR(20) UNIQUE,
    Admin_Password VARCHAR(90),
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



CREATE TABLE Servicetypes (
    ServiceTypeId VARCHAR(7) PRIMARY KEY,
    ServiceName VARCHAR(MAX),
    ServiceDescription VARCHAR(MAX),
    Multiplier DECIMAL(10,2),
    isActive BIT
);
 


CREATE TABLE DiscountRules (
    DiscountRuleId VARCHAR(7) PRIMARY KEY,
    DiscountName VARCHAR(MAX),
    CriteriaDescription VARCHAR(MAX),
    isActive BIT,
    DiscPercentage DECIMAL(10,2)
);



CREATE TABLE AddOns (
    AddOnId VARCHAR(10) PRIMARY KEY,
    AddOnsName VARCHAR(MAX),
    Rate DECIMAL(10,2),
    PricingType VARCHAR(MAX),
    isActive BIT
);




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
    FirstTimeBooking BIT,
    CarpetedRooms INTEGER,

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
    LineAmount decimal,

    FOREIGN KEY (Booking_id)
        REFERENCES Bookings(BookingId),

    FOREIGN KEY (AddOn_id)
        REFERENCES AddOns(AddOnId)
);

