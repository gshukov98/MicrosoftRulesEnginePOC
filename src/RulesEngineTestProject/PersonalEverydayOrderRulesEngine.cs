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

        return new RulesEngine.RulesEngine(workflows.ToArray());
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
        Rule personalEverydayOrderRule = new Rule();
        personalEverydayOrderRule.RuleName = "PersonalEverydayOrder";
        personalEverydayOrderRule.RuleExpressionType = RuleExpressionType.LambdaExpression;
        personalEverydayOrderRule.Operator = "And";
        personalEverydayOrderRule.SuccessEvent = "1";
        personalEverydayOrderRule.ErrorMessage = "Order not qualified.";

        List<Rule> nestedRules = new List<Rule>();
        IsVipRule(nestedRules);
        IsOnetimeOrderRule(nestedRules);
        IsNotRefundedRule(nestedRules);

        personalEverydayOrderRule.Rules = nestedRules;
        return personalEverydayOrderRule;
    }

    private static void IsVipRule(List<Rule> nestedRules)
    {
        Rule isVipUserRule = new Rule();
        isVipUserRule.RuleName = "IsVipUser";
        isVipUserRule.RuleExpressionType = RuleExpressionType.LambdaExpression;
        isVipUserRule.ErrorMessage = "User is not VIP.";
        isVipUserRule.Expression = "input1.IsVip == true";
        nestedRules.Add(isVipUserRule);
    }

    private static void IsOnetimeOrderRule(List<Rule> nestedRules)
    {
        Rule isOnetimeOrderRule = new Rule();
        isOnetimeOrderRule.RuleName = "IsOnetimeOrder";
        isOnetimeOrderRule.RuleExpressionType = RuleExpressionType.LambdaExpression;
        isOnetimeOrderRule.ErrorMessage = "Order is not with proper type.";
        isOnetimeOrderRule.Expression = "input1.IsOnetimeOrder == true";
        nestedRules.Add(isOnetimeOrderRule);
    }

    private static void IsNotRefundedRule(List<Rule> nestedRules)
    {
        Rule isNotRefundedRule = new Rule();
        isNotRefundedRule.RuleName = "IsNotRefundedOrder";
        isNotRefundedRule.RuleExpressionType = RuleExpressionType.LambdaExpression;
        isNotRefundedRule.ErrorMessage = "Order was refunded.";
        isNotRefundedRule.Expression = "input1.IsRefunded == false";
        nestedRules.Add(isNotRefundedRule);
    }
}
