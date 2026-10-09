// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace Ecologistics.PaymentManagement.Core.Dtos;

public record CreateSubAccountRequest(
    string BusinessName,
    string SettlementBankCode,     // from ListBanks — NOT the bank name
    string AccountNumber,
    decimal PercentageCharge,      // required by Paystack; never relied on — we always pass transaction_charge: 0 explicitly
    string? Description = null,
    string? PrimaryContactEmail = null,
    string? PrimaryContactName = null,
    string? PrimaryContactPhone = null);

public record UpdateSubAccountRequest(
    string BusinessName,
    string SettlementBankCode,
    string? AccountNumber = null,
    decimal PercentageCharge = 0,
    string? Description = null,
    bool? Active = null);

public record PaystackSubAccount(
    string SubaccountCode,   // "ACCT_xxx" — store this on TenantPaystackAccount
    string BusinessName,
    string SettlementBank,   // bank NAME in responses, even though you send a bank CODE
    string AccountNumber,
    decimal PercentageCharge,
    bool Active);

public record PaystackBank(string Name, string Code, string Currency);

// ---------- Transaction models ----------
// Currency defaults to GHS — EcoLogistics operates in Ghanaian Cedis only, for now.
// All amounts are in pesewas (GHS subunit), matching the *InKobo naming kept for
// consistency with Paystack's own "amount in kobo/subunit" terminology.

public record InitializeTransactionRequest(
    string Email,
    long AmountInKobo,
    string Reference,
    string SubaccountCode,
    long TransactionChargeInKobo,
    string Currency = "GHS",
    string Bearer = "account",     // platform absorbs Paystack's own fee; subaccount gets 100% of the order amount
    string? CallbackUrl = null);

public record InitializeTransactionResult(
    string AuthorizationUrl,
    string AccessCode,
    string Reference);

public record VerifyTransactionResult(
    string Status,              // "success" | "failed" | "abandoned"
    string Reference,
    long TransactionId,
    string GatewayResponse,
    PaystackAuthorization? Authorization);

// ---------- Charge models ----------

public record ChargeAuthorizationRequest(
    string AuthorizationCode,
    string Email,
    long AmountInKobo,
    string Reference,
    string SubaccountCode,
    long TransactionChargeInKobo,
    string Currency = "GHS",
    string Bearer = "account");

public record ChargeMobileMoneyRequest(
    string Phone,
    string Provider,            // "mtn" | "vod" | "atl"
    string Email,
    long AmountInKobo,
    string Reference,
    string SubaccountCode,
    long TransactionChargeInKobo,
    string Currency = "GHS",
    string Bearer = "account");

public record SubmitOtpRequest(string Otp, string Reference);

public record ChargeResult(
    string Status,              // "success" | "send_otp" | "pay_offline" | "failed" | ...
    string Reference,
    long? TransactionId,
    string? GatewayResponse,
    PaystackAuthorization? Authorization);

public record PaystackAuthorization(
    string AuthorizationCode,
    bool Reusable,
    string Signature,
    string? Last4,
    string? Brand,              // card brand, e.g. "visa"
    string? Bank);
