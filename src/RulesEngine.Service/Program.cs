using RulesEngine.Service;
using RulesEngineTestProject.OrdersRefund;
using RulesEngineTestProject.OrdersType;
using RulesEngineTestProject.VipMembers;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddLogging();
        services.AddHostedService<Worker>();
        services.AddTransient<VipMemberChecker>();
        services.AddTransient<OrderRefundChecker>();
        services.AddTransient<OrderTypeChecker>();
    })
    .Build();

await host.RunAsync();
