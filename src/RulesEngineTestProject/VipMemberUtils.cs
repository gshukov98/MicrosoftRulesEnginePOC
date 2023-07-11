using Microsoft.Extensions.DependencyInjection;

namespace RulesEngineTestProject;

public static class VipMemberUtils
{
    public static bool IsVipMember(string userId, IServiceProvider provider)
    {
        VipMemberChecker vipChecker = provider.GetRequiredService<VipMemberChecker>();
        bool isVip = vipChecker.IsVipMember(userId).GetAwaiter().GetResult();

        return isVip;
    }
}
