using RulesEngine.Extensions;
using RulesEngine.Models;
using RulesEngineTestProject;

namespace RulesEngine.Service
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            PersonalEverydayOrderRulesEngine rulesEngineTest = new PersonalEverydayOrderRulesEngine();
            RulesEngine rulesEngine = rulesEngineTest.ConstructRulesEngine();

            PersonalEverydayOrderInput input = new PersonalEverydayOrderInput("123", true, true, false);
            List<RuleResultTree> result = await rulesEngine.ExecuteAllRulesAsync("Personal Everyday Order Workflow", input);

            result.OnSuccess((eventName) =>
            {
                _logger.LogInformation("Achieved discount %eventName", eventName);
            });

            result.OnFail(() =>
            {
                _logger.LogInformation("Failed to achieve discount");
            });
        }
    }
}