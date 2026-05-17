-- Insert Users
INSERT INTO users (username, email, password_hash, role, created_at) VALUES
('somchai_admin', 'somchai.manager@foodhub.com', 'hash_mgr_001', 'manager', CURRENT_TIMESTAMP - INTERVAL '90 days'),
('anong_manager', 'anong.manager@foodhub.com', 'hash_mgr_002', 'manager', CURRENT_TIMESTAMP - INTERVAL '84 days'),
('kittipong_manager', 'kittipong.manager@foodhub.com', 'hash_mgr_003', 'manager', CURRENT_TIMESTAMP - INTERVAL '78 days'),
('supansa_manager', 'supansa.manager@foodhub.com', 'hash_mgr_004', 'manager', CURRENT_TIMESTAMP - INTERVAL '72 days'),
('narin_manager', 'narin.manager@foodhub.com', 'hash_mgr_005', 'manager', CURRENT_TIMESTAMP - INTERVAL '66 days'),

('john_client', 'john.client@foodhub.com', 'hash_cli_001', 'client', CURRENT_TIMESTAMP - INTERVAL '65 days'),
('emma_client', 'emma.client@foodhub.com', 'hash_cli_002', 'client', CURRENT_TIMESTAMP - INTERVAL '63 days'),
('liam_client', 'liam.client@foodhub.com', 'hash_cli_003', 'client', CURRENT_TIMESTAMP - INTERVAL '61 days'),
('olivia_client', 'olivia.client@foodhub.com', 'hash_cli_004', 'client', CURRENT_TIMESTAMP - INTERVAL '59 days'),
('noah_client', 'noah.client@foodhub.com', 'hash_cli_005', 'client', CURRENT_TIMESTAMP - INTERVAL '57 days'),
('ava_client', 'ava.client@foodhub.com', 'hash_cli_006', 'client', CURRENT_TIMESTAMP - INTERVAL '55 days'),
('william_client', 'william.client@foodhub.com', 'hash_cli_007', 'client', CURRENT_TIMESTAMP - INTERVAL '53 days'),
('sophia_client', 'sophia.client@foodhub.com', 'hash_cli_008', 'client', CURRENT_TIMESTAMP - INTERVAL '51 days'),
('james_client', 'james.client@foodhub.com', 'hash_cli_009', 'client', CURRENT_TIMESTAMP - INTERVAL '49 days'),
('mia_client', 'mia.client@foodhub.com', 'hash_cli_010', 'client', CURRENT_TIMESTAMP - INTERVAL '47 days'),
('benjamin_client', 'benjamin.client@foodhub.com', 'hash_cli_011', 'client', CURRENT_TIMESTAMP - INTERVAL '45 days'),
('amelia_client', 'amelia.client@foodhub.com', 'hash_cli_012', 'client', CURRENT_TIMESTAMP - INTERVAL '43 days'),
('lucas_client', 'lucas.client@foodhub.com', 'hash_cli_013', 'client', CURRENT_TIMESTAMP - INTERVAL '41 days'),
('charlotte_client', 'charlotte.client@foodhub.com', 'hash_cli_014', 'client', CURRENT_TIMESTAMP - INTERVAL '39 days'),
('henry_client', 'henry.client@foodhub.com', 'hash_cli_015', 'client', CURRENT_TIMESTAMP - INTERVAL '37 days'),
('harper_client', 'harper.client@foodhub.com', 'hash_cli_016', 'client', CURRENT_TIMESTAMP - INTERVAL '35 days'),
('alexander_client', 'alexander.client@foodhub.com', 'hash_cli_017', 'client', CURRENT_TIMESTAMP - INTERVAL '33 days'),
('evelyn_client', 'evelyn.client@foodhub.com', 'hash_cli_018', 'client', CURRENT_TIMESTAMP - INTERVAL '31 days'),
('daniel_client', 'daniel.client@foodhub.com', 'hash_cli_019', 'client', CURRENT_TIMESTAMP - INTERVAL '29 days'),
('ella_client', 'ella.client@foodhub.com', 'hash_cli_020', 'client', CURRENT_TIMESTAMP - INTERVAL '27 days');

