// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace Ecologistics.PaymentManagement.Core.Features.AddCashPaymentMethod;

public class AddCashPaymentMethodCommandHandler(IPaymentManagementDbContext dbContext)
{
    public async Task Handle(AddCashPaymentMethodCommand command, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(command);

        UserPaymentMethod paymentMethod = new()
        {
            Id = new UserPaymentMethodId(Guid.CreateVersion7()),
            CreatedBy = command.UserId,
            Method = PaymentMethodType.Cash,
            Status = PaymentMethodStatus.Active,
        };

        dbContext.UserPaymentMethods.Add(paymentMethod);
        await dbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
    }
}
