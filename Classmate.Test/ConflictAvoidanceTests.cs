using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using static FarzanHajian.Classmate.Classmate;

namespace Classmate.Test
{
    [TestClass]
    public class ConflictAvoidanceTests
    {
        [TestMethod]
        public void StringStringString()
        {
            var result = Classes("Foo", "Bar", "Baz");
            Assert.AreEqual(result, "Foo Bar Baz");
        }

        [TestMethod]
        public void StringObjectObject()
        {
            int j = 3;
            var result = Classes(
                "Foo",
                new { Yes = true, No = If(() => j > 5), YesYes = If(() => j == 3) },
                new { NoWay = true, NoNo = If(() => j == 3) }
            );
            Assert.AreEqual(result, "Foo Yes YesYes NoWay NoNo");
        }

        [TestMethod]
        public void NullStringString()
        {
            Action action = new Action(() => Classes(null, "Bar", "Baz"));
            Assert.ThrowsException<ArgumentNullException>(action);
        }

        [TestMethod]
        public void NullStringStringNull()
        {
            var result = Classes(null, "Bar", "Baz", null);
            Assert.AreEqual(result, "Bar Baz");
        }

        [TestMethod]
        public void LambdaStringString()
        {
            int i = 10;
            var result = Classes(() => i >= 10, "Bar", "Baz");
            Assert.AreEqual(result, "Bar");
        }
    }
}