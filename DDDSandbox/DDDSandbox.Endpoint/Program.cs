// See https://aka.ms/new-console-template for more information
using NServiceBus;

Console.WriteLine("Hello, World!");

Console.Title = "DDDSandbox.Endpoint";
var endpointConfiguration = new EndpointConfiguration("DDDSandbox.Endpoint");
//endpointConfiguration.UsePersistence<LearningPersistence>();
endpointConfiguration.UseTransport(new LearningTransport());
endpointConfiguration.UseSerialization<SystemJsonSerializer>();

var endpointInstance = await Endpoint.Start(endpointConfiguration);
Console.WriteLine("Press any key to exit");
Console.ReadKey();
await endpointInstance.Stop();