-- Insert Restaurants
INSERT INTO restaurants (name, manager_id, address, category, created_at) VALUES
('Bangkok Spice House', 1, 'Sukhumvit Soi 24, Khlong Toei, Bangkok', 'Thai', CURRENT_TIMESTAMP - INTERVAL '80 days'),
('Sakura Dining', 2, 'Thonglor Soi 10, Watthana, Bangkok', 'Japanese', CURRENT_TIMESTAMP - INTERVAL '78 days'),
('Seoul Garden BBQ', 3, 'Ratchadaphisek Road, Huai Khwang, Bangkok', 'Korean', CURRENT_TIMESTAMP - INTERVAL '76 days'),
('Golden Dragon Kitchen', 4, 'Yaowarat Road, Samphanthawong, Bangkok', 'Chinese', CURRENT_TIMESTAMP - INTERVAL '74 days'),
('Ocean Pearl Seafood', 5, 'Rama 3 Road, Yannawa, Bangkok', 'Seafood', CURRENT_TIMESTAMP - INTERVAL '72 days'),
('Urban Brew Cafe', 1, 'Ari Soi 2, Phaya Thai, Bangkok', 'Cafe', CURRENT_TIMESTAMP - INTERVAL '70 days'),
('Butter Bliss Bakery', 2, 'Ekkamai Road, Watthana, Bangkok', 'Bakery', CURRENT_TIMESTAMP - INTERVAL '68 days'),
('Prime Cut Steakhouse', 3, 'Silom Road, Bang Rak, Bangkok', 'Steakhouse', CURRENT_TIMESTAMP - INTERVAL '66 days');

-- Insert Promotions
INSERT INTO promotions (restaurant_id, title, price, conditions, total_quota, start_date, end_date) VALUES
(1, 'Thai Lunch Set', 199.00, 'Valid weekdays only', 200, CURRENT_TIMESTAMP - INTERVAL '15 days', CURRENT_TIMESTAMP + INTERVAL '15 days'),
(1, 'Family Dinner Deal', 499.00, 'Minimum 4 persons', 80, CURRENT_TIMESTAMP + INTERVAL '5 days', CURRENT_TIMESTAMP + INTERVAL '35 days'),
(1, 'Songkran Special', 159.00, 'One per customer', 150, CURRENT_TIMESTAMP - INTERVAL '70 days', CURRENT_TIMESTAMP - INTERVAL '40 days'),

(2, 'Sushi Combo', 299.00, 'Dine in only', 250, CURRENT_TIMESTAMP - INTERVAL '20 days', CURRENT_TIMESTAMP + INTERVAL '20 days'),
(2, 'Premium Omakase', 899.00, 'Reservation required', 30, CURRENT_TIMESTAMP + INTERVAL '10 days', CURRENT_TIMESTAMP + INTERVAL '40 days'),
(2, 'Ramen Festival', 229.00, 'Lunch hours only', 180, CURRENT_TIMESTAMP - INTERVAL '60 days', CURRENT_TIMESTAMP - INTERVAL '25 days'),

(3, 'Korean BBQ Buffet', 399.00, '2 hours limit', 300, CURRENT_TIMESTAMP - INTERVAL '10 days', CURRENT_TIMESTAMP + INTERVAL '20 days'),
(3, 'Soju Night', 259.00, 'After 6 PM', 100, CURRENT_TIMESTAMP + INTERVAL '7 days', CURRENT_TIMESTAMP + INTERVAL '37 days'),
(3, 'Kimchi Week', 149.00, 'One redemption only', 150, CURRENT_TIMESTAMP - INTERVAL '65 days', CURRENT_TIMESTAMP - INTERVAL '35 days'),

(4, 'Dim Sum Morning', 189.00, 'Before noon', 220, CURRENT_TIMESTAMP - INTERVAL '18 days', CURRENT_TIMESTAMP + INTERVAL '12 days'),
(4, 'Peking Duck Set', 699.00, 'Advance booking', 40, CURRENT_TIMESTAMP + INTERVAL '4 days', CURRENT_TIMESTAMP + INTERVAL '30 days'),
(4, 'Tea House Promo', 129.00, 'Tea included', 180, CURRENT_TIMESTAMP - INTERVAL '55 days', CURRENT_TIMESTAMP - INTERVAL '20 days'),

(5, 'Grilled River Prawn', 599.00, 'Weekend only', 60, CURRENT_TIMESTAMP - INTERVAL '12 days', CURRENT_TIMESTAMP + INTERVAL '18 days'),
(5, 'Seafood Bucket', 799.00, '2 persons minimum', 50, CURRENT_TIMESTAMP + INTERVAL '8 days', CURRENT_TIMESTAMP + INTERVAL '32 days'),
(5, 'Lobster Festival', 990.00, 'Limited stock', 25, CURRENT_TIMESTAMP - INTERVAL '58 days', CURRENT_TIMESTAMP - INTERVAL '28 days'),

(6, 'Coffee & Cake', 149.00, 'All day', 400, CURRENT_TIMESTAMP - INTERVAL '14 days', CURRENT_TIMESTAMP + INTERVAL '14 days'),
(6, 'Brunch Special', 259.00, '10 AM - 2 PM', 150, CURRENT_TIMESTAMP + INTERVAL '3 days', CURRENT_TIMESTAMP + INTERVAL '33 days'),
(6, 'Iced Latte Week', 99.00, 'One cup per visit', 300, CURRENT_TIMESTAMP - INTERVAL '50 days', CURRENT_TIMESTAMP - INTERVAL '15 days'),

(7, 'Croissant Set', 129.00, 'Morning only', 350, CURRENT_TIMESTAMP - INTERVAL '8 days', CURRENT_TIMESTAMP + INTERVAL '22 days'),
(7, 'Birthday Cake Deal', 599.00, '48h preorder', 70, CURRENT_TIMESTAMP + INTERVAL '6 days', CURRENT_TIMESTAMP + INTERVAL '36 days'),
(7, 'Cookie Festival', 89.00, 'Takeaway only', 250, CURRENT_TIMESTAMP - INTERVAL '52 days', CURRENT_TIMESTAMP - INTERVAL '18 days'),

(8, 'Steak Lunch', 499.00, 'Weekdays only', 120, CURRENT_TIMESTAMP - INTERVAL '16 days', CURRENT_TIMESTAMP + INTERVAL '16 days'),
(8, 'Wine Pairing', 999.00, 'Evening only', 35, CURRENT_TIMESTAMP + INTERVAL '9 days', CURRENT_TIMESTAMP + INTERVAL '39 days'),
(8, 'Grill Night', 699.00, 'Reservation required', 90, CURRENT_TIMESTAMP - INTERVAL '62 days', CURRENT_TIMESTAMP - INTERVAL '22 days');

-- Insert Promotion Tickets
INSERT INTO promotion_tickets (user_id, promotion_id, status, purchase_date, used_date)
SELECT
6 + ((g - 1) % 20),
CASE
WHEN g <= 20 THEN 1 + ((g - 1) % 5)
WHEN g <= 50 THEN 6 + ((g - 1) % 6)
ELSE 13 + ((g - 1) % 12)
END,
CASE
WHEN g % 5 = 0 THEN 'Expired'
WHEN g % 3 = 0 THEN 'Used'
ELSE 'Active'
END,
CURRENT_TIMESTAMP - ((g % 90) || ' days')::INTERVAL,
CASE
WHEN g % 3 = 0 AND g % 5 <> 0
THEN CURRENT_TIMESTAMP - ((g % 40) || ' days')::INTERVAL
ELSE NULL
END
FROM generate_series(1,80) g;

-- Insert Reviews
INSERT INTO reviews (user_id, restaurant_id, rating, comment, created_at) VALUES
(6,1,5,'Amazing authentic flavors and generous portions.', CURRENT_TIMESTAMP - INTERVAL '85 days'),
(7,1,4,'Great service and delicious food.', CURRENT_TIMESTAMP - INTERVAL '81 days'),
(8,1,5,'One of the best Thai restaurants in Bangkok.', CURRENT_TIMESTAMP - INTERVAL '77 days'),
(9,1,3,'Good food but slightly crowded.', CURRENT_TIMESTAMP - INTERVAL '73 days'),
(10,1,2,'Service was slow during dinner.', CURRENT_TIMESTAMP - INTERVAL '69 days'),
(11,1,4,'Very flavorful dishes and friendly staff.', CURRENT_TIMESTAMP - INTERVAL '65 days'),
(12,1,5,'Excellent green curry.', CURRENT_TIMESTAMP - INTERVAL '61 days'),

(13,2,5,'Fresh sushi and elegant presentation.', CURRENT_TIMESTAMP - INTERVAL '84 days'),
(14,2,4,'Very good ramen broth.', CURRENT_TIMESTAMP - INTERVAL '80 days'),
(15,2,3,'Average service but tasty food.', CURRENT_TIMESTAMP - INTERVAL '76 days'),
(16,2,5,'Outstanding omakase experience.', CURRENT_TIMESTAMP - INTERVAL '72 days'),
(17,2,4,'Quality ingredients and nice ambiance.', CURRENT_TIMESTAMP - INTERVAL '68 days'),
(18,2,5,'Definitely worth the price.', CURRENT_TIMESTAMP - INTERVAL '64 days'),
(19,2,4,'Loved the salmon nigiri.', CURRENT_TIMESTAMP - INTERVAL '60 days'),

(20,3,5,'Fantastic BBQ meats and side dishes.', CURRENT_TIMESTAMP - INTERVAL '83 days'),
(21,3,4,'Kimchi was excellent.', CURRENT_TIMESTAMP - INTERVAL '79 days'),
(22,3,3,'Decent food but a bit noisy.', CURRENT_TIMESTAMP - INTERVAL '75 days'),
(23,3,5,'Authentic Korean flavors.', CURRENT_TIMESTAMP - INTERVAL '71 days'),
(24,3,4,'Good value for money.', CURRENT_TIMESTAMP - INTERVAL '67 days'),
(25,3,5,'Staff helped us grill perfectly.', CURRENT_TIMESTAMP - INTERVAL '63 days'),

(6,4,4,'Great dim sum selection.', CURRENT_TIMESTAMP - INTERVAL '82 days'),
(7,4,5,'Best Chinese food in the area.', CURRENT_TIMESTAMP - INTERVAL '78 days'),
(8,4,3,'Food was okay, nothing special.', CURRENT_TIMESTAMP - INTERVAL '74 days'),
(9,4,4,'Nice tea and dumplings.', CURRENT_TIMESTAMP - INTERVAL '70 days'),
(10,4,2,'Waited too long for a table.', CURRENT_TIMESTAMP - INTERVAL '66 days'),
(11,4,5,'Excellent roast duck.', CURRENT_TIMESTAMP - INTERVAL '62 days'),

(12,5,5,'Fresh seafood and great seasoning.', CURRENT_TIMESTAMP - INTERVAL '81 days'),
(13,5,4,'Loved the grilled prawns.', CURRENT_TIMESTAMP - INTERVAL '77 days'),
(14,5,5,'Very premium dining experience.', CURRENT_TIMESTAMP - INTERVAL '73 days'),
(15,5,4,'Nice waterfront atmosphere.', CURRENT_TIMESTAMP - INTERVAL '69 days'),
(16,5,3,'Good but expensive.', CURRENT_TIMESTAMP - INTERVAL '65 days'),
(17,5,5,'Perfect for family dinner.', CURRENT_TIMESTAMP - INTERVAL '61 days'),

(18,6,5,'Excellent coffee and desserts.', CURRENT_TIMESTAMP - INTERVAL '80 days'),
(19,6,4,'Relaxing atmosphere.', CURRENT_TIMESTAMP - INTERVAL '76 days'),
(20,6,3,'Coffee was decent.', CURRENT_TIMESTAMP - INTERVAL '72 days'),
(21,6,4,'Friendly baristas.', CURRENT_TIMESTAMP - INTERVAL '68 days'),
(22,6,2,'Table was not very clean.', CURRENT_TIMESTAMP - INTERVAL '64 days'),
(23,6,5,'Best latte in Ari.', CURRENT_TIMESTAMP - INTERVAL '60 days'),

(24,7,5,'Fresh pastries every morning.', CURRENT_TIMESTAMP - INTERVAL '79 days'),
(25,7,4,'Croissants were buttery and crisp.', CURRENT_TIMESTAMP - INTERVAL '75 days'),
(6,7,3,'Cake was okay.', CURRENT_TIMESTAMP - INTERVAL '71 days'),
(7,7,4,'Nice packaging and service.', CURRENT_TIMESTAMP - INTERVAL '67 days'),
(8,7,2,'Limited seating area.', CURRENT_TIMESTAMP - INTERVAL '63 days'),
(9,7,5,'Excellent desserts.', CURRENT_TIMESTAMP - INTERVAL '59 days'),

(10,8,5,'Perfectly cooked steak.', CURRENT_TIMESTAMP - INTERVAL '78 days'),
(11,8,4,'Elegant dining room.', CURRENT_TIMESTAMP - INTERVAL '74 days'),
(12,8,3,'Portions could be larger.', CURRENT_TIMESTAMP - INTERVAL '70 days'),
(13,8,5,'High quality meat.', CURRENT_TIMESTAMP - INTERVAL '66 days'),
(14,8,4,'Great wine recommendations.', CURRENT_TIMESTAMP - INTERVAL '62 days'),
(15,8,1,'Steak arrived cold.', CURRENT_TIMESTAMP - INTERVAL '58 days');

-- Insert Promotions
INSERT INTO promotions (restaurant_id, title, price, conditions, total_quota, start_date, end_date) VALUES

-- Restaurant 1 : Bangkok Spice House
(1, 'Pad Kra Pao Power Meal', 219.00, 'Lunch only', 180, CURRENT_TIMESTAMP - INTERVAL '11 days', CURRENT_TIMESTAMP + INTERVAL '19 days'),
(1, 'Mango Sticky Rice Delight', 149.00, 'Dessert included', 220, CURRENT_TIMESTAMP + INTERVAL '7 days', CURRENT_TIMESTAMP + INTERVAL '37 days'),
(1, 'Royal Thai Curry Feast', 589.00, 'For 2 persons', 90, CURRENT_TIMESTAMP - INTERVAL '58 days', CURRENT_TIMESTAMP - INTERVAL '18 days'),
(1, 'Spicy Seafood Adventure', 449.00, 'Dinner only', 75, CURRENT_TIMESTAMP - INTERVAL '6 days', CURRENT_TIMESTAMP + INTERVAL '24 days'),
(1, 'Northern Thai Discovery', 329.00, 'Dine in only', 140, CURRENT_TIMESTAMP + INTERVAL '12 days', CURRENT_TIMESTAMP + INTERVAL '42 days'),
(1, 'Street Food Collection', 269.00, 'One set per customer', 200, CURRENT_TIMESTAMP - INTERVAL '44 days', CURRENT_TIMESTAMP - INTERVAL '9 days'),
(1, 'Coconut Soup Experience', 189.00, 'Afternoon only', 160, CURRENT_TIMESTAMP - INTERVAL '15 days', CURRENT_TIMESTAMP + INTERVAL '15 days'),
(1, 'Bangkok Chef Signature', 699.00, 'Reservation required', 40, CURRENT_TIMESTAMP + INTERVAL '4 days', CURRENT_TIMESTAMP + INTERVAL '34 days'),
(1, 'Thai Festival Sharing Set', 799.00, 'For 4 persons', 35, CURRENT_TIMESTAMP - INTERVAL '63 days', CURRENT_TIMESTAMP - INTERVAL '23 days'),
(1, 'Green Curry Lovers Deal', 239.00, 'Valid everyday', 250, CURRENT_TIMESTAMP - INTERVAL '8 days', CURRENT_TIMESTAMP + INTERVAL '22 days'),

-- Restaurant 2 : Sakura Dining
(2, 'Tokyo Bento Experience', 349.00, 'Lunch only', 190, CURRENT_TIMESTAMP - INTERVAL '13 days', CURRENT_TIMESTAMP + INTERVAL '17 days'),
(2, 'Hokkaido Seafood Bowl', 429.00, 'Dine in only', 120, CURRENT_TIMESTAMP + INTERVAL '6 days', CURRENT_TIMESTAMP + INTERVAL '36 days'),
(2, 'Matcha Dessert Journey', 179.00, 'One per customer', 220, CURRENT_TIMESTAMP - INTERVAL '51 days', CURRENT_TIMESTAMP - INTERVAL '11 days'),
(2, 'Yakitori Evening Set', 299.00, 'After 5 PM', 170, CURRENT_TIMESTAMP - INTERVAL '9 days', CURRENT_TIMESTAMP + INTERVAL '21 days'),
(2, 'Kyoto Couple Dinner', 899.00, 'For 2 persons', 45, CURRENT_TIMESTAMP + INTERVAL '11 days', CURRENT_TIMESTAMP + INTERVAL '41 days'),
(2, 'Nigiri Collection Box', 389.00, 'Takeaway available', 140, CURRENT_TIMESTAMP - INTERVAL '47 days', CURRENT_TIMESTAMP - INTERVAL '12 days'),
(2, 'Teriyaki Chicken Combo', 259.00, 'Lunch and dinner', 210, CURRENT_TIMESTAMP - INTERVAL '5 days', CURRENT_TIMESTAMP + INTERVAL '25 days'),
(2, 'Udon Comfort Meal', 229.00, 'Weekdays only', 240, CURRENT_TIMESTAMP + INTERVAL '8 days', CURRENT_TIMESTAMP + INTERVAL '38 days'),
(2, 'Chef Knife Selection', 1199.00, 'Reservation required', 25, CURRENT_TIMESTAMP - INTERVAL '68 days', CURRENT_TIMESTAMP - INTERVAL '28 days'),
(2, 'Osaka Street Bite Set', 319.00, 'Evening only', 150, CURRENT_TIMESTAMP - INTERVAL '14 days', CURRENT_TIMESTAMP + INTERVAL '16 days'),

-- Restaurant 3 : Seoul Garden BBQ
(3, 'Gangnam BBQ Premium', 459.00, '90 minute limit', 180, CURRENT_TIMESTAMP - INTERVAL '12 days', CURRENT_TIMESTAMP + INTERVAL '18 days'),
(3, 'Korean Fried Chicken Box', 279.00, 'Takeaway only', 220, CURRENT_TIMESTAMP + INTERVAL '5 days', CURRENT_TIMESTAMP + INTERVAL '35 days'),
(3, 'Army Stew Festival', 339.00, 'Shared set', 130, CURRENT_TIMESTAMP - INTERVAL '57 days', CURRENT_TIMESTAMP - INTERVAL '17 days'),
(3, 'Seoul Midnight Combo', 399.00, 'After 8 PM', 95, CURRENT_TIMESTAMP - INTERVAL '7 days', CURRENT_TIMESTAMP + INTERVAL '23 days'),
(3, 'Cheese Tteokbokki Set', 199.00, 'One redemption only', 250, CURRENT_TIMESTAMP + INTERVAL '13 days', CURRENT_TIMESTAMP + INTERVAL '43 days'),
(3, 'Kpop Fans Dinner', 549.00, 'For 2 persons', 80, CURRENT_TIMESTAMP - INTERVAL '49 days', CURRENT_TIMESTAMP - INTERVAL '14 days'),
(3, 'Bulgogi Signature Meal', 289.00, 'Lunch only', 200, CURRENT_TIMESTAMP - INTERVAL '10 days', CURRENT_TIMESTAMP + INTERVAL '20 days'),
(3, 'Kimchi Pancake Night', 169.00, 'After 6 PM', 230, CURRENT_TIMESTAMP + INTERVAL '9 days', CURRENT_TIMESTAMP + INTERVAL '39 days'),
(3, 'Busan Seafood Pot', 649.00, 'Reservation required', 50, CURRENT_TIMESTAMP - INTERVAL '61 days', CURRENT_TIMESTAMP - INTERVAL '21 days'),
(3, 'Hot Stone Bibimbap Deal', 249.00, 'All day', 260, CURRENT_TIMESTAMP - INTERVAL '4 days', CURRENT_TIMESTAMP + INTERVAL '26 days'),

-- Restaurant 4 : Golden Dragon Kitchen
(4, 'Shanghai Dumpling Feast', 299.00, 'Lunch only', 210, CURRENT_TIMESTAMP - INTERVAL '11 days', CURRENT_TIMESTAMP + INTERVAL '19 days'),
(4, 'Dragon Family Banquet', 899.00, 'For 4 persons', 55, CURRENT_TIMESTAMP + INTERVAL '10 days', CURRENT_TIMESTAMP + INTERVAL '40 days'),
(4, 'Wok Master Collection', 379.00, 'Dinner only', 140, CURRENT_TIMESTAMP - INTERVAL '54 days', CURRENT_TIMESTAMP - INTERVAL '14 days'),
(4, 'Szechuan Fire Challenge', 259.00, 'Spicy menu only', 180, CURRENT_TIMESTAMP - INTERVAL '8 days', CURRENT_TIMESTAMP + INTERVAL '22 days'),
(4, 'Chinese Tea Ceremony', 149.00, 'Afternoon only', 250, CURRENT_TIMESTAMP + INTERVAL '14 days', CURRENT_TIMESTAMP + INTERVAL '44 days'),
(4, 'Roasted Pork Special', 329.00, 'One per table', 160, CURRENT_TIMESTAMP - INTERVAL '43 days', CURRENT_TIMESTAMP - INTERVAL '8 days'),
(4, 'Cantonese Seafood Deal', 469.00, 'Reservation recommended', 100, CURRENT_TIMESTAMP - INTERVAL '13 days', CURRENT_TIMESTAMP + INTERVAL '17 days'),
(4, 'Noodle Master Bowl', 229.00, 'Weekdays only', 230, CURRENT_TIMESTAMP + INTERVAL '6 days', CURRENT_TIMESTAMP + INTERVAL '36 days'),
(4, 'Golden Emperor Set', 1099.00, 'Chef table only', 20, CURRENT_TIMESTAMP - INTERVAL '66 days', CURRENT_TIMESTAMP - INTERVAL '26 days'),
(4, 'Panda Snack Collection', 189.00, 'All day', 280, CURRENT_TIMESTAMP - INTERVAL '5 days', CURRENT_TIMESTAMP + INTERVAL '25 days'),

-- Restaurant 5 : Ocean Pearl Seafood
(5, 'Andaman Seafood Basket', 629.00, 'Weekend only', 90, CURRENT_TIMESTAMP - INTERVAL '9 days', CURRENT_TIMESTAMP + INTERVAL '21 days'),
(5, 'Blue Crab Deluxe', 549.00, 'Dinner only', 120, CURRENT_TIMESTAMP + INTERVAL '7 days', CURRENT_TIMESTAMP + INTERVAL '37 days'),
(5, 'Squid Grill Festival', 359.00, 'One per customer', 180, CURRENT_TIMESTAMP - INTERVAL '53 days', CURRENT_TIMESTAMP - INTERVAL '13 days'),
(5, 'Sea Breeze Couple Meal', 799.00, 'For 2 persons', 65, CURRENT_TIMESTAMP - INTERVAL '14 days', CURRENT_TIMESTAMP + INTERVAL '16 days'),
(5, 'Fresh Oyster Hour', 249.00, '4 PM - 6 PM', 240, CURRENT_TIMESTAMP + INTERVAL '11 days', CURRENT_TIMESTAMP + INTERVAL '41 days'),
(5, 'Island Lobster Experience', 1299.00, 'Reservation required', 18, CURRENT_TIMESTAMP - INTERVAL '64 days', CURRENT_TIMESTAMP - INTERVAL '24 days'),
(5, 'Spicy Crab Curry Pot', 429.00, 'Lunch only', 150, CURRENT_TIMESTAMP - INTERVAL '6 days', CURRENT_TIMESTAMP + INTERVAL '24 days'),
(5, 'Shrimp Lovers Combo', 319.00, 'All day', 230, CURRENT_TIMESTAMP + INTERVAL '9 days', CURRENT_TIMESTAMP + INTERVAL '39 days'),
(5, 'Captain Seafood Tower', 1499.00, 'For 4 persons', 15, CURRENT_TIMESTAMP - INTERVAL '46 days', CURRENT_TIMESTAMP - INTERVAL '11 days'),
(5, 'Coastal Family Dinner', 899.00, 'Family set', 45, CURRENT_TIMESTAMP - INTERVAL '3 days', CURRENT_TIMESTAMP + INTERVAL '27 days'),

-- Restaurant 6 : Urban Brew Cafe
(6, 'Espresso Energy Pack', 139.00, 'Before noon', 300, CURRENT_TIMESTAMP - INTERVAL '12 days', CURRENT_TIMESTAMP + INTERVAL '18 days'),
(6, 'Cold Brew Discovery', 159.00, 'All day', 260, CURRENT_TIMESTAMP + INTERVAL '8 days', CURRENT_TIMESTAMP + INTERVAL '38 days'),
(6, 'Coffee Bean Workshop', 499.00, 'Booking required', 40, CURRENT_TIMESTAMP - INTERVAL '50 days', CURRENT_TIMESTAMP - INTERVAL '10 days'),
(6, 'Ari Brunch Escape', 289.00, '10 AM - 2 PM', 170, CURRENT_TIMESTAMP - INTERVAL '7 days', CURRENT_TIMESTAMP + INTERVAL '23 days'),
(6, 'Caramel Latte Season', 169.00, 'One cup per visit', 280, CURRENT_TIMESTAMP + INTERVAL '13 days', CURRENT_TIMESTAMP + INTERVAL '43 days'),
(6, 'Barista Choice Menu', 219.00, 'Dine in only', 210, CURRENT_TIMESTAMP - INTERVAL '42 days', CURRENT_TIMESTAMP - INTERVAL '7 days'),
(6, 'Cheesecake Afternoon', 199.00, '2 PM - 5 PM', 190, CURRENT_TIMESTAMP - INTERVAL '10 days', CURRENT_TIMESTAMP + INTERVAL '20 days'),
(6, 'Coffee Lovers Duo', 259.00, 'Buy 2 save more', 220, CURRENT_TIMESTAMP + INTERVAL '5 days', CURRENT_TIMESTAMP + INTERVAL '35 days'),
(6, 'Signature Brew Flight', 359.00, 'Limited daily', 75, CURRENT_TIMESTAMP - INTERVAL '59 days', CURRENT_TIMESTAMP - INTERVAL '19 days'),
(6, 'Morning Croissant Combo', 179.00, 'Morning only', 330, CURRENT_TIMESTAMP - INTERVAL '4 days', CURRENT_TIMESTAMP + INTERVAL '26 days'),

-- Restaurant 7 : Butter Bliss Bakery
(7, 'French Pastry Journey', 229.00, 'Morning only', 240, CURRENT_TIMESTAMP - INTERVAL '11 days', CURRENT_TIMESTAMP + INTERVAL '19 days'),
(7, 'Chocolate Cake Party', 699.00, 'Preorder required', 65, CURRENT_TIMESTAMP + INTERVAL '6 days', CURRENT_TIMESTAMP + INTERVAL '36 days'),
(7, 'Mini Tart Collection', 189.00, 'Takeaway only', 210, CURRENT_TIMESTAMP - INTERVAL '55 days', CURRENT_TIMESTAMP - INTERVAL '15 days'),
(7, 'Butter Croissant Basket', 159.00, 'All day', 300, CURRENT_TIMESTAMP - INTERVAL '8 days', CURRENT_TIMESTAMP + INTERVAL '22 days'),
(7, 'Macaron Color Box', 249.00, '6 pieces', 170, CURRENT_TIMESTAMP + INTERVAL '12 days', CURRENT_TIMESTAMP + INTERVAL '42 days'),
(7, 'Premium Bread Bundle', 199.00, 'After 4 PM', 230, CURRENT_TIMESTAMP - INTERVAL '48 days', CURRENT_TIMESTAMP - INTERVAL '13 days'),
(7, 'Strawberry Shortcake', 279.00, 'Weekend only', 140, CURRENT_TIMESTAMP - INTERVAL '14 days', CURRENT_TIMESTAMP + INTERVAL '16 days'),
(7, 'Cookie Lovers Pack', 139.00, 'One box only', 320, CURRENT_TIMESTAMP + INTERVAL '7 days', CURRENT_TIMESTAMP + INTERVAL '37 days'),
(7, 'Birthday Surprise Box', 899.00, 'Reservation required', 35, CURRENT_TIMESTAMP - INTERVAL '67 days', CURRENT_TIMESTAMP - INTERVAL '27 days'),
(7, 'Morning Muffin Set', 169.00, 'Before noon', 260, CURRENT_TIMESTAMP - INTERVAL '5 days', CURRENT_TIMESTAMP + INTERVAL '25 days'),

-- Restaurant 8 : Prime Cut Steakhouse
(8, 'Black Angus Selection', 899.00, 'Dinner only', 80, CURRENT_TIMESTAMP - INTERVAL '13 days', CURRENT_TIMESTAMP + INTERVAL '17 days'),
(8, 'Chef Grill Experience', 1299.00, 'Reservation required', 30, CURRENT_TIMESTAMP + INTERVAL '9 days', CURRENT_TIMESTAMP + INTERVAL '39 days'),
(8, 'Beef Lovers Festival', 699.00, 'Limited daily', 95, CURRENT_TIMESTAMP - INTERVAL '60 days', CURRENT_TIMESTAMP - INTERVAL '20 days'),
(8, 'Rib Steak Signature', 759.00, 'Dine in only', 120, CURRENT_TIMESTAMP - INTERVAL '7 days', CURRENT_TIMESTAMP + INTERVAL '23 days'),
(8, 'Wine Cellar Pairing', 1499.00, 'After 6 PM', 25, CURRENT_TIMESTAMP + INTERVAL '14 days', CURRENT_TIMESTAMP + INTERVAL '44 days'),
(8, 'Weekend Grill Escape', 649.00, 'Friday to Sunday', 140, CURRENT_TIMESTAMP - INTERVAL '47 days', CURRENT_TIMESTAMP - INTERVAL '12 days'),
(8, 'Tenderloin Prestige', 999.00, 'Chef special', 60, CURRENT_TIMESTAMP - INTERVAL '10 days', CURRENT_TIMESTAMP + INTERVAL '20 days'),
(8, 'Couple Anniversary Set', 1599.00, 'For 2 persons', 35, CURRENT_TIMESTAMP + INTERVAL '6 days', CURRENT_TIMESTAMP + INTERVAL '36 days'),
(8, 'Smoke House Collection', 829.00, 'Evening only', 100, CURRENT_TIMESTAMP - INTERVAL '52 days', CURRENT_TIMESTAMP - INTERVAL '17 days'),
(8, 'Premium Meat Tasting', 1899.00, 'Reservation only', 20, CURRENT_TIMESTAMP - INTERVAL '4 days', CURRENT_TIMESTAMP + INTERVAL '26 days');