using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SOAExample.Model
{
  public class Event
  {
    public Event()
    {
      ReservedTickets = new List<TicketReservation>();
      PurchasedTickets = new List<TicketPurchase>();
    }

    public Guid Id { get; set; }
    public string Name { get; set; }
    public int Allocation { get; set; }
    public List<TicketReservation> ReservedTickets { get; set; }
    public List<TicketPurchase> PurchasedTickets { get; set; }

    public TicketReservation ReserveTicke(int quantity) 
    {
      if (!CanReserveTicket(quantity)) 
      {
        ThrowExceptionWhenTicketConnotBeReserved();
      }

      TicketReservation reservation = 
        TicketReservationFactory.CreateReservation(this, quantity);

      ReservedTickets.Add(reservation);

      return reservation;
    }

    public bool CanReserveTicket(int quantity) 
    {
      return AvailableAllocation() >= quantity;
    }

    public int AvailableAllocation() 
    {
      int salesAndReservations = 0;

      PurchasedTickets.ForEach(t => salesAndReservations += t.TicketQuantity);
      ReservedTickets.ForEach(t => salesAndReservations += t.TicketQuantity);

      return Allocation - salesAndReservations;
    }

    public bool CanPurchaseTicketWith(Guid reservationId) 
    {
      return HasReservationWith(reservationId) && GetReservationWith(reservationId).StillActive();
    }

    public TicketReservation GetReservationWith(Guid reservationId)
    {
      return ReservedTickets.FirstOrDefault(t => t.Id == reservationId) 
        ?? throw new ApplicationException($"no reservation with the id {reservationId}");

      //if (!HasReservationWith(reservationId)) 
      //{
      //  throw new ApplicationException($"no reservation with the id {reservationId}");
      //}
    }

    public string DetermineWhyTicketCannotbePurchasedWith(Guid reservationId)
    {
      var issues = new StringBuilder();

      if(!HasReservationWith(reservationId))
      {
        issues.AppendLine($"No ticket reservation with the id {reservationId}");
        
        return issues.ToString();
      }

      TicketReservation ticket = GetReservationWith(reservationId);

      if (ticket.HasExpired())
      {
        issues.AppendLine("Reservation has expired");
      }

      if (ticket.HasBeenRedeemed)
      {
        issues.AppendLine("Reservation has been redeemed");
      }

      return issues.ToString();
    }

    private void ThrowExceptionWhenTicketConnotBeReserved() 
    {
      throw new ApplicationException("No tickets left for reservation");
    }

    private bool HasReservationWith(Guid reservationId)
    {
      return ReservedTickets.Exists(t=> t.Id == reservationId);
    }
  }
}