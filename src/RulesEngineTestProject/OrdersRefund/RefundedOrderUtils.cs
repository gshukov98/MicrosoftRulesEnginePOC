using Microsoft.Extensions.DependencyInjection;

namespace RulesEngineTestProject.OrdersRefund;

public static class RefundedOrderUtils
{
    public static bool IsRefundedOrder(string userId, string orderId, IServiceProvider provider)
    {
        OrderRefundChecker checker = provider.GetRequiredService<OrderRefundChecker>();
        bool isRefunded = checker.IsRefundedOrderAsync(userId, orderId).GetAwaiter().GetResult();

        return isRefunded;
    }
}
