using RulesEngine.Models;

namespace RulesEngineTestProject;

public class PersonalEverydayOrderRulesEngine
{
    public RulesEngine.RulesEngine ConstructRulesEngine()
    {
        List<Workflow> workflows = new List<Workflow>
        {
            PersonalEverydayOrderWorkflow()
        };

        ReSettings reSettingsWithCustomTypes = new ReSettings
        {
            CustomTypes = new Type[] { typeof(VipMemberUtils), typeof(OrderTypeUtils), typeof(RefundedOrderUtils) }
        };

        return new RulesEngine.RulesEngine(workflows.ToArray(), reSettingsWithCustomTypes);
    }

    private Workflow PersonalEverydayOrderWorkflow()
    {
        Workflow workflow = new Workflow();
        workflow.WorkflowName = "Personal Everyday Order Workflow";
        workflow.Rules = new List<Rule>() { PersonalEverydayOrderRule() };

        return workflow;
    }

    private Rule PersonalEverydayOrderRule()
    {
        //Top level rule
        Rule personalEverydayOrderRule = new Rule();
        personalEverydayOrderRule.RuleName = "PersonalEverydayOrder";
        personalEverydayOrderRule.RuleExpressionType = RuleExpressionType.LambdaExpression;
        personalEverydayOrderRule.Operator = "And";
        personalEverydayOrderRule.SuccessEvent = "1";
        personalEverydayOrderRule.ErrorMessage = "Order not qualified.";

        // Nested rules
        List<Rule> nestedRules = new List<Rule>();
        IsVipRule(nestedRules);
        IsOnetimeOrderRule(nestedRules);
        IsNotRefundedRule(nestedRules);

        //Local params (used for expressions in nested rules)
        personalEverydayOrderRule.LocalParams = GetLocalParams();

        personalEverydayOrderRule.Rules = nestedRules;
        return personalEverydayOrderRule;
    }

    private static List<LocalParam> GetLocalParams()
    {
        List<LocalParam> localParams = new List<LocalParam>();
        LocalParam vipMemberLocalParam = new LocalParam()
        {
            Name = "checkVipMember",
            Expression = "VipMemberUtils.IsVipMember(input1.UserId,input1.Provider) == true"
        };
        localParams.Add(vipMemberLocalParam);

        LocalParam isOnetimeOrderLocalParam = new LocalParam()
        {
            Name = "checkOrderType",
            Expression = "OrderTypeUtils.IsOnetimeOrder(input1.UserId,input1.IsOnetimeOrder) == true"
        };
        localParams.Add(isOnetimeOrderLocalParam);

        LocalParam isRefundedOrderLocalParam = new LocalParam()
        {
            Name = "checkOrderRefund",
            Expression = "RefundedOrderUtils.IsRefundedOrder(input1.UserId,input1.IsRefunded) == false"
        };
        localParams.Add(isRefundedOrderLocalParam);
        return localParams;
    }

    private static void IsVipRule(List<Rule> nestedRules)
    {
        Rule isVipUserRule = new Rule();
        isVipUserRule.RuleName = "IsVipUser";
        isVipUserRule.RuleExpressionType = RuleExpressionType.LambdaExpression;
        isVipUserRule.ErrorMessage = "User is not VIP.";
        isVipUserRule.Expression = "checkVipMember";
        nestedRules.Add(isVipUserRule);
    }

    private static void IsOnetimeOrderRule(List<Rule> nestedRules)
    {
        Rule isOnetimeOrderRule = new Rule();
        isOnetimeOrderRule.RuleName = "IsOnetimeOrder";
        isOnetimeOrderRule.RuleExpressionType = RuleExpressionType.LambdaExpression;
        isOnetimeOrderRule.ErrorMessage = "Order is not with proper type.";
        isOnetimeOrderRule.Expression = "checkOrderType";
        nestedRules.Add(isOnetimeOrderRule);
    }

    private static void IsNotRefundedRule(List<Rule> nestedRules)
    {
        Rule isNotRefundedRule = new Rule();
        isNotRefundedRule.RuleName = "IsNotRefundedOrder";
        isNotRefundedRule.RuleExpressionType = RuleExpressionType.LambdaExpression;
        isNotRefundedRule.ErrorMessage = "Order was refunded.";
        isNotRefundedRule.Expression = "checkOrderRefund";
        nestedRules.Add(isNotRefundedRule);
    }
}
