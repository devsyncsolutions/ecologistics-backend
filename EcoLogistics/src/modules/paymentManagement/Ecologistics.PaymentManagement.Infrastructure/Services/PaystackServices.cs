// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace Ecologistics.PaymentManagement.Infrastructure.Services;

public class PaystackServices: IPaystackServices
{
    private readonly HttpClient _http;
    private static readonly JsonSerializerOptions s_jsonOptions = new()
    {
        PropertyNameCaseInsensitive = true
    };

    public PaystackServices(IHttpClientFactory httpClient, IOptions<PaystackOptions> options)
    {
        ArgumentNullException.ThrowIfNull(httpClient);
        ArgumentNullException.ThrowIfNull(options);
        _http = httpClient.CreateClient(options.Value.SectionName);
    }

    public async Task<PaystackSubAccount> CreateSubAccount(CreateSubAccountRequest request, CancellationToken ct)
    {
        ArgumentNullException.ThrowIfNull(request);

        var wire = new SubAccountWireRequest(
            BusinessName: request.BusinessName,
            SettlementBank: request.SettlementBankCode,
            AccountNumber: request.AccountNumber,
            PercentageCharge: request.PercentageCharge,
            Description: request.Description,
            PrimaryContactEmail: request.PrimaryContactEmail,
            PrimaryContactName: request.PrimaryContactName,
            PrimaryContactPhone: request.PrimaryContactPhone);

        SubAccountWireData wireData = await Post<SubAccountWireRequest, SubAccountWireData>("/subaccount", wire, ct).ConfigureAwait(false);
        return MapSubAccount(wireData);
    }

    public async Task<PaystackSubAccount> GetSubAccount(string idOrCode, CancellationToken ct)
    {
        SubAccountWireData data = await Get<SubAccountWireData>($"/subaccount/{Uri.EscapeDataString(idOrCode)}", ct).ConfigureAwait(false);
        return MapSubAccount(data);
    }

    public async Task<PaystackSubAccount> UpdateSubAccount(string idOrCode, UpdateSubAccountRequest request,
        CancellationToken ct)
    {
        ArgumentNullException.ThrowIfNull(request);

        var wire = new SubAccountWireRequest(
            BusinessName: request.BusinessName,
            SettlementBank: request.SettlementBankCode,
            AccountNumber: request.AccountNumber!,
            PercentageCharge: request.PercentageCharge,
            Description: request.Description,
            PrimaryContactEmail: null,
            PrimaryContactName: null,
            PrimaryContactPhone: null,
            Active: request.Active);

        SubAccountWireData data = await Put<SubAccountWireRequest, SubAccountWireData>(
            $"/subaccount/{Uri.EscapeDataString(idOrCode)}", wire, ct).ConfigureAwait(false);
        return MapSubAccount(data);
    }

    public async Task<IReadOnlyList<PaystackBank>> ListBanks(string country, CancellationToken ct)
    {
        List<BankWireData> data = await Get<List<BankWireData>>($"/bank?country={Uri.EscapeDataString(country)}", ct).ConfigureAwait(false);
        return data.Select(b => new PaystackBank(b.Name, b.Code, b.Currency)).ToList();
    }

    public async Task<InitializeTransactionResult> InitializeTransaction(InitializeTransactionRequest request,
        CancellationToken ct)
    {
        ArgumentNullException.ThrowIfNull(request);

        var wire = new InitializeWireRequest(
            Email: request.Email,
            Amount: request.AmountInKobo,
            Reference: request.Reference,
            Subaccount: request.SubaccountCode,
            TransactionCharge: request.TransactionChargeInKobo,
            Currency: request.Currency,
            Bearer: request.Bearer,
            CallbackUrl: request.CallbackUrl);

        InitializeWireData data = await Post<InitializeWireRequest, InitializeWireData>("/transaction/initialize", wire, ct).ConfigureAwait(false);
        return new InitializeTransactionResult(data.AuthorizationUrl, data.AccessCode, data.Reference);
    }

    public async Task<VerifyTransactionResult> VerifyTransaction(string reference, CancellationToken ct)
    {
        ChargeWireData data = await Get<ChargeWireData>($"/transaction/verify/{Uri.EscapeDataString(reference)}", ct).ConfigureAwait(false);
        return new VerifyTransactionResult(
            data.Status, data.Reference, data.Id, data.GatewayResponse, MapAuthorization(data.Authorization));
    }

    public async Task<ChargeResult> ChargeAuthorization(ChargeAuthorizationRequest request, CancellationToken ct)
    {
        ArgumentNullException.ThrowIfNull(request);

        var wire = new ChargeAuthorizationWireRequest(
            AuthorizationCode: request.AuthorizationCode,
            Email: request.Email,
            Amount: request.AmountInKobo,
            Reference: request.Reference,
            Subaccount: request.SubaccountCode,
            TransactionCharge: request.TransactionChargeInKobo,
            Currency: request.Currency,
            Bearer: request.Bearer);

        var data = await Post<ChargeAuthorizationWireRequest, ChargeWireData>(
            "/transaction/charge_authorization", wire, ct).ConfigureAwait(false);
        return MapCharge(data);
    }

    public async Task<ChargeResult> ChargeMobileMoney(ChargeMobileMoneyRequest request, CancellationToken ct)
    {
        ArgumentNullException.ThrowIfNull(request);

        var wire = new ChargeMobileMoneyWireRequest(
            Email: request.Email,
            Amount: request.AmountInKobo,
            Reference: request.Reference,
            Subaccount: request.SubaccountCode,
            TransactionCharge: request.TransactionChargeInKobo,
            Currency: request.Currency,
            Bearer: request.Bearer,
            MobileMoney: new MobileMoneyWire(request.Phone, request.Provider));

        ChargeWireData data = await Post<ChargeMobileMoneyWireRequest, ChargeWireData>("/charge", wire, ct).ConfigureAwait(false);
        return MapCharge(data);
    }

    public async Task<ChargeResult> SubmitOtp(SubmitOtpRequest request, CancellationToken ct)
    {
        ArgumentNullException.ThrowIfNull(request);
        var wire = new SubmitOtpWireRequest(request.Otp, request.Reference);
        ChargeWireData data = await Post<SubmitOtpWireRequest, ChargeWireData>("/charge/submit_otp", wire, ct).ConfigureAwait(false);
        return MapCharge(data);
    }

    // ---------- HTTP plumbing ----------
    private async Task<TData> Get<TData>(string path, CancellationToken ct) =>
        await Send<object?, TData>(HttpMethod.Get, path, null, ct).ConfigureAwait(false);
    private async Task<TData> Post<TRequest, TData>(string path, TRequest body, CancellationToken ct) =>
        await Send<TRequest, TData>(HttpMethod.Post, path, body, ct).ConfigureAwait(false);
    private async Task<TData> Put<TRequest, TData>(string path, TRequest body, CancellationToken ct) =>
        await Send<TRequest, TData>(HttpMethod.Put, path, body, ct).ConfigureAwait(false);
    private async Task<TData> Send<TRequest, TData>(
        HttpMethod method, string path, TRequest? body, CancellationToken ct)
    {
        using var httpRequest = new HttpRequestMessage(method, path);
        if (body is not null)
            httpRequest.Content = JsonContent.Create(body, options: s_jsonOptions);

        using HttpResponseMessage response = await _http.SendAsync(httpRequest, ct).ConfigureAwait(false);
        PaystackEnvelope<TData>? envelope = await response.Content.ReadFromJsonAsync<PaystackEnvelope<TData>>(s_jsonOptions, ct)
            .ConfigureAwait(false);

        if (envelope is null || !envelope.Status)
            throw new PaystackApiException(envelope?.Message ?? $"Paystack request to {path} failed with no body");

        return envelope.Data;
    }

    private static PaystackSubAccount MapSubAccount(SubAccountWireData d) =>
        new(d.SubaccountCode, d.BusinessName, d.SettlementBank, d.AccountNumber, d.PercentageCharge, d.Active);
    private static ChargeResult MapCharge(ChargeWireData d) =>
        new(d.Status, d.Reference, d.Id, d.GatewayResponse, MapAuthorization(d.Authorization));
    private static PaystackAuthorization? MapAuthorization(AuthorizationWireData? a) => a is null ? null :
        new PaystackAuthorization(a.AuthorizationCode, a.Reusable, a.Signature, a.Last4, a.Brand, a.Bank);
}
