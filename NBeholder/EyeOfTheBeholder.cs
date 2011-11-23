using System;
using System.Collections.Generic;
using System.Reflection;
using System.IO;
using System.Text;

namespace NBeholder
{
    public class EyeOfTheBeholder
    {
        public IList<IDictionary<string, string>> RunningAssemblies()
        {
            IList<IDictionary<string, string>> runningAssemblies = new List<IDictionary<string, string>>();

            AppDomain ad = AppDomain.CurrentDomain;

            foreach (Assembly asm in ad.GetAssemblies())
            {
                if (!IsDynamic(asm))
                {
                    IDictionary<string, string> assemblyData = new Dictionary<string, string>();

                    // get general info (Assembly Name, Version, Culture & PublicKeyToken)
                    string[] generalInfoArray = asm.FullName.Split(new string[] { ", " }, StringSplitOptions.None);
                    assemblyData.Add("AssemblyName", generalInfoArray[0]);
                    assemblyData.Add("Version", generalInfoArray[1].Substring(
                        generalInfoArray[1].IndexOf("=") + 1
                        ));
                    assemblyData.Add("Culture", generalInfoArray[2].Substring(
                        generalInfoArray[2].IndexOf("=") + 1
                        ));
                    assemblyData.Add("PublicKeyToken", generalInfoArray[3].Substring(
                        generalInfoArray[3].IndexOf("=") + 1
                        ));

                    // get filename
                    string codeBase = asm.CodeBase;
                    assemblyData.Add("Filename", codeBase); // call it filename or codebase?

                    // get filesize
                    Uri uri = new Uri(codeBase);
                    long bytesLength = new FileInfo(uri.LocalPath).Length;
                    long kbytesLength = bytesLength / 1024;
                    assemblyData.Add("Filesize", kbytesLength.ToString() + " KB");


                    runningAssemblies.Add(assemblyData);
                }
            }

            return runningAssemblies;
        }

        public string RunningAssembliesAsXml()
        {
            // TODO: write this using XmlDocument instead of StringBuilder?

            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("<RunningAssemblies>");

            foreach (IDictionary<string, string> assemblyInfo in this.RunningAssemblies())
            {
                stringBuilder.AppendLine("\t<RunningAssembly>");

                foreach (KeyValuePair<string, string> assemblyInfoField in assemblyInfo)
                {
                    stringBuilder.AppendFormat("\t\t<{0}>{1}</{0}>\n", assemblyInfoField.Key, assemblyInfoField.Value);
                }

                stringBuilder.AppendLine("\t</RunningAssembly>");
            }

            stringBuilder.AppendLine("</RunningAssemblies>");

            return stringBuilder.ToString();
        }

        private bool IsDynamic(Assembly asm)
        {
            return (asm is System.Reflection.Emit.AssemblyBuilder) ||
                asm.GetType().FullName == "System.Reflection.Emit.InternalAssemblyBuilder";
        }
    }

}