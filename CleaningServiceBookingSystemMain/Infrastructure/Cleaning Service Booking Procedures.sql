CREATE DATABASE CleaningServiceBooking
using = CleaningServiceBooking
GO
/*Customer PROCEDURES*/
/* =================================================================================================================================*/
CREATE PROCEDURE GetCustomer
    @CustomerID VARCHAR (7)
AS
BEGIN
     SELECT *
     FROM Customers 
     WHERE CustomerId=@CustomerID;
END;
GO
CREATE PROCEDURE UpdateCustomer
    @CustomerID VARCHAR (7),
    @Fullname VARCHAR(MAX),
    @Phonenumber VARCHAR(10),
    @Email VARCHAR(255),
    @PhysAddress VARCHAR(255),
    @UpdatedAt DATE,
    @UpdatedBy VARCHAR(MAX)

AS 
BEGIN
      UPDATE Customers
      SET 
          Fullname = @Fullname,
          Phonenumber = @Phonenumber,
          Email = @Email,
          PhysAddress = @PhysAddress,
          UpdatedAt = @UpdatedAt,
          UpdatedBy = @UpdatedBy
      WHERE CustomerId = @CustomerID;
END;
GO
CREATE PROCEDURE AddCustomer
    @CustomerID VARCHAR (7),
    @Fullname VARCHAR(MAX),
    @Phonenumber VARCHAR(10),
    @Email VARCHAR(255),
    @PhysAddress VARCHAR(255),
    @CreatedAt DATE,
    @CreatedBy VARCHAR(MAX)

AS 
BEGIN
        INSERT INTO Customers
    (
        CustomerId,
        Fullname,
        Phonenumber,
        Email,
        PhysAddress,
        CreatedAt,
        CreatedBy
    )
    VALUES
    (
        @CustomerId,
        @Fullname,
        @Phonenumber,
        @Email,
        @PhysAddress,
        @CreatedAt,
        @CreatedBy
    );
END;
GO
CREATE PROCEDURE GetAllCustomers
AS
BEGIN
    SELECT *
    FROM Customers;
END;
GO
CREATE PROCEDURE DeleteCustomer
 @CustomerID VARCHAR (7)
AS
BEGIN
      DELETE
      FROM Customers
      WHERE CustomerId = @CustomerID;
END;
GO
/* Housetype PROCEDURES ===========================================================================================================================================================================*/

CREATE PROCEDURE AddHouseType
    @HouseTypesid VARCHAR(7),
    @HouseName VARCHAR(MAX),
    @BaseRate DECIMAL(10,2),
    @RatePerRoom DECIMAL(10,2),
    @MinRooms INT,
    @MaxRooms INT,
    @isActive BIT
AS
BEGIN

    INSERT INTO Housetypes
    (
        HouseTypesid,
        HouseName,
        BaseRate,
        RatePerRoom,
        MinRooms,
        MaxRooms,
        isActive
    )
    VALUES
    (
        @HouseTypesid,
        @HouseName,
        @BaseRate,
        @RatePerRoom,
        @MinRooms,
        @MaxRooms,
        @isActive
    );

END;
GO
CREATE PROCEDURE GetAllHouseTypes
AS
BEGIN
    SELECT * 
    FROM Housetypes;
END
GO
/*SERVICE PROCEDURES ==================================================================================================================================================================================================== */

GO

CREATE PROCEDURE AddDiscountRule
    @DiscountRuleId VARCHAR(7),
    @DiscountName VARCHAR(MAX),
    @CriteriaDescription VARCHAR(MAX),
    @isActive BIT,
    @DiscPercentage DECIMAL(10,2)
AS
BEGIN

    INSERT INTO DiscountRules
    (
        DiscountRuleId,
        DiscountName,
        CriteriaDescription,
        isActive,
        DiscPercentage
    )
    VALUES
    (
        @DiscountRuleId,
        @DiscountName,
        @CriteriaDescription,
        @isActive,
        @DiscPercentage
    );

END;

GO
CREATE PROCEDURE AddAddOn
    @AddOnId VARCHAR(10),
    @AddOnsName VARCHAR(MAX),
    @Rate DECIMAL(10,2),
    @PricingType VARCHAR(MAX),
    @isActive BIT
AS
BEGIN

    INSERT INTO AddOns
    (
        AddOnId,
        AddOnsName,
        Rate,
        PricingType,
        isActive
    )
    VALUES
    (
        @AddOnId,
        @AddOnsName,
        @Rate,
        @PricingType,
        @isActive
    );

