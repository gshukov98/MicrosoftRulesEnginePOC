using RulesEngine.Service;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddLogging();
        services.AddHostedService<Worker>();
    })
    .Build();

await host.RunAsync();
