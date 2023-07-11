namespace RulesEngineTestProject;

public class PersonalEverydayOrderInput
{
    public PersonalEverydayOrderInput(string userId, bool isOnetimeOrder, bool isRefunded, IServiceProvider provider)
    {
        UserId = userId;
        IsOnetimeOrder = isOnetimeOrder;
        IsRefunded = isRefunded;
        Provider = provider;
    }

    public string UserId { get; private set; }

    public bool IsOnetimeOrder { get; private set; }

    public bool IsRefunded { get; private set; }

    public IServiceProvider Provider { get; private set; }
}