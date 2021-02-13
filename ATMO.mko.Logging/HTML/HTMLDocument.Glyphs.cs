using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Glyphs = MKPRG.Naming.Glyphs;

namespace ATMO.mko.Logging.HTML
{
    /// <summary>
    /// Glyphen (miniaturen mit fester, allgemeingültiger Semantik)
    /// </summary>
    partial class HTMLDocument
    {
        //public static class Glyphs
        //{
        //    public static class Aerospace
        //    {
        //        public static string Rocket => "&#1F680;";
        //        public static string RocketStarts = "&#1F66D;";

        //        public static string Satellite => "&#1F6E0";

        //        public static string SupersonicJet => "&#1F6E6;";
        //        public static string Jet => "&#1F6E7;";
        //        public static string FlyingDisk => "&#1F6F8;";
        //    }

        //    public static class Animals
        //    {
        //        public static string Frog => "&#x1F438;";

        //        public static string EgyptFalcon => "&#x13261;";

        //    }

        //    public static class ArrowsAndLines
        //    {

        //        // Pfeile, Ausrichtung nach Kompass- Rose

        //        public static string arrN => @"&#x2191;";
        //        public static string arrNO => @"&#x2197;";
        //        public static string arrO => @"&#x2192;";
        //        public static string arrSO => @"&#x2198;";
        //        public static string arrS => @"&#x2193;";
        //        public static string arrSW => @"&#x2199;";
        //        public static string arrW => @"&#x2190;";
        //        public static string arrNW => @"&#x2196";

        //        public static string arrRightDown => "&#x21B4;";
        //        public static string arrDownLeft => "&#x21B4;";

        //        // Linien
        //        public static string lineHoriz => "&#x2501;";
        //        public static string lineVert => "&#x2503;";

        //        public static string lineHorizDashed => "&#x2505;";
        //        public static string lineVertDashed => "&#x2507;";

        //        public static string lineTopRight => "&#x250F;";
        //        public static string lineRightBottom => "&#x2513;";
        //        public static string lineBottomLeft => "&#x251B;";
        //        public static string lineLeftTop => "&#x2517;";

        //        public static string lineVertForkRight => "&#x2523;";
        //        public static string lineVertForkLeft => "&#x252B;";

        //        public static string lineHorzForkTop => "&#x253B;";
        //        public static string lineHorzForkBottom => "&#x2533;";

        //        public static string lineCrossHorzVert => "&#x254B;";
        //        public static string lineDiagonal => "&#x2573;";

        //        /// <summary>
        //        /// Pfeil zeigt Rotation im Uhrzeigersinn
        //        /// </summary>
        //        public static string rotClockwise => @"&#x21BB;";

        //        /// <summary>
        //        /// Pfeil zeigt Rotation gegen Uhrzeigersinn
        //        /// </summary>
        //        public static string rotAnticlockwise => @"&#x21BA;";

        //    }

        //    public static class Automaton
        //    {

        //        /// <summary>
        //        /// Darstellung des Zusandes eines endlichen Automaten: aktiver Zustand
        //        /// </summary>
        //        public static string ActiveState => Shapes.Circled_Bullet;

        //        /// <summary>
        //        /// Darstellung des Zusandes eines endlichen Automaten: inaktiver Zustand
        //        /// </summary>
        //        public static string InactiveState => Shapes.Circled_Bullet_white;
        //    }

        //    public static class Algorithm
        //    {
        //        /// <summary>
        //        /// Symbol für eine Alternative
        //        /// </summary>
        //        public static string alternate => "&#2387;";

        //    }

        //    public static class Authorization
        //    {
        //        /// <summary>
        //        /// Vorhängeschloss geschlossen
        //        /// </summary>
        //        public static string Lock => "&#x1F512;";

        //        /// <summary>
        //        /// Vorhängeschloss geöffnet
        //        /// </summary>
        //        public static string UnLock => "&#x1F513;";

        //    }

        //    public static class Commerce
        //    {
        //        /// <summary>
        //        /// Einkaufswagen
        //        /// </summary>
        //        public static string Basket => "&#1F6D2;";
        //    }

        //    public static class Computer
        //    {

        //        /// <summary>
        //        /// Tastatur
        //        /// </summary>
        //        public static string KeyBoard => "&#2328;";

