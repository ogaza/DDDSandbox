using SOAExample.Model;
using SOAExample.WCF.DataContract;

namespace SOAExample.WCF.Service
{
  public static class TicketPurchaseExtensions
  {
    public static PurchaseTicketResponse ConvertTo(this TicketPurchase ticketPurchase) 
    {
      return new PurchaseTicketResponse
      {
        TicketId = ticketPurchase.Id.ToString(),
        EventName = ticketPurchase.Event.Name,
        EventId = ticketPurchase.Event.Id.ToString(),
        NoOfTickets = ticketPurchase.TicketQuantity
      };
    }
  }

  public static class TicketReservationExtensions 
  {
    public static ReserveTicketResponse ConvertTo(this TicketReservation ticketReservation)
    {
      return new ReserveTicketResponse
      {
        ReservationNumber = ticketReservation.Id.ToString(),
        ExpirationDate = ticketReservation.ExpiryTime,
        EventName = ticketReservation.Event.Name,
        EventId = ticketReservation.Event.Id.ToString(),
        NoOfTickets = ticketReservation.TicketQuantity,
      };
    }
  }
}
