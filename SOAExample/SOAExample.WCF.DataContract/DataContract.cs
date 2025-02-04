using System;
using System.Runtime.Serialization;

namespace SOAExample.WCF.DataContract
{
  [DataContract]
  public abstract class Response
  {
    [DataMember]
    public bool IsSuccess { get; set; }

    [DataMember]
    public string Message { get; set; }
  }

  public class PurchaseTicketResponse : Response
  {
    [DataMember]
    public string TicketId { get; set; }

    [DataMember]
    public string EventName { get; set; }

    [DataMember]
    public string EventId { get; set; }

    [DataMember]
    public int NoOfTickets { get; set; }
  }

  public class ReserveTicketResponse : Response
  {
    [DataMember]
    public string ReservationNumber { get; set; }

    [DataMember]
    public DateTime ExpirationDate { get; set; }

    [DataMember]
    public string EventName { get; set; }

    [DataMember]
    public string EventId { get; set; }

    [DataMember]
    public int NoOfTickets { get; set; }
  }

  [DataContract]
  public class PurchaseTicketRequest
  {
    [DataMember]
    public string CorrelationId { get; set; }
    [DataMember]
    public string ReservationId { get; set; }
    [DataMember]
    public string EventId { get; set; }
  }

  [DataContract]
  public class ReserveTicketRequest
  {
    [DataMember]
    public string EventId { get; set; }

    [DataMember]
    public int NoOfTickets { get; set; }
  }
}
