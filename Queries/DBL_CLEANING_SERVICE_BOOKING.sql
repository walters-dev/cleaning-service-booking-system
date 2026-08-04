CREATE TABLE BookingAddOns(
BookingAddOnId VARCHAR (7) PRIMARY KEY,
Booking_id VARCHAR (7),
FOREIGN KEY (Booking_id)REFERENCES Bookings(BookingId),
AddOn_id VARCHAR (10),
FOREIGN KEY (AddOn_id)REFERENCES AddOns(AddOnId),
Quantity INT,
LineAmount VARCHAR (255)
);

CREATE TABLE AdminTable(
Admin_Id VARCHAR (7) PRIMARY KEY,
Username VARCHAR (20),
Admin_Password VARCHAR (50),
Email VARCHAR (50),
Salt TEXT
);

CREATE TABLE AddOns(
AddOnId VARCHAR (10) PRIMARY KEY,
AddOnsName TEXT,
Rate DECIMAL,
PricingType TEXT,
isActive BOOLEAN
);

INSERT INTO AddOns(AddOnId, AddOnsName ,Rate, PricingType)
VALUES ('AOID003','Laundry Add-On', 100, ' Flat add-on fee per booking. ');

Select * 
From AddOns

CREATE TABLE Bookings(
BookingId VARCHAR (7) PRIMARY KEY,
Customers_id VARCHAR (7),
FOREIGN KEY (Customers_id) REFERENCES Customers(CustomerId),
Housetypes_id VARCHAR(7),
FOREIGN KEY (Housetypes_id) REFERENCES Housetypes(HouseTypesid),
ServiceTypes_id VARCHAR (7),
FOREIGN KEY (ServiceTypes_id) REFERENCES ServiceTypes(ServiceTypeId),
DiscountRule_id VARCHAR (7),
FOREIGN KEY (DiscountRule_id) REFERENCES DiscountRules(DiscountRuleId),
BookingDate DATE,
NumberOfRooms INTEGER,
IsRecurring BOOLEAN,
RecurringBookingType TEXT,
SubTotal DECIMAL(10,2),
DiscountAmount DECIMAL(10,2),
SurchargeAmount DECIMAL(10,2),
TotalAmount DECIMAL(10,2),
Status TEXT,
CreatedAt DATE,
UpdatedAt DATE,
CreatedBy TEXT,
UpdatedBy TEXT
);


CREATE TABLE Customers (
CustomerId VARCHAR (7) PRIMARY KEY,
Fullname TEXT,
Phonenumber VARCHAR (10),
Email VARCHAR (255) UNIQUE,
Address VARCHAR (255),
CreatedAt DATE,
UpdatedAt DATE,
CreatedBy TEXT,
UpdatedBy TEXT
);

CREATE TABLE Housetypes(
HouseTypesid VARCHAR(7) PRIMARY KEY,
HouseName TEXT,
BaseRate DECIMAL(10,2),
RatePerRoom DECIMAL(10,2),
MinRooms INTEGER,
MaxRooms INTEGER,
isActive BOOLEAN
);

INSERT INTO Housetypes(HouseTypesid, HouseName, BaseRate, RatePerRoom, MinRooms, MaxRooms)
VALUES ('HT004','Large House', 900, 150, 5, 12);

Select * 
From Housetypes


CREATE TABLE Servicetypes(
ServiceTypeId VARCHAR (7) PRIMARY KEY,
ServiceName TEXT,
Description TEXT,
Multiplier DECIMAL(10,2),
isActive BOOLEAN
);

INSERT INTO Servicetypes(ServiceTypeId, ServiceName, Description, Multiplier)
VALUES ('ST003', 'Move-in/Move-out Clean', 'Higher effort clean for empty or recently occupied properties. ', 1.50);

Select *
From Servicetypes

CREATE TABLE DiscountRules(
DiscountRuleId VARCHAR (7) PRIMARY KEY,
DiscountName TEXT,
CriteriaDescription TEXT,
isActive BOOLEAN,
Percentage DECIMAL(10,2)
);

INSERT INTO DiscountRules(DiscountRuleId, DiscountName, CriteriaDescription, Percentage)
VALUES ('DR003', 'Recurring Booking Discount', 'Customer chooses weekly or bi-weekly recurring service(Do not stack with large booking discount). ', 12);

Select *
From DiscountRules


