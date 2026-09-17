// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace Ecologistics.TenantManagement.Domain.TenantAggregate;

public class Tenant : BaseAggregate<TenantId>
{
    private readonly Collection<TenantCompanyPurpose> _companyPurposes = [];
    public IReadOnlyCollection<TenantCompanyPurpose> CompanyPurposes => _companyPurposes.AsReadOnly();

    private readonly Collection<Member> _members = [];
    public IReadOnlyCollection<Member> Members => _members.AsReadOnly();

    public string RegistrationNumber { get; private set; } = string.Empty;
    public string CompanyName { get; private set; } = string.Empty;
    public Uri? LogoUrl { get; private set; }
    public TenantStatus Status { get; private set; } = TenantStatus.Unverified;
    public decimal TotalBalance { get; private set; } = decimal.Zero;

    public static Tenant Create(string companyName, Uri? logoUrl, string createdBy, IReadOnlyList<CompanyPurposeId> companyPurposeIds, string registrationNumber)
    {
        ArgumentNullException.ThrowIfNull(createdBy);
        ArgumentNullException.ThrowIfNull(companyPurposeIds);

        var newTenant = new Tenant
        {
            Id = new TenantId(Guid.CreateVersion7()),
            CompanyName = companyName,
            RegistrationNumber = registrationNumber,
            LogoUrl = logoUrl,
            Status = TenantStatus.Unverified,
            TotalBalance = decimal.Zero
        };

        var newMember = new Member(newTenant.Id, true, createdBy)
        {
            Id = new MemberId(Guid.CreateVersion7())
        };

        newTenant._members.Add(newMember);

        foreach (var companyPurposeId in companyPurposeIds)
        {
            var purpose = new TenantCompanyPurpose(newTenant.Id, companyPurposeId) { Id = Guid.CreateVersion7() };
            newTenant._companyPurposes.Add(purpose);
        }

        newTenant.AddDomainEvent(new TenantCreatedEvent(
            newTenant.RegistrationNumber,
            newTenant.CompanyName,
            createdBy));

        return newTenant;
    }

    public void UpdateTenantStatus(TenantStatus status)
    {
        if (Status == status) return;

        Status = status;
        AddDomainEvent(new TenantStatusUpdatedEvent(
            Id,
            Status));
    }

    public decimal UpdateTenantBalance(decimal amount)
    {
        if (amount <= 0)
            throw new ArgumentOutOfRangeException(nameof(amount), "Amount must be greater than zero.");

        TotalBalance += amount;
        return TotalBalance;
    }

    public Uri UpdateTenantLogo(Uri? logoUrl)
    {
        ArgumentNullException.ThrowIfNull(logoUrl);

        LogoUrl = logoUrl;
        return LogoUrl;
    }

    public Member AddMember(string userId)
    {
        ArgumentNullException.ThrowIfNull(userId);

        if(_members.Any(m => m.UserId == userId))
            throw new BadRequestException("User already is a member of this tenant.");

        var newMember = new Member(Id, false, userId) { Id = new MemberId(Guid.CreateVersion7()) };

        _members.Add(newMember);

        return newMember;
    }

    public void RemoveMember(MemberId memberId)
    {
        ArgumentNullException.ThrowIfNull(memberId);

        var memberToRemove = _members.FirstOrDefault(m => m.Id == memberId);
        if (memberToRemove is null)
            throw new NotFoundException("Member not found.");
    }

    public void AddCompanyPurpose(CompanyPurposeId companyPurposeId)
    {
        ArgumentNullException.ThrowIfNull(companyPurposeId);

        if(_companyPurposes.Any(cp => cp.CompanyPurposeId == companyPurposeId))
            throw new BadRequestException("Company purpose already exists.");

        var newCompanyPurpose = new TenantCompanyPurpose(Id, companyPurposeId) { Id = Guid.CreateVersion7() };

        _companyPurposes.Add(newCompanyPurpose);
    }
}
