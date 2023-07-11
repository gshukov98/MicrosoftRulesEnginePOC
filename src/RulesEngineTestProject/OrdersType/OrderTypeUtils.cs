using Microsoft.Extensions.DependencyInjection;

namespace RulesEngineTestProject.OrdersType;

public static class OrderTypeUtils
{
    public static bool IsOnetimeOrder(string userId, string orderId, IServiceProvider provider)
    {
        OrderTypeChecker checker = provider.GetRequiredService<OrderTypeChecker>();
        bool isOnetimeOrder = checker.IsOnetimeOrderAsync(userId, orderId).GetAwaiter().GetResult();

        return isOnetimeOrder;
    }
}
