﻿using System;
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
        private EyeOfTheBeholder eye;

        [SetUp]
        public void Setup()
        {
            eye = new EyeOfTheBeholder();    
        }

        [Test]
        public void EyeOfTheBeholder_ReturnsCorrectDataStructure()
        {
            var runningAssemblies = eye.RunningAssemblies();
            Assert.IsInstanceOf(typeof(IList<IDictionary<string, string>>), runningAssemblies);
        }

        [Test]
        public void EyeOfTheBeholder_ShouldFindSystemDLL()
        {
            var runningAssemblies = eye.RunningAssemblies();
            Assert.True(runningAssemblies.Any(x => x["AssemblyName"] == "System"));
        }

        [Test]
        public void EyeOfTheBeholder_ShouldFindNBeholderDLL()
        {
            var runningAssemblies = eye.RunningAssemblies();
            Assert.True(runningAssemblies.Any(x => x["AssemblyName"] == "NBeholder"));
        }

        [Test]
        public void EyeOfTheBeholder_GenerateXml_ShouldReadGeneratedXML()
        {
            XDocument eyeXml = XDocument.Parse(eye.RunningAssembliesAsXml());

            var queryResult =
                eyeXml.Elements("RunningAssemblies").Elements("RunningAssembly").Single(r => r.Element("AssemblyName").Value == "NBeholder");

            Assert.AreEqual(queryResult.Element("AssemblyName").Value, "NBeholder");
        }

        [Test]
        public void EyeOfTheBeholder_ShouldFindSystemDataDll()
        {
            Assembly.Load("System.Data, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089");
            var runningAssemblies = eye.RunningAssemblies();
            Assert.True(runningAssemblies.Any(x => x["AssemblyName"].Equals("System.Data")));
        }
    }
}
