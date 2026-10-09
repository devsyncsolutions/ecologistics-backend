// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace Ecologistics.PaymentManagement.Core.Features.GetInvoices;

public class GetInvoicesQueryHandler(
    IPaymentManagementDbContext dbContext,
    ILoggedInUserService loggedInUserService)
{
    public async Task<PagedResponse<InvoiceDto>> Handle(GetInvoicesQuery request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        IQueryable<PickupJobInvoice> query = dbContext
            .PickupJobInvoices
            .OrderByDescending(p => p.CreatedAt)
            .Where(p => p.CreatedBy == loggedInUserService.GetUserId());

        int pageSize = request.PageSize <= 0 ? 10 : request.PageSize;

        int totalCount = await query.CountAsync(cancellationToken).ConfigureAwait(false);

        List<InvoiceDto> pagedList = await query
            .Select(p => new InvoiceDto(
                p.ReferenceNumber,
                p.PaymentCompletedOn.ToString(CultureInfo.InvariantCulture),
                p.ContentName,
                p.AmountPayable,
                p.Status.ToString(),
                p.InvoiceUrl == null ? string.Empty : p.InvoiceUrl.ToString()))
            .Skip((request.Page -1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken)
            .ConfigureAwait(false);

        return new PagedResponse<InvoiceDto>
        {
            Items = pagedList,
            TotalCount = totalCount,
            Page = request.Page,
            PageSize = pageSize,
            TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize)
        };
    }
}
