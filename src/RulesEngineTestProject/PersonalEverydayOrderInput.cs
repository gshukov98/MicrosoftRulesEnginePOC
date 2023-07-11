namespace RulesEngineTestProject;

public class PersonalEverydayOrderInput
{
    public PersonalEverydayOrderInput(string userId, bool isVip, bool isOnetimeOrder, bool isRefunded)
    {
        UserId = userId;
        IsVip = isVip;
        IsOnetimeOrder = isOnetimeOrder;
        IsRefunded = isRefunded;
    }

    public string UserId { get; private set; }

    public bool IsVip { get; private set; }

    public bool IsOnetimeOrder { get; private set; }

    public bool IsRefunded { get; private set; }
}