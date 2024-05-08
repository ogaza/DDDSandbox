using DDDSandbox.Messages;
using NServiceBus;

namespace DDDSandbox.Endpoint.Handlers
{
  public class MyHandler :
    IHandleMessages<MyMessage>
  {
    public Task Handle(MyMessage message, IMessageHandlerContext context)
    {
      return Task.CompletedTask;
    }
  }
}
