using RulesEngine.Service;
using RulesEngineTestProject;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddLogging();
        services.AddHostedService<Worker>();
        services.AddTransient<VipMemberChecker>();
    })
    .Build();

await host.RunAsync();
