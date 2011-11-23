using System;
using System.Data;
using System.IO;
using NBeholder;

namespace NBeholderWeb
{
    public partial class NBeholder : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            EyeOfTheBeholder eye = new EyeOfTheBeholder();

            DataSet eyeAnswerXml = new DataSet();
            eyeAnswerXml.ReadXml(new StringReader(eye.RunningAssembliesAsXml()));

            assembliesRepeater.DataSource = eyeAnswerXml;
            assembliesRepeater.DataBind();
        }
    }
}