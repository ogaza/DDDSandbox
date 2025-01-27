using System;

namespace SOAExample.Model
{
  public class TicketReservation
  {
    public Guid Id { get; set; }
    public Event Event {  get; set; }
    public DateTime ExpiryTime { get; set; }
    public int TicketQuantity { get; set; }
    public bool HasBeenRedeemed { get; set; }

    public bool HasExpired() => DateTime.Now > ExpiryTime;

    public bool StillActive() => !HasBeenRedeemed && !HasExpired();
  }

  public class TicketPurchase 
  {
    public Guid Id { get; set; }
    public Event Event { get; set; }
    public int TicketQuantity { get; set; }
  }

  public static class TicketPurchaseFactory 
  {
    public static TicketPurchase CreateTicket(Event @event, int ticketQuantity)
    {
      return new TicketPurchase 
      {
        Id = Guid.NewGuid(),
        Event = @event,
        TicketQuantity = ticketQuantity
      };
    }
  }

  public static class TicketReservationFactory
  {
    public static TicketReservation CreateReservation(Event @event, int ticketQuantity)
    {
      return new TicketReservation
      {
        Id = Guid.NewGuid(),
        Event = @event,
        TicketQuantity = ticketQuantity,
        ExpiryTime = DateTime.Now.AddMinutes(1)
      };
    }
  }
}
