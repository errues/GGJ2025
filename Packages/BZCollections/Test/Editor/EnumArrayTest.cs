using NUnit.Framework;
using System.Collections.Generic;

namespace BZ.Core.Collections.Test
{
    public class EnumArrayTest
    {
        public enum TestEnum
        {
            FIRST_ELEMENT,
            SECOND_ELEMENT,
            THIRD_ELEMENT
        }

        // A Test behaves as an ordinary method
        [Test]
        public void TestEnumTestSimplePasses()
        {
            EnumArray<string, TestEnum> enumArray = new EnumArray<string, TestEnum>();

            enumArray[TestEnum.SECOND_ELEMENT] = TestEnum.SECOND_ELEMENT.ToString();
            enumArray[TestEnum.FIRST_ELEMENT] = TestEnum.FIRST_ELEMENT.ToString();
            enumArray[TestEnum.THIRD_ELEMENT] = TestEnum.THIRD_ELEMENT.ToString();

            for (int i = 0; i < enumArray.Count; i++)
            {
                Assert.AreEqual(enumArray[i], ((TestEnum)i).ToString());
            }
        }

        [Test]
        public void TestEnumTestContains()
        {
            EnumArray<string, TestEnum> enumArray = new EnumArray<string, TestEnum>();

            enumArray[TestEnum.SECOND_ELEMENT] = TestEnum.SECOND_ELEMENT.ToString();
            enumArray[TestEnum.FIRST_ELEMENT] = TestEnum.FIRST_ELEMENT.ToString();
            enumArray[TestEnum.THIRD_ELEMENT] = TestEnum.THIRD_ELEMENT.ToString();

            Assert.IsTrue(enumArray.Contains(TestEnum.FIRST_ELEMENT.ToString()));
        }

        [Test]
        public void TestEnumTestIndexOf()
        {
            EnumArray<string, TestEnum> enumArray = new EnumArray<string, TestEnum>();

            enumArray[TestEnum.SECOND_ELEMENT] = TestEnum.SECOND_ELEMENT.ToString();
            enumArray[TestEnum.FIRST_ELEMENT] = TestEnum.FIRST_ELEMENT.ToString();
            enumArray[TestEnum.THIRD_ELEMENT] = TestEnum.THIRD_ELEMENT.ToString();

            Assert.IsTrue(enumArray.IndexOf(TestEnum.FIRST_ELEMENT.ToString()) == (int)TestEnum.FIRST_ELEMENT);
            Assert.IsTrue(enumArray.IndexOf("OutValue") == -1);
        }

        [Test]
        public void TestEnumTestFind()
        {
            EnumArray<string, TestEnum> enumArray = new EnumArray<string, TestEnum>();

            enumArray[TestEnum.SECOND_ELEMENT] = TestEnum.SECOND_ELEMENT.ToString();
            enumArray[TestEnum.FIRST_ELEMENT] = TestEnum.FIRST_ELEMENT.ToString();
            enumArray[TestEnum.THIRD_ELEMENT] = TestEnum.THIRD_ELEMENT.ToString();

            Assert.IsTrue(enumArray.Find(TestEnum.THIRD_ELEMENT.ToString()) == TestEnum.THIRD_ELEMENT);
            Assert.Catch(typeof(KeyNotFoundException), () => enumArray.Find("OutValue"));
        }
    }
}
