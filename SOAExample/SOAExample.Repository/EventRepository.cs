using SOAExample.Model;
using System;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace SOAExample.Repository
{
  public class EventRepository : IEventRepository
  {
    public Event FindBy(Guid id)
    {
      Event? @event = default;

      string queryString = @"
        SELECT * FROM dbo.Events WHERE Id = @eventId;
        SELECT * FROM dbo.PurchasedTickets WHERE EventId = @eventId;
        SELECT * FROM dbo.ReservedTickets WHERE EventId = @eventId;
      ";

      using (var connection = 
        new SqlConnection(_connectionString)) 
      {
        SqlCommand command = connection.CreateCommand();
        command.CommandText = queryString;

        var parameter = 
          new SqlParameter("@eventId", id.ToString());

        command.Parameters.Add(parameter);

        using var reader = command.ExecuteReader();
        if (reader.HasRows)
        {
          reader.Read();

          @event = new Event
          {
            Id = new Guid(reader["Id"].ToString()),
            Name = reader["Name"].ToString(),
            Allocation = int.Parse(reader["Allocation"].ToString()),
            ReservedTickets = new List<TicketReservation>(),
            PurchasedTickets = new List<TicketPurchase>()
          };
        }

        if (reader.NextResult())
        {
          while (reader.Read()) 
          {
            TicketPurchase purchase = new TicketPurchase
            {
              Id = new Guid(reader["Id"].ToString()),
              TicketQuantity = 
                int.Parse(reader["TicketQuantity"].ToString()),
              Event = @event
            };

            @event.PurchasedTickets.Add(purchase);
          }
        }

        if (reader.NextResult())
        {
          while (reader.Read())
          {
            TicketReservation reservation = new TicketReservation
            {
              Id = new Guid(reader["Id"].ToString()),
              ExpiryTime = 
                DateTime.Parse(reader["ExpiryTime"].ToString()),
              TicketQuantity =
                int.Parse(reader["TicketQuantity"].ToString()),
              HasBeenRedeemed =
                bool.Parse(reader["HasBeenRedeemed"].ToString()),
              Event = @event
            };

            @event.ReservedTickets.Add(reservation);
          }
        }
      }

      return @event;
    }

    public void Save(Event @event)
    {
      // saving the Event entity skipped
      // as it is not required in this example

      RemovePurchasedAndReservedTicketsFrom(@event);
      InsertPurchasedTicketsFrom(@event);
      InsertReservedTicketsFrom(@event);
    }
    public void RemovePurchasedAndReservedTicketsFrom(Event @event)
    {
      string sql = @"
        DELETE PurchasedTickets WHERE EventId = @eventId;
        DELETE ReservedTickets WHERE EventId = @eventId;
      ";

      using SqlConnection connection = new SqlConnection(_connectionString);
      
      SqlCommand command = connection.CreateCommand();
      command.CommandText = sql;

      SqlParameter sqlParameter =
        new SqlParameter("@eventId", @event.Id.ToString());
      command.Parameters.Add(sqlParameter);

      connection.Open();
      command.ExecuteNonQuery();
    }
    public void InsertPurchasedTicketsFrom(Event @event)
    {
      string sql = @"
        INSERT INTO PurchasedTickets (Id, EventId, TicketQuantity)
        VALUES (@Id, @EventId, @TicketQuantity)
      ";

      foreach (TicketPurchase purchase in @event.PurchasedTickets)
      {
        using SqlConnection connection = new SqlConnection(_connectionString);

        SqlCommand cmd = connection.CreateCommand();
        cmd.CommandText = sql;

        cmd.Parameters.Add(new SqlParameter("@Id", purchase.Id.ToString()));
        cmd.Parameters.Add(
          new SqlParameter("@EventId", purchase.Event.Id.ToString()));
        cmd.Parameters.Add(
          new SqlParameter("@TicketQuantity", purchase.TicketQuantity));

        connection.Open();
        cmd.ExecuteNonQuery();
      }
    }
    public void InsertReservedTicketsFrom(Event @event)
    {
      string sql = @"
        INSERT INTO 
          ReservedTickets 
          (Id, EventId, TicketQuantity, ExpiryTime, HasBeenRedeemed)
        VALUES 
          (@Id, @EventId, @TicketQuantity, @ExpiryTime, @HasBeenRedeemed)
      ";

      foreach (TicketReservation reservation in @event.ReservedTickets) 
      {
        using SqlConnection connection = new SqlConnection(_connectionString);
        
        SqlCommand cmd = connection.CreateCommand();
        cmd.CommandText = sql;

        cmd.Parameters.Add(new SqlParameter("@Id", reservation.Id.ToString()));
        cmd.Parameters.Add(
          new SqlParameter("@EventId", reservation.Event.Id.ToString()));
        cmd.Parameters.Add(
          new SqlParameter("@TicketQuantity", reservation.TicketQuantity));
        cmd.Parameters.Add(
          new SqlParameter("@ExpiryTime", reservation.ExpiryTime));
        cmd.Parameters.Add(
          new SqlParameter("@HasBeenRedeemed", reservation.HasBeenRedeemed));

        connection.Open();
        cmd.ExecuteNonQuery();
      }
    }

    private string _connectionString;
  }
}
