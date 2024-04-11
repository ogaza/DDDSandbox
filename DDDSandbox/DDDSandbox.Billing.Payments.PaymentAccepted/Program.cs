// See https://aka.ms/new-console-template for more information

using NServiceBus;

Console.Title = "DDDSandbox.Billing.Payments.PaymentAccepted";
var endpointConfiguration = new EndpointConfiguration("DDDSandbox.Billing.Payments.PaymentAccepted");

endpointConfiguration.UseSerialization<SystemJsonSerializer>();
endpointConfiguration.UseTransport(new LearningTransport());

var endpointInstance = await Endpoint.Start(endpointConfiguration);
Console.WriteLine("Press any key to exit");
Console.ReadKey();
await endpointInstance.Stop();