END;
GO
CREATE PROCEDURE AddBooking
    @BookingId VARCHAR(7),
    @Customers_id VARCHAR(7),
    @Housetypes_id VARCHAR(7),
    @ServiceTypes_id VARCHAR(7),
    @DiscountRule_id VARCHAR(7),
    @BookingDate DATE,
    @NumberOfRooms INT,
    @IsRecurring BIT,
    @RecurringBookingType VARCHAR(MAX),
    @SubTotal DECIMAL(10,2),
    @DiscountAmount DECIMAL(10,2),
    @SurchargeAmount DECIMAL(10,2),
    @TotalAmount DECIMAL(10,2),
    @BookingStatus VARCHAR(MAX),
    @CreatedAt DATE,
    @CreatedBy VARCHAR(MAX)
AS
BEGIN

    INSERT INTO Bookings
    (
        BookingId,
        Customers_id,
        Housetypes_id,
        ServiceTypes_id,
        DiscountRule_id,
        BookingDate,
        NumberOfRooms,
        IsRecurring,
        RecurringBookingType,
        SubTotal,
        DiscountAmount,
        SurchargeAmount,
        TotalAmount,
        BookingStatus,
        CreatedAt,
        CreatedBy
    )
    VALUES
    (
        @BookingId,
        @Customers_id,
        @Housetypes_id,
        @ServiceTypes_id,
        @DiscountRule_id,
        @BookingDate,
        @NumberOfRooms,
        @IsRecurring,
        @RecurringBookingType,
        @SubTotal,
        @DiscountAmount,
        @SurchargeAmount,
        @TotalAmount,
        @BookingStatus,
        @CreatedAt,
        @CreatedBy
    );

END;
GO
CREATE PROCEDURE GetBooking
    @BookingId VARCHAR(7)
AS
BEGIN

    SELECT
        b.BookingId,
        c.Fullname AS CustomerName,
        h.HouseName,
        s.ServiceName,
        d.DiscountName,
        b.BookingDate,
        b.NumberOfRooms,
        b.IsRecurring,
        b.RecurringBookingType,
        b.SubTotal,
        b.DiscountAmount,
        b.SurchargeAmount,
        b.TotalAmount,
        b.BookingStatus,
        b.CreatedAt,
        b.UpdatedAt,
        b.CreatedBy,
        b.UpdatedBy

    FROM Bookings b

    INNER JOIN Customers c
        ON b.Customers_id = c.CustomerId

    INNER JOIN Housetypes h
        ON b.Housetypes_id = h.HouseTypesid

    INNER JOIN Servicetypes s
        ON b.ServiceTypes_id = s.ServiceTypeId

    LEFT JOIN DiscountRules d
        ON b.DiscountRule_id = d.DiscountRuleId

    WHERE b.BookingId = @BookingId

    ORDER BY b.BookingDate DESC;

END;
GO
CREATE PROCEDURE UpdateBooking
    @BookingId VARCHAR(7),
    @Customers_id VARCHAR(7),
    @Housetypes_id VARCHAR(7),
    @ServiceTypes_id VARCHAR(7),
    @DiscountRule_id VARCHAR(7),
    @BookingDate DATE,
    @NumberOfRooms INT,
    @IsRecurring BIT,
    @RecurringBookingType VARCHAR(MAX),
    @SubTotal DECIMAL(10,2),
    @DiscountAmount DECIMAL(10,2),
    @SurchargeAmount DECIMAL(10,2),
    @TotalAmount DECIMAL(10,2),
    @BookingStatus VARCHAR(MAX),
    @UpdatedAt DATE,
    @UpdatedBy VARCHAR(MAX)
AS
BEGIN

    UPDATE Bookings
    SET
        Customers_id = @Customers_id,
        Housetypes_id = @Housetypes_id,
        ServiceTypes_id = @ServiceTypes_id,
        DiscountRule_id = @DiscountRule_id,
        BookingDate = @BookingDate,
        NumberOfRooms = @NumberOfRooms,
        IsRecurring = @IsRecurring,
        RecurringBookingType = @RecurringBookingType,
        SubTotal = @SubTotal,
        DiscountAmount = @DiscountAmount,
        SurchargeAmount = @SurchargeAmount,
        TotalAmount = @TotalAmount,
        BookingStatus = @BookingStatus,
        UpdatedAt = @UpdatedAt,
        UpdatedBy = @UpdatedBy
    WHERE BookingId = @BookingId;

END;
GO
CREATE PROCEDURE AddBookingAddOn
    @BookingAddOnId VARCHAR(7),
    @Booking_id VARCHAR(7),
    @AddOn_id VARCHAR(10),
    @Quantity INT,
    @LineAmount VARCHAR(255)
AS
BEGIN

    INSERT INTO BookingAddOns
    (
        BookingAddOnId,
        Booking_id,
        AddOn_id,
        Quantity,
        LineAmount
    )
    VALUES
    (
        @BookingAddOnId,
        @Booking_id,
        @AddOn_id,
        @Quantity,
        @LineAmount
    );

END;
GO
CREATE PROCEDURE BookingListByDateRange
    @StartDate DATE,
    @EndDate DATE
AS
BEGIN

    SELECT
        b.BookingId,
        c.Fullname AS CustomerName,
        h.HouseName,
        s.ServiceName,
        b.BookingDate,
        b.NumberOfRooms,
        b.TotalAmount,
        b.BookingStatus

    FROM Bookings b

    INNER JOIN Customers c
        ON b.Customers_id = c.CustomerId

    INNER JOIN Housetypes h
        ON b.Housetypes_id = h.HouseTypesid

    INNER JOIN Servicetypes s
        ON b.ServiceTypes_id = s.ServiceTypeId

    WHERE b.BookingDate BETWEEN @StartDate AND @EndDate

    ORDER BY b.BookingDate ASC;

END;
GO
CREATE PROCEDURE CustomerBookingHistory
    @CustomerId VARCHAR(7)
AS
BEGIN

    SELECT
        c.CustomerId,
        c.Fullname AS CustomerName,
        b.BookingId,
        b.BookingDate,
        h.HouseName,
        s.ServiceName,
        b.NumberOfRooms,
        b.TotalAmount,
        b.BookingStatus

    FROM Customers c

    INNER JOIN Bookings b
        ON c.CustomerId = b.Customers_id

    INNER JOIN Housetypes h
        ON b.Housetypes_id = h.HouseTypesid

    INNER JOIN Servicetypes s
        ON b.ServiceTypes_id = s.ServiceTypeId

    WHERE c.CustomerId = @CustomerId

    ORDER BY b.BookingDate DESC;

END;
GO
CREATE PROCEDURE RevenueSummary
AS
BEGIN

    SELECT
        s.ServiceName,

        COUNT(b.BookingId) AS BookingCount,

        SUM(b.TotalAmount) AS TotalRevenue

    FROM Bookings b

    INNER JOIN Servicetypes s
        ON b.ServiceTypes_id = s.ServiceTypeId

    WHERE b.BookingStatus = 'Completed'

    GROUP BY s.ServiceName

    ORDER BY TotalRevenue DESC;

END;
GO
CREATE PROCEDURE BookingsByHouseType
AS
BEGIN

    SELECT
        h.HouseName,

        COUNT(b.BookingId) AS BookingCount

    FROM Housetypes h

    INNER JOIN Bookings b
        ON h.HouseTypesid = b.Housetypes_id

    GROUP BY h.HouseName

    ORDER BY BookingCount DESC;

END;
GO
CREATE PROCEDURE DiscountUsageSummary
AS
BEGIN

    SELECT
        d.DiscountName,

        b.BookingId,

        c.Fullname AS CustomerName,

        b.SubTotal,

        b.DiscountAmount,

        (b.SubTotal - b.DiscountAmount) AS AmountAfterDiscount

    FROM Bookings b

    INNER JOIN DiscountRules d
        ON b.DiscountRule_id = d.DiscountRuleId

    INNER JOIN Customers c
        ON b.Customers_id = c.CustomerId

    WHERE b.DiscountRule_id IS NOT NULL

    ORDER BY d.DiscountName ASC, b.BookingDate DESC;

END;
GO
GO
/*length procedures*/

GO
CREATE PROCEDURE AdminRowCount
AS
BEGIN
SELECT
    COUNT(AdminTable.Admin_Id) AS RowsCount
FROM AdminTable
END;

GO
CREATE PROCEDURE CustomersRowCount
AS
BEGIN
SELECT
    COUNT(Customers.CustomerId) AS RowsCount
FROM Customers
END;

GO
CREATE PROCEDURE HousetypesRowCount
AS
BEGIN
SELECT
    COUNT(Housetypes.HouseTypesid) AS RowsCount
FROM Housetypes
END;

GO
CREATE PROCEDURE ServicetypesRowCount
AS
BEGIN
SELECT
    COUNT(Servicetypes.ServiceTypeId) AS RowsCount
FROM Servicetypes
END;

GO
CREATE PROCEDURE DiscountRulesRowCount
AS
BEGIN
SELECT
    COUNT(DiscountRules.DiscountRuleId) AS RowsCount
FROM DiscountRules
END;

GO
CREATE PROCEDURE AddOnsRowCount
AS
BEGIN
SELECT
    COUNT(AddOns.AddOnId) AS RowsCount
FROM AddOns
END;

GO
CREATE PROCEDURE BookingsRowCount
AS
BEGIN
SELECT
    COUNT(Bookings.BookingId) AS RowsCount
FROM Bookings
END;

GO
CREATE PROCEDURE BookingAddOnsRowCount
AS
BEGIN
SELECT
    COUNT(BookingAddOns.BookingAddOnId) AS RowsCount
FROM BookingAddOns
END;
/*password procedure*/
GO
CREATE PROCEDURE GetAdminPassword
@AdminID VARCHAR(7)
AS
BEGIN
SELECT
    AdminTable.Admin_Password
FROM AdminTable
WHERE AdminTable.Admin_Id = @AdminID
END;