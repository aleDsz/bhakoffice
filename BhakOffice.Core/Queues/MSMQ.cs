using System;
using System.Collections.Generic;
using System.Messaging;
using BhakOffice.Types;
using System.Linq;

namespace BhakOffice.Core.Queues {
  public class MSMQ {
    protected MessageQueue _queue = new MessageQueue();
    protected List<QueueMessage> _messages = new List<Types.QueueMessage>();
    protected readonly BinaryMessageFormatter _formatter = new BinaryMessageFormatter();

    private MSMQ(String queue_name) {
      this._queue = new MessageQueue(queue_name);
    }

    public static MSMQ GetNotificationsQueuer() {
      return new MSMQ(Global.NotificationsQueue);
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
          transaction.Abort();
          return new Response(Returns.Error, ex.Message);
        }
      }
    }

    public Response Read(int count = 10) {
      var messages = this._queue
                     .GetAllMessages()
                     .Take(count)
                     .ToArray();

      foreach (var msg in messages) {
        msg.Formatter = _formatter;
        var message = (QueueMessage) msg.Body;
        message.id = msg.Id;
        this._messages.Add(message);
      }

      if (this._messages.Count == 0) {
        return new Response(Returns.Error, "There's no message avaliable");
      } else {
        return new Response(Returns.OK, this._messages);
      }
    }

    public Response Acknowledge(String message_id) {
      using (var transaction = new MessageQueueTransaction()) {
        try {
          transaction.Begin();
          var message = this._queue.ReceiveById(message_id);
          message.Formatter = _formatter;
          transaction.Commit();

          return new Response(Returns.OK, "Acknowledged");
        }
        catch (Exception ex) {
          transaction.Abort();
          return new Response(Returns.Error, "Not acknowledged");
        }
      }
    }
  }
}
