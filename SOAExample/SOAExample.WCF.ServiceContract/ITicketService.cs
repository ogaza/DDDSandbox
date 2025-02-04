using SOAExample.WCF.DataContract;
using System.ServiceModel;

namespace SOAExample.WCF.ServiceContract
{
  [ServiceContract]
  public interface ITicketService
  {
    [OperationContract()]
    ReserveTicketResponse ReserveTicket(ReserveTicketRequest request);

    [OperationContract()]
    PurchaseTicketResponse PurchaseTicket(PurchaseTicketRequest request);
  }
}
