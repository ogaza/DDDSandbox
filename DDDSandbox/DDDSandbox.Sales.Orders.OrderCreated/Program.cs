// See https://aka.ms/new-console-template for more information
using NServiceBus;

Console.Title = "DDDSandbox.Sales.Orders.OrderCreated";
var endpointConfiguration = new EndpointConfiguration("DDDSandbox.Sales.Orders.OrderCreated");
endpointConfiguration.UseTransport(new LearningTransport());
endpointConfiguration.UseSerialization<SystemJsonSerializer>();

var endpointInstance = await Endpoint.Start(endpointConfiguration);
Console.WriteLine("Press any key to exit");
Console.ReadKey();
await endpointInstance.Stop();

// based on sample code from
// https://github.com/Particular/docs.particular.net/tree/9afc805de7deff29e877ae70057ce87cf98fdd3c/samples/pubsub/native/Core_8

// probably instead of running as a console app
// wouold be better to create a nservicebus host
// https://github.com/Particular/docs.particular.net/tree/master/samples/hosting/generic-host/Core_8