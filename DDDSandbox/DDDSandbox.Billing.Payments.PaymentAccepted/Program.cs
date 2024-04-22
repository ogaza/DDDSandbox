// See https://aka.ms/new-console-template for more information

using NServiceBus;

Console.Title = "DDDSandbox.Billing.Payments.PaymentAccepted";
var endpointConfiguration = new EndpointConfiguration("DDDSandbox.Billing.Payments.PaymentAccepted");

endpointConfiguration.UseSerialization<SystemJsonSerializer>();
endpointConfiguration.UseTransport(new LearningTransport());

#region fault tollerance
// https://github.com/Particular/docs.particular.net/blob/master/samples/faulttolerance/sample.md
var recoverability = endpointConfiguration.Recoverability();
recoverability.Immediate(settings =>
{
  settings.NumberOfRetries(0);
});
recoverability.Delayed(settings =>
{
  settings.NumberOfRetries(0);
});

#endregion

var endpointInstance = await Endpoint.Start(endpointConfiguration);
Console.WriteLine("Press any key to exit");
Console.ReadKey();
await endpointInstance.Stop();
