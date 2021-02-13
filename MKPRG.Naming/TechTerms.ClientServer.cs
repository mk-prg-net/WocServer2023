using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKPRG.Naming.TechTerms.ClientServer
{
    /// <summary>
    /// mko, 4.8.2020
    /// </summary>
    public class Client
     : NamingBase
    {

        public const long UID = 0x75D91483;

        public Client()
            : base(UID)
        {
        }


        public override string CNT => "Client";
        public override string CN => CNT;
        public override string DE => CNT;
        public override string EN => CNT;
        public override string ES => CNT;
    }

    /// <summary>
    /// mko, 4.8.2020
    /// </summary>
    public class Server
     : NamingBase
    {

        public const long UID = 0x31598E40;

        public Server()
            : base(UID)
        {
        }


        public override string CNT => "Server";
        public override string CN => CNT;
        public override string DE => CNT;
        public override string EN => CNT;
        public override string ES => CNT;
    }




    /// <summary>
    /// mko, 22.6.2020
    /// </summary>
    public class Download
     : NamingBase
    {

        public const long UID = 0x5AD29EDC;

        public Download()
            : base(UID)
        {
        }


        public override string CNT => "download";
        public override string CN => CNT;
        public override string DE => CNT;
        public override string EN => CNT;
        public override string ES => CNT;
    }

    /// <summary>
    /// mko, 22.6.2020
    /// </summary>
    public class Upload
     : NamingBase
    {

        public const long UID = 0x5BA03619;

        public Upload()
            : base(UID)
        {
        }


        public override string CNT => "upload";
        public override string CN => CNT;
        public override string DE => CNT;
        public override string EN => CNT;
        public override string ES => CNT;
    }


    /// <summary>
    /// mko, 22.6.2020
    /// </summary>
    public class Service
     : NamingBase
    {

        public const long UID = 0xF44FA9D;

        public Service()
            : base(UID)
        {
        }


        public override string CNT => "service";
        public override string CN => EN;
        public override string DE => "Dienst";
        public override string EN => "Service";
        public override string ES => "Servicio";
    }


    /// <summary>
    /// mko, 22.6.2020
    /// </summary>
    public class WebService
     : NamingBase
    {

        public const long UID = 0x1456FACC;

        public WebService()
            : base(UID)
        {
        }


        public override string CNT => "webService";
        public override string CN => EN;
        public override string DE => "Web- Dienst";
        public override string EN => "Web service";
        public override string ES => "Servicio Web";
    }

    /// <summary>
    /// mko, 22.6.2020
    /// </summary>
    public class WebServer
     : NamingBase
    {

        public const long UID = 0xD7567E26;

        public WebServer()
            : base(UID)
        {
        }


        public override string CNT => "webServer";
        public override string CN => EN;
        public override string DE => EN;
        public override string EN => "Webserver";
        public override string ES => EN;
    }


}
