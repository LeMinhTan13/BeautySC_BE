DROP DATABASE IF EXISTS beautysc_db;
CREATE DATABASE beautysc_db;
use beautysc_db;


CREATE TABLE skin_type (
    skin_type_id INT AUTO_INCREMENT PRIMARY KEY,
    skin_type_name VARCHAR(50) NOT NULL,
    priority INT NOT NULL
);

CREATE TABLE skin_test (
    skin_test_id INT AUTO_INCREMENT PRIMARY KEY,
    skin_test_name VARCHAR(50) NOT NULL,
    `status` BOOLEAN NOT NULL 
);

CREATE TABLE skin_type_question (
    skin_type_question_id INT AUTO_INCREMENT PRIMARY KEY,
    `description` TEXT NOT NULL,
    skin_test_id INT,
    FOREIGN KEY (skin_test_id) REFERENCES skin_test(skin_test_id)
);

CREATE TABLE skin_type_answer (
    skin_type_answer_id INT AUTO_INCREMENT PRIMARY KEY,
    `description` TEXT NOT NULL,
    skin_type_question_id INT NOT NULL,
    skin_type_id INT NOT NULL,
    FOREIGN KEY (skin_type_question_id) REFERENCES skin_type_question(skin_type_question_id),
    FOREIGN KEY (skin_type_id) REFERENCES skin_type(skin_type_id)
);

CREATE TABLE category (
    category_id INT AUTO_INCREMENT PRIMARY KEY,
    category_name VARCHAR(255) NOT NULL
);

CREATE TABLE `routine` (
    routine_id INT AUTO_INCREMENT PRIMARY KEY,
    routine_name VARCHAR(50) NOT NULL,    
    `status` BOOLEAN NOT NULL,
    skin_type_id INT NOT NULL,
    FOREIGN KEY (skin_type_id) REFERENCES skin_type(skin_type_id)
);

CREATE TABLE routine_detail (
	routine_detail_id INT AUTO_INCREMENT PRIMARY KEY,
    routine_detail_name VARCHAR(50) NOT NULL, 
    routine_id INT NOT NULL,
    FOREIGN KEY (routine_id) REFERENCES `routine`(routine_id)
);

CREATE TABLE routine_step (
	routine_step_id INT AUTO_INCREMENT PRIMARY KEY,
    step INT NOT NULL,
    instruction TEXT NOT NULL,
    routine_detail_id INT NOT NULL,
    category_id INT NOT NULL,
    FOREIGN KEY (routine_detail_id) REFERENCES routine_detail(routine_detail_id),
    FOREIGN KEY (category_id) REFERENCES category(category_id) 
);

CREATE TABLE `account` (
    account_id INT AUTO_INCREMENT PRIMARY KEY,
    email VARCHAR(255) NOT NULL UNIQUE,
    `password` VARCHAR(255) NOT NULL,
    `role` ENUM('Manager', 'Staff', 'Customer') NOT NULL
);

CREATE TABLE refresh_token (
    refresh_token_id INT AUTO_INCREMENT PRIMARY KEY,
    token VARCHAR(255) NOT NULL,
    account_id INT NOT NULL,
    FOREIGN KEY (account_id) REFERENCES `account`(account_id)
);

CREATE TABLE `customer` (
    customer_id INT AUTO_INCREMENT PRIMARY KEY,   
    account_Id INT UNIQUE KEY NOT NULL,
    full_name VARCHAR(255) NOT NULL,
    birthday DATE,
    phone_number VARCHAR(15), 
    confirmed_email BOOLEAN NOT NULL DEFAULT 0,
    image VARCHAR(255),
    `status` BOOLEAN NOT NULL DEFAULT 1,
	skin_type_id INT,
     FOREIGN KEY (account_Id) REFERENCES account(account_Id),
    FOREIGN KEY (skin_type_id) REFERENCES skin_type(skin_type_id)
);

CREATE TABLE shipping_address (
    shipping_address_id INT AUTO_INCREMENT PRIMARY KEY,
    address VARCHAR(150) NOT NULL, 
    phone_number VARCHAR(15) NOT NULL,
    is_default BOOLEAN NOT NULL,
    customer_id INT NOT NULL,
    FOREIGN KEY (customer_id) REFERENCES `customer`(customer_id)
);

CREATE TABLE brand (
    brand_id INT AUTO_INCREMENT PRIMARY KEY,
    brand_name VARCHAR(255) NOT NULL
);

CREATE TABLE product (
    product_id INT AUTO_INCREMENT PRIMARY KEY,
    product_name VARCHAR(100) NOT NULL,
    summary TEXT,
    size VARCHAR(10) NOT NULL,
    price DECIMAL(10,2) NOT NULL,
	quantity INT NOT NULL,
    discount DECIMAL(4,2) DEFAULT 0 NOT NULL,
    created_date DATETIME DEFAULT CURRENT_TIMESTAMP,
    is_recommended BOOLEAN NOT NULL DEFAULT 0,
    `status` BOOLEAN NOT NULL DEFAULT 1,
    brand_id INT NOT NULL,
    category_id INT NOT NULL,
    FOREIGN KEY (category_id) REFERENCES category(category_id),
    FOREIGN KEY (brand_id) REFERENCES brand(brand_id)
);

CREATE TABLE product_image (
    product_image_id INT AUTO_INCREMENT PRIMARY KEY,
    url VARCHAR(255),
    product_id INT NOT NULL,
    FOREIGN KEY (product_id) REFERENCES product(product_id)
);

CREATE TABLE `function` (
    function_id INT AUTO_INCREMENT PRIMARY KEY,
    function_name VARCHAR(50) NOT NULL 
);

CREATE TABLE product_skin_type (
	product_skin_type_id INT AUTO_INCREMENT PRIMARY KEY,
    product_id INT NOT NULL,
    skin_type_id INT NOT NULL,
    FOREIGN KEY (product_id) REFERENCES product(product_id),
    FOREIGN KEY (skin_type_id) REFERENCES skin_type(skin_type_id)
);

CREATE TABLE product_function (
	product_function_id INT AUTO_INCREMENT PRIMARY KEY,
    product_id INT NOT NULL,
    function_id INT NOT NULL,
    FOREIGN KEY (product_id) REFERENCES product(product_id),
    FOREIGN KEY (function_id) REFERENCES `function`(function_id)
);

CREATE TABLE ingredient (
    ingredient_id INT PRIMARY KEY AUTO_INCREMENT,                      
    ingredient_name VARCHAR(255) NOT NULL                     
);

CREATE TABLE product_ingredient (    
    product_ingredient_id INT PRIMARY KEY AUTO_INCREMENT,                                    
    concentration DECIMAL(5, 2),                  
    product_id INT NOT NULL,
    ingredient_id INT NOT NULL,
    FOREIGN KEY (product_id) REFERENCES product(product_id),
    FOREIGN KEY (ingredient_id) REFERENCES ingredient(ingredient_id)
);

CREATE TABLE voucher (
	voucher_id INT AUTO_INCREMENT PRIMARY KEY,
    voucher_name VARCHAR(255) NOT NULL,
    voucher_code VARCHAR(255) NOT NULL,
    `description` VARCHAR(255),
    discount_amount DECIMAL(10, 2) NOT NULL,
    start_date DATETIME NOT NULL,
    end_date DATETIME NOT NULL,
    minimum_purchase DECIMAL(10, 2),
    `status` BOOLEAN NOT NULL DEFAULT 1
);

CREATE TABLE `order` (
    order_id INT AUTO_INCREMENT PRIMARY KEY,
    total_amount DECIMAL(10,2),
    address VARCHAR(150), 
    phone_number VARCHAR(10),
    `status` ENUM('InCart', 'Pending', 'Shipping', 'Complete', 'Cancel') NOT NULL,
    created_date DATETIME,    
    customer_id INT NOT NULL,
    voucher_id INT,
    FOREIGN KEY (customer_id) REFERENCES `customer`(customer_id),
    FOREIGN KEY (voucher_id) REFERENCES `voucher`(voucher_id)
);

CREATE TABLE order_detail (
    order_detail_id INT AUTO_INCREMENT PRIMARY KEY,
    price DECIMAL(10,2) NOT NULL,
    quantity INT NOT NULL,    
	product_id INT NOT NULL,  
    order_id INT NOT NULL,  
    FOREIGN KEY (product_id) REFERENCES product(product_id),
    FOREIGN KEY (order_id) REFERENCES `order`(order_id)
);

CREATE TABLE feedback (
    feedback_id INT AUTO_INCREMENT PRIMARY KEY,
    rating FLOAT NOT NULL,
    `Comment` TEXT,
    created_date DATETIME DEFAULT CURRENT_TIMESTAMP NOT NULL,
    `status` BOOLEAN NOT NULL DEFAULT 1,
    customer_id INT NOT NULL,
    product_id INT NOT NULL,
    FOREIGN KEY (customer_id) REFERENCES `customer`(customer_id),
    FOREIGN KEY (product_id) REFERENCES product(product_id)
);

CREATE TABLE `transaction` (
    transaction_id INT AUTO_INCREMENT PRIMARY KEY,
    amount DECIMAL(10,2) NOT NULL,
    `description` VARCHAR(200),
    created_date DATETIME DEFAULT CURRENT_TIMESTAMP NOT NULL,
    order_id INT NOT NULL,
    FOREIGN KEY (order_id) REFERENCES `order`(order_id)
);

INSERT INTO skin_type (skin_type_name, priority) VALUES
('Oily skin', 1),
('Combination skin', 2),
('Sensitive skin', 3),
('Normal skin', 4),
('Dry skin', 5);

INSERT INTO category (category_name) VALUES
('Cleanser'),     -- Sản phẩm làm sạch da
('Moisturizer'),  -- Sản phẩm dưỡng ẩm
('Serum'),        -- Serum chăm sóc da
('Sunscreen'),    -- Kem chống nắng
('Exfoliator');   -- Sản phẩm tẩy tế bào chết

INSERT INTO `account` (email, `password`, `role`) VALUES
('customer1@example.com', '$2a$11$wtFq3R/acjsYL.k8CXT9nO1alaldf/yWdqOk/x80W9s6fcqPbo6Nu', 'Customer'),
('customer2@example.com', '$2a$11$wtFq3R/acjsYL.k8CXT9nO1alaldf/yWdqOk/x80W9s6fcqPbo6Nu', 'Customer'),
('customer3@example.com', '$2a$11$wtFq3R/acjsYL.k8CXT9nO1alaldf/yWdqOk/x80W9s6fcqPbo6Nu', 'Customer'),
('manager@example.com', '$2a$11$wtFq3R/acjsYL.k8CXT9nO1alaldf/yWdqOk/x80W9s6fcqPbo6Nu', 'Manager'),
('jane.smith@example.com', '$2a$11$wtFq3R/acjsYL.k8CXT9nO1alaldf/yWdqOk/x80W9s6fcqPbo6Nu', 'Staff');