        //        public static string Computermouse => "&#1F5B0";

        //        public static string ReturnKey => "&#x23CE;";

        //        public static string PC => "&#1F4BB;";

        //        public static string oldPC => "&#1F4B3;";

        //        public static string Server => "&#1F5A5;";

        //        public static string Computergame => "&#1F3AE;";

        //    }

        //    public static class DataAndDocuments
        //    {
        //        public static string Key => "&#x1F511;";

        //        /// <summary>
        //        /// Ordner
        //        /// </summary>
        //        public static string Folder => "&#x1F4C1;";

        //        public static string DocumentEmpty => "&#1F5CB;";
        //        public static string DocumentWithText => "&#1F5CE;";
        //        public static string DocumentWithImage => "&#1F5BA;";

        //        public static string Book => "&#x1F56E;";
        //        public static string Books => "&#x1F4DA;";

        //        public static string Newspaper => "&#1F4F0;";

        //        public static string Compression => "&#1F5DC;";

        //        public static string LineChart => "&#1F5E0;";

        //        public static class DataQuantity
        //        {
        //            public static string DataFlood => Glyphs.VariousSigns.Tsunami;
        //        }

        //        public static class Hyperlinks
        //        {
        //            public static string Link => "&#x1F517;";

        //            public static string Anchor => Glyphs.VariousSigns.Anchor;
        //        }

        //        public static class Emails
        //        {
        //            public static string Email => "&#1F4E7;";

        //            public static string Mailbox => "&#1F4EA;";

        //            public static string Attachment => "&#1F4CE;";
        //        }

        //        /// <summary>
        //        /// Datenschutz
        //        /// </summary>
        //        public class Protection
        //        {
        //            public static string AntiVirus => Glyphs.War.Protector;
        //            public static string LossOfData => Glyphs.VariousSigns.Skull;
        //        }

        //    }

        //    public static class DFC
        //    {

        //        /// <summary>
        //        /// DFC- Projekt
        //        /// </summary>
        //        public static string Project => Geographic.Globe;

        //        //public static string Station => @"&#x1D4E2;";
        //        public static string Station => Shapes.Circled_S;


        //        /// <summary>
        //        /// DFC Prozessmodul
        //        /// </summary>
        //        public static string Processmodule => "&#x2394;";

        //        public static string Assy => Math.SquaredPlus;

        //        public static string ATD => "&#x25F2;";

        //        public static string ATZ => $"{ArrowsAndLines.arrO}{ATD}";

        //        public static string TDP => "&#x1F5CB;";

        //        public static string CTS =>  Metrology.StopWatch;

        //        public static string Eplan => Shapes.SquaredSpiral;

        //        public static string MechBom => Tools.Gear;

        //        /// <summary>
        //        /// DFC Einzelteil
        //        /// </summary>
        //        public static string SinglePart => Shapes.WhiteSquare;

        //        /// <summary>
        //        /// ATMO- Standort
        //        /// </summary>
        //        public static string ATMOSite => VariousSigns.Factory;

        //        public static string DokuHaken => DataAndDocuments.Book;

        //        public static string AT3 => Shapes.UpperRightShadowedWhiteSqare;

        //        public static string ElectroBom => Weather.flash;

        //        public static string SparePart => Tools.Wrench;

        //    }

        //    public static class ElectricalEngineering
        //    {
        //        public static string ElectricalIntersection => "&#x23E7;";

        //    }

        //    public static class Events
        //    {

        //        /// <summary>
        //        /// Fehlersymbol
        //        /// </summary>
        //        public static string Error => Weather.Thunderstrom;

        //        public static string Success => Gestures.ThumbsUp;

        //        /// <summary>
        //        /// Pures Infozeichen
        //        /// </summary>
        //        public static string Info => Shapes.Circled_i; 

        //        /// <summary>
        //        /// Statusmeldung
        //        /// </summary>
        //        public static string Status => Shapes.Circled_s;
        //    }

        //    public static class Geographic
        //    {
        //        /// <summary>
        //        /// Globus
        //        /// </summary>
        //        public static string Globe => @"&#x1F310;";

        //        public static string EarthGlobeEuropeAfrica => @"&#x1F30D;";

