using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SPADemo.Formater
{
    /// <summary>
    /// mko, 9.2.2023
    /// </summary>
    public class HtmlWebApiFormater 
        : System.Net.Http.Formatting.BufferedMediaTypeFormatter
    {
        public HtmlWebApiFormater()
        {
            SupportedMediaTypes.Add(new System.Net.Http.Headers.MediaTypeHeaderValue("text/html"));
        }

        public override bool CanReadType(Type type)
        {
            return false;
        }

        public override bool CanWriteType(Type type)
        {
            var ret = type == typeof(Models.HtmlContainer);
            return ret;
        }

        public override void WriteToStream(Type type, object value, System.IO.Stream writeStream, System.Net.Http.HttpContent content)
        {
            var html = (Models.HtmlContainer)value;

            var writer = new System.IO.StreamWriter(writeStream);
            writer.Write(html.Html);
            writer.Flush();

        }
    }
}