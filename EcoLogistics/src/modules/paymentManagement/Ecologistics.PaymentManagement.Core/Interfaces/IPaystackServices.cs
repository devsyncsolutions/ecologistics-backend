// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace Ecologistics.PaymentManagement.Core.Interfaces;

public interface IPaystackServices
{
    // Subaccounts — one per Tenant, created once during tenant onboarding
    Task<PaystackSubAccount> CreateSubAccount(CreateSubAccountRequest request, CancellationToken ct);
    Task<PaystackSubAccount> GetSubAccount(string idOrCode, CancellationToken ct);
    Task<PaystackSubAccount> UpdateSubAccount(string idOrCode, UpdateSubAccountRequest request, CancellationToken ct);

    // Banks — needed to resolve a bank name to the code Paystack expects in settlement_bank
    Task<IReadOnlyList<PaystackBank>> ListBanks(string country, CancellationToken ct);

    // Transactions — card flow (initialize, client completes via Inline) + verification
    Task<InitializeTransactionResult> InitializeTransaction(InitializeTransactionRequest request, CancellationToken ct);
    Task<VerifyTransactionResult> VerifyTransaction(string reference, CancellationToken ct);

    // Charges — reusing a saved authorization, and fresh mobile money charges
    Task<ChargeResult> ChargeAuthorization(ChargeAuthorizationRequest request, CancellationToken ct);
    Task<ChargeResult> ChargeMobileMoney(ChargeMobileMoneyRequest request, CancellationToken ct);
    Task<ChargeResult> SubmitOtp(SubmitOtpRequest request, CancellationToken ct);
}
