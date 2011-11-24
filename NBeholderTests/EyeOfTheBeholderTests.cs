using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;
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

        [Test]
        public void EyeOfTheBeholder_GenerateXml_ShouldReadGeneratedXML()
        {
            EyeOfTheBeholder eye = new EyeOfTheBeholder();
            XDocument eyeXml = XDocument.Parse(eye.RunningAssembliesAsXml());

            var queryResult =
                eyeXml.Elements("RunningAssemblies").Elements("RunningAssembly").Single(r => r.Element("AssemblyName").Value == "NBeholder");

            Assert.AreEqual(queryResult.Element("AssemblyName").Value, "NBeholder");
        }

        [Test]
        public void EyeOfTheBeholder_ShouldFindSystemDataDll()
        {
            Assembly.Load("System.Data, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089");
            EyeOfTheBeholder eye = new EyeOfTheBeholder();
            var runningAssemblies = eye.RunningAssemblies();
            Assert.True(runningAssemblies.Any(x => x["AssemblyName"].Equals("System.Data")));
        }
    }
}
