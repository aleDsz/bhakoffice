using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BhakOffice.Core.Queues;
using BhakOffice.Types;

namespace BhakOffice.Services.Notifications.Queuers {
  public class Processor {
    public static void Run(MSMQ queuer, QueueMessage message) {
      if (message.topic == "product_created") {
        queuer.Acknowledge(message.id);
      }
    }
  }
}
