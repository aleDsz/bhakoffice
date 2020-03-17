using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BhakOffice.Core.Tests {
  [TestClass]
  public class SetupTests {

    [TestCategory("Migration")]
    [TestMethod]
    public void TestSucessInstall() {
      var result = BhakOffice.Core.Setup.Install();
      Assert.AreEqual(true, result.IsSuccess());
    }

    [TestCategory("Migration")]
    [TestMethod]
    public void TestFailInstall() {
      var result = BhakOffice.Core.Setup.Install(BhakOffice.Types.Returns.Error);
      Assert.AreEqual(false, result.IsSuccess());
    }
  }
}