        //        public static string EarthGlobeAsiaAustralia => @"&#x1F30F;";

        //        public static string EarthGlobeAmerica => @"&#x1F30E;";
        //    }

        //    public static class Gestures
        //    {

        //        public static string ThumbsUp => "&#x1F592;";
        //        public static string ThumbsUpFilled => "&#x1F44D;";


        //        public static string ThumbsDown => "&#x1F593;";
        //        public static string ThumbsDownFilled => "&#x1F44E;";

        //        public static string ForefingerRight => "&#x261E;";

        //        public static string ForefingerLeft => "&#x261C;";
        //    }

        //    public static class Math
        //    {

        //        /// <summary>
        //        /// Eingekreistes Pluszeichen
        //        /// </summary>
        //        public static string CircledPlus => "&#x2295;";

        //        /// <summary>
        //        /// Eingekreistes S
        //        /// </summary>
        //        public static string CircledS => "&#x24C8;";

        //        /// <summary>
        //        /// Eingerahmtes Plus
        //        /// </summary>
        //        public static string SquaredPlus => "&#x229E;";

        //        /// <summary>
        //        /// Kräftiges Pluszeichen
        //        /// </summary>
        //        public static string HeavyPlus => "&#x2795;";


        //        /// <summary>
        //        /// Eingekreistes Minus
        //        /// </summary>
        //        public static string circledMinus => "&#x2296;";

        //        /// <summary>
        //        /// Eingerahmtes Minus
        //        /// </summary>
        //        public static string squaredMinus => "&#x229F;";

        //        /// <summary>
        //        /// Kräftiges Minus
        //        /// </summary>
        //        public static string heavyMiuns => "&#x2796;";

        //        public static string SigmaSumme = "&#x03A3;";

        //        public static string Quantity => SigmaSumme; //"#";

        //    }

        //    public static class Metrology
        //    {
        //        public static string StopWatch => "&#x23F1;";

        //        public static string TriangularRuler => "&#x1F4D0;";

        //        /// <summary>
        //        /// Lineal/Maßstab
        //        /// </summary>
        //        public static string Ruler => "&#x1F4CF;";

        //    }

        //    public static class Net
        //    {
        //        public static string Cloud => Weather.Cloud;

        //        public static string LAN => "&1F5A7;";
        //    }

        //    public static class Sets
        //    {
        //        public static class Selections
        //        {
        //            /// <summary>
        //            /// geprüft, ok Haken
        //            /// </summary>
        //            public static string checkMark => "&#x2713;";


        //            /// <summary>
        //            /// geprüft, ok Haken fett
        //            /// </summary>
        //            public static string heavyCheckMark => "&#x2714;";

        //            /// <summary>
        //            /// geprüft, ok Haken fein
        //            /// </summary>
        //            public static string lightCheckMark => "&#x1F5F8;";


        //        }
        //    }

        //    public static class Shapes
        //    {
        //        public static string KaroImQuadrat => "&#x26CB;";


        //        public static string UpperRightShadowedWhiteSqare => "&#x2752;";

        //        public static string JoinedSquares => "&#x29C9;";

        //        public static string SquaredSpiral => "&#xA874;";

        //        public static string SquaredPositionMarks => "&#x2BD0;";

        //        /// <summary>
        //        /// Weisses Quadrat
        //        /// </summary>
        //        public static string WhiteSquare => "&#x25FB;";

        //        /// <summary>
        //        /// Quadratisches Gitter
        //        /// </summary>
        //        public static string Gitter => @"&#x25A6;";

        //        public static string Circled_Bullet => "&#x29BF;";

        //        public static string Circled_Bullet_white => "&#x29BE;";

        //        public static string squaredFreeSymbol => "&#1F193;";

        //        public static string squaredNewSymbol => "&#1F195;";

        //        public static string squaredIDSymbol => "&#1F194;";

        //        public static string squaredSOSSymbol => "&#1F198;";

        //        public static string squaredOKSymbol => "&#1F197;";

        //        /// <summary>
        //        /// Eingekreistes i
        //        /// </summary>
        //        public static string Circled_i => "&#x24D8;";

        //        public static string Circled_s => "&#x24E2;";
        //        public static string Circled_S => "&#x24C8;";



        //    }

        //    public static class Smileys
        //    {
        //        public static string Smiley => "&#x263A;";

        //        public static string NonSmiley => "&#x2639;";
        //    }

        //    public static class Software
        //    {
        //        public static string SoftwareEngineering => "&#29F0";
        //    }

        //    public static class Support
        //    {
        //        /// <summary>
        //        /// Rettungskraft/Helfer
        //        /// </summary>
        //        public static string Rescuer => "&#x26D1;";

        //        public static string SOS => Shapes.squaredSOSSymbol;


        //    }

        //    public static class Tools
        //    {
        //        public static string Toolbox => "&#x1F9F0;";

        //        public static string Screw => "&#x1F529;";

        //        public static string Gear => "&#x2699;";

        //        /// <summary>
        //        /// Hammer gekreuzt mit Schraubenschlüssel
        //        /// </summary>
        //        public static string HammerAndWrench => "&#x1F6E0;";

        //        /// <summary>
        //        /// Schraubschlüssel
        //        /// </summary>
        //        public static string Wrench => "&#x1F527;";

        //    }

        //    public static class Transactions
        //    {

        //        /// <summary>
        //        /// Symbol für Widerruf
        //        /// </summary>
        //        public static string reject => ArrowsAndLines.rotAnticlockwise;

        //        /// <summary>
        //        /// Symbolf für Rückgangig machen.
        //        /// </summary>
        //        public static string revoke => "&#238C;";

        //        /// <summary>
        //        /// Rückgängig Symbol
        //        /// </summary>
        //        public static string Undo => "&#x238C;";

        //    }

        //    public static class VariousSigns
        //    {
        //        /// <summary>
        //        /// Radioaktivitäts- Warnzeichen
        //        /// </summary>
        //        public static string warningSignRadioactivity => "&#x2622;";

        //        /// <summary>
        //        /// Totenkopf 
        //        /// </summary>
        //        public static string Skull => "&#x2620;";

        //        /// <summary>
        //        /// Wiederverwertung
        //        /// </summary>
        //        public static string Recycling => "&#x2672;";

        //        /// <summary>
        //        /// Mülleimer
        //        /// </summary>
        //        public static string Trashcan => "&#x1F5D1;";

        //        /// <summary>
        //        /// Anker
        //        /// </summary>
        //        public static string Anchor => "&#x2693;";

        //        public static string Ghost => "&#x1F47B;";


        //        /// <summary>
        //        /// Fabrik
        //        /// </summary>
        //        public static string Factory => "&#x1F3ED;";

        //        /// <summary>
        //        /// Infozeichen mit einem Kreis
        //        /// </summary>
        //        public static string InfoCircled => "&#x1F6C8;";

        //        /// <summary>
        //        /// Schlüssel in einer Box
        //        /// </summary>
        //        public static string BoxedKey => "&#x26BF;";

        //        public static string Collision => "&#x1F4A5;";

        //        public static string Joker => "&#1F0BF;";

        //        public static string Tsunami => "&#1F30A";

        //        public static string UnderConstruction => "&#1F3D7";

        //        public static string InfoSign => "&#x2139;";




        //    }

        //    public static class War
        //    {
        //        /// <summary>
        //        /// Meissner Porzellan
        //        /// </summary>
        //        public static string CrossedSwords => "&#x2694;";

        //        /// <summary>
        //        /// Schutzschild
        //        /// </summary>
        //        public static string Protector => "&#1F6E1;";

        //        /// <summary>
        //        /// Abfangjäger
        //        /// </summary>
        //        public static string Interceptor => Aerospace.SupersonicJet;


        //        public static string Death => VariousSigns.Skull;

        //    }

        //    public static class Weather
        //    {
        //        /// <summary>
        //        /// Blitz
        //        /// </summary>
        //        public static string flash => @"&#x21AF;";


        //        /// <summary>
        //        /// Gewitterzeichen
        //        /// </summary>
        //        public static string Thunderstrom => "&#x26C8;";

        //        public static string Cloud => "&#x2601;";

        //        public static string BlackSun => "&#2600;";

        //        public static string WhiteSun => "&#263C;";

        //        public static string BightkSun => "&#1F323;";


        //    }

        //}

        /// <summary>
        /// Pfeil nach Norden
        /// </summary>
        public HTMLDocument glyphArrN
        {
            get
            {
                bldDoc.Append(Glyphs.ArrowsAndLines.arrN);
                return this;
            }
        }

        /// <summary>
        /// Pfeil nach Nordost
        /// </summary>
        public HTMLDocument glyphArrNO
        {
            get
            {
                bldDoc.Append(Glyphs.ArrowsAndLines.arrNO);
                return this;
            }
        }

        /// <summary>
        /// Pfeil nach Ost
        /// </summary>
        public HTMLDocument glyphArrO
        {
            get
            {
                bldDoc.Append(Glyphs.ArrowsAndLines.arrO);
                return this;
            }
        }

        /// <summary>
        /// Pfeil nach Südost
        /// </summary>
        public HTMLDocument glyphArrSO
        {
            get
            {
                bldDoc.Append(Glyphs.ArrowsAndLines.arrSO);
                return this;
            }
        }

        /// <summary>
        /// Pfeil nach Süden
        /// </summary>
        public HTMLDocument glyphArrS
        {
            get
            {
                bldDoc.Append(Glyphs.ArrowsAndLines.arrS);
                return this;
            }
        }

        /// <summary>
        /// Pfeil nach Südwesten
        /// </summary>
        public HTMLDocument glyphArrSW
        {
            get
            {
                bldDoc.Append(Glyphs.ArrowsAndLines.arrSW);
                return this;
            }
        }

        /// <summary>
        /// Pfeil nach Westen
        /// </summary>
        public HTMLDocument glyphArrW
        {
            get
            {
                bldDoc.Append(Glyphs.ArrowsAndLines.arrW);
                return this;
            }
        }

        /// <summary>
        /// Pfeil nach Nordwesten
        /// </summary>
        public HTMLDocument glyphArrNW
        {
            get
            {
                bldDoc.Append(Glyphs.ArrowsAndLines.arrNW);
                return this;
            }
        }

        /// <summary>
        /// Pfeil nach Nordwesten
        /// </summary>
        public HTMLDocument glyphRotAnticlockwise
        {
            get
            {
                bldDoc.Append(Glyphs.ArrowsAndLines.rotAnticlockwise);
                return this;
            }
        }

        /// <summary>
        /// Pfeil nach Nordwesten
        /// </summary>
        public HTMLDocument glyphRotClockwise
        {
            get
            {
                bldDoc.Append(Glyphs.ArrowsAndLines.rotClockwise);
                return this;
            }
        }

        /// <summary>
        /// Pfeil zurück
        /// </summary>
        public HTMLDocument glyphUndo
        {
            get
            {
                bldDoc.Append(Glyphs.Transactions.Undo);
                return this;
            }
        }


        /// <summary>
        /// Informationszeichen
        /// </summary>
        public HTMLDocument glyphInformation
        {
            get
            {
                bldDoc.Append(Glyphs.VariousSigns.InfoCircled);
                return this;
            }
        }

        /// <summary>
        /// Zustand, Status
        /// </summary>
        public HTMLDocument glyphStatus
        {
            get
            {
                bldDoc.Append(Glyphs.Events.Status);
                return this;
            }
        }

        /// <summary>
        /// Symbol für aktiven Zustand in einem Zustadsgraphen
        /// </summary>
        public HTMLDocument glyphAutomatonActiveState
        {
            get
            {
                bldDoc.Append(Glyphs.Automaton.ActiveState);
                return this;
            }
        }

        public HTMLDocument glyphAutomatonInactiveState
        {
            get
            {
                bldDoc.Append(Glyphs.Automaton.InactiveState);
                return this;
            }
        }

        /// <summary>
        /// Pfeil nach Nordwesten
        /// </summary>
        public HTMLDocument glyphGlobe
        {
            get
            {
                bldDoc.Append(Glyphs.Geographic.Globe);
                return this;
            }
        }

        public HTMLDocument glyphGlobeEuropeAfrica
        {
            get
            {
                bldDoc.Append(Glyphs.Geographic.EarthGlobeEuropeAfrica);
                return this;
            }
        }

        public HTMLDocument glyphGlobeAsiaAustralia
        {
            get
            {
                bldDoc.Append(Glyphs.Geographic.EarthGlobeAsiaAustralia);
                return this;
            }
        }

        public HTMLDocument glyphGlobeAmerica
        {
            get
            {
                bldDoc.Append(Glyphs.Geographic.EarthGlobeAmerica);
                return this;
            }
        }



        /// <summary>
        /// Pfeil nach Nordwesten
        /// </summary>
        public HTMLDocument glyphFlash
        {
            get
            {
                bldDoc.Append(Glyphs.Weather.flash);
                return this;
            }
        }

        /// <summary>
        /// Fehlersymbol
        /// </summary>
        public HTMLDocument glyphError
        {
            get
            {
                bldDoc.Append(Glyphs.Events.Error);
                return this;
            }
        }


        /// <summary>
        /// Pfeil nach Nordwesten
        /// </summary>
        public HTMLDocument glyphWhiteSquare
        {
            get
            {
                bldDoc.Append(Glyphs.Shapes.WhiteSquare);
                return this;
            }
        }

        /// <summary>
        /// DFC Projektsymbol
        /// </summary>
        public HTMLDocument glyphProject
        {
            get
            {
                bldDoc.Append(Glyphs.DFC.Project);
                return this;
            }
        }

        public HTMLDocument glyphProjectWithBaseLine
        {
            get
            {
                bldDoc.Append(Glyphs.DFC.Project);
                return this;
            }
        }


        /// <summary>
        /// DFC Projektsymbol
        /// </summary>
        public HTMLDocument glyphStation
        {
            get
            {
                bldDoc.Append(Glyphs.DFC.Station);
                return this;
            }
        }

        /// <summary>
        /// DFC Projektsymbol
        /// </summary>
        public HTMLDocument glyphProcessmodul
        {
            get
            {
                bldDoc.Append(Glyphs.DFC.Processmodule);
                return this;
            }
        }

        /// <summary>
        /// Gebiet der Mechanik- Konstruktion
        /// </summary>
        public HTMLDocument glyphMechArea
        {
            get
            {
                bldDoc.Append(Glyphs.DFC.MechBom);
                return this;
            }
        }

        /// <summary>
        /// Gebiet der Electro- Konstruktion
        /// </summary>
        public HTMLDocument glyphElectroArea
        {
            get
            {
                bldDoc.Append(Glyphs.DFC.ElectroBom);
                return this;
            }
        }

        /// <summary>
        /// DFC Projektsymbol
        /// </summary>
        public HTMLDocument glyphAssy
        {
            get
            {
                bldDoc.Append(Glyphs.DFC.Assy);
                return this;
            }
        }

        /// <summary>
        /// Pfeil nach Nordwesten
        /// </summary>
        public HTMLDocument glyphSinglePart
        {
            get
            {
                bldDoc.Append(Glyphs.DFC.SinglePart);
                return this;
            }
        }

        public HTMLDocument glyphATD
        {
            get
            {
                bldDoc.Append(Glyphs.DFC.ATD);
                return this;
            }
        }

        public HTMLDocument glyphATZ
        {
            get
            {
                bldDoc.Append(Glyphs.DFC.ATZ);
                return this;
            }
        }

        public HTMLDocument glyphAT3
        {
            get
            {
                bldDoc.Append(Glyphs.DFC.AT3);
                return this;
            }
        }

        public HTMLDocument glyphTDP
        {
            get
            {
                bldDoc.Append(Glyphs.DFC.TDP);
                return this;
            }
        }

        public HTMLDocument glyphEPlan
        {
            get
            {
                bldDoc.Append(Glyphs.DFC.Eplan);
                return this;
            }
        }

        public HTMLDocument glyphCTS
        {
            get
            {
                bldDoc.Append(Glyphs.DFC.CTS);
                return this;
            }
        }

        public HTMLDocument glyphEDC
        {
            get
            {
                bldDoc.Append(Glyphs.Tools.HammerAndWrench);
                return this;
            }
        }

        public HTMLDocument glyphSFC
        {
            get
            {
                bldDoc.Append(Glyphs.Tools.HammerAndWrench);
                return this;
            }
        }



        /// <summary>
        /// Pfeil nach Nordwesten
        /// </summary>
        public HTMLDocument glyphRecycling
        {
            get
            {
                bldDoc.Append(Glyphs.VariousSigns.Recycling);
                return this;
            }
        }

        /// <summary>
        /// Pfeil nach Nordwesten
        /// </summary>
        public HTMLDocument glyphRescuer
        {
            get
            {
                bldDoc.Append(Glyphs.Support.Rescuer);
                return this;
            }
        }

        /// <summary>
        /// Pfeil nach Nordwesten
        /// </summary>
        public HTMLDocument glyphReject
        {
            get
            {
                bldDoc.Append(Glyphs.Transactions.reject);
                return this;
            }
        }

        /// <summary>
        /// Pfeil nach Nordwesten
        /// </summary>
        public HTMLDocument glyphReturnKey
        {
            get
            {
                bldDoc.Append(Glyphs.Computer.ReturnKey);
                return this;
            }
        }

        public HTMLDocument glyphSkull
        {
            get
            {
                bldDoc.Append(Glyphs.VariousSigns.Skull);
                return this;
            }
        }

        public HTMLDocument glyphSmiley
        {
            get
            {
                bldDoc.Append(Glyphs.Smileys.Smiley);
                return this;
            }
        }

        public HTMLDocument glyphNonSmiley
        {
            get
            {
                bldDoc.Append(Glyphs.Smileys.NonSmiley);
                return this;
            }
        }

        public HTMLDocument glyphAnchor
        {
            get
            {
                bldDoc.Append(Glyphs.VariousSigns.Anchor);
                return this;
            }
        }

        public HTMLDocument glyphBoxedKey
        {
            get
            {
                //bldDoc.Append(Glyphs.VariousSigns.BoxedKey);
                return this;
            }
        }

        public HTMLDocument glyphCloud
        {
            get
            {
                bldDoc.Append(Glyphs.Net.Cloud);
                return this;
            }
        }

        public HTMLDocument glyphCollision
        {
            get
            {
                bldDoc.Append(Glyphs.VariousSigns.Collision);
                return this;
            }
        }

        public HTMLDocument glyphCrossedSwords
        {
            get
            {
                bldDoc.Append(Glyphs.War.CrossedSwords);
                return this;
            }
        }

        /// <summary>
        /// Ordner
        /// </summary>
        public HTMLDocument glyphFolder
        {
            get
            {
                bldDoc.Append(Glyphs.DataAndDocuments.Folder);
                return this;
            }
        }

        /// <summary>
        /// Fabrik
        /// </summary>
        public HTMLDocument glyphFactory
        {
            get
            {
                bldDoc.Append(Glyphs.VariousSigns.Factory);
                return this;
            }
        }

        /// <summary>
        /// ATMO- Standort
        /// </summary>
        public HTMLDocument glyphATMOSite
        {
            get
            {
                bldDoc.Append(Glyphs.DFC.ATMOSite);
                return this;
            }
        }

        /// <summary>
        /// Zeigefinger nach links
        /// </summary>
        public HTMLDocument glyphForefingerLeft
        {
            get
            {
                bldDoc.Append(Glyphs.Gestures.ForefingerLeft);
                return this;
            }
        }

        /// <summary>
        /// Zeigefinger nach rechts
        /// </summary>
        public HTMLDocument glyphForefingerRight
        {
            get
            {
                bldDoc.Append(Glyphs.Gestures.ForefingerRight);
                return this;
            }
        }

        /// <summary>
        /// Daumen hoch
        /// </summary>
        public HTMLDocument glyphThumbsUp
        {
            get
            {
                bldDoc.Append(Glyphs.Gestures.ThumbsUp);
                return this;
            }
        }

        /// <summary>
        /// Anzeige von Erfolg
        /// </summary>
        public HTMLDocument glyphSuccess
        {
            get
            {
                bldDoc.Append(Glyphs.Events.Success);
                return this;
            }
        }

        /// <summary>
        /// Daumen runter
        /// </summary>
        public HTMLDocument glyphThumbsDown
        {
            get
            {
                bldDoc.Append(Glyphs.Gestures.ThumbsDown);
                return this;
            }
        }



        /// <summary>
        /// Zahnrad
        /// </summary>
        public HTMLDocument glyphGear
        {
            get
            {
                bldDoc.Append(Glyphs.Tools.Gear);
                return this;
            }
        }

        /// <summary>
        /// Geist/Gespenst
        /// </summary>
        public HTMLDocument glyphGhost
        {
            get
            {
                bldDoc.Append(Glyphs.VariousSigns.Ghost);
                return this;
            }
        }

        public HTMLDocument glyphHammerAndWrench
        {
            get
            {
                bldDoc.Append(Glyphs.Tools.HammerAndWrench);
                return this;
            }
        }

        public HTMLDocument glyphKey
        {
            get
            {
                bldDoc.Append(Glyphs.DataAndDocuments.Key);
                return this;
            }
        }

        public HTMLDocument glyphLink
        {
            get
            {
                bldDoc.Append(Glyphs.DataAndDocuments.Hyperlinks.Link);
                return this;
            }
        }

        public HTMLDocument glyphLock
        {
            get
            {
                bldDoc.Append(Glyphs.Authorization.Lock);
                return this;
            }
        }

        public HTMLDocument glyphUnLock
        {
            get
            {
                bldDoc.Append(Glyphs.Authorization.UnLock);
                return this;
            }
        }

        public HTMLDocument glyphTrashcan
        {
            get
            {
                bldDoc.Append(Glyphs.VariousSigns.Trashcan);
                return this;
            }
        }

        public HTMLDocument glyphTriangularRuler
        {
            get
            {
                bldDoc.Append(Glyphs.Metrology.TriangularRuler);
                return this;
            }
        }

        public HTMLDocument glyphWarningSignRadioactivity
        {
            get
            {
                bldDoc.Append(Glyphs.VariousSigns.warningSignRadioactivity);
                return this;
            }
        }

        public HTMLDocument glyphToolbox
        {
            get
            {
                bldDoc.Append(Glyphs.Tools.Toolbox);
                return this;
            }
        }

        public HTMLDocument SquaredPlus
        {
            get
            {
                bldDoc.Append(Glyphs.Math.SquaredPlus);
                return this;
            }
        }

        public HTMLDocument CircledPlus
        {
            get
            {
                bldDoc.Append(Glyphs.Math.SquaredPlus);
                return this;
            }
        }

        public HTMLDocument HeavyPlus
        {
            get
            {
                bldDoc.Append(Glyphs.Math.HeavyPlus);
                return this;
            }
        }

        public HTMLDocument CircledMinus
        {
            get
            {
                bldDoc.Append(Glyphs.Math.circledMinus);
                return this;
            }
        }

        public HTMLDocument CircledS
        {
            get
            {
                bldDoc.Append(Glyphs.Shapes.Circled_S);
                return this;
            }
        }

        public HTMLDocument SquaredMinus
        {
            get
            {
                bldDoc.Append(Glyphs.Math.squaredMinus);
                return this;
            }
        }


        public HTMLDocument HeavyMinus
        {
            get
            {
                bldDoc.Append(Glyphs.Math.heavyMiuns);
                return this;
            }
        }


        public HTMLDocument glyphSigmaSumme
        {
            get
            {
                bldDoc.Append(Glyphs.Math.SigmaSumme);
                return this;
            }
        }



        /// <summary>
        /// Menge, Quantität
        /// </summary>
        public HTMLDocument glyphQty
        {
            get
            {
                bldDoc.Append(Glyphs.Math.Quantity);
                return this;
            }
        }


        public HTMLDocument glyphCheckMark
        {
            get
            {
                bldDoc.Append(Glyphs.Sets.Selections.checkMark);
                return this;
            }
        }

        public HTMLDocument glyphHeavyCheckMark
        {
            get
            {
                bldDoc.Append(Glyphs.Sets.Selections.heavyCheckMark);
                return this;
            }
        }

        public HTMLDocument glyphLightCheckMark
        {
            get
            {
                bldDoc.Append(Glyphs.Sets.Selections.lightCheckMark);
                return this;
            }
        }

        public HTMLDocument glyphDokuHaken
        {
            get
            {
                bldDoc.Append(Glyphs.DFC.DokuHaken);
                return this;
            }
        }

        public HTMLDocument glyphSparePart
        {
            get
            {
                bldDoc.Append(Glyphs.DFC.SparePart);
                return this;
            }
        }
    }
}
