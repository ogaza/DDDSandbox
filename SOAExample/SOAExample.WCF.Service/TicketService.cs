using SOAExample.Model;
using SOAExample.Repository;
using SOAExample.WCF.DataContract;
using SOAExample.WCF.ServiceContract;
using System;
using System.ServiceModel.Activation;

namespace SOAExample.WCF.Service
{
  [AspNetCompatibilityRequirements(
    RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
  public class TicketService : ITicketService
  {
    public PurchaseTicketResponse PurchaseTicket(PurchaseTicketRequest request)
    {
      var response = new PurchaseTicketResponse { IsSuccess = false };

      try
      {
        // check for a duplicate transaction using the 
        // Idempotent pattern
        if (!_responseHistory.IsUniqueRequest(request.CorrelationId)) 
        {
          return _responseHistory.RetrievePreviousResponseFor(request.CorrelationId);
        }

        Event @event = _eventRepository.FindBy(new Guid(request.EventId));

        if(!@event.CanPurchaseTicketWith(new Guid(request.ReservationId)))
        {
          response.Message =
            @event.DetermineWhyTicketCannotbePurchasedWith(new Guid(request.ReservationId));

          return response;
        }

        TicketPurchase ticket = @event.PurchaseTicketWith(new Guid(request.ReservationId));

        response = ticket.ConvertTo();
        response.IsSuccess = true;
      }
      catch (Exception)
      {
        response.Message = "Unknown error.";
      }

      return response;
    }

    public ReserveTicketResponse ReserveTicket(ReserveTicketRequest request)
    {
      var response = new ReserveTicketResponse
      {
        IsSuccess = false
      };

      try
      {
        var @event = _eventRepository.FindBy(new Guid(request.EventId));

        if (@event.CanReserveTicket(request.NoOfTickets)) 
        {
          TicketReservation reservation = @event.ReserveTicket(request.NoOfTickets);

          _eventRepository.Save(@event);

          response = reservation.ConvertTo();
          response.IsSuccess = true;

          return response;
        }

        response.Message = $"There are no {request.NoOfTickets} available.";
      }
      catch (Exception)
      {
        response.Message = "Unexpected error";
      }

      return response;
    }

    public TicketService()
    {

      // instead of using Inversion of Control container to inject
      // the repository, we opt for a hard-coded impementation
      // just to keep the code simple

      _eventRepository = new EventRepository();
    }
    private IEventRepository _eventRepository;

    private static MessageResponseHistory<PurchaseTicketResponse> _responseHistory =
      new MessageResponseHistory<PurchaseTicketResponse>();
  }
}
