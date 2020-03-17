using Microsoft.VisualStudio.TestTools.UnitTesting;
using BhakOffice.Core.Queues;
using BhakOffice.Types;
using System.Collections;
using System.Linq;
using System.Collections.Generic;

namespace BhakOffice.Core.Tests.Queues {
  [TestClass]
  public class MSMQTests {

    [TestCategory("Queues")]
    [TestMethod]
    public void TestReadMessages() {
      var queuer = MSMQ.GetNotificationsQueuer();
      var status = queuer.Send("test", new object());

      switch (status) {
        case Response ok when ok.type == Types.Returns.OK:
          var result = queuer.Read();
          Assert.IsTrue(result.IsSuccess());
          break;

        case Response error when error.type == Types.Returns.Error:
          Assert.IsTrue(false);
          break;

        default:
          Assert.IsTrue(false);
          break;
      }
    }

    [TestCategory("Queues")]
    [TestMethod]
    public void TestSendMessage() {
      var queuer = MSMQ.GetNotificationsQueuer();
      var result = queuer.Send("test", new ArrayList());

      Assert.IsTrue(result.IsSuccess());
    }

    [TestCategory("Queues")]
    [TestMethod]
    public void TestAcknowledgeMessage() {
      var queuer = MSMQ.GetNotificationsQueuer();
      var result = queuer.Read(1);
      var message = (result.GetResult() as List<QueueMessage>).FirstOrDefault();

      var ack = queuer.Acknowledge(message.id);

      Assert.IsTrue(ack.IsSuccess());
    }
  }
}
