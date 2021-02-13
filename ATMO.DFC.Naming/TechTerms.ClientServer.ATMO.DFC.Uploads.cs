using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// mko, 1.2.2021
/// </summary>
namespace MKPRG.Naming.TechTerms.ClientServer.ATMO.DFC.Uploads
{
    public class CTSUpload
        : NamingBase
    {

        public const long UID = 0xF493DA56;

        public CTSUpload()
            : base(UID)
        {
        }

        public override string CNT => "ctsUpload";
        public override string CN => "上传CTS文件";
        public override string DE => "CTS Dateien hochladen";
        public override string EN => "Upload CTS Files";
        public override string ES => "Cargar archivos CTS";

        public override string Glyph => Glyphs.ClientServer.Upload;
    }

    public class EDCUpload
    : NamingBase
    {

        public const long UID = 0x7F80DC39;

        public EDCUpload()
            : base(UID)
        {
        }

        public override string CNT => "edcUpload";
        public override string CN => "上传EDC文件";
        public override string DE => "EDC Dateien hochladen";
        public override string EN => "Upload EDC Files";
        public override string ES => "Cargar archivos EDC";

        public override string Glyph => Glyphs.ClientServer.Upload;
    }


    public class SFCUpload
        : NamingBase
    {

        public const long UID = 0x2D7AD713;

        public SFCUpload()
            : base(UID)
        {
        }

        public override string CNT => "sfcUpload";
        public override string CN => "上传SFC文件";
        public override string DE => "SFC Dateien hochladen";
        public override string EN => "Upload SFC Files";
        public override string ES => "Cargar archivos SFC";

        public override string Glyph => Glyphs.ClientServer.Upload;
    }

    public class CTSDownload
    : NamingBase
    {

        public const long UID = 0xADDA916E;

        public CTSDownload()
            : base(UID)
        {
        }

        public override string CNT => "ctsDownload";
        public override string CN => "下载CTS文件";
        public override string DE => "CTS Dateien herunterladen";
        public override string EN => "Download CTS Files";
        public override string ES => "Descargar archivos CTS";

        public override string Glyph => Glyphs.ClientServer.Download;
    }

    public class EDCUploadWarningFilenameDoesNotMeetNamingConventions
    : NamingBase
    {

        public const long UID = 0x2C7EA3CF;

        public EDCUploadWarningFilenameDoesNotMeetNamingConventions()
            : base(UID)
        {
        }

        public override string CNT => "filenameDoesNotMeetNamingConventionsForEDC";
        public override string CN => "文件名不符合电气EDC(PROJECT.STATION_MATNO.pdf)的命名惯例。EDC被分配给所选节点的材料编号。";
        public override string DE => "Dateiname entspricht nicht den Namenskonventionen der elektrischen EDC (PROJECT.STATION_MATNO.pdf). EDC ist der Materialnummer des ausgewählten Knotens zugeordnet.";
        public override string EN => "Filename does not meet naming conventions the electrical EDC (PROJECT.STATION_MATNO.pdf). EDC is assigned to the material number of selected node.";
        public override string ES => "El nombre del archivo no cumple con las convenciones de nomenclatura del EDC eléctrico (PROJECT.STATION_MATNO.pdf). El EDC se asigna al número de material del nodo seleccionado.";

        public override string Glyph => Glyphs.VariousSigns.WarningSign;
    }
           

    public class UploadTransactionSuccsessfulStarted
        : NamingBase
    {

        public const long UID = 0xAD521634;

        public UploadTransactionSuccsessfulStarted()
            : base(UID)
        {
        }

        public override string CNT => "uploadTransactionSuccsessfulStarted";
        public override string CN => "文件上传事务在DFC2客户端中成功启动。文件成功发送至DFC后台。原始文件被复制到子目录DFC_Uploaded_Bak。请重新加载项目，检查服务器端是否成功完成上传。同时检查邮件中是否有错误信息! ";
        public override string DE => "Die Dokument- Upload Transaktion wurde erfolgreich im DFC2 Client gestartet. Die Dokumente wurden erfolgreich an das DFC Backend gesendet. Die originalen Dateien wurden in das Unterverzeichnis DFC_Uploaded_Bak kopiert. Bitte Projekt neu laden und prüfen, ob der Upload serverseitig erfolgreich abgeschlossen wurde. Überprüfen sie auch die Emails auf Fehlermeldungen!";
        public override string EN => "The document upload transaction was successfully started in the DFC2 client. The documents were successfully sent to the DFC backend. The original files were copied to the subdirectory DFC_Uploaded_Bak. Please reload the project and check if the upload was completed successfully on the server side. Also check the emails for error messages! ";
        public override string ES => "La transacción de carga de documentos se ha iniciado con éxito en el cliente DFC2. Los documentos fueron enviados con éxito al backend de DFC. Los archivos originales se copiaron en el subdirectorio DFC_Uploaded_Bak. Vuelva a cargar el proyecto y compruebe si la carga se ha completado con éxito en el lado del servidor. Compruebe también los mensajes de error en los correos electrónicos. ";

        public override string Glyph => Glyphs.Events.Success;
    }

}
