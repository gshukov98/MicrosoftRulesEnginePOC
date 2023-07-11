namespace RulesEngineTestProject.OrdersRefund;

public class OrderRefundChecker
{
    public Task<bool> IsRefundedOrderAsync(string userId, string orderId)
    {
        if (orderId.Equals("321"))
            return Task.FromResult(true);

        return Task.FromResult(false);
    }
}
