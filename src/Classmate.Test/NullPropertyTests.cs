using Microsoft.VisualStudio.TestTools.UnitTesting;
using static FarzanHajian.Classmate.Classmate;

namespace Classmate.Test
{
    public class NullPropertyTests
    {
        public class NullPropertyTest
        {
            public bool? Foo { get; set; }

            public bool Bar { get; set; }

            public bool Naz { get; set; }
        }

        [TestMethod]
        public void NullProperty()
        {
            var result = Classes(new NullPropertyTest { Foo = null, Bar = true, Naz = false });
            Assert.AreEqual(result, "Bar");
        }
    }
}