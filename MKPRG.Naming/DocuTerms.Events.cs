using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKPRG.Naming.DocuTerms.Event
{
    /// <summary>
    /// mko, 21.2.2020
    /// Erfolgsmeldung
    /// </summary>
    public class Succeeded
        : NamingBase
    {
        /// <summary>
        /// Unique Identifier of eSucceeded
        /// </summary>
        public const long UID = 0xBBD8056D;

        public Succeeded()
            : base(UID)
        {
        }

        public override string CNT => "succeeded";

        public override string CN => "成功的";

        public override string DE => "Erfolgreich";

        public override string EN => "succeeded";

        public override string ES => "Exitoso";

        public static Succeeded _() => new Succeeded();

        public override string Glyph => Glyphs.Events.Success;
    }

    /// <summary>
    /// mko, 21.2.2020
    /// Fehlermeldung
    /// </summary>
    public class Fails
        : NamingBase
    {
        /// <summary>
        /// Unique Identifier of eFails
        /// </summary>
        public const long UID = 0xC5E321AC;

        public Fails()
            : base(UID)
        {
        }

        /// <summary>
        /// Achtung: fails war die ursprüngliche Bezeichnung für Fehlschläge in Dokuterms
        ///          und sollte aus Kompatibilitätsgründen beibehalten werden
        /// </summary>
        public override string CNT => "fails";

        public override string CN => "错误";

        public override string DE => "Fehler";

        public override string EN => "Error";

        public override string ES => EN;

        public static Fails _() => new Fails();

        public override string Glyph => Glyphs.Events.Error;
    }

    /// <summary>
    /// mko, 21.2.2020
    /// Warnmeldung
    /// </summary>
    public class Warn
        : NamingBase
    {
        /// <summary>
        /// Unique Identifier of eWarn
        /// </summary>
        public const long UID = 0xFB8328D1;

        public Warn()
            : base(UID)
        {
        }

        public override string CNT => "warn";

        public override string CN => "警告";

        public override string DE => "Warnung";

        public override string EN => "Warning";

        public override string ES => "Advertencia";

        public static Warn _() => new Warn();

        public override string Glyph => Glyphs.VariousSigns.WarningSign;
    }

    /// <summary>
    /// mko, 27.2.2020
    /// Inforamtionsmeldungen
    /// </summary>
    public class Info
    : NamingBase
    {

        /// <summary>
        /// Unique Identifier of eInfo
        /// </summary>
        public const long UID = 0x710973A0;

        /// <summary>
        /// mko, 21.2.2020
        /// Warnmeldung
        /// </summary>
        public Info()
            : base(UID)
        {
        }

        public override string CNT => "info";

        public override string CN => "资讯";

        public override string DE => "Information";

        public override string EN => "Information";

        public override string ES => "Información";

        public static Info _() => new Info();

        public override string Glyph => Glyphs.Events.Info;
    }

    /// <summary>
    /// mko, 27.2.2020
    /// Dokumentation des Starts von Prozessen
    /// </summary>
    public class Start
        : NamingBase
    {
        /// <summary>
        /// Unique Identifier of eStart
        /// </summary>
        public const long UID = 0x7FDDBFF;

        public Start()
            : base(UID)
        {
        }

        public override string CNT => "start";

        public override string CN => "开始";

        public override string DE => "Start";

        public override string EN => "Start";

        public override string ES => "Comienza";

        public static Start _() => new Start();

        public override string Glyph => Glyphs.Navigation.GotoStart;
    }


    /// <summary>
    /// mko, 27.2.2020
    /// Dokumentation des Endes von Prozessen
    /// </summary>
    public class End
        : NamingBase
    {

        /// <summary>
        /// Unique Identifier of eEnd
        /// </summary>
        public const long UID = 0x5726D223;

        public End()
            : base(UID)
        {
        }

        public override string CNT => "End";

        public override string CN => "结束";

        public override string DE => "Ende";

        public override string EN => "End";

        public override string ES => "Fin";

        public static End _() => new End();

        public override string Glyph => Glyphs.Navigation.GotoEnd;
    }


    public class NotCompleted
    : NamingBase
    {

        /// <summary>
        /// Unique Identifier of eNotCompleted
        /// </summary>
        public const long UID = 0x3315DFD;

        public NotCompleted()
            : base(UID)
        {
        }

        /// <summary>
        /// Achtung: notCompleted war die ursprüngliche Bezeichnung für Fehlschläge in Dokuterms
        ///          und sollte aus Kompatibilitätsgründen beibehalten werden
        /// </summary>
        public override string CNT => "notCompleted";

        public override string CN => "未完成";

        public override string DE => "Noch nicht fertigestellt";

        public override string EN => "Not completed";

        public override string ES => "No se completó";

        public static End _() => new End();

        public override string Glyph => Glyphs.VariousSigns.WarningSign;
    }
}
