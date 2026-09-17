-- Seed script for BalanceThresholdPerDistrict

INSERT INTO "BalanceThresholdPerDistrict"
(
    "Id",
    "DistrictId",
    "ThresholdAmount",
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
    100.00, -- Set the threshold amount to 100.00 for all districts
    '2026-08-06 20:22:30+00',
    'System',
    '2026-08-06 20:22:30+00',
    'System',
    false,
    NULL,
    NULL
FROM "Districts" d
WHERE NOT EXISTS (
    SELECT 1
    FROM "BalanceThresholdPerDistrict" btpd
    WHERE btpd."DistrictId" = d."Id"
);
