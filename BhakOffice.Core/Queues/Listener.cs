using System;
using System.Collections.Generic;
using System.Messaging;
using BhakOffice.Types;

namespace BhakOffice.Core.Queues {
  public class Listener {
    protected MessageQueue _queue = new MessageQueue();
    protected List<QueueMessage> _messages = new List<Types.QueueMessage>();

    private Listener(String queue_name) {
      this._queue = new MessageQueue(queue_name);
      this._queue.Formatter = new XmlMessageFormatter(new Type[] {
        typeof(QueueMessage)
      });
    }

    public static Listener GetNotificationsListener() {
      return new Listener(Global.NotificationsQueue);
    }

    public void Start() {
      this._queue.PeekCompleted += this.AfterRetrieveMessages;
    }

    public Response GetMessages() {
      if (this._messages.Count == 0) {
        return new Response(Returns.Error, "There's no message avaliable");
      } else {
        return new Response(Returns.OK, this._messages);
      }
    }

    public Response Acknowledge(String message_id) {
      var message = this._queue.ReceiveById(message_id, MessageQueueTransactionType.Single);
      
      if (message.Body is Types.QueueMessage) {
        return new Response(Returns.OK, "Acknowledged");
      } else {
        return new Response(Returns.Error, "Not acknowledged");
      }
    }

    private void AfterRetrieveMessages(Object sender, PeekCompletedEventArgs e) {
      var message = this._queue.EndPeek(e.AsyncResult);

      var body = (QueueMessage)message.Body;
      body.id = message.Id;

      this._messages.Add(body);
      this._queue.BeginPeek();
    }
  }
}
