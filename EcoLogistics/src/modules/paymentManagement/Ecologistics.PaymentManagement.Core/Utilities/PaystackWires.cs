// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace Ecologistics.PaymentManagement.Core.Utilities;

public record PaystackEnvelope<T>(
        [property: JsonPropertyName("status")] bool Status,
        [property: JsonPropertyName("message")] string Message,
        [property: JsonPropertyName("data")] T Data);

    public record SubAccountWireRequest(
        [property: JsonPropertyName("business_name")] string BusinessName,
        [property: JsonPropertyName("settlement_bank")] string SettlementBank,
        [property: JsonPropertyName("account_number")] string AccountNumber,
        [property: JsonPropertyName("percentage_charge")] decimal PercentageCharge,
        [property: JsonPropertyName("description")] string? Description,
        [property: JsonPropertyName("primary_contact_email")] string? PrimaryContactEmail,
        [property: JsonPropertyName("primary_contact_name")] string? PrimaryContactName,
        [property: JsonPropertyName("primary_contact_phone")] string? PrimaryContactPhone,
        [property: JsonPropertyName("active")] bool? Active = null);

    public record SubAccountWireData(
        [property: JsonPropertyName("subaccount_code")] string SubaccountCode,
        [property: JsonPropertyName("business_name")] string BusinessName,
        [property: JsonPropertyName("settlement_bank")] string SettlementBank,
        [property: JsonPropertyName("account_number")] string AccountNumber,
        [property: JsonPropertyName("percentage_charge")] decimal PercentageCharge,
        [property: JsonPropertyName("active")] bool Active);

    public record BankWireData(
        [property: JsonPropertyName("name")] string Name,
        [property: JsonPropertyName("code")] string Code,
        [property: JsonPropertyName("currency")] string Currency);

    public record InitializeWireRequest(
        [property: JsonPropertyName("email")] string Email,
        [property: JsonPropertyName("amount")] long Amount,
        [property: JsonPropertyName("reference")] string Reference,
        [property: JsonPropertyName("subaccount")] string Subaccount,
        [property: JsonPropertyName("transaction_charge")] long TransactionCharge,
        [property: JsonPropertyName("currency")] string Currency,
        [property: JsonPropertyName("bearer")] string Bearer,
        [property: JsonPropertyName("callback_url")] string? CallbackUrl);

    public record InitializeWireData(
        [property: JsonPropertyName("authorization_url")] string AuthorizationUrl,
        [property: JsonPropertyName("access_code")] string AccessCode,
        [property: JsonPropertyName("reference")] string Reference);

    public record ChargeAuthorizationWireRequest(
        [property: JsonPropertyName("authorization_code")] string AuthorizationCode,
        [property: JsonPropertyName("email")] string Email,
        [property: JsonPropertyName("amount")] long Amount,
        [property: JsonPropertyName("reference")] string Reference,
        [property: JsonPropertyName("subaccount")] string Subaccount,
        [property: JsonPropertyName("transaction_charge")] long TransactionCharge,
        [property: JsonPropertyName("currency")] string Currency,
        [property: JsonPropertyName("bearer")] string Bearer);

    public record MobileMoneyWire(
        [property: JsonPropertyName("phone")] string Phone,
        [property: JsonPropertyName("provider")] string Provider);

    public record ChargeMobileMoneyWireRequest(
        [property: JsonPropertyName("email")] string Email,
        [property: JsonPropertyName("amount")] long Amount,
        [property: JsonPropertyName("reference")] string Reference,
        [property: JsonPropertyName("subaccount")] string Subaccount,
        [property: JsonPropertyName("transaction_charge")] long TransactionCharge,
        [property: JsonPropertyName("currency")] string Currency,
        [property: JsonPropertyName("bearer")] string Bearer,
        [property: JsonPropertyName("mobile_money")] MobileMoneyWire MobileMoney);

    public record SubmitOtpWireRequest(
        [property: JsonPropertyName("otp")] string Otp,
        [property: JsonPropertyName("reference")] string Reference);

    public record ChargeWireData(
        [property: JsonPropertyName("status")] string Status,
        [property: JsonPropertyName("reference")] string Reference,
        [property: JsonPropertyName("id")] long Id,
        [property: JsonPropertyName("gateway_response")] string GatewayResponse,
        [property: JsonPropertyName("authorization")] AuthorizationWireData? Authorization);

    public record AuthorizationWireData(
        [property: JsonPropertyName("authorization_code")] string AuthorizationCode,
        [property: JsonPropertyName("reusable")] bool Reusable,
        [property: JsonPropertyName("signature")] string Signature,
        [property: JsonPropertyName("last4")] string? Last4,
        [property: JsonPropertyName("brand")] string? Brand,
        [property: JsonPropertyName("bank")] string? Bank);
