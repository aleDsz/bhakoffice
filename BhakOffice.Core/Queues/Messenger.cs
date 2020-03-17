using System;
using System.Collections.Generic;
using System.Messaging;
using BhakOffice.Types;

namespace BhakOffice.Core.Queues {
  public class Messenger {
    protected MessageQueue _queue = new MessageQueue();
    private static readonly BinaryMessageFormatter _formatter = new BinaryMessageFormatter();

    private Messenger(String queue_name) {
      this._queue = new MessageQueue(queue_name);
    }

    public static Messenger GetNotificationsMessenger() {
      return new Messenger(Global.NotificationsQueue);
    }

    public Response Send(String topic, Object payload) {
      using (var transaction = new MessageQueueTransaction()) {
        try {
          transaction.Begin();
          var message = new QueueMessage() { topic = topic, payload = payload };
          this._queue.Send(new Message(message, _formatter), topic, MessageQueueTransactionType.Single);
          transaction.Commit();

          return new Response(Returns.OK, message);
        }
        catch (Exception ex) {
          return new Response(Returns.Error, ex.Message);
        }
      }
    }
  }
}
