using NUnit.Framework;

namespace BZ.Core.Collections.Test
{
    public class BlackboardTest
    {
        // A Test behaves as an ordinary method
        [Test]
        public void BlackboardTestSimplePasses()
        {
            IBlackBoard bb = BlackBoard.CreateBlackBoard();

            bb.Write<string>("my string", "my value");
            bb.Write<int>("my int", 1);
            bb.Write<int>("delete", 1);
            bb.Write<CustomObject>("my custom object", new CustomObject() { myData = "my data" });

            var myString = bb.Read<string>("my string");
            var myInt = bb.Read<int>("my int");
            var myCustom = bb.Read<CustomObject>("my custom object");
            bb.Delete("delete");

            Assert.AreEqual("my value", myString);
            Assert.AreEqual(1, myInt);
            Assert.AreEqual("my data", myCustom.myData);
            Assert.IsFalse(bb.ContainsParameter("delete"));
        }
    }
}

public class CustomObject
{
    public string myData;
}
