using System;
using System.Collections.Generic;
using System.Reflection;
using System.IO;

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

                    // get filename
                    string codeBase = asm.CodeBase;
                    assemblyData.Add("Filename", codeBase); // call it filename or codebase?

                    // get filesize
                    Uri uri = new Uri(codeBase);
                    long bytesLength = new FileInfo(uri.LocalPath).Length;
                    long kbytesLength = bytesLength / 1024;
                    assemblyData.Add("Filesize", kbytesLength.ToString() + " KB");

                    // get general info (Assembly Name, Version, Culture & PublicKeyToken)
                    string[] generalInfoArray = asm.FullName.Split(new string[] {", "}, StringSplitOptions.None);
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

                    runningAssemblies.Add(assemblyData);
                }
            }

            return runningAssemblies;
        }

        private bool IsDynamic(Assembly asm)
        {
            return (asm is System.Reflection.Emit.AssemblyBuilder) ||
                asm.GetType().FullName == "System.Reflection.Emit.InternalAssemblyBuilder";
        }
    }

}