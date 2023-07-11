namespace RulesEngineTestProject.OrdersType;

public class OrderTypeChecker
{
    public Task<bool> IsOnetimeOrderAsync(string userId, string orderId)
    {
        if (orderId.Equals("123"))
            return Task.FromResult(true);

        return Task.FromResult(false);
    }
}
