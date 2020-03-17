using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using BhakOffice.Core.Queues;
using BhakOffice.Services.Notifications.Queuers;
using BhakOffice.Types;

namespace BhakOffice.Services.Notifications {
  public partial class Application : ServiceBase {
    public Application() {
      InitializeComponent();
    }

    protected override void OnStart(string[] args) {
      var queuer = MSMQ.GetNotificationsQueuer();
      var result = queuer.Read();

      if (result.IsSuccess()) {
        var messages = (List<QueueMessage>)result.GetResult();

        foreach (var message in messages) {
          var thread = new Thread(() => Processor.Run(queuer, message));
          thread.Start();
        }
      } else {
        // Log error
      }
    }

    protected override void OnStop() {
      //
    }
  }
}
