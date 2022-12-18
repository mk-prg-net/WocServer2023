using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKPRG.Naming.TechTerms.Net.Http
{
    public class Http
        : NamingBase
    {
        public const long UID = 0xF3F800EE;

        public Http()
            : base(UID)
        {
        }

        public override string CNT => "http";
        public override string CN => "HTTP协议";
        public override string DE => "HTTP Protokoll";
        public override string EN => "HTTP protocol";
        public override string ES => "Protocolo HTTP";
    }

    public class HttpRequest
        : NamingBase
    {
        public const long UID = 0x31B2F298;

        public HttpRequest()
            : base(UID)
        {
        }

        public override string CNT => "httpRequest";
        public override string CN => "http请求";
        public override string DE => EN;
        public override string EN => "HTTP Request";
        public override string ES => EN;
    }

    public class HttpResponse
        : NamingBase
    {
        public const long UID = 0xB3653546;

        public HttpResponse()
            : base(UID)
        {
        }

        public override string CNT => "httpResponse";
        public override string CN => "http响应";
        public override string DE => EN;
        public override string EN => "HTTP Response";
        public override string ES => EN;
    }

    public class HttpVerb
        : NamingBase
    {
        public const long UID = 0xB35D413C;

        public HttpVerb()
            : base(UID)
        {
        }

        public override string CNT => "httpVerb";
        public override string CN => "HTTP动词";
        public override string DE => "HTTP Verb";
        public override string EN => "HTTP Verb";
        public override string ES => "Verbo HTTP";
    }

    public class StatusCode
    : NamingBase
    {
        public const long UID = 0x9C00A906;

        public StatusCode()
            : base(UID)
        {
        }

        public override string CNT => "httpStatusCode";
        public override string CN => "HTTP状态";
        public override string DE => "HTTP Status";
        public override string EN => "HTTP Status";
        public override string ES => "Estado HTTP ";
    }


    public class Get
        : NamingBase
    {
        public const long UID = 0x9807F92B;

        public Get()
            : base(UID)
        {
        }

        public override string CNT => "httpGet";
        public override string CN => EN;
        public override string DE => EN;
        public override string EN => "HTTP GET";
        public override string ES => EN;
    }

    public class Head
        : NamingBase
    {
        public const long UID = 0x77DD5BB4;

        public Head()
            : base(UID)
        {
        }

        public override string CNT => "httpHead";
        public override string CN => EN;
        public override string DE => EN;
        public override string EN => "HTTP HEAD";
        public override string ES => EN;
    }


    public class Post
        : NamingBase
    {
        public const long UID = 0xA995A4B1;

        public Post()
            : base(UID)
        {
        }

        public override string CNT => "httpPost";
        public override string CN => EN;
        public override string DE => EN;
        public override string EN => "HTTP POST";
        public override string ES => EN;
    }

    public class Put
        : NamingBase
    {
        public const long UID = 0x96E8F6A8;

        public Put()
            : base(UID)
        {
        }

        public override string CNT => "httpPut";
        public override string CN => EN;
        public override string DE => EN;
        public override string EN => "HTTP PUT";
        public override string ES => EN;
    }


    public class Delete
    : NamingBase
    {
        public const long UID = 0x200064FE;

        public Delete()
            : base(UID)
        {
        }

        public override string CNT => "httpDelete";
        public override string CN => EN;
        public override string DE => EN;
        public override string EN => "HTTP DELETE";
        public override string ES => EN;
    }

    public class Options
        : NamingBase
    {
        public const long UID = 0x1B939AC9;

        public Options()
            : base(UID)
        {
        }

        public override string CNT => "httpOptions";
        public override string CN => EN;
        public override string DE => EN;
        public override string EN => "HTTP OPTIONS";
        public override string ES => EN;
    }
}
