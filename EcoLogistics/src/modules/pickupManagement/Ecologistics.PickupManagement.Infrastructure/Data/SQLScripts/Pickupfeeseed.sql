-- Seed script for PickupFee
--
-- ⚠️ SCHEMA NOTE: The PickupFee entity has a `Commission` decimal property, but the
-- migration you shared only creates an `Amount` column — no `Commission` column exists
-- yet. The ALTER TABLE below adds it. If you already have a separate migration that
-- added this column, DELETE the ALTER TABLE block before running this script.
ALTER TABLE "PickupFees"
    ADD COLUMN IF NOT EXISTS "Commission" numeric NOT NULL DEFAULT 0;

-- Needed for gen_random_uuid(); safe to run even if already enabled.
CREATE EXTENSION IF NOT EXISTS pgcrypto;

-- Cross-joins every row currently in "Districts" with every row in "WasteMeasurements",
-- so it covers all districts regardless of when/how they were seeded — no hardcoded IDs.
--
-- Pricing tiers (Amount and Commission both flat currency amounts; Commission is
-- kept at or under 15% of Amount for each tier):
--   Small             -> Amount 20.00,  Commission 2.00   (10% of Amount)
--   Medium            -> Amount 50.00,  Commission 6.00   (12% of Amount)
--   Large             -> Amount 90.00,  Commission 12.60  (14% of Amount)
--   Heavy/Industrial  -> Amount 150.00, Commission 22.50  (15% of Amount)
--
-- The WHERE NOT EXISTS guard makes this safe to re-run: it won't create duplicate
-- (DistrictId, WasteMeasurementId) pairs even without a unique constraint on the table.
INSERT INTO "PickupFees"
(
    "Id",
    "DistrictId",
    "WasteMeasurementId",
    "Amount",
    "Commission",
    "CreatedAt",
    "CreatedBy",
    "UpdatedAt",
    "UpdatedBy",
    "IsDeleted",
    "DeletedAt",
    "DeletedBy"
)
SELECT
    gen_random_uuid(),
    d."Id",
    wm."Id",
    CASE wm."Name"
        WHEN 'Small'             THEN 20.00
        WHEN 'Medium'            THEN 50.00
        WHEN 'Large'             THEN 90.00
        WHEN 'Heavy/Industrial'  THEN 150.00
        END,
    CASE wm."Name"
        WHEN 'Small'             THEN 2.00
        WHEN 'Medium'            THEN 6.00
        WHEN 'Large'             THEN 12.60
        WHEN 'Heavy/Industrial'  THEN 22.50
        END,
    '2026-08-06 20:22:30+00',
    'System',
    '2026-08-06 20:22:30+00',
    'System',
    false,
    NULL,
    NULL
FROM "Districts" d
         CROSS JOIN "WasteMeasurements" wm
WHERE NOT EXISTS (
    SELECT 1
    FROM "PickupFees" pf
    WHERE pf."DistrictId" = d."Id"
      AND pf."WasteMeasurementId" = wm."Id"
);
