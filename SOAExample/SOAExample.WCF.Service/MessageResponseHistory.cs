using System.Collections.Generic;

namespace SOAExample.WCF.Service
{
  /// <summary>
  /// Simple in-memory implementation of the Idempotent Pattern.
  /// Holds the history of service responses associated with
  /// their correlation ids.
  /// </summary>
  /// <typeparam name="T"></typeparam>
  public class MessageResponseHistory<T>
  {
    public T RetrievePreviousResponseFor(string correlationId)
      => _responseHistory[correlationId];

    public void LogResponse(string correlationId, T response) 
    {
      if (_responseHistory.ContainsKey(correlationId)) 
      {
        _responseHistory[correlationId] = response;
        
        return;
      }

      _responseHistory.Add(correlationId, response);
    }

    public bool IsUniqueRequest(string correlationId)
      => _responseHistory.ContainsKey(correlationId);
    

    public MessageResponseHistory()
    {
      _responseHistory = new Dictionary<string, T>();
    }

    private Dictionary<string, T> _responseHistory;

  }
}
