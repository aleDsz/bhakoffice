using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BhakOffice.Core.Tests.Queues {
  [TestClass]
  public class ListenerTests {

    [TestCategory("Queues")]
    [TestMethod]
    public void TestSuccessGetMessages() {
      var listener = Core.Queues.Listener.GetNotificationsListener();

      listener.Start();

      Thread.Sleep(4000);

      var result = listener.GetMessages();

      Assert.IsTrue(result.IsSuccess());
    }
  }
}
