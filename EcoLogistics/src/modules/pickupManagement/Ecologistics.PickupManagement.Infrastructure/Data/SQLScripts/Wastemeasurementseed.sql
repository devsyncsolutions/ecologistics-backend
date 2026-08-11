-- Seed script for WasteMeasurement
-- Matches exact columns/types from the EF Core migration.
--
-- ⚠️ NOTE: The migration defines the weight-range columns with a trailing space
-- in their actual Postgres names: "WeightRangeMax " and "WeightRangeMin "
-- (from HasColumnName("WeightRangeMax ") / HasColumnName("WeightRangeMin ")).
-- This is almost certainly an unintentional typo in your entity configuration.
-- The INSERT below targets the columns exactly as they exist today (with the
-- trailing space, double-quoted) so it will run against your current schema.
-- Recommend fixing the HasColumnName(...) calls and adding a rename migration,
-- then dropping the trailing space from this script afterwards.

INSERT INTO "WasteMeasurements"
(
    "Id",
    "Name",
    "Description",
    "UnitOfMeasurement",
    "WeightRangeMax ",
    "WeightRangeMin ",
    "CreatedAt",
    "CreatedBy",
    "UpdatedAt",
    "UpdatedBy",
    "IsDeleted",
    "DeletedAt",
    "DeletedBy"
)
VALUES
    (
        '66bb20ab-b1cf-4e0c-a6f4-1cb40a37dfb8',
        'Small',
        '',
        'kg',
        25,
        0,
        '2026-08-06 20:22:30+00',
        'System',
        '2026-08-06 20:22:30+00',
        'System',
        false,
        NULL,
        NULL
    ),
    (
        '8a388949-ac2b-47ca-9a2c-e0e340cb4fe2',
        'Medium',
        '',
        'kg',
        100,
        26,
        '2026-08-06 20:22:30+00',
        'System',
        '2026-08-06 20:22:30+00',
        'System',
        false,
        NULL,
        NULL
    ),
    (
        '0d108a38-6bc7-49bf-8765-fbb47ee3d59d',
        'Large',
        '',
        'kg',
        200,
        101,
        '2026-08-06 20:22:30+00',
        'System',
        '2026-08-06 20:22:30+00',
        'System',
        false,
        NULL,
        NULL
    ),
    (
        'd039082f-42a7-4d3c-86a5-e1412f1e8ca9',
        'Heavy/Industrial',
        '',
        'kg',
        500,
        201,
        '2026-08-06 20:22:30+00',
        'System',
        '2026-08-06 20:22:30+00',
        'System',
        false,
        NULL,
        NULL
    )
    ON CONFLICT ("Id") DO NOTHING;
