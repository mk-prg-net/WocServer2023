using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SPADemo.Formater
{
    /// <summary>
    /// Formatiert Naming Container als JSON
    /// </summary>
    public class NamingContainerFormater
        : System.Net.Http.Formatting.BufferedMediaTypeFormatter
    {
        public NamingContainerFormater()
        {
            SupportedMediaTypes.Add(new System.Net.Http.Headers.MediaTypeHeaderValue("json/nc"));
        }

        public override bool CanReadType(Type type)
        {
            return false;
        }

        public override bool CanWriteType(Type type)
        {
            var ret = type == typeof(MKPRG.Naming.INaming);
            return ret;
        }

        public override void WriteToStream(Type type, object value, System.IO.Stream writeStream, System.Net.Http.HttpContent content)
        {
            var nc = (MKPRG.Naming.INaming)value;

            var ncJson = Newtonsoft.Json.JsonConvert.SerializeObject(nc);            

            var writer = new System.IO.StreamWriter(writeStream);
            writer.Write(ncJson);
            writer.Flush();
        }
    }

}
