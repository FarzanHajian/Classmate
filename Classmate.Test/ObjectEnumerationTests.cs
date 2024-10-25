using Microsoft.VisualStudio.TestTools.UnitTesting;
using static FarzanHajian.Classmate.Classmate;

namespace Classmate.Test
{
    [TestClass]
    public class ObjectEnumerationTests
    {
        [TestMethod]
        public void Empty()
        {
            var result = Classes();
            Assert.AreEqual(result, "");
        }

        [TestMethod]
        public void Strings()
        {
            var result = Classes("Foo", "Bar", "", "Baz Mar", null, " Zab  Faz ", " ");
            Assert.AreEqual(result, "Foo Bar Baz Mar Zab  Faz");
        }

        [TestMethod]
        public void BooleanQueries()
        {
            bool trueValue = 12 % 2 == 0;
            bool falseValue = 12 % 2 == 1;
            var result = Classes(
                "Foo".If(true),
                "Bar".If(false),
                "Baz".If(trueValue || falseValue)
            );
            Assert.AreEqual(result, "Foo Baz");
        }

        [TestMethod]
        public void BooleanQueriesWithElseValues()
        {
            bool trueValue = 12 % 2 == 0;
            bool falseValue = 12 % 2 == 1;
            var result = Classes(
                "Foo".If(true, "Food"),
                "Bar".If(false, "Bad"),
                "Baz".If(trueValue || falseValue)
            );
            Assert.AreEqual(result, "Foo Bad Baz");
        }

        [TestMethod]
        public void LambdaQueries()
        {
            bool trueValue = 12 % 2 == 0;
            bool falseValue = 12 % 2 == 1;
            var result = Classes(
                "Foo".If(() => true),
                "Bar".If(() => false),
                "Baz".If(() => trueValue || falseValue)
            );
            Assert.AreEqual(result, "Foo Baz");
        }

        [TestMethod]
        public void LambdaQueriesWithElseValues()
        {
            bool trueValue = 12 % 2 == 0;
            bool falseValue = 12 % 2 == 1;
            var result = Classes(
                "Foo".If(() => true, "Food"),
                "Bar".If(() => false, "Bad"),
                "Baz".If(() => trueValue || falseValue)
            );
            Assert.AreEqual(result, "Foo Bad Baz");
        }

        [TestMethod]
        public void BooleanObjects()
        {
            bool trueValue = 12 % 2 == 0;
            bool falseValue = 12 % 2 == 1;
            var result = Classes(
                new { Foo = true },
                new { Bar = false, Baz = trueValue || falseValue, Gaz = true }
            );
            Assert.AreEqual(result, "Foo Baz Gaz");
        }

        [TestMethod]
        public void LambdaObjects()
        {
            int i = 12;
            var result = Classes(
                new { Foo = If(() => true) },
                new { Bar = If(() => false), Baz = If(() => i >= 10) }
            );
            Assert.AreEqual(result, "Foo Baz");
        }

        [TestMethod]
        public void Mixture()
        {
            int i = 12;
            bool trueValue = 12 % 2 == 0;
            bool falseValue = 12 % 2 == 1;
            var result = Classes(
                "Foo",
                "Faz",
                "Bar".If(() => i >= 20),
                "Gaz",
                new { Maz = true, Naz = If(() => i > 10), Laz = If(() => i == 8) },
                "Baz".If(trueValue || falseValue)
            );
            Assert.AreEqual(result, "Foo Faz Gaz Maz Naz Baz");
        }
    }
}