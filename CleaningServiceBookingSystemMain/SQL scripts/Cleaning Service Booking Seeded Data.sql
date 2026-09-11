INSERT INTO Housetypes(HouseTypesid, HouseName, BaseRate, RatePerRoom, MinRooms, MaxRooms)
VALUES
('HT001', 'Apartment/Flat', 350, 80, 1, 4),
('HT002', 'Townhouse', 500, 100, 2, 6),
('HT003', 'Standard House', 650, 120, 3, 8),
('HT004', 'Large House', 900, 150, 5, 12)

Insert Into Servicetypes(ServiceTypeId, ServiceName, Multiplier, ServiceDescription)
Values
('ST001', 'Standard Clean', 1.00, 'General cleaning service calculated using base rate plus room rate.'),
('ST002', 'Deep Clean', 1.35, 'More intensive cleaning with higher labour time. '),
('ST003', 'Move-In/Move-Out Clean', 1.50, 'Higher effort clean for empty or recently occupied properties. ')

Insert Into DiscountRules(DiscountRuleId, DiscountName, DiscPercentage, CriteriaDescription)
Values
('DR001', 'First-Time Customer Discount', 0.10,'Customer has no previous completed booking. Apply once only.'),
('DR002', 'Large Discount Booking', 0.15,'Booking has 6 or more rooms. Do not stack with first-time discount. '),
('DR003', 'Recurring Booking Discount', 0.12, 'Customer chooses weekly or bi-weekly recurring service. Do not stack with large booking discount. ')

Insert into AddOns(AddOnId, AddOnsName, Rate, PricingType)
Values 
('AD001', 'Window Cleaning', 150, 'Flat add-on fee per booking. ' ),
('AD002', 'Carpet Cleaning', 200, 'Fee per carpeted room selected. '),
('AD003', 'Laundry Add-On', 100, 'Flat add-on fee per booking.')

INSERT INTO AdminTable(Admin_Id, Username, Admin_Password, Email)
VALUES
('AT1','Admin','$2a$12$l8VsryAFB6I5iM44fc4XVuuxlq/EdYnBSVtRnUHWIG7c6BKhwO85i','admin@gmail.com')