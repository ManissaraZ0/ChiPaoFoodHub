CREATE TABLE "users" (
  "id" integer GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
  "username" varchar(100) NOT NULL,
  "email" varchar(150) UNIQUE NOT NULL,
  "password_hash" varchar NOT NULL,
  "role" varchar NOT NULL,
  "created_at" timestamp NOT NULL DEFAULT (now())
);

CREATE TABLE "restaurants" (
  "id" integer GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
  "name" varchar NOT NULL,
  "manager_id" integer NOT NULL,
  "address" text NOT NULL,
  "category" varchar(50) NOT NULL,
  "created_at" timestamp NOT NULL DEFAULT (now())
);

CREATE TABLE "promotions" (
  "id" integer GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
  "restaurant_id" integer NOT NULL,
  "title" varchar NOT NULL,
  "price" decimal(10,2) NOT NULL,
  "conditions" text NOT NULL,
  "total_quota" integer NOT NULL DEFAULT 0,
  "start_date" timestamp NOT NULL,
  "end_date" timestamp NOT NULL
);

CREATE TABLE "promotion_tickets" (
  "id" integer GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
  "user_id" integer NOT NULL,
  "promotion_id" integer NOT NULL,
  "status" varchar NOT NULL,
  "purchase_date" timestamp NOT NULL DEFAULT (now()),
  "used_date" timestamp
);

CREATE TABLE "reviews" (
  "id" integer GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
  "user_id" integer NOT NULL,
  "restaurant_id" integer NOT NULL,
  "rating" integer NOT NULL,
  "comment" text,
  "created_at" timestamp NOT NULL DEFAULT (now())
);

COMMENT ON COLUMN "users"."role" IS 'client, manager';

COMMENT ON COLUMN "promotion_tickets"."status" IS 'Active, Used, Expired';

COMMENT ON COLUMN "reviews"."rating" IS '1 to 5';

ALTER TABLE "restaurants" ADD FOREIGN KEY ("manager_id") REFERENCES "users" ("id") DEFERRABLE INITIALLY IMMEDIATE;

ALTER TABLE "promotions" ADD FOREIGN KEY ("restaurant_id") REFERENCES "restaurants" ("id") DEFERRABLE INITIALLY IMMEDIATE;

ALTER TABLE "promotion_tickets" ADD FOREIGN KEY ("user_id") REFERENCES "users" ("id") DEFERRABLE INITIALLY IMMEDIATE;

ALTER TABLE "promotion_tickets" ADD FOREIGN KEY ("promotion_id") REFERENCES "promotions" ("id") DEFERRABLE INITIALLY IMMEDIATE;

ALTER TABLE "reviews" ADD FOREIGN KEY ("user_id") REFERENCES "users" ("id") DEFERRABLE INITIALLY IMMEDIATE;

ALTER TABLE "reviews" ADD FOREIGN KEY ("restaurant_id") REFERENCES "restaurants" ("id") DEFERRABLE INITIALLY IMMEDIATE;