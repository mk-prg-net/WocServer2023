using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKPRG.Naming
{
    /// <summary>
    /// Sammlung von Glyphen und Ideogrammen. Dabei handelt es sich um eine strukturierte Auswahl von Unicodes,
    /// die für die hier entwickleten Anwendungen mit einer Semmantik verknüpft werden.
    /// 
    /// mko, 26.1.2021
    /// Erstelle aus der Klasse ATMO.mko.Logging.HTML.HTMLDocument.Glyphs
    /// </summary>
    public static class Glyphs
    {

        public static string toStr(string Glyph)
        {
            return System.Net.WebUtility.HtmlDecode(Glyph);
        }

        public static class Aerospace
        {
            public static string Rocket => "&#x1F680;";
            public static string RocketStarts = "&#x1F66D;";

            public static string Satellite => "&#x1F6E0";

            public static string SupersonicJet => "&#xF6E6;";
            public static string Jet => "&#x1F6E7;";
            public static string FlyingDisk => "&#x1F6F8;";
        }

        public static class Animals
        {
            public static string Frog => "&#x1F438;";

            public static string EgyptFalcon => "&#x13261;";

            public static string Rabbit => "&#x1F407;";

            public static string Snail => "&#x1F40C;";

            public static string Elephant => "&#x1F418;";

            public static string Bug => "&#x1F41B;";

            public static string Ant => "&#x1F41C;";

            public static string Bee => "&#x1F41D;";

        }

        public static class ArrowsAndLines
        {

            // Pfeile, Ausrichtung nach Kompass- Rose

            public static string arrN => @"&#x2191;";
            public static string arrNO => @"&#x2197;";
            public static string arrO => @"&#x2192;";
            public static string arrSO => @"&#x2198;";
            public static string arrS => @"&#x2193;";
            public static string arrSW => @"&#x2199;";
            public static string arrW => @"&#x2190;";
            public static string arrNW => @"&#x2196";

            // Abknickende Pfeile
            public static string arrRightDown => "&#x21B4;";
            public static string arrDownLeft => "&#x21B5;";

            public static string arrLongUpLeft => "&#x21B0;";
            public static string arrLongUpRight => "&#x21B1;";

            public static string arrLongDownLeft => "&#x21B2;";
            public static string arrLongDownRight => "&#x21B3;";


            // Linien
            public static string lineHoriz => "&#x2501;";
            public static string lineVert => "&#x2503;";

            public static string lineHorizDashed => "&#x2505;";
            public static string lineVertDashed => "&#x2507;";

            public static string lineTopRight => "&#x250F;";
            public static string lineRightBottom => "&#x2513;";
            public static string lineBottomLeft => "&#x251B;";
            public static string lineLeftTop => "&#x2517;";

            public static string lineVertForkRight => "&#x2523;";
            public static string lineVertForkLeft => "&#x252B;";

            public static string lineHorzForkTop => "&#x253B;";
            public static string lineHorzForkBottom => "&#x2533;";

            public static string lineCrossHorzVert => "&#x254B;";
            public static string lineDiagonal => "&#x2573;";

            /// <summary>
            /// Pfeil zeigt Rotation im Uhrzeigersinn
            /// </summary>
            public static string rotClockwise => @"&#x21BB;";

            /// <summary>
            /// Pfeil zeigt Rotation gegen Uhrzeigersinn
            /// </summary>
            public static string rotAnticlockwise => @"&#x21BA;";

        }

        public static class Astronomy
        {
            public static string Merkur => "&#x263F;";
            public static string Venus => "&#x2640;";
            public static string Erde => "&#x2641;";
            public static string Mars => "&#x2642;";
            public static string Jupiter => "&#x2643;";
            public static string Saturn => "&#x2644;";
            public static string Uranus => "&#x2645;";
            public static string Neptun => "&#x2646;";
            public static string Pluto => "&#x2647;";

            public static string Ceres => "&#x26B3;";
            public static string Pallas => "&#x26B4;";
            public static string Juno => "&#x26B5;";
            public static string Vesta => "&#x26B6;";
            public static string Chiron => "&#x26B7;";
        }

        public static class Automaton
        {

            /// <summary>
            /// Darstellung des Zusandes eines endlichen Automaten: aktiver Zustand
            /// </summary>
            public static string ActiveState => Shapes.Circled_Bullet;

            /// <summary>
            /// Darstellung des Zusandes eines endlichen Automaten: inaktiver Zustand
            /// </summary>
            public static string InactiveState => Shapes.Circled_Bullet_white;
        }

        public static class Algorithm
        {
            /// <summary>
            /// Symbol für eine Alternative
            /// </summary>
            public static string alternate => "&#x2387;";

            public static string Program => "&#x2338;"; //"&#x21E3;";

        }

        public static class Authentication
        {
            public static string ID => "&#x275A;&#x2551;&#x2759;";
            public static string Name => "&#1302D;";
        }

        public static class Authorization
        {
            /// <summary>
            /// Vorhängeschloss geschlossen
            /// </summary>
            public static string Lock => "&#x1F512;";

            /// <summary>
            /// Vorhängeschloss geöffnet
            /// </summary>
            public static string UnLock => "&#x1F513;";

            public static string Forbidden => "&#x1F6C7;";

        }

        public static class Chemistry
        {
            public static string Retorte => "&#x1F76D;";

            public static string Benzol => "&#x23E3;";

        }

        public static class ClientServer
        {
            public static string Upload => "&#x2912;";

            public static string Download => "&#x2913;";

            public static string Server => Computer.Server;

            public static string Client => Computer.oldPC;

            public static string ClientServerSymbol => Net.LAN;

        }

        public static class Commerce
        {
            /// <summary>
            /// Einkaufswagen
            /// </summary>
            public static string Basket => "&#x1F6D2;";
        }

        public static class Computer
        {

            /// <summary>
            /// Tastatur
            /// </summary>
            public static string KeyBoard => "&#x2328;";

            public static string Computermouse => "&#x1F5B0";

            public static string ReturnKey => "&#x23CE;";

            public static string PC => "&#x1F5B3;";

            public static string oldPC => "&#x1F4B3;";

            public static string Server => "&#x1F5A5;";

            public static string PrinterBig => "&#x1F5A8;";

            public static string Printer => "&#x1F5B6;";


            public static string Computergame => "&#x1F3AE;";

            /// <summary>
            /// Bandlaufwerk
            /// </summary>
            public static string TapeOrStreamer => "&#x2707;";

            public static string EnterKey => $"{Math.bracketOpen}{ReturnKey}{Math.bracketClosed}";

        }

        public static class Communication
        {

            public static string Telecommunication => Aerospace.Satellite;

            public static string Fax => "&#x213B;";
            public static string FaxIcon => "&#x1F5B7;";

            public static string Tel => "&#x2121;";

            public static string PhoneWhite => "&#x1F57E;";
            public static string Phone => "&#x1F57F;";

            public static string MobilePhone = "&#x1F4F1;";

            public static string TelBox => "&#x2706;";

            public static string Airmail => "&#x1F585;";

            public static class Emails
            {
                public static string Email => "&#x1F4E7;";

                public static string Mailbox => "&#x1F4EA;";

                public static string Attachment => "&#x1F4CE;";
            }


        }

        public static class DataAndDocuments
        {
            public static string Key => "&#x1F511;";

            /// <summary>
            /// Ordner
            /// </summary>
            public static string Folder => "&#x1F4C1;";

            public static string DocumentEmpty => "&#x1F5CB;";
            public static string DocumentWithText => "&#x1F5CE;";
            public static string DocumentWithImage => "&#x1F5BA;";

            public static string Book => "&#x1F56E;";
            public static string Books => "&#x1F4DA;";

            public static string Newspaper => "&#x1F4F0;";

            public static string Compression => "&#x1F5DC;";

            public static string LineChart => "&#x1F5E0;";
            public static string LineChartLineUp => "&#x1F4C8;";
            public static string LineChartLineDown => "&#x1F4C9;";
            public static string BarChart => "&#x1F4CA;";


            public static class DataQuantity
            {
                public static string DataFlood => Glyphs.VariousSigns.Tsunami;
            }

            public static class Hyperlinks
            {
                public static string Link => "&#x1F517;";

                public static string Anchor => Glyphs.VariousSigns.Anchor;
            }


            /// <summary>
            /// Datenschutz
            /// </summary>
            public class Protection
            {
                public static string AntiVirus => Glyphs.War.Protector;
                public static string LossOfData => Glyphs.VariousSigns.Skull;
                public static string Sign => "&#x2711;";
            }

        }

        public static class DateAndTime
        {
            public static string Time => Metrology.StopWatch;

            public static string Date => "&#x1F4C5;";

            public static string Day => "&#x263C;";

            public static string Night => "&#x263E;";
        }

        public static class DFC
        {

            /// <summary>
            /// DFC- Projekt
            /// </summary>
            public static string Project => Geographic.Globe;

            //public static string Station => @"&#x1D4E2;";
            public static string Station => Shapes.Circled_S;


            /// <summary>
            /// DFC Prozessmodul
            /// </summary>
            public static string Processmodule => Math.Function;

            public static string Assy => Math.SquaredPlus;

            public static string ATB => "&#x1D30B;";

            public static string ATB_2 => "&#x1F70E;";


            public static string ATD => "&#x25F2;";

            public static string ATZ => $"{ArrowsAndLines.arrO}{ATD}";

            public static string TDP => "&#x1F5CB;";

            public static string CTS => Metrology.StopWatch;

            public static string Eplan => Shapes.SquaredSpiral;

            public static string MechBom => Tools.Gear;

            /// <summary>
            /// DFC Einzelteil
            /// </summary>
            public static string SinglePart => Shapes.WhiteSquare;

            /// <summary>
            /// ATMO- Standort
            /// </summary>
            public static string ATMOSite => VariousSigns.Factory;

            public static string DokuHaken => DataAndDocuments.Book;

            public static string AT3 => Shapes.UpperRightShadowedWhiteSqare;

            public static string ElectroBom => ElectricalEngineering.ElectricalFlash; //Weather.flash;

            public static string SparePart => Tools.Wrench;

        }

        public static class DocuTerms
        {
            public static string Instance = "&#x1F70E;;";

            public static string Method = "&#x2394;";

            public static string Event = Weather.flash;

            public static string Property = Shapes.Circled_Bullet_white;

            public static string Return => Transactions.reject;
        }

        public static class ElectricalEngineering
        {

            public static string ElectricalFlash => "&#x2301";

            public static string ElectricalIntersection => "&#x23E7;";

            /// <summary>
            /// Spule, Induktivität
            /// </summary>
            public static string Inductivity => "&#x214F;";

            public static string InductivityVertical => "&#xA4BA;";

            public static string Capacity => "&#x27DB;";

            public static string CapacityVertical => "&#x1183;";

            /// <summary>
            /// Differenzenverstärker
            /// </summary>
            public static string DifferentialAmp => "&#x234D;";

            public static string Bell => "&#x237E;";

            public static string MonoFlop => "&#x238D;";

            public static string Hysterese => "&#x238E;";

            public static string Grounding => "&#x23DA;";

            public static string Fuse => "&#x23DB;";

        }

        public static class Events
        {

            /// <summary>
            /// Fehlersymbol
            /// </summary>
            public static string Error => Weather.Thunderstrom;

            public static string Success => Gestures.ThumbsUp;

            /// <summary>
            /// Pures Infozeichen
            /// </summary>
            public static string Info => "&#x1F6C8;"; //Shapes.Circled_i;

            /// <summary>
            /// Statusmeldung
            /// </summary>
            public static string Status => Shapes.Circled_s;
        }

        public static class Geographic
        {
            /// <summary>
            /// Globus
            /// </summary>
            public static string Globe => @"&#x1F310;";

            public static string EarthGlobeEuropeAfrica => @"&#x1F30D;";

            public static string EarthGlobeAsiaAustralia => @"&#x1F30F;";

            public static string EarthGlobeAmerica => @"&#x1F30E;";
        }

        public static class Gestures
        {

            public static string ThumbsUp => "&#x1F592;";
            public static string ThumbsUpFilled => "&#x1F44D;";


            public static string ThumbsDown => "&#x1F593;";
            public static string ThumbsDownFilled => "&#x1F44E;";

            public static string ForefingerRight => "&#x261E;";

            public static string ForefingerLeft => "&#x261C;";
        }        

        public static class LawAndOrder
        {
            public static string Copyright => "&#x00A9;";

            public static string CopyrightAudio => "&#x2117;";

            public static string RegisteredMark => "&#x00AE;";

            public static string Trademark => "&#x2122;";
        }

        public static class Math
        {
            public static string Alpha => "&#x1D6C2;";

            public static string FracA => "&#x1D56C;";

            public static string A => "&#1D5D4;";

            public static string DeltaOp => "&#1D6E5;";


            /// <summary>
            /// Nummero- Zeichen
            /// </summary>
            public static string No => "&#x2116;";


            public static string AllQuantor => "&#x2200;";

            public static string ExistQuantor => "&#x2203;";

            public static string notExistQuantor => "&#x2204;";

            public static string IsElementOf => "&#x2208;";

            public static string notIsElementOf => "&#x2209;";

            // Mengen
            public static string OpenSet => "&#x007B;";
            public static string CloseSet => "&#x007B;";

            public static string Ellipsis => "&#x2026;";

            // Listen von Funktionsargumenten
            public static string OpenArgList => "(";
            public static string CloseArgList => ")";

            public static string OpenAngleArgList => "&#x27E8;";
            public static string CloseAngleArgList => "&#x27E9;";


            public static string bracketOpen => "[";
            public static string bracketClosed => "[";

            /// <summary>
            /// Zurodnung
            /// </summary>
            public static string Mapping => "&#x22B7;";

            /// <summary>
            /// Eingekreistes Pluszeichen
            /// </summary>
            public static string CircledPlus => "&#x2295;";

            /// <summary>
            /// Eingekreistes S
            /// </summary>
            public static string CircledS => "&#x24C8;";

            /// <summary>
            /// Eingerahmtes Plus
            /// </summary>
            public static string SquaredPlus => "&#x229E;";

            /// <summary>
            /// Kräftiges Pluszeichen
            /// </summary>
            public static string HeavyPlus => "&#x2795;";


            /// <summary>
            /// Eingekreistes Minus
            /// </summary>
            public static string circledMinus => "&#x2296;";

            /// <summary>
            /// Eingerahmtes Minus
            /// </summary>
            public static string squaredMinus => "&#x229F;";


            public static string crossProductOp => "&#x2A09;";

            /// <summary>
            /// Ring- Operator (Produkt von Funktionen)
            /// </summary>
            public static string FunctionMul => "&#x2218;";

            /// <summary>
            /// Kräftiges Minus
            /// </summary>
            public static string heavyMiuns => "&#x2796;";

            public static string SigmaSumme = "&#x03A3;";

            public static string Quantity => SigmaSumme; //"#";

            public static string Calculator => "&#x1F5A9;";

            /// <summary>
            /// 
            /// </summary>
            public static string Function => "&#x2394;";

            public static string SquareRoot => "&#x221A;";

            public static string CubeRoot => "&#x221B;";

            public static string ForthRoot => "&#x221C;";

        }

        public static class Metrology
        {
            public static string StopWatch => "&#x23F1;";


            public static string Timer => "&#x23F2;";

            public static string TriangularRuler => "&#x1F4D0;";

            /// <summary>
            /// Lineal/Maßstab
            /// </summary>
            public static string Ruler => "&#x1F4CF;";

            public static string Balance => "&#x2696;";

            public static string GradCelsius => "&#x2103;";

            public static string GradFahrenheit => "&#x2109;";

            public static string Ounce => "&#xU2125;";

        }

        public static class Navigation
        {

            public static string PrevPage => "&#x2397;";
            public static string NextPage => "&#x2398;";

            public static string GotoEnd => "&#x2B72;";
            public static string GotoStart => "&#x2B70";

        }

        public static class Net
        {
            public static string Cloud => Weather.Cloud;

            public static string LAN => "&#x1F5A7;";
        }

        public static class Physics
        {
            public static string Atom => "&#x269B;";
        }

        public static class Runtime
        {
            public static string ProcessAndContinue => "&#x27F4;";

            public static string ProcessAndBack => "&#x2B32;";

            public static string CircularProcess => "&#x2B94;"; // "&#x27F2;";


            public static string Start => Aerospace.RocketStarts;

            public static string Stop => "&#x1F6CB;";

            public static string RuntimeError => $"{Metrology.Timer}{Weather.flash}";

            public static string Execute => Computer.EnterKey;

            public static string Cancel => "&#x2418;";
            public static string CancelX => "&#x1F5D9;";

            /// <summary>
            /// Symbolisiert einen abgebrochnen Prozess
            /// </summary>
            public static string Aborted => $"{ProcessAndContinue}{CancelX}";

            public static string Finished => $"{ProcessAndContinue}|";
        }

        public static class Sets
        {
            /// <summary>
            /// Allgemeine Liste
            /// </summary>
            public static string List => "&#x25A4;";

            public static class Selections
            {
                /// <summary>
                /// geprüft, ok Haken
                /// </summary>
                public static string checkMark => "&#x2713;";


                /// <summary>
                /// geprüft, ok Haken fett
                /// </summary>
                public static string heavyCheckMark => "&#x2714;";

                /// <summary>
                /// geprüft, ok Haken fein
                /// </summary>
                public static string lightCheckMark => "&#x1F5F8;";


            }
        }

        public static class Shapes
        {
            public static string KaroImQuadrat => "&#x26CB;";


            public static string UpperRightShadowedWhiteSqare => "&#x2752;";

            public static string JoinedSquares => "&#x29C9;";

            public static string SquaredSpiral => "&#xA874;";

            public static string SquaredPositionMarks => "&#x2BD0;";

            /// <summary>
            /// Weisses Quadrat
            /// </summary>
            public static string WhiteSquare => "&#x25FB;";

            /// <summary>
            /// Quadratisches Gitter
            /// </summary>
            public static string Gitter => @"&#x25A6;";

            public static string Circled_Bullet => "&#x29BF;";

            public static string Circled_Bullet_white => "&#x29BE;";

            public static string squaredFreeSymbol => "&#x1F193;";

            public static string squaredNewSymbol => "&#x1F195;";

            public static string squaredIDSymbol => "&#x1F194;";

            public static string squaredSOSSymbol => "&#x1F198;";

            public static string squaredOKSymbol => "&#x1F197;";

            /// <summary>
            /// Eingekreistes i
            /// </summary>
            public static string Circled_i => "&#x24D8;";

            public static string Circled_s => "&#x24E2;";
            public static string Circled_S => "&#x24C8;";

            public static string Circle => "&#x2B58;";


            public static string EllipseHorizontal => "&#x2B2D;";
            public static string EllipseVertical => "&#x2B2F;";

            /// <summary>
            /// Viereck
            /// </summary>
            public static string Square => "&#x2B1C;";
            public static string SquareDotted => "&#x2B1A;";

            public static string Karo => "&#x2B26;";

            /// <summary>
            /// Vierzackiger Stern
            /// </summary>
            public static string StarFourPointed => "&#x2BCE;";



            /// <summary>
            /// Fünfeck
            /// </summary>
            public static string Pentagon => "&#x2B20;";

            /// <summary>
            /// Sechseck
            /// </summary>
            public static string Hexagon => "&#x2B21;";

            public static string OctagonBlack => "&#x2B21;";







        }

        public static class Smileys
        {
            public static string Smiley => "&#x263A;";

            public static string NonSmiley => "&#x2639;";
        }

        public static class Software
        {
            public static string SoftwareEngineering => "&#x29F0";

            public static string Compiler => $"{Math.Alpha}{Math.Mapping}{Math.A}";

            public static string APLFunctionUnderlined = "&#x236E;";

            public static string SyntaxError => $"&#x2376;{Weather.flash}";

            public static string Debugger => Animals.Bug;

        }

        public static class Support
        {
            /// <summary>
            /// Rettungskraft/Helfer
            /// </summary>
            public static string Rescuer => "&#x26D1;";

            public static string SOS => Shapes.squaredSOSSymbol;


        }

        public static class Text
        {
            public static string SPC = "&nbsp;";
            public static string SPCsmall = "&nnbsp;";
        }

        public static class Tools
        {
            public static string Toolbox => "&#x1F9F0;";

            public static string Screw => "&#x1F529;";

            public static string Gear => "&#x2699;";

            /// <summary>
            /// Hammer gekreuzt mit Schraubenschlüssel
            /// </summary>
            public static string HammerAndWrench => "&#x1F6E0;";

            /// <summary>
            /// Schraubschlüssel
            /// </summary>
            public static string Wrench => "&#x1F527;";

        }

        public static class Traffic
        {

            /// <summary>
            /// Vorfahrtszeichen
            /// </summary>
            public static string RightOfWay => "&#x26DB;";

            public static string Lane => "&#xA228;";

            /// <summary>
            /// Zeichen für Spurverengung
            /// </summary>
            public static string LaneNarrowing => "&#x26D9;";

            public static string OncomingTraffic => "&#x26D7;";

            public static string PetrolStation => "&#x26FD;";
            
            /// <summary>
            /// LKW
            /// </summary>
            public static string Truck => "&#x26DF;";

            public static string Sportscar => "&#x1F3CE;";

            public static string Motorbike => "&#x1F3CD;";

            public static string Airplane => "&#x2708;";

            public static string Departure => "&#x1F6EB;";

            public static string Landing => "&#x1F6EC;";

            /// <summary>
            /// Achtung: Schleudergefahr
            /// </summary>
            public static string DangerOfSkidding => "&#x26D0;";

            public static string NoEntry => "&#x26D4;";

            

        }

        public static class Transactions
        {

            /// <summary>
            /// Symbol für Widerruf
            /// </summary>
            public static string reject => ArrowsAndLines.rotAnticlockwise;

            /// <summary>
            /// Symbolf für Rückgangig machen.
            /// </summary>
            public static string revoke => "&#x238C;";

            /// <summary>
            /// Rückgängig Symbol
            /// </summary>
            public static string Undo => "&#x238C;";

        }

        public static class Trees
        {
            public static string Tree => "&#x2F4A;"; //"&#x312D;"; "&#x2C00"; "&#x1210;";

            public static string TreeUp => "&#x15D0;";

            public static string TreeDown => "&#x15D1;";

            public static string TreeLeft => "&#x15D2;";

            public static string TreeRight => "&#x15D5;";

            public static string TreeRightWithoutRoot => "&#x269F;";

            public static string TreeLeftWithoutRoot => "&#x269E;";


            public static string Root => "&#x1430;"; //  Math.CubeRoot;
        }

        public static class VariousSigns
        {
            public static string WarningSign => "&#x26A0;";

            public static string Home => "&#x2302;";

            public static string Diameter => "&#x2300;";

            public static string Magnifier => "&#x1F50E;";

            /// <summary>
            /// Radioaktivitäts- Warnzeichen
            /// </summary>
            public static string warningSignRadioactivity => "&#x2622;";

            public static string Alarm => "&#x2383;";

            /// <summary>
            /// Totenkopf 
            /// </summary>
            public static string Skull => "&#x2620;";

            /// <summary>
            /// Wiederverwertung
            /// </summary>
            public static string Recycling => "&#x2672;";

            /// <summary>
            /// Mülleimer
            /// </summary>
            public static string Trashcan => "&#x1F5D1;";


            /// <summary>
            /// Anker
            /// </summary>
            public static string Anchor => "&#x2693;";

            public static string Ghost => "&#x1F47B;";


            public static string Flower => "&#x2698;";

            /// <summary>
            /// Fabrik
            /// </summary>
            public static string Factory => "&#x1F3ED;";

            /// <summary>
            /// Infozeichen mit einem Kreis
            /// </summary>
            public static string InfoCircled => "&#x1F6C8;";

            /// <summary>
            /// Schlüssel in einer Box
            /// </summary>
            public static string BoxedKey => "&#x26BF;";

            public static string Collision => "&#x1F4A5;";

            public static string Joker => "&#x1F0BF;";

            public static string Tsunami => "&#x1F30A";

            public static string UnderConstruction => "&#x1F3D7";

            public static string InfoSign => "&#x2139;";

            public static string Mountains => "&#x1A12;"; // "&#113F;";

            public static string AtomicPowerStation => "&#x15C0;";

            public static string Sonnenschirm => "&#x26F1;";

            public static string UnknownSign => "&#x2BD1;";

            public static string Eye => "&#x1F441;";
            public static string Eyes => "&#x1F440;";

            /// <summary>
            /// Bombe
            /// </summary>
            public static string Bomb => "&#x1F4A3;";

            public static string Explosion => "&#x1F4A5;";

            public static string Bell => "&#x1F56D;";
            public static string BellOff => "&#x1F515;";

            public static string SpeechBubble => "&#x1F4AC;";

        }

        public static class Validation
        {
            public static string Contradiction => Weather.flash;

            public static string Valid => "&#x1F5F8;";

            public static string Invalid => "&#x1F5F4;";
        }

        public static class War
        {
            /// <summary>
            /// Meissner Porzellan
            /// </summary>
            public static string CrossedSwords => "&#x2694;";

            /// <summary>
            /// Schutzschild
            /// </summary>
            public static string Protector => "&#x1F6E1;";

            /// <summary>
            /// Abfangjäger
            /// </summary>
            public static string Interceptor => Aerospace.SupersonicJet;


            public static string Death => VariousSigns.Skull;

            /// <summary>
            /// Star Wars Tie Jäger, weiss
            /// </summary>
            public static string TieFighter => "&#x29F2;";

            /// <summary>
            /// Star Wars Tie Jäger, schwarz
            /// </summary>
            public static string TieFighterBlack => "&#x29F3;";

        }

        public static class Weather
        {
            /// <summary>
            /// Blitz
            /// </summary>
            public static string flash => @"&#x21AF;";


            /// <summary>
            /// Gewitterzeichen
            /// </summary>
            public static string Thunderstrom => "&#x26C8;";

            public static string Cloud => "&#x2601;";

            public static string BlackSun => "&#x2600;";

            public static string WhiteSun => "&#x263C;";

            public static string BightkSun => "&#x1F323;";


        }

    }
}
