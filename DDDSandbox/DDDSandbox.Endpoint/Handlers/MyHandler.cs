using DDDSandbox.Messages;
using NServiceBus;

namespace DDDSandbox.Endpoint.Handlers
{
    public class MyHandler :
      IHandleMessages<MyMessage>
    {
        //static ILog log = LogManager.GetLogger<MyHandler>();

        public Task Handle(MyMessage message, IMessageHandlerContext context)
        {
            //await context.Reply(message);
            //log.Info("Message received at endpoint");
            return Task.CompletedTask;
        }
    }
}
