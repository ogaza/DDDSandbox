﻿using DDDSandbox.Messages;
using NServiceBus;

namespace DDDSandbox.API.Handlers
{
  public class MyHandler :
    IHandleMessages<MyMessage>
  {
    //static ILog log = LogManager.GetLogger<MyHandler>();

    public Task Handle(MyMessage message, IMessageHandlerContext context)
    {
      //log.Info("Message received at endpoint");
      return Task.CompletedTask;
    }
  }
}
