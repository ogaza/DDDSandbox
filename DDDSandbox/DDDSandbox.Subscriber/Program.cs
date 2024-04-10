// See https://aka.ms/new-console-template for more information

using NServiceBus;

Console.Title = "Subscriber";
var endpointConfiguration = new EndpointConfiguration("DDDSandbox.Subscriber");

endpointConfiguration.UseSerialization<SystemJsonSerializer>();
endpointConfiguration.UseTransport(new LearningTransport());

var endpointInstance = await Endpoint.Start(endpointConfiguration);
Console.WriteLine("Press any key to exit");
Console.ReadKey();
await endpointInstance.Stop();

// based on sample code from
// https://github.com/Particular/docs.particular.net/tree/9afc805de7deff29e877ae70057ce87cf98fdd3c/samples/pubsub/native/Core_8
