namespace RulesEngineTestProject;

public class PersonalEverydayOrderInput
{
    public PersonalEverydayOrderInput(string userId, string orderId, IServiceProvider provider)
    {
        UserId = userId;
        OrderId = orderId;
        Provider = provider;
    }

    public string UserId { get; private set; }

    public string OrderId { get; private set; }

    public IServiceProvider Provider { get; private set; }
}