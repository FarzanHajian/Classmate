using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using static FarzanHajian.Classmate.Classmate;

namespace Classmate.Test
{
    [TestClass]
    public class ExclusiveTests
    {
        [TestMethod]
        public void String()
        {
            int i = 12;
            var result = Classes(i % 2 == 0, "Yes", "No");
            Assert.AreEqual(result, "Yes");
        }

        [TestMethod]
        public void ObjectsYes()
        {
            int i = 12;
            int j = 3;
            var result = Classes(
                i % 2 == 0,
                new { Yes = true, No = If(() => j > 5), YesYes = If(() => j == 3) },
                new { NoWay = true, NoNo = If(() => j == 3) }
            );
            Assert.AreEqual(result, "Yes YesYes");
        }

        [TestMethod]
        public void ObjectsNo()
        {
            int i = 12;
            int j = 3;
            var result = Classes(
                i % 5 == 0,
                new { Yes = true, No = If(() => j > 5), YesYes = If(() => j == 3) },
                new { NoWay = true, NoNo = If(() => j == 3) }
            );
            Assert.AreEqual(result, "NoWay NoNo");
        }

        [TestMethod]
        public void ObjectsWithNull()
        {
            int i = 12;
            int j = 3;
            var result = Classes(
                i % 2 == 0,
                null,
                new { Noway = true, NoNo = If(() => j == 3) }
            );
            Assert.AreEqual(result, "");
        }

        [TestMethod]
        public void StringsWithLamdaPredicate()
        {
            int i = 12;
            var result = Classes(() => i % 2 == 0, "Yes", "No");
            Assert.AreEqual(result, "Yes");
        }

        [TestMethod]
        public void ObjectsYesWithLamdaPredicate()
        {
            int i = 12;
            int j = 3;
            var result = Classes(
                () => i % 2 == 0,
                new { Yes = true, No = If(() => j > 5), YesYes = If(() => j == 3) },
                new { NoWay = true, NoNo = If(() => j == 3) }
            );
            Assert.AreEqual(result, "Yes YesYes");
        }

        [TestMethod]
        public void ObjectsNoWithLamdaPredicate()
        {
            int i = 12;
            int j = 3;
            var result = Classes(
                () => i % 5 == 0,
                new { Yes = true, No = If(() => j > 5), YesYes = If(() => j == 3) },
                new { NoWay = true, NoNo = If(() => j == 3) }
            );
            Assert.AreEqual(result, "NoWay NoNo");
        }

        [TestMethod]
        public void ObjectsWithNullWithLamdaPredicate()
        {
            int i = 12;
            int j = 3;
            var result = Classes(
                () => i % 2 == 0,
                null,
                new { Noway = true, NoNo = If(() => j == 3) }
            );
            Assert.AreEqual(result, "");
        }

        [TestMethod]
        public void NullPredicate()
        {
            Action action = new Action(() => Classes(null, "Yes", "No"));
            Assert.ThrowsException<ArgumentNullException>(action);
        }
    }
}