using System.Collections.Generic;
using System.Linq;
using NBeholder;
using NUnit.Framework;

namespace NBeholderTests
{
    [TestFixture]
    public class EyeOfTheBeholderTests
    {
        [Test]
        public void EyeOfTheBeholder_ReturnsCorrectDataStructure()
        {
            EyeOfTheBeholder eye = new EyeOfTheBeholder();
            var runningAssemblies = eye.RunningAssemblies();
            Assert.IsInstanceOf(typeof(IList<IDictionary<string, string>>), runningAssemblies);
        }

        [Test]
        public void EyeOfTheBeholder_ShouldFindSystemDLL()
        {
            EyeOfTheBeholder eye = new EyeOfTheBeholder();
            var runningAssemblies = eye.RunningAssemblies();
            Assert.True(runningAssemblies.Any(x => x["AssemblyName"] == "System"));
        }

        [Test]
        public void EyeOfTheBeholder_ShouldFindNBeholderDLL()
        {
            EyeOfTheBeholder eye = new EyeOfTheBeholder();
            var runningAssemblies = eye.RunningAssemblies();
            Assert.True(runningAssemblies.Any(x => x["AssemblyName"] == "NBeholder"));
        }
    }
}