INSERT INTO `customer` (account_id, full_name, birthday, phone_number, skin_type_id, `status`) VALUES
(1, 'Customer 1', '1992-03-12', NULL, 1, 1),
(2, 'Customer 2', '1992-03-12', NULL, 2, 1),
(3, 'Customer 3', '1992-03-12', NULL, 3, 1);

INSERT INTO brand (brand_name) VALUES 
('The Ordinary'),
('CeraVe'),
('La Roche-Posay'),
('Neutrogena'),
('COSRX'),
('Paula\'s Choice'),
('Kiehl\'s'),
('Drunk Elephant'),
('Cetaphil'),
('Innisfree');

INSERT INTO `function` (function_name) VALUES
('Hydrating'),
('Brightening'),
('Anti-aging'),
('Soothing');

INSERT INTO ingredient (ingredient_name) VALUES 
('Hyaluronic Acid'),
('Niacinamide'),
('Vitamin C'),
('Salicylic Acid'),
('Retinol'),
('Aloe Vera Extract'),
('Green Tea Extract'),
('Ceramides'),
('Peptides'),
('Alpha Arbutin'),
('Glycolic Acid'),
('Lactic Acid'),
('Zinc Oxide'),
('Shea Butter'),
('Squalane'),
('Tea Tree Oil'),
('Centella Asiatica Extract'),
('Panthenol'),
('Allantoin'),
('Kojic Acid');

INSERT INTO product (product_name, summary, size, price, quantity, discount, is_recommended, brand_id, category_id, created_date) VALUES
('Product 1', 'Product 1', '100ml', 100000, 100, 0, true, 1, 1, '2024-01-01 10:00:00'),
('Product 2', 'Product 2', '100ml', 200000, 300, 0.2, true, 1, 1, '2024-01-01 10:00:00'),
('Product 3', 'Product 3', '50ml', 150000, 50, 0.1, true, 2, 2, '2023-01-01 10:00:00'),
('Product 4', 'Product 4', '75ml', 120000, 80, 0.15, false, 3, 3, '2023-02-15 14:30:00'),
('Product 5', 'Product 5', '100ml', 180000, 60, 0.2, true, 4, 4, '2023-03-10 09:45:00'),
('Product 6', 'Product 6', '30ml', 90000, 100, 0.05, false, 5, 5, '2023-04-05 16:20:00'),
('Product 7', 'Product 7', '50ml', 110000, 70, 0.1, true, 6, 1, '2023-05-20 11:10:00'),
('Product 8', 'Product 8', '60ml', 130000, 90, 0.25, false, 7, 2, '2023-06-12 13:50:00'),
('Product 9', 'Product 9', '100ml', 200000, 40, 0.3, true, 8, 3, '2023-07-08 08:30:00'),
('Product 10', 'Product 10', '50ml', 95000, 120, 0.1, false, 9, 4, '2023-08-25 17:00:00'),
('Product 11', 'Product 11', '75ml', 140000, 60, 0.2, true, 10, 5, '2023-09-14 12:15:00'),
('Product 12', 'Product 12', '100ml', 160000, 80, 0.15, false, 1, 1, '2023-10-30 10:45:00'),
('Product 13', 'Product 13', '50ml', 105000, 100, 0.1, true, 2, 2, '2023-11-22 14:00:00'),
('Product 14', 'Product 14', '60ml', 125000, 70, 0.25, false, 3, 3, '2023-12-18 09:30:00'),
('Product 15', 'Product 15', '100ml', 190000, 50, 0.3, true, 4, 4, '2024-01-05 16:45:00'),
('Product 16', 'Product 16', '30ml', 85000, 90, 0.05, false, 5, 5, '2024-02-10 11:20:00'),
('Product 17', 'Product 17', '50ml', 115000, 60, 0.1, true, 6, 1, '2024-03-15 13:10:00');

INSERT INTO product_image (url, product_id) VALUES 
('https://mint07.com/wp-content/uploads/2015/10/sua-rua-mat-Simple-Kind-To-Skin-Refreshing-Facial-Wash-Gel-review-1.jpg', 1),
('https://mint07.com/wp-content/uploads/2015/10/sua-rua-mat-Simple-Kind-To-Skin-Refreshing-Facial-Wash-Gel-review-1.jpg', 1),
('https://mint07.com/wp-content/uploads/2015/10/sua-rua-mat-Simple-Kind-To-Skin-Refreshing-Facial-Wash-Gel-review-1.jpg', 1),

('https://mint07.com/wp-content/uploads/2015/10/sua-rua-mat-Simple-Kind-To-Skin-Refreshing-Facial-Wash-Gel-review-1.jpg', 2),
('https://mint07.com/wp-content/uploads/2015/10/sua-rua-mat-Simple-Kind-To-Skin-Refreshing-Facial-Wash-Gel-review-1.jpg', 2),
('https://mint07.com/wp-content/uploads/2015/10/sua-rua-mat-Simple-Kind-To-Skin-Refreshing-Facial-Wash-Gel-review-1.jpg', 2),

('https://mint07.com/wp-content/uploads/2015/10/sua-rua-mat-Simple-Kind-To-Skin-Refreshing-Facial-Wash-Gel-review-1.jpg', 3),
('https://mint07.com/wp-content/uploads/2015/10/sua-rua-mat-Simple-Kind-To-Skin-Refreshing-Facial-Wash-Gel-review-1.jpg', 3),
('https://mint07.com/wp-content/uploads/2015/10/sua-rua-mat-Simple-Kind-To-Skin-Refreshing-Facial-Wash-Gel-review-1.jpg', 3),

('https://mint07.com/wp-content/uploads/2015/10/sua-rua-mat-Simple-Kind-To-Skin-Refreshing-Facial-Wash-Gel-review-1.jpg', 4),
('https://mint07.com/wp-content/uploads/2015/10/sua-rua-mat-Simple-Kind-To-Skin-Refreshing-Facial-Wash-Gel-review-1.jpg', 4),
('https://mint07.com/wp-content/uploads/2015/10/sua-rua-mat-Simple-Kind-To-Skin-Refreshing-Facial-Wash-Gel-review-1.jpg', 4),

('https://mint07.com/wp-content/uploads/2015/10/sua-rua-mat-Simple-Kind-To-Skin-Refreshing-Facial-Wash-Gel-review-1.jpg', 5),
('https://mint07.com/wp-content/uploads/2015/10/sua-rua-mat-Simple-Kind-To-Skin-Refreshing-Facial-Wash-Gel-review-1.jpg', 5),
('https://mint07.com/wp-content/uploads/2015/10/sua-rua-mat-Simple-Kind-To-Skin-Refreshing-Facial-Wash-Gel-review-1.jpg', 5),

('https://mint07.com/wp-content/uploads/2015/10/sua-rua-mat-Simple-Kind-To-Skin-Refreshing-Facial-Wash-Gel-review-1.jpg', 6),
('https://mint07.com/wp-content/uploads/2015/10/sua-rua-mat-Simple-Kind-To-Skin-Refreshing-Facial-Wash-Gel-review-1.jpg', 6),
('https://mint07.com/wp-content/uploads/2015/10/sua-rua-mat-Simple-Kind-To-Skin-Refreshing-Facial-Wash-Gel-review-1.jpg', 6),

('https://mint07.com/wp-content/uploads/2015/10/sua-rua-mat-Simple-Kind-To-Skin-Refreshing-Facial-Wash-Gel-review-1.jpg', 7),
('https://mint07.com/wp-content/uploads/2015/10/sua-rua-mat-Simple-Kind-To-Skin-Refreshing-Facial-Wash-Gel-review-1.jpg', 7),
('https://mint07.com/wp-content/uploads/2015/10/sua-rua-mat-Simple-Kind-To-Skin-Refreshing-Facial-Wash-Gel-review-1.jpg', 7),

('https://mint07.com/wp-content/uploads/2015/10/sua-rua-mat-Simple-Kind-To-Skin-Refreshing-Facial-Wash-Gel-review-1.jpg', 8),
('https://mint07.com/wp-content/uploads/2015/10/sua-rua-mat-Simple-Kind-To-Skin-Refreshing-Facial-Wash-Gel-review-1.jpg', 8),
('https://mint07.com/wp-content/uploads/2015/10/sua-rua-mat-Simple-Kind-To-Skin-Refreshing-Facial-Wash-Gel-review-1.jpg', 8),

('https://mint07.com/wp-content/uploads/2015/10/sua-rua-mat-Simple-Kind-To-Skin-Refreshing-Facial-Wash-Gel-review-1.jpg', 9),
('https://mint07.com/wp-content/uploads/2015/10/sua-rua-mat-Simple-Kind-To-Skin-Refreshing-Facial-Wash-Gel-review-1.jpg', 9),
('https://mint07.com/wp-content/uploads/2015/10/sua-rua-mat-Simple-Kind-To-Skin-Refreshing-Facial-Wash-Gel-review-1.jpg', 9),

('https://mint07.com/wp-content/uploads/2015/10/sua-rua-mat-Simple-Kind-To-Skin-Refreshing-Facial-Wash-Gel-review-1.jpg', 10),
('https://mint07.com/wp-content/uploads/2015/10/sua-rua-mat-Simple-Kind-To-Skin-Refreshing-Facial-Wash-Gel-review-1.jpg', 10),
('https://mint07.com/wp-content/uploads/2015/10/sua-rua-mat-Simple-Kind-To-Skin-Refreshing-Facial-Wash-Gel-review-1.jpg', 10),

('https://mint07.com/wp-content/uploads/2015/10/sua-rua-mat-Simple-Kind-To-Skin-Refreshing-Facial-Wash-Gel-review-1.jpg', 11),
('https://mint07.com/wp-content/uploads/2015/10/sua-rua-mat-Simple-Kind-To-Skin-Refreshing-Facial-Wash-Gel-review-1.jpg', 11),
('https://mint07.com/wp-content/uploads/2015/10/sua-rua-mat-Simple-Kind-To-Skin-Refreshing-Facial-Wash-Gel-review-1.jpg', 11),

('https://mint07.com/wp-content/uploads/2015/10/sua-rua-mat-Simple-Kind-To-Skin-Refreshing-Facial-Wash-Gel-review-1.jpg', 12),
('https://mint07.com/wp-content/uploads/2015/10/sua-rua-mat-Simple-Kind-To-Skin-Refreshing-Facial-Wash-Gel-review-1.jpg', 12),
('https://mint07.com/wp-content/uploads/2015/10/sua-rua-mat-Simple-Kind-To-Skin-Refreshing-Facial-Wash-Gel-review-1.jpg', 12),

('https://mint07.com/wp-content/uploads/2015/10/sua-rua-mat-Simple-Kind-To-Skin-Refreshing-Facial-Wash-Gel-review-1.jpg', 13),
('https://mint07.com/wp-content/uploads/2015/10/sua-rua-mat-Simple-Kind-To-Skin-Refreshing-Facial-Wash-Gel-review-1.jpg', 13),
('https://mint07.com/wp-content/uploads/2015/10/sua-rua-mat-Simple-Kind-To-Skin-Refreshing-Facial-Wash-Gel-review-1.jpg', 13),

('https://mint07.com/wp-content/uploads/2015/10/sua-rua-mat-Simple-Kind-To-Skin-Refreshing-Facial-Wash-Gel-review-1.jpg', 14),
('https://mint07.com/wp-content/uploads/2015/10/sua-rua-mat-Simple-Kind-To-Skin-Refreshing-Facial-Wash-Gel-review-1.jpg', 14),
('https://mint07.com/wp-content/uploads/2015/10/sua-rua-mat-Simple-Kind-To-Skin-Refreshing-Facial-Wash-Gel-review-1.jpg', 14),

('https://mint07.com/wp-content/uploads/2015/10/sua-rua-mat-Simple-Kind-To-Skin-Refreshing-Facial-Wash-Gel-review-1.jpg', 15),
('https://mint07.com/wp-content/uploads/2015/10/sua-rua-mat-Simple-Kind-To-Skin-Refreshing-Facial-Wash-Gel-review-1.jpg', 15),
('https://mint07.com/wp-content/uploads/2015/10/sua-rua-mat-Simple-Kind-To-Skin-Refreshing-Facial-Wash-Gel-review-1.jpg', 15),

('https://mint07.com/wp-content/uploads/2015/10/sua-rua-mat-Simple-Kind-To-Skin-Refreshing-Facial-Wash-Gel-review-1.jpg', 16),
('https://mint07.com/wp-content/uploads/2015/10/sua-rua-mat-Simple-Kind-To-Skin-Refreshing-Facial-Wash-Gel-review-1.jpg', 16),
('https://mint07.com/wp-content/uploads/2015/10/sua-rua-mat-Simple-Kind-To-Skin-Refreshing-Facial-Wash-Gel-review-1.jpg', 16),

('https://mint07.com/wp-content/uploads/2015/10/sua-rua-mat-Simple-Kind-To-Skin-Refreshing-Facial-Wash-Gel-review-1.jpg', 17),
('https://mint07.com/wp-content/uploads/2015/10/sua-rua-mat-Simple-Kind-To-Skin-Refreshing-Facial-Wash-Gel-review-1.jpg', 17),
('https://mint07.com/wp-content/uploads/2015/10/sua-rua-mat-Simple-Kind-To-Skin-Refreshing-Facial-Wash-Gel-review-1.jpg', 17);

-- Thêm liên kết product_function
INSERT INTO product_function (product_id, function_id) VALUES
(1, 1), (1, 2), (2, 3), (2, 4), (3, 1), (3, 2), (4, 3), (4, 4), (5, 1), (5, 3), (6, 2), (6, 4), (7, 1), (7, 2),
(8, 3), (8, 4), (9, 1), (9, 3), (10, 2), (10, 4), (11, 1), (11, 2), (12, 3), (12, 4),
(13, 1), (13, 3), (14, 2), (14, 4), (15, 1), (15, 2), (16, 3), (16, 4), (17, 1), (17, 3);

-- Thêm liên kết product_ingredient
INSERT INTO product_ingredient (product_id, ingredient_id, concentration) VALUES
(1, 1, 0.4), (2, 2, 0.6), (2, 4, 0.6), (3, 3, 0.5), (3, 5, 0.3), (4, 6, 0.4), (4, 7, 0.2), (5, 8, 0.6), (5, 9, 0.1),
(6, 10, 0.5), (6, 11, 0.3), (7, 12, 0.4), (7, 13, 0.2), (8, 14, 0.6), (8, 15, 0.1),
(9, 16, 0.5), (9, 17, 0.3), (10, 18, 0.4), (10, 19, 0.2), (11, 20, 0.6), (11, 1, 0.1),
(12, 2, 0.5), (12, 3, 0.3), (13, 4, 0.4), (13, 5, 0.2), (14, 6, 0.6), (14, 7, 0.1),
(15, 8, 0.5), (15, 9, 0.3), (16, 10, 0.4), (16, 11, 0.2), (17, 12, 0.6), (17, 13, 0.1);

-- Thêm liên kết product_skin_type
INSERT INTO product_skin_type (product_id, skin_type_id) VALUES
(1, 1), (1, 2), (2, 2), (2, 3), (3, 1), (3, 2), (4, 2), (4, 3), (5, 3), (5, 4), (6, 4), (6, 5), (7, 1), (7, 3),
(8, 2), (8, 4), (9, 3), (9, 5), (10, 1), (10, 4), (11, 2), (11, 5), (12, 1), (12, 3),
(13, 2), (13, 4), (14, 3), (14, 5), (15, 1), (15, 4), (16, 2), (16, 5), (17, 1), (17, 3);

INSERT INTO voucher (voucher_name, voucher_code, `description`, discount_amount, start_date, end_date, minimum_purchase, `status`) VALUES
('Summer Sale', 'SUMMER2025', 'Discount for summer season', 20000.00, '2025-01-01 00:00:00', '2025-06-30 23:59:59', 100000.00, 1),
('Black Friday', 'BLACKFRIDAY25', 'Black Friday Special Discount', 25000.00, '2025-11-24 00:00:00', '2025-11-25 23:59:59', 50000.00, 1),
('Christmas Offer', 'XMAS2025', 'Christmas Discount', 15000.00, '2025-12-20 00:00:00', '2025-12-25 23:59:59', 75000.00, 1),
('New Year Sale', 'NEWYEAR2025', 'New Year Celebration Discount', 30000.00, '2025-01-01 00:00:00', '2025-01-05 23:59:59', 120000.00, 1),
('Clearance Sale', 'CLEARANCE10', 'Discount on clearance items', 10000.00, '2025-03-01 00:00:00', '2025-03-15 23:59:59', NULL, 1);


-- Thêm 20 đơn hàng
INSERT INTO `order` (customer_id, `status`, created_date, total_amount) VALUES
(1, 'Complete', '2023-01-05 10:00:00', 360000),   -- Order 1
(2, 'Complete', '2023-02-10 14:30:00', 585000),  -- Order 2
(3, 'Complete', '2023-03-15 09:45:00', 684000),   -- Order 3
(1, 'Complete', '2023-04-20 16:20:00', 315000),  -- Order 4
(2, 'Complete', '2023-05-25 11:10:00', 630000),  -- Order 5
(3, 'Complete', '2023-06-30 13:50:00', 342000),   -- Order 6
(1, 'Complete', '2023-07-05 08:30:00', 472500),  -- Order 7
(2, 'Complete', '2023-08-10 17:00:00', 532000),  -- Order 8
(3, 'Complete', '2023-09-15 12:15:00', 435000),   -- Order 9
(1, 'Complete', '2023-10-20 10:45:00', 540000),  -- Order 10
(2, 'Complete', '2023-11-25 14:00:00', 408000),  -- Order 11
(3, 'Complete', '2023-12-30 09:30:00', 472500),   -- Order 12
(1, 'Complete', '2024-01-05 16:45:00', 532000),  -- Order 13
(2, 'Complete', '2024-02-10 11:20:00', 435000),  -- Order 14
(3, 'Complete', '2024-03-15 13:10:00', 540000),   -- Order 15
(1, 'Complete', '2024-04-20 10:00:00', 408000),  -- Order 16
(2, 'Complete', '2024-05-25 14:30:00', 472500),  -- Order 17
(3, 'Complete', '2024-06-30 09:45:00', 532000),   -- Order 18
(1, 'Complete', '2024-07-05 16:20:00', 435000),  -- Order 19
(2, 'Complete', '2024-08-10 11:10:00', 540000);  -- Order 20

-- Thêm order_detail (đã tính toán sẵn giá sau khi áp dụng discount)
INSERT INTO order_detail (price, quantity, product_id, order_id) VALUES
-- Order 1
(100000, 2, 1, 1),   -- Product 1: 100000 * (1 - 0) = 100000
(160000, 1, 2, 1),   -- Product 2: 200000 * (1 - 0.2) = 160000
-- Order 2
(135000, 3, 3, 2),   -- Product 3: 150000 * (1 - 0.1) = 135000
(102000, 2, 4, 2),   -- Product 4: 120000 * (1 - 0.15) = 102000
-- Order 3
(144000, 1, 5, 3),   -- Product 5: 180000 * (1 - 0.2) = 144000
(85500, 4, 6, 3),    -- Product 6: 90000 * (1 - 0.05) = 85500
-- Order 4
(99000, 2, 7, 4),    -- Product 7: 110000 * (1 - 0.1) = 99000
(97500, 1, 8, 4),    -- Product 8: 130000 * (1 - 0.25) = 97500
-- Order 5
(140000, 3, 9, 5),   -- Product 9: 200000 * (1 - 0.3) = 140000
(85500, 2, 10, 5),   -- Product 10: 95000 * (1 - 0.1) = 85500
-- Order 6
(112000, 1, 11, 6),  -- Product 11: 140000 * (1 - 0.2) = 112000
(136000, 2, 12, 6),  -- Product 12: 160000 * (1 - 0.15) = 136000
-- Order 7
(94500, 3, 13, 7),   -- Product 13: 105000 * (1 - 0.1) = 94500
(93750, 1, 14, 7),   -- Product 14: 125000 * (1 - 0.25) = 93750
-- Order 8
(133000, 2, 15, 8),  -- Product 15: 190000 * (1 - 0.3) = 133000
(80750, 4, 16, 8),   -- Product 16: 85000 * (1 - 0.05) = 80750
-- Order 9
(103500, 1, 17, 9),  -- Product 17: 115000 * (1 - 0.1) = 103500
(200000, 2, 1, 9),   -- Product 1: 100000 * (1 - 0) = 100000
-- Order 10
(480000, 3, 2, 10),  -- Product 2: 200000 * (1 - 0.2) = 160000 * 3 = 480000
(270000, 2, 3, 10),  -- Product 3: 150000 * (1 - 0.1) = 135000 * 2 = 270000
-- Order 11
(102000, 1, 4, 11),  -- Product 4: 120000 * (1 - 0.15) = 102000
(288000, 2, 5, 11),  -- Product 5: 180000 * (1 - 0.2) = 144000 * 2 = 288000
-- Order 12
(256500, 3, 6, 12),  -- Product 6: 90000 * (1 - 0.05) = 85500 * 3 = 256500
(99000, 1, 7, 12),   -- Product 7: 110000 * (1 - 0.1) = 99000
-- Order 13
(195000, 2, 8, 13),  -- Product 8: 130000 * (1 - 0.25) = 97500 * 2 = 195000
(140000, 1, 9, 13),  -- Product 9: 200000 * (1 - 0.3) = 140000
-- Order 14
(256500, 3, 10, 14), -- Product 10: 95000 * (1 - 0.1) = 85500 * 3 = 256500
(224000, 2, 11, 14), -- Product 11: 140000 * (1 - 0.2) = 112000 * 2 = 224000
-- Order 15
(136000, 1, 12, 15), -- Product 12: 160000 * (1 - 0.15) = 136000
(189000, 2, 13, 15), -- Product 13: 105000 * (1 - 0.1) = 94500 * 2 = 189000
-- Order 16
(281250, 3, 14, 16), -- Product 14: 125000 * (1 - 0.25) = 93750 * 3 = 281250
(133000, 1, 15, 16), -- Product 15: 190000 * (1 - 0.3) = 133000
-- Order 17
(161500, 2, 16, 17), -- Product 16: 85000 * (1 - 0.05) = 80750 * 2 = 161500
(103500, 1, 17, 17), -- Product 17: 115000 * (1 - 0.1) = 103500
-- Order 18
(300000, 3, 1, 18),  -- Product 1: 100000 * (1 - 0) = 100000 * 3 = 300000
(320000, 2, 2, 18),  -- Product 2: 200000 * (1 - 0.2) = 160000 * 2 = 320000
-- Order 19
(135000, 1, 3, 19),  -- Product 3: 150000 * (1 - 0.1) = 135000
(204000, 2, 4, 19),  -- Product 4: 120000 * (1 - 0.15) = 102000 * 2 = 204000
-- Order 20
(432000, 3, 5, 20),  -- Product 5: 180000 * (1 - 0.2) = 144000 * 3 = 432000
(85500, 1, 6, 20);   -- Product 6: 90000 * (1 - 0.05) = 85500

-- Thêm transaction với created_date giống order
INSERT INTO `transaction` (amount, `description`, order_id, created_date) VALUES
-- Order 1
(360000, 'Payment for Order 1', 1, '2023-01-05 10:00:00'),
-- Order 2
(585000, 'Payment for Order 2', 2, '2023-02-10 14:30:00'),
-- Order 3
(684000, 'Payment for Order 3', 3, '2023-03-15 09:45:00'),
-- Order 4
(315000, 'Payment for Order 4', 4, '2023-04-20 16:20:00'),
-- Order 5
(630000, 'Payment for Order 5', 5, '2023-05-25 11:10:00'),
-- Order 6
(342000, 'Payment for Order 6', 6, '2023-06-30 13:50:00'),
-- Order 7
(472500, 'Payment for Order 7', 7, '2023-07-05 08:30:00'),
-- Order 8
(532000, 'Payment for Order 8', 8, '2023-08-10 17:00:00'),
-- Order 9
(435000, 'Payment for Order 9', 9, '2023-09-15 12:15:00'),
-- Order 10
(540000, 'Payment for Order 10', 10, '2023-10-20 10:45:00'),
-- Order 11
(408000, 'Payment for Order 11', 11, '2023-11-25 14:00:00'),
-- Order 12
(472500, 'Payment for Order 12', 12, '2023-12-30 09:30:00'),
-- Order 13
(532000, 'Payment for Order 13', 13, '2024-01-05 16:45:00'),
-- Order 14
(435000, 'Payment for Order 14', 14, '2024-02-10 11:20:00'),
-- Order 15
(540000, 'Payment for Order 15', 15, '2024-03-15 13:10:00'),
-- Order 16
(408000, 'Payment for Order 16', 16, '2024-04-20 10:00:00'),
-- Order 17
(472500, 'Payment for Order 17', 17, '2024-05-25 14:30:00'),
-- Order 18
(532000, 'Payment for Order 18', 18, '2024-06-30 09:45:00'),
-- Order 19
(435000, 'Payment for Order 19', 19, '2024-07-05 16:20:00'),
-- Order 20
(540000, 'Payment for Order 20', 20, '2024-08-10 11:10:00');

-- Thêm feedback
INSERT INTO feedback (rating, `comment`, customer_id, product_id) VALUES
-- Feedback từ Customer 1
(4.5, 'Sản phẩm rất tốt, da tôi cải thiện rõ rệt sau 2 tuần sử dụng.', 1, 1),
(3.8, 'Sản phẩm ổn, nhưng mùi hơi khó chịu.', 1, 2),
(5.0, 'Tuyệt vời! Da tôi mịn màng hơn hẳn.', 1, 3),
-- Feedback từ Customer 2
(4.0, 'Sản phẩm tốt, nhưng giá hơi cao.', 2, 4),
(4.7, 'Hiệu quả nhanh, da đỡ khô hẳn.', 2, 5),
(3.5, 'Sản phẩm tạm được, không gây kích ứng.', 2, 6),
-- Feedback từ Customer 3
(4.2, 'Sản phẩm phù hợp với da nhạy cảm.', 3, 7),
(4.8, 'Rất hài lòng, da sáng hơn sau 1 tháng.', 3, 8),
(3.0, 'Sản phẩm không phù hợp với da tôi.', 3, 9),
-- Feedback từ Customer 1
(4.9, 'Sản phẩm tuyệt vời, sẽ mua lại.', 1, 10),
(4.1, 'Hiệu quả tốt, nhưng đóng gói cần cải thiện.', 1, 11),
-- Feedback từ Customer 2
(4.6, 'Sản phẩm dưỡng ẩm rất tốt.', 2, 12),
(3.9, 'Sản phẩm tạm ổn, không gây bít tắc lỗ chân lông.', 2, 13),
-- Feedback từ Customer 3
(4.3, 'Sản phẩm làm sạch da nhẹ nhàng.', 3, 14),
(4.7, 'Rất thích, da đỡ dầu hẳn.', 3, 15),
-- Feedback từ Customer 1
(4.0, 'Sản phẩm tốt, nhưng cần thời gian để thấy hiệu quả.', 1, 16),
(4.5, 'Sản phẩm phù hợp với da dầu.', 1, 17),
-- Feedback từ Customer 2
(3.7, 'Sản phẩm không gây kích ứng, nhưng hiệu quả chậm.', 2, 1),
(4.2, 'Sản phẩm tốt, giá cả hợp lý.', 2, 2),
-- Feedback từ Customer 3
(4.8, 'Sản phẩm làm da sáng hơn rõ rệt.', 3, 3),
(4.4, 'Sản phẩm dưỡng ẩm tốt, không gây nhờn.', 3, 4);

-- của tui-- 

INSERT INTO skin_test (skin_test_id, skin_test_name, status)
VALUES (1, 'c',true);

INSERT INTO skin_type_question (skin_type_question_id, description, skin_test_id)
VALUES
(1, 'Assess your skin moisturization needs ,Please check all that are true about how often you must use a moisturizer for your skin to feel hydrated. (Multiple answers are preferred.)', 1),
(2, 'Assess your skin\'s sebum production.Please check all that are true about your facial skin. (Multiple answers are preferred.)', 1),
(3, 'Assess your skin\'s underlying inflammation.Check all the following that you have had in the last 4 weeks: (Multiple answers allowed)', 1),
(14, 'Assessing Oil and Moisture Levels on Your Skin', 1),
(15, 'How does your skin feel throughout the day?', 1),
(4, 'Do you want to lighten dark spots on your skin?Do you want skin lighteners in your skin care products to treat hyper pigmentation? (Choose one answer)', 1),
(5, 'Lifestyle habits Check all that apply to you. (Multiple answers allowed)', 1),
(6, 'Suncare Habits.Check all that apply to you. (Multiple answers allowed)', 1);

INSERT INTO skin_type_answer (skin_type_answer_id, description, skin_type_question_id,skin_type_id)
VALUES
(1, 'I can use any soap to wash my face without developing dryness.', 1, 1),
(2, 'I do not apply any products to my facial skin after cleansing.', 1, 4),
(3, 'I never or only occasionally apply a moisturizer.', 1, 2),
(4, 'I apply a moisturizer to my face once a day.', 1, 3),
(5, 'I apply a moisturizer to my face twice a day.', 1, 5),
(6, 'My facial skin is rough or dry', 2, 5),
(7, 'My facial skin is oily in some areas', 2, 2),
(8, 'My face is very oily.', 2, 1),
(9, 'My face is uncomfortable if I do not use a moisturizer', 2, 3),
(10, 'I like the feel of heavy creams and/or oil on my skin', 2, 5),
(11, 'None of the above', 2, 4),
(12, 'Acne (pimples)', 3, 1),
(13, 'Facial redness and/or flushing', 3, 3),
(14, 'Stinging or burning', 3, 3),
(15, 'A rash with itching, scaling and redness', 3, 5),
(16, 'Irritation from shaving the face', 3, 3),
(17, 'None of the above', 3, 4),
(18, 'My skin feels oily, especially in the T-zone (forehead, nose, chin).', 14, 1),
(19, 'My skin feels dry, tight, and lacking moisture.', 14, 5),
(20, 'My skin feels dry in some areas (cheeks) and oily in others (T-zone).', 14, 2),
(21, 'My skin feels soft, neither too oily nor too dry.', 14, 4),
(22, 'My skin feels easily irritated, itchy, and shows redness or rashes.', 14, 3),
(23, 'My skin gets oily by midday, especially in the T-zone (forehead, nose, chin), and I often need to blot it.', 15, 1),
(24, 'My skin feels comfortable all day—neither too oily nor too dry.', 15, 2),
(25, 'My skin feels dry and rough, with visible fine lines or wrinkles, especially as the day goes on.', 15, 5),
(26, 'My skin is oily in some areas (like my forehead and nose) but dry in others (like my cheeks).', 15, 2),
(27, 'My skin feels comfortable all day—neither too oily nor too dry.', 15, 4),
(28, 'My skin becomes irritated, red, or itchy, especially if I use certain products or when the weather changes.', 15, 3),
(29, 'My skin pigment is uneven AND I want to lighten darker areas on my face', 4, 2),
(30, 'My skin pigment is even AND I have no dark spots or darker areas', 4, 4),
(31, 'I have freckles or dark spots AND I do not want to remove', 4, 4),
(32, 'I currently smoke cigarettes or cigars', 5, 5),
(33, 'I have smoked over 50 cigarettes or cigars in my life', 5, 3),
(34, 'I am exposed to second-hand smoke on a weekly basis', 5, 3),
(35, 'I often get less than 7 hours of sleep a night', 5, 1),
(36, 'I feel stress at least 2 hours a day', 5, 1),
(37, 'Are you exposed to pollution or bad air quality more than 3 times a week?', 5, 3),
(38, 'I eat sugary foods over 3 times a week', 5, 1),
(39, 'I exercise less than 3 hours a week.', 5, 5),
(40, 'I do not eat fruit or vegetables every day.', 5, 5),
(41, 'None of the above', 5, 4),
(42, 'I have been to a tanning bed more than 3 times in my life.', 6, 4),
(43, 'I am exposed to the sun for over 3 hours a week.', 6, 3),
(44, 'I spend over 3 hours a week close to a window during daylight hours (including driving).', 6, 5),
(45, 'My face has been sunburned and peeled more than twice in my life.', 6, 3),
(46, 'I do not take daily antioxidant supplements like vitamin E and C.', 6, 5),
(47, 'One of my parents has more wrinkles than others their age.', 6, 5),
(48, 'I do not wear sunscreen every day', 6, 3),
(49, 'I do not wear sunscreen during outdoor activities', 6, 3),
(50, 'None of the above', 6, 4);

