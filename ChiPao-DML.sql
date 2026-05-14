-- DML Script for Restaurant Promotion System

-- 1. Insert Users (2 Managers, 3 Clients)
INSERT INTO "users" ("id", "username", "email", "password_hash", "role", "created_at") VALUES
(1, 'manager_somchai', 'somchai@example.com', 'hashed_pw_1', 'manager', CURRENT_TIMESTAMP),
(2, 'manager_suda', 'suda@example.com', 'hashed_pw_2', 'manager', CURRENT_TIMESTAMP),
(3, 'client_nawat', 'nawat@example.com', 'hashed_pw_3', 'client', CURRENT_TIMESTAMP),
(4, 'client_pim', 'pim@example.com', 'hashed_pw_4', 'client', CURRENT_TIMESTAMP),
(5, 'client_krit', 'krit@example.com', 'hashed_pw_5', 'client', CURRENT_TIMESTAMP);

-- 2. Insert Restaurants
INSERT INTO "restaurants" ("id", "name", "manager_id", "address", "category", "created_at") VALUES
(1, 'Somchai Seafood', 1, '123 Sukhumvit Rd, Bangkok', 'Seafood', CURRENT_TIMESTAMP),
(2, 'Suda Cafe & Bakery', 2, '456 Ari, Bangkok', 'Cafe', CURRENT_TIMESTAMP),
(3, 'Somchai Izakaya', 1, '789 Thong Lo, Bangkok', 'Japanese', CURRENT_TIMESTAMP);

-- 3. Insert Promotions
INSERT INTO "promotions" ("id", "restaurant_id", "title", "price", "conditions", "total_quota", "start_date", "end_date") VALUES
(1, 1, 'Seafood Buffet 30% Off', 699.00, 'Dine-in only, max 2 hours', 100, CURRENT_TIMESTAMP - INTERVAL '10 days', CURRENT_TIMESTAMP + INTERVAL '30 days'),
(2, 2, 'Buy 1 Get 1 Iced Latte', 80.00, 'Takeaway only between 1 PM - 4 PM', 200, CURRENT_TIMESTAMP - INTERVAL '5 days', CURRENT_TIMESTAMP + INTERVAL '15 days'),
(3, 3, 'Free Salmon Sashimi', 0.00, 'With a minimum spend of 1000 THB', 50, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP + INTERVAL '60 days'),
(4, 1, 'Early Bird Special Expired', 499.00, 'Only for the first 10 customers', 10, CURRENT_TIMESTAMP - INTERVAL '60 days', CURRENT_TIMESTAMP - INTERVAL '30 days');

-- 4. Insert Promotion Tickets
INSERT INTO "promotion_tickets" ("id", "user_id", "promotion_id", "status", "purchase_date", "used_date") VALUES
(1, 3, 1, 'Active', CURRENT_TIMESTAMP - INTERVAL '2 days', NULL),
(2, 4, 1, 'Used', CURRENT_TIMESTAMP - INTERVAL '3 days', CURRENT_TIMESTAMP - INTERVAL '1 day'),
(3, 5, 2, 'Active', CURRENT_TIMESTAMP - INTERVAL '1 day', NULL),
(4, 3, 2, 'Used', CURRENT_TIMESTAMP - INTERVAL '4 days', CURRENT_TIMESTAMP - INTERVAL '4 days'),
(5, 4, 4, 'Expired', CURRENT_TIMESTAMP - INTERVAL '50 days', NULL),
(6, 5, 3, 'Active', CURRENT_TIMESTAMP, NULL);

-- 5. Insert Reviews
INSERT INTO "reviews" ("id", "user_id", "restaurant_id", "rating", "comment", "created_at") VALUES
(1, 4, 1, 5, 'The seafood was extremely fresh! Highly recommended.', CURRENT_TIMESTAMP - INTERVAL '1 day'),
(2, 3, 2, 4, 'Good coffee, but the bakery section was a bit lacking today.', CURRENT_TIMESTAMP - INTERVAL '4 days'),
(3, 5, 1, 3, 'Food is good but the service is quite slow during peak hours.', CURRENT_TIMESTAMP - INTERVAL '10 days'),
(4, 4, 3, 5, 'Best Izakaya in Thong Lo. The promotion was worth it.', CURRENT_TIMESTAMP - INTERVAL '2 days'),
(5, 5, 2, 5, 'Love the atmosphere, perfect place to work.', CURRENT_TIMESTAMP - INTERVAL '5 hours');