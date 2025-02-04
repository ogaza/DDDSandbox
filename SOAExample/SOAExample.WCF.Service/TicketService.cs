using SOAExample.WCF.DataContract;
using SOAExample.WCF.ServiceContract;

namespace SOAExample.WCF.Service
{
  public class TicketService : ITicketService
  {
    public PurchaseTicketResponse PurchaseTicket(PurchaseTicketRequest request)
    {
      return new PurchaseTicketResponse();
    }

    public ReserveTicketResponse ReserveTicket(ReserveTicketRequest request)
    {
      return new ReserveTicketResponse();
    }
  }
}
