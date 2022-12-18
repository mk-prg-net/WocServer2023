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
    /// Der Download- Nomen
    /// </summary>
    public class Download
     : NamingBase
    {
        public const long UID = 0x5AD29EDC;

        public static Download I { get; } = new Download();

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
    /// mko, 6.5.2021
    /// download- Verb
    /// </summary>
    public class DoDownload
        : NamingBase, Grammar.IInProgressActivity
    {

        public const long UID = 0xF72F5809;

        public static DoDownload I { get; } = new DoDownload();

        public DoDownload()
            : base(UID)
        {
        }


        public override string CNT => "doDownload";
        public override string CN => "下载";
        public override string DE => "lädt herunter";
        public override string EN => "downloads";
        public override string ES => "descargar";

        public override string Glyph => Glyphs.ClientServer.Download;
    }


    /// <summary>
    /// mko, 6.5.2021
    /// 
    /// </summary>
    public class CanBeDownloaded
         : NamingBase, Grammar.IModalPhrase
    {

        public const long UID = 0x249E7B55;

        public CanBeDownloaded()
            : base(UID)
        {
        }


        public override string CNT => "canBeDownloaded";
        public override string CN => "可以下载";
        public override string DE => "kann heruntergeladen werden";
        public override string EN => "can be downloaded";
        public override string ES => "se puede descargar";

        public override string Glyph => Glyphs.ClientServer.Download;
    }

    public class CantBeDownloaded
           : NamingBase, Grammar.IModalPhrase
    {

        public const long UID = 0xD67D1E47;

        public CantBeDownloaded()
            : base(UID)
        {
        }

        public override string CNT => "cantBeDownloaded";
        public override string CN => "无法下载";
        public override string DE => "kann nicht heruntergeladen werden";
        public override string EN => "can not be downloaded";
        public override string ES => "no se puede descargar";

        public override string Glyph => Glyphs.Authorization.Forbidden;
    }



    /// <summary>
    /// mko, 22.6.2020
    /// </summary>
    public class Upload
     : NamingBase, Grammar.IInProgressActivity
    {

        public const long UID = 0x5BA03619;

        public static Upload I { get; } = new Upload();

        public Upload()
            : base(UID)
        {
        }


        public override string CNT => "upload";
        public override string CN => "上传";
        public override string DE => "hochladen";
        public override string EN => "upload";
        public override string ES => "cargar";

        public override string Glyph => Glyphs.ClientServer.Upload;
    }

    /// <summary>
    /// mko, 6.5.2021
    /// upload- Verb
    /// </summary>
    public class DoUpload
     : NamingBase, Grammar.IInProgressActivity
    {

        public const long UID = 0x71E1877;

        public static DoUpload I { get; } = new DoUpload();

        public DoUpload()
            : base(UID)
        {
        }


        public override string CNT => "doUpload";
        public override string CN => "做上传";
        public override string DE => "lädt hoch";
        public override string EN => "do upload";
        public override string ES => "cargar";

        public override string Glyph => Glyphs.ClientServer.Upload;
    }


    /// <summary>
    /// mko, 6.5.2021
    /// </summary>
    public class CanBeUpload
     : NamingBase, Grammar.IModalPhrase
    {

        public const long UID = 0x37BA13C9;

        public CanBeUpload()
            : base(UID)
        {
        }


        public override string CNT => "canBeUploaded";
        public override string CN => "上传";
        public override string DE => "kann hochgeladen werden";
        public override string EN => "can be uploaded";
        public override string ES => "se puede cargar";

        public override string Glyph => Glyphs.ClientServer.Upload;
    }

    /// <summary>
    /// mko, 6.5.2021
    /// </summary>
    public class CantBeUpload
     : NamingBase, Grammar.IModalPhrase
    {

        public const long UID = 0xA91A1D7E;

        public CantBeUpload()
            : base(UID)
        {
        }

        public override string CNT => "cantBeUploaded";
        public override string CN => "不能上传";
        public override string DE => "kann nicht hochgeladen werden";
        public override string EN => "can not be uploaded";
        public override string ES => "no se puede cargar";

        public override string Glyph => Glyphs.Authorization.Forbidden;
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

        public override string Glyph => Glyphs.Math.Functions.Function;
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

        public override string Glyph => Glyphs.Net.Cloud + Glyphs.Math.Functions.Function;
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

        public override string Glyph => Glyphs.Net.Cloud + Glyphs.Computer.Server;
    }

}
