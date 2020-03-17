using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace BhakOffice.Services.Notifications {
  static class Program {
    /// <summary>
    /// The main entry point for the application.
    /// </summary>
    static void Main() {
      var services = new ServiceBase[] { new Application() };
      ServiceBase.Run(services);
    }
  }
}
