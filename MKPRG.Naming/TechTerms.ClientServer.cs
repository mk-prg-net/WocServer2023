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
        public override string CN => "客户";
        public override string DE => "Kunde";
        public override string EN => "Client";
        public override string ES => "Cliente";

        public override string Glyph => Glyphs.ClientServer.Client;
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
        public override string CN => "服务器";
        public override string DE => "Server";
        public override string EN => "Server";
        public override string ES => "Servidor";


        public override string Glyph => Glyphs.ClientServer.Server;
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
        public override string CN => "下载";
        public override string DE => "Herunterladen";
        public override string EN => "Download";
        public override string ES => "Descargar";

        public override string Glyph => Glyphs.ClientServer.Download;

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
        public override string CN => "上传";
        public override string DE => "Hochladen";
        public override string EN => "Upload";
        public override string ES => "Cargar";

        public override string Glyph => Glyphs.ClientServer.Upload;
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
        public override string CN => "服务项目";
        public override string DE => "Dienst";
        public override string EN => "Service";
        public override string ES => "Servicio";

        public override string Glyph => Glyphs.Math.Function;

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
        public override string CN => "网络服务";
        public override string DE => "Web- Dienst";
        public override string EN => "Web service";
        public override string ES => "Servicio Web";

        public override string Glyph => $"{Glyphs.Math.Function}{Glyphs.Geographic.Globe}";
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
        public override string CN => "网络服务器";
        public override string DE => EN;
        public override string EN => "Webserver";
        public override string ES => EN;

        public override string Glyph => Glyphs.ClientServer.Server;
    }

}
