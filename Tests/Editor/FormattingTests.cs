using System;
using NUnit.Framework;
using Unity.ProjectAuditor.Editor.Utils;

namespace Unity.ProjectAuditor.EditorTests
{
    class FormattingTests
    {
        [Test]
        [TestCase((ulong)16, "16 B")]
        [TestCase((ulong)512, "0.5 KB")]
        [TestCase((ulong)1024, "1.0 KB")]
        [TestCase((ulong)1024 * 1024, "1.0 MB")]
        [TestCase((ulong)1024 * 1024 * 1024, "1.00 GB")]
        public void Formatting_Size_IsFormatted(ulong asNumber, string asString)
        {
            Assert.AreEqual(asString, Formatting.FormatSize(asNumber));
        }

        [Test]
        public void Formatting_Duration_IsFormatted()
        {
            var time = new TimeSpan(10, 24, 30);
            const string formatted = "10:24:30";

            Assert.AreEqual(formatted, Formatting.FormatDuration(time));
        }

        [Test]
        public void Formatting_TimeNaN_IsFormatted()
        {
            Assert.AreEqual("NaN", Formatting.FormatTime(float.NaN));
        }

        [Test]
        public void Formatting_TimeInSeconds_IsFormatted()
        {
            var time = new TimeSpan(0, 0, 0, 6, 123);
            const string formatted = "6.12 s"; // truncated to 2 decimals

            Assert.AreEqual(formatted, Formatting.FormatTime(time));
        }

        [TestCase(0.12345f, "12.3 %")]
        [TestCase(0.5f, "50.0 %")]
        [TestCase(0.0f, "0.0 %")]
        [TestCase(1.0f, "100.0 %")]
        public void Formatting_Percentage_IsFormatted(float number, string expectedResult)
        {
            Assert.AreEqual(expectedResult, Formatting.FormatPercentage(number));
        }
    }
}
