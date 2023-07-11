namespace RulesEngineTestProject;

public class VipMemberChecker
{
    public Guid Id { get; set; }

    public VipMemberChecker()
    {
        Id = Guid.NewGuid();
    }

    public Task<bool> IsVipMember(string userId)
    {
        try
        {
            if (int.Parse(userId) % 2 != 0)
                return Task.FromResult(false);

            return Task.FromResult(true);
        }
        catch
        {
            return Task.FromResult(false);
        }
    }
}
