using System;

namespace SOAExample.Model
{
  public interface IEventRepository 
  {
    Event FindBy(Guid id);
    void Save(Event @event);
  }
}