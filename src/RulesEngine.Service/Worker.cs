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
            await SucessfulAsync(rulesEngine);
            await FailedUserIsNotVipAsync(rulesEngine);
            await FailedNotOnetimeOrderAsync(rulesEngine);
            await FailedRefundedOrderAsync(rulesEngine);
            await FailedAllNestedRulesFailedAsync(rulesEngine);
        }

        private async Task SucessfulAsync(RulesEngine rulesEngine)
        {
            PersonalEverydayOrderInput successfulInput = new PersonalEverydayOrderInput("123", true, true, false);
            List<RuleResultTree> resultSuccessful = await rulesEngine.ExecuteAllRulesAsync("Personal Everyday Order Workflow", successfulInput);

            _logger.LogInformation("Executing with successful input.");

            resultSuccessful.OnSuccess((eventName) =>
            {
                _logger.LogInformation("Multiplier {eventName}", eventName);
            });

            resultSuccessful.OnFail(() =>
            {
                _logger.LogInformation("Failed to achieve discount.");
            });
        }

        private async Task FailedUserIsNotVipAsync(RulesEngine rulesEngine)
        {
            PersonalEverydayOrderInput successfulInput = new PersonalEverydayOrderInput("123", false, true, false);
            List<RuleResultTree> resultSuccessful = await rulesEngine.ExecuteAllRulesAsync("Personal Everyday Order Workflow", successfulInput);

            _logger.LogInformation("Executing with not vip user.");

            resultSuccessful.OnSuccess((eventName) =>
            {
                _logger.LogInformation("Multiplier {eventName}", eventName);
            });

            resultSuccessful.OnFail(() =>
            {
                _logger.LogInformation("Failed to achieve discount.");
            });
        }

        private async Task FailedNotOnetimeOrderAsync(RulesEngine rulesEngine)
        {
            PersonalEverydayOrderInput successfulInput = new PersonalEverydayOrderInput("123", true, false, false);
            List<RuleResultTree> resultSuccessful = await rulesEngine.ExecuteAllRulesAsync("Personal Everyday Order Workflow", successfulInput);

            _logger.LogInformation("Executing with not onetime order.");

            resultSuccessful.OnSuccess((eventName) =>
            {
                _logger.LogInformation("Multiplier {eventName}", eventName);
            });

            resultSuccessful.OnFail(() =>
            {
                _logger.LogInformation("Failed to achieve discount.");
            });
        }

        private async Task FailedRefundedOrderAsync(RulesEngine rulesEngine)
        {
            PersonalEverydayOrderInput successfulInput = new PersonalEverydayOrderInput("123", true, true, true);
            List<RuleResultTree> resultSuccessful = await rulesEngine.ExecuteAllRulesAsync("Personal Everyday Order Workflow", successfulInput);

            _logger.LogInformation("Executing with refunded order.");

            resultSuccessful.OnSuccess((eventName) =>
            {
                _logger.LogInformation("Multiplier {eventName}", eventName);
            });

            resultSuccessful.OnFail(() =>
            {
                _logger.LogInformation("Failed to achieve discount.");
            });
        }

        private async Task FailedAllNestedRulesFailedAsync(RulesEngine rulesEngine)
        {
            PersonalEverydayOrderInput successfulInput = new PersonalEverydayOrderInput("123", false, false, true);
            List<RuleResultTree> resultSuccessful = await rulesEngine.ExecuteAllRulesAsync("Personal Everyday Order Workflow", successfulInput);

            _logger.LogInformation("Executing with all rules failed.");

            resultSuccessful.OnSuccess((eventName) =>
            {
                _logger.LogInformation("Multiplier {eventName}", eventName);
            });

            resultSuccessful.OnFail(() =>
            {
                _logger.LogInformation("Failed to achieve discount.");
            });
        }
    }
}