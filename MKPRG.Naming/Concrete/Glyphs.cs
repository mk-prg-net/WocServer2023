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
    /// </summary>
    public static class Glyphs
    {

        public static string toStr(string Glyph)
        {
            return System.Net.WebUtility.HtmlDecode(Glyph);
        }

        public static class Access
        {
            public static string Save => Computer.FloppyDiskWhite;
            public static string Delete => VariousSigns.Trashcan;

            public static string Read => VariousSigns.Eye; // Edit.Higlight; 

            public static string Write => "&#x1F58E;";

            public static string New => LiveCycle.Create;

        }

        public static class Aerospace
        {
            public static string Rocket => "&#x1F680;";
            public static string RocketStarts => "&#x1F66D;";

            public static string Satellite => "&#x1F6E0;";

            public static string SupersonicJet => "&#x1F6E6;";
            public static string Jet => "&#x1F6E7;";
            public static string FlyingDisk => "&#x1F6F8;";

            public static string MoonLander => "&#x1F736;";

            public static string MoonLanderOfAliens => "&#x1F70E;";


            public static string TieFighter => "&#x29F2;";

            /// <summary>
            /// Star Wars Tie Jäger, schwarz
            /// </summary>
            public static string TieFighterBlack => "&#x29F3;";

            public static string TieFighter2 => "&#x1F725;";
            public static string TieFighter3 => "&#x1F726;";
            public static string SpaceStation => "&#x1F723;";

            public static string Enterprise => "&#x1F724;";
        }

        public static class Animals
        {
            public static string Frog => "&#x1F438;";

            public static string EgyptFalcon => "&#x13261;";

            public static string Rabbit => "&#x1F407;";

            public static string Snail => "&#x1F40C;";

            public static string Elephant => "&#x1F418;";

            public static string Bug => "&#x1F41B;";

            public static string BugEgypt => "&#x131A3;";

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
            public static string arrNW => @"&#x2196;";

            // Abknickende Pfeile
            public static string arrRightDown => "&#x21B4;";
            public static string arrDownLeft => "&#x21B5;";

            public static string arrLongUpLeft => "&#x21B0;";
            public static string arrLongUpRight => "&#x21B1;";

            public static string arrLongDownLeft => "&#x21B2;";
            public static string arrLongDownRight => "&#x21B3;";

            public static string dblArrRight => "&#x21D2;";
            public static string dblArrLeftFromBar => "&#x2906;";
            public static string dblArrRightFromBar => "&#x2907;";

            public static string longArrLeft => "&#x27F5;";
            public static string longArrRight => "&#x27F6;";

            public static string longDblArrLeft => "&#x27F8;";
            public static string longDblArrRight => "&#x27F9;";

            public static string longDblArrLeftRight => "&#x27FA;";

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

            public static string uTurnLeft => "&#x2B8C;";

            public static string uTurnRight => "&#x2B8E;";

            public static string uTurnUp => "&#x2B8D;";

            public static string uTurnDown => "&#x2B8F;";

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

            public static string Comet => "&#x2604;";
            public static string CometAlchemist => "&#x1F738;";
            public static string BlackStar => "&#x2605;";
            public static string WhiteStar => "&#x2606;";

            public static string DoubleStar => "&#x2051;";

            public static string ThreeStars => "&#x2042;";

            public static string WhiteStarEndlessBorderline => "&#x269D;";

            public static string BlackSun => "&#x2600;";
            public static string WhiteSun => "&#x263C;";
            public static string BrightSun => "&#x1F323;";

            public static string MoonQuaterLeft => "&#x263D;";
            public static string MoonQuaterRight => "&#x263E;";

            public static string MoonPhase0 => "&#x1F311;";
            public static string MoonPhase25Left => "&#x1F312;";
            public static string MoonPhase20Left => "&#x1F313;";
            public static string MoonPhase75Left => "&#x1F314;";
            public static string MoonPhase100 => "&#x1F315;";
            public static string MoonPhase75Right => "&#x1F316;";
            public static string MoonPhase50Right => "&#x1F317;";
            public static string MoonPhase25Right => "&#x1F318;";

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

            public static string optional => "&#x2325;";

            public static string select => Trees.MultiBranchUp;

            public static string Program => "&#x2338;"; //"&#x21E3;";

            public static string Sequnce => "&#x21E2;";

            public static string Sequnce2 => "&#x290F;";

            // Next

            public static string NextOp => "&#x2192;";

            public static string NextOpStop => "&#x21E5;";

            public static string NextOpIf => "&#x291E;";

            public static string Step => "&#x21B7;";  //"&#x130BD;";

            public static string Step2 => "&#x130BD;";


            public static string CallSub => "&#x21B4;";

            // Skip next
            public static string SkipNextOp => "&#x2933;";
            public static string SkipBackOp => "&#x2B3F;";

            public static string JumpForward => "&#x2BA3;"; //"&#x21B7;";

            public static string JumpBackward => "&#x2BA2;"; //"&#x21B6;";

            public static string Return => "&#x2923;";


            public static string Loop => "&#x2941;";
            public static string LoopBack => "&#x2940;";

            public static string LoopOpen => "&#x21BB;";

            public static string Repeat => ArrowsAndLines.uTurnLeft;

            public static string Swap => "&#x2928;";

            public static string Function => Math.Functions.Function;

            public static string CopyToLeft => "&#x2254;";

            public static string CopyToRight => "&#x2255;";

            public static string ShiftToLeft => "&#x226A;";

            public static string ShiftToRight => "&#x226B;";



        }

        public static class Authentication
        {
            public static string ID => Shapes.squaredIDSymbol;
            public static string Name => "&#x1302D;";

            public static string User => Peoples.Person;

            public static string Members => Peoples.Persons;            

            public static string Role => Chess.KnightWhite;
            public static string Roles => Chess.KnightBlack + Chess.PawnBlack;

            public static string AdminRole => Chess.QueenBlack;

            public static string Customer => VariousSigns.Crown;

        }

        public static class Authorization
        {

            public static string Access => Gestures.OpenHand;


            /// <summary>
            /// Vorhängeschloss geschlossen
            /// </summary>
            public static string Lock => "&#x1F512;";

            /// <summary>
            /// Vorhängeschloss geöffnet
            /// </summary>
            public static string UnLock => "&#x1F513;";

            public static string Forbidden => Traffic.ProhibitedSign; // "&#x1F6C7;";

            public static string Granted => Traffic.RightOfWaySign; // "&#x1F6C7;";

            public static string AdminRights => "&#x1F731;";

            public static string PermissionVector => $"{Math.Functions.OpenAngleArgList}{Math.r}|{Math.w}|{Math.x}{Math.Functions.CloseAngleArgList}";
        }


        public static class Blocks
        {
            public static string Full => "&#x2588;";

            public static string Left_3_4 => "&#x258A;";

            public static string Left_5_8 => "&#x258B;";

            public static string Left_1_2 => "&#x258C;";

            public static string Left_3_8 => "&#x258D;";

            public static string Left_1_4 => "&#x258E;";

        }

        public static class Chemistry
        {
            public static string Retorte => "&#x1F76D;";

            /// <summary>
            /// Reagenzglass
            /// </summary>
            public static string TestTube => "&#x1F9EA;";

            public static string PetriDish => "&#x1F9EB;";

            public static string DNA => "&#x1F9EC;";

            public static string Benzol => "&#x23E3;";

            public static string Reactor => "&#x2A50;";

            public static string ReactorEmpty => "&#x2A4C;";

            public static string Reaction => VariousSigns.Explosion;
        }

        public static class Chess
        {
            public static string PawnWhite => "&#x2659;"; //  Schach, Bauer
            public static string RookWhite => "&#x2656;"; //  Schach, Turm
            public static string KnightWhite => "&#x2658;"; //  Schach, Springer
            public static string BishopWhite => "&#x2657;"; //  Schach, Läufer
            public static string QueenWhite => "&#x2655;"; //  Schach, Dame weiss
            public static string KingWhite => "&#x2654;"; //  Schach, Dame weiss


            public static string PawnBlack => "&#x265F;"; //  Schach, Bauer
            public static string RookBlack => "&#x265C;"; //  Schach, Turm
            public static string KnightBlack => "&#x265E;"; //  Schach, Springer
            public static string BishopBlack => "&#x265D;"; //  Schach, Läufer
            public static string QueenBlack => "&#x265B;"; //  Schach, Dame weiss
            public static string KingBlack => "&#x265A;"; //  Schach, König schwarz

        }

        public static class ClientServer
        {
            public static string Upload => "&#x2912;";

            public static string Download => "&#x2913;";

            public static string Server => Computer.Server;

            public static string Client => Computer.oldPC;

            public static string ClientServerSymbol => Net.LAN;

        }

        public static class Colors
        {
            public static string ColorPalette = "&#x1F3A8;";
        }

        public static class Commerce
        {
            /// <summary>
            /// Einkaufswagen
            /// </summary>
            public static string Basket => "&#x1F6D2;";

            public static string MoneyBag => "&#x1F4B0;";


            public static string KaufmannMinus => "&#x2052;";

            public static string CreditCard => "&#x1F4B3;";

            public static class Currencies
            {

                public static string Euro => "&#x20AC;";
                public static string RupiePakistan => "&#x20A8;";
                public static string RupieIndian => "&#x20B9;";
                public static string Schekel => "&#x20AA;";
                public static string Won => "&#x20A9;";
                public static string Peso => "&#x20B1;";
                public static string Austral => "&#x20B3;";
                public static string Hrywnja => "&#x20B4;";
                public static string TurkLira => "&#x20BA;";
                public static string Rubel => "&#x20BD;";
                public static string BitCoin => "&#x20BF;";
            }
        }

        public static class Computer
        {

            /// <summary>
            /// Tastatur
            /// </summary>
            public static string KeyBoard => "&#x2328;";

            public static string Computermouse => "&#x1F5B0;";

            public static string ReturnKey => "&#x23CE;";

            public static string PC => "&#x1F5B3;";

            public static string oldPC => "&#x1F5B3;";

            public static string Server => "&#x1F5A5;";

            public static string PrinterBig => "&#x1F5A8;";

            public static string Printer => "&#x1F5B6;";


            public static string Computergame => "&#x1F3AE;";

            /// <summary>
            /// Bandlaufwerk
            /// </summary>
            public static string TapeOrStreamer => "&#x2707;";

            public static string EnterKey => "&#x2386;";

            public static string EnterKey2 => "&#x23CE;";

            public static string ReturnBackspaceKey => $"{Math.Functions.bracketOpen}{ReturnKey}{Math.Functions.bracketClosed}";

            public static string FloppyDiskWhite => "&#x1F5AB;";

            public static string FloppyDiskBlack => "&#x1F5AC;";

            public static string StreamerTape => "&#x1F5AD;";

            public static string DataTape => "&#x2707;";

            public static string HDD => "&#x1F5B4;";

            public static string Display => "&#x1F5B5;";

            public static string SpeechRecognition => "&#x1F5E3;";

        }

        public static class Communication
        {
            /// <summary>
            /// Sprechender Kopf, allgemeines Symbol für Kommunikation
            /// </summary>
            public static string SpeakingHead => "&#x1F5E3;";

            public static string Lips => "&#x1F5E2;";

            public static string SpeechBubbleRight => "&#x1F5E8;";
            public static string SpeechBubbleLeft => "&#x1F5E9;";
            public static string SpeechBubbleDialog => "&#x1F5EA;";
            public static string SpeechBubbleDiscussion => "&#x1F5EB;";


            public static string Telecommunication => Aerospace.Satellite;

            public static string Fax => "&#x213B;";
            public static string FaxIcon => "&#x1F5B7;";

            public static string Tel => "&#x2121;";

            public static string PhoneWhite => "&#x1F57E;";
            public static string Phone => "&#x1F57F;";

            public static string MobilePhone => "&#x1F4F1;";

            public static string TelBox => "&#x2706;";

            public static string Airmail => "&#x1F585;";

            public static string JapanesePostalSign => "&#x3036;";

            public static string JapanesePostOffice => "&#x1F3E3;";

            public static string EuropeanPostOffice => "&#x1F3E4;";

            public static string PostalHorn => "&#x1F4EF;";

            /// <summary>
            /// Brief
            /// </summary>
            public static string Mail => "&#x1F582;";

            public static string Send => "&#x1F585;";
            public static string Receive => "&#x1F4E1;";

            public static string Receiver => "&#x1F4FB;";

            public static string IncommingMail => "&#x1F4E8;";
            public static string DownloadMail => "&#x1F4E9;";

            public static string OutBox => "&#x1F4E4;";
            public static string InBox => "&#x1F4E5;";

            public static string Sender => "&#x3020;";

            public static string Send2 => "&#x1F4F6;";

            public static class Emails
            {
                public static string Email => "&#x1F4E7;";

                public static string Mailbox => "&#x1F4EA;";

                public static string Attachment => "&#x1F4CE;";
            }


        }

        public static class Culture
        {
            public static class Language
            {
                public static string CNT => "**";                       // Joker
                public static string Chiniese => "&#x1F1E8;&#x1F1F3;";  // 🇨🇳 
                public static string German => "&#x1F1E9;&#x1F1EA;";    // 🇩🇪
                public static string English => "&#x1F1EC;&#x1F1E7;";   // 🇪🇳
                public static string Spanish => "&#x1F1EA;&#x1F1F8;";   // 🇪🇸
                public static string Polish => "&#x1F1F5;&#x1F1F1;";    // 🇵🇱
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
            public static string DocumentWithImage2 => "&#x1F5BB;";

            public static string Scroll => "&#x1F4DC;";

            public static string FileStore => "&#x1F5C4;";

            public static string CardIndex => "&#x1F5C2;";


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

                public static string DataCompression => "&#x1F5DC;";


            }

            public static class Hyperlinks
            {
                public static string Link => "&#x1F517;";

                public static string Anchor => Glyphs.VariousSigns.Anchor;
            }

            public static class SemanticMarkup
            {
                public static string Article => "§";

                public static string TextParagraph => "&#x00B6;";

                public static string DetailInformations => Shapes.StarFourPointed;
            }


            /// <summary>
            /// Datenschutz
            /// </summary>
            public class Protection
            {
                public static string DataProtection => VariousSigns.Regenschirm;

                public static string AntiVirus => War.Protector;
                public static string LossOfData => VariousSigns.Skull;
                public static string Sign => "&#x2711;";
                public static string Signed => "&#x1F396;";
                public static string DigitallySigned => "&#x1F50F;";

                public static string SafeArea => War.Fortress;
            }

            public class Classification
            {
                public static string ProtectedByCopyright => LawAndOrder.Copyright;

                public static string Confidential => War.Protector;

                public static string TopSecret => War.ProtectorWithBlackCross;

                public static string TradeSecret => War.Protector;

                public static string Public => VariousSigns.Bierzelt;

            }

            public class Workflow
            {
                public static string New => $"{LiveCycle.Create_Add}{DocumentEmpty}";
                public static string OpenForRead => $"{VariousSigns.Eye}{DocumentWithText}";
                public static string OpenForWrite => $"{Access.Write}{DocumentWithText}";
                //public static string Close => Sets.Selections.
            }

        }

        public static class DateAndTime
        {
            public static string Time => Metrology.StopWatch;

            public static string Date => "&#x1F4C5;";

            public static string Day => "&#x263C;";

            public static string Night => "&#x263E;";

            public static string Pre => "&#x21E5;";

            public static string Post => "&#x27FC;";
        }


        public static class DocuTerms
        {
            // ᚥ
            public static string DocuTermSign => "&#x16A5;"; //"&#x1D53B;&#x1D54B;";

            // 🗲ᚥ
            public static string InvalidDocuTerm => $"{Validation.Invalid}&#x16A5;";

            // ᛭
            public static string Comment => "&#x16ED;";

            // ᛚ unary Function Prefix
            public static string UnaryFunction => "&#x16DA;";

            // ᛝ 
            public static string Instance => "&#x16DD;";

            // ᛖ
            public static string Method => "&#x16D6;";

            // ᚪ 
            //public static string Function => "&#x16AA;";

            // ᛏ 
            public static string Return => "&#x16CF;";

            // ᛪ 
            public static string Event => "&#x16EA;"; 

            // ᛜ 
            public static string Property => "&#x16DC;";

            // ᚲ Get Property Value: Getter            
            public static string GetPropVal => "&#x16B2;";

            // ᚹ
            public static string ListBegin => "&#x16B9;";

            // ᛒ
            public static string TextBegin => "&#x16D2;";

            // ᛩ
            public static string ListEnd => "&#x16E9;";

            // ᚦ
            public static string Date => "&#x16A6;";

            // ᛠ
            public static string Time => "&#x16E0;";

            // ᚠ
            public static string Version => "&#x16A0;";

            // ᛔ
            public static string BoolPrefix => "&#x16D4;";

            // ᛕ
            public static string IntPrefix => "&#x16D5;";

            // ᚱ rational Number: is a pair of nominator and denominator
            public static string RationalNumber => "&#x16B1;";

            // ᚪ 
            public static string FloatingPointNumber => "&#x16AA;";

            // ᚻ  //ᛞ
            public static string NID_Prefix => "&#x16BB;";

            // ᛥ Array of Basic Values
            public static string ArrayPrefix => "&#x16E5;";

            // ᛊ Array Pic Value at Index Operator
            public static string ArrayPicValAtIx => "&#x16CA;";

            // ᛍ
            public static string NameWildcard => "&#x16CD;";

            // ᛫
            public static string ValueWildcard => "&#x16EB;";

            // ᛯ Semantic Reference between Naming Containers
            public static string SemanticRef => "&#x16EF;";           

        }

        /// <summary>
        /// ᚾᚤᛏ = Nyt (die nützliche) Flussname im Lied der Grímnismál (Edda): https://de.wikipedia.org/wiki/Liste_der_Fl%C3%BCsse_im_Lied_Gr%C3%ADmnism%C3%A1l
        /// 
        /// Martin Korneffel, 20.12.2023
        /// Schlüsselwörter der formalen Sprache zur Beschreibung von Datenflussgraphen, NYT
        /// 
        /// </summary>
        public static class NYT
        {
            // ᛭
            public static string Comment => "&#x16ED;";

            // ᚥ W Stack Array Ref
            public static string StackArrayRef => "&#x16A5;";

            // ᚤ Y Array Beginn
            public static string YArrayBegin => "&#x16A4;";

            // ᛟ OTHALAN Define
            public static string OthalanDefine = "&#x16DF;";

            // ᛡ IOR Dereference
            public static string IorDereference = "&#x16E1;";

            // ᛚ L unary Function Prefix
            public static string UnaryFunction => "&#x16DA;";

            // ᛣ CALC Data Flow Stage begin
            public static string CalcBeginStage => "&#x16E3;";

            // ᛉ EOLHX Data Flow Stage end
            public static string EolhxEndStage => "&#x16C9;";

            // ᛋ SIEGEL Branch
            public static string SiegelBranch => "&#x16CB;";

            // ᛊ SOWILO Branch
            public static string SowiloBranch => "&#x16CA;";

            // ᛏ TYR
            public static string Return => "&#x16CF;";

            // ᛇ IWAZ StringBegin
            public static string IwazStringBegin => "&#x16C7;";

            // ᛢ CWEORTH StringCat
            public static string CweorthStringCat => "&#x16E2;";


            // ᚹ WYNN List Begin
            public static string WynnListBegin => "&#x16B9;";

            // ᛒ BJARKAN Bool
            public static string BjarkanBool => "&#x16D2;";

            // ᛩ Q List End
            public static string QListEnd => "&#x16E9;";


            // ᛠ
            public static string Time => "&#x16E0;";

            // ᚠ FEHU Hierarchical List
            public static string FehuHirachy => "&#x16A0;";

            // ᛕ P Cardinal Number
            public static string PIntPrefix => "&#x16D5;";

            // ᚱ RAD rational Number: is a pair of nominator and denominator
            public static string RadNomDenom => "&#x16B1;";

            // ᚪ Ac Floating Point Number
            public static string AcFloatingPointNum => "&#x16AA;";

            // ᚻ  Haegl Naming ID
            public static string HaeglNID => "&#x16BB;";

            // ᛯ TVIMADUR Semantic Reference between Naming Containers
            public static string TvimadurSemanticRef => "&#x16EF;";

        }


        public static class Edit
        {
            public static string DelRight => "&#x2326;";
            public static string Del => "&#x2327;";
            public static string Keyboard => "&#x2328;";
            public static string DelLeft => "&#x232B;";

            public static string EnterKey => "&#x23CE;";

            public static string Insert => "&#x2380;";

            public static string Higlight => "&#x2383;";
            public static string Underline => "&#x2381;";

            public static string ClearScreen => "&#x239A;";
            public static string PrintScreen => "&#x2399;";

            public static string NewLineCarriageReturn => "&#x2B92;";

        }

        public static class Education
        {
            public static string GraduationCap => "&#x1F393;";

        }

        public static class ElectricalEngineering
        {

            public static string ElectricalFlash => "&#x2301;";

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

            public static string Wave => "&#x301C;";
            public static string Waves => "&#x3030;";

        }

        public static class Engineering
        {
            /// <summary>
            /// Allgemeines Symbol für die Konstruktion
            /// </summary>
            public static string Construction => Metrology.TriangularRuler;

            public static string Zirkel => "&#x2222;";

            public static string Zirkel3 => "&#x29A0;";

            /// <summary>
            /// Bleistift
            /// </summary>
            public static string Pencil => "&#x1F589;";

            public static string ControlKnobs => "&#x1F39B;";

            public static string LevelSlider => "&#x1F39A;";
        }

        public static class Events
        {

            /// <summary>
            /// Fehlersymbol
            /// </summary>
            public static string Error => Weather.flash;

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

            public static string WorldMap => "&#x1F5FA;";
        }

        public static class Gestures
        {

            public static string OpenHand => "&#x1F590;";

            public static string ThumbsUp => "&#x1F592;";
            public static string ThumbsUpFilled => "&#x1F44D;";


            public static string ThumbsDown => "&#x1F593;";
            public static string ThumbsDownFilled => "&#x1F44E;";

            public static string ForefingerRight => "&#x261E;";

            public static string ForefingerLeft => "&#x261C;";

            public static string ForeFingerUp => "&#x261D;";

            public static string ForeFingerDown => "&#x261F;";
        }

        public static class LawAndOrder
        {
            public static string Paragraph => "&#x00A7;";

            public static string Copyright => "&#x00A9;";

            public static string CopyrightAudio => "&#x2117;";

            public static string RegisteredMark => "&#x00AE;";

            public static string Trademark => "&#x2122;";
        }

        public static class LiveCycle
        {
            public static string LiveCycleSign => $"{Creator}{Algorithm.Sequnce}{Death}";

            public static string Creator => Astronomy.BlackSun; //"&#x1304F;";

            public static string Create => "&#x1F5E4;";
            public static string Create2 => "&#x15C8;";
            public static string Create_PlusInsideU => "&#x228E;";
            public static string Create_CircledPlus => "&#x2295;";

            public static string Create_Add => Math.Groups.HeavyPlus;

            public static string Kill => Sets.Selections.checkNegativeMark;
            public static string Death => VariousSigns.LatinCross; // "&#x13062;";

        }

        public static class Mahjongg
        {
            public static string RedDragon => "&#x1F004;";

            public static string Spring => "&#x1F026;";

            public static string Summer => "&#x1F027;";

            public static string Autumn => "&#x1F028;";

            public static string Winter => "&#x1F029;";
        }

        public static class Math
        {
            public static string Count => "&#xFF03;"; // ＃

            public static string Inifnit => "&#x221E;";

            public static string EndOfProof => "&#x220E;";

            public static string Alpha => "&#x1D6C2;";

            public static string A => "&#x1D400;";

            public static string B => "&#x1D401;";

            public static string C => "&#x1D402;";

            public static string D => "&#x1D403;";

            public static string E => "&#x1D404;";

            public static string F => "&#x1D405;";

            public static string G => "&#x1D406;";

            public static string M => "&#x1D40C;";

            public static string R => "&#x1D411;";

            public static string S => "&#x1D412;";

            public static string T => "&#x1D413;";

            public static string ASansSerif => "&#x1D5D4;";

            public static string MSansSerif => "&#x1D5E0;";

            public static string AFrac => "&#x1D56C;";

            public static string Rfrac => "&#x1D57D;";

            public static string Sfrac => "&#x1D57E;";

            public static string Tfrac => "&#x1D57F;";

            public static string Mfrac => "&#x1D578;";

            public static string BScript => "&#x212C;";

            public static string EScript => "&#x2130;";

            public static string FScript => "&#x2131;";

            public static string LScript => "&#x2112;";

            public static string RScript => "&#x211B;";

            public static string MScript => "&#x2133;";

            public static string a => "&#x1D44E;";

            public static string b => "&#x1D44F;";

            public static string c => "&#x1D450;";

            public static string d => "&#x1D451;";

            public static string e => "&#x1D452;";

            public static string f => "&#x1D453;";

            public static string g => "&#x1D454;";

            public static string i => "&#x1D456;";

            public static string j => "&#x1D457;";

            public static string k => "&#x1D458;";

            public static string l => "&#x1D459;";

            public static string m => "&#x1D45A;";

            public static string n => "&#x1D45B;";

            public static string o => "&#x1D45C;";

            public static string p => "&#x1D45D;";

            public static string q => "&#x1D45E;";

            public static string r => "&#x1D45F;";

            public static string s => "&#x1D460;";

            public static string t => "&#x1D461;";

            public static string u => "&#x1D462;";

            public static string v => "&#x1D463;";

            public static string w => "&#x1D464;";

            public static string x => "&#x1D4CD;";

            public static string y => "&#x1D4CE;";

            public static string z => "&#x1D4CF;";

            public static string aVect => "&#x1D51E;";

            public static string bVect => "&#x1D51F;";

            public static string cVect => "&#x1D520;";

            public static string uVect => "&#x1D532;";

            public static string vVect => "&#x1D533;";

            public static string wVect => "&#x1D534;";

            public static string xVect => "&#x1D535;";

            public static string yVect => "&#x1D536;";

            public static string zVect => "&#x1D537;";


            public static string xBold => "&#x1D605;";

            public static string PiBold => "&#x1D77F;";

            public static string DeltaOp => "&#x1D6E5;";

            /// <summary>
            /// Eingekreistes S
            /// </summary>
            public static string CircledS => "&#x24C8;";


            /// <summary>
            /// Nummero- Zeichen
            /// </summary>
            public static string No => "&#x2116;";

            public static string Definition => "&#x1D46B;&#x1D486;&#x1D487;";

            public static string QED => "&#x16DC;";

            public static string AllQuantor => "&#x2200;";

            public static string ExistQuantor => "&#x2203;";

            public static string notExistQuantor => "&#x2204;";


            public static string crossProductOp => "&#x2A09;";

            /// <summary>
            /// Ring- Operator (Produkt von Funktionen)
            /// </summary>
            public static string FunctionMul => "&#x2218;";

            /// <summary>
            /// Kräftiges Minus
            /// </summary>
            public static string heavyMiuns => "&#x2796;";

            public static string SigmaSumme => "&#x03A3;";

            public static string Quantity => SigmaSumme; //"#";

            public static string Calculator => "&#x1F5A9;";

            /// <summary>
            /// 
            /// </summary>


            public static string SquareRoot => "&#x221A;";

            public static string CubeRoot => "&#x221B;";

            public static string ForthRoot => "&#x221C;";

            public static class Functions
            {
                public static string Sinus => "&#x223F;";

                // Listen von Funktionsargumenten
                public static string OpenArgList => "(";
                public static string CloseArgList => ")";

                public static string OpenAngleArgList => "&#x27E8;";
                public static string CloseAngleArgList => "&#x27E9;";


                public static string bracketOpen => "[";
                public static string bracketClosed => "]";

                public static string hexgonBracketOpen => "&#xFE5D;";
                public static string hexgonBracketClosed => "&#xFE5E;";

                public static string Domain => "&#x1F74E;";
                public static string Domain2 => "&#x1F746;";
                public static string CoDomain => Domain;

                /// <summary>
                /// Zurodnung
                /// </summary>
                public static string Mapping => "&#x22B7;";

                public static string Function => "&#x2394;";

                /// <summary>
                /// Funktionssymbol f
                /// </summary>
                public static string FunctionF => "&#x1D453;";

            }


            public static class Relations
            {
                public static string identical => "&#x2261;";

                public static string notIdentical => "&#x2262;";

                public static string equal => "=";

                public static string notEqual => "&#x2260;";

                public static string lower => "<";

                public static string lowerOrEqual => "&#x2266;";

                public static string greater => ">";

                public static string greaterOrEqual => "&#x2267;";

                public static string between => "&#x226C;";

            }

            public static class Sets
            {
                // Mengen

                public static string Set => $"{OpenSet}{Ellipsis}{CloseSet}"; //"&#x1F74E;";

                public static string OpenSet => "&#x007B;";
                public static string CloseSet => "&#x007D;";

                public static string Ellipsis => "&#x2026;";

                public static string EmptySet => "&#x2205;";

                public static string UnknownElement => "&#x2048;";

                public static string Boolean => "&#x1D539;";

                public static string NaturalNumbers => "&#x2115;";

                public static string Integers => "&#x2124;";

                public static string RationalNumers => "&#x211A;";

                public static string RealNumbers => "&#x211D;";

                public static string ComplexNumbers => "&#x2102;";


                public static string subsetOf => "&#x2282;";

                public static string supersetOf => "&#x2283;";

                public static string IsElementOf => "&#x2208;";

                public static string notIsElementOf => "&#x2209;";

                public static string union => "&#x222A;";

                public static string intersect => "&#x2229;";


                public static string unionMultiple => "&#x22C3;";

                public static string intersectMultiple => "&#x22C2;";


                public static string NotOutOfRange => $"{x} {IsElementOf} {RScript}";

                public static string OutOfRange => $"{x} {notIsElementOf} {RScript}";

                public static string NotANumber => $"{x} {notIsElementOf} {RationalNumers}";

                public static string NotAnInt => $"{x} {notIsElementOf} {Integers}";

                public static string FilterOp => "&#x2B44;";

                public static string SortAsc => "&#x29C0;";

                public static string SortDesc => "&#x29C1;";


            }

            public static class Bool
            {
                public static string Boolean => " &#x1D539;";

                // ⊨
                public static string True => "&#x22A8;";

                // ⊭
                public static string False => "&#x22AD;";


                public static string and => "&#x2227;";
                public static string or => "&#x2228;";
                public static string not => "&#x223C;";

                public static string implication => "&#x21D2;";
            }

            public static class Groups
            {

                public static string LinkOp => "&#x2218;";

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
                public static string CircledMinus => "&#x2296;";

                /// <summary>
                /// Eingerahmtes Minus
                /// </summary>
                public static string SquaredMinus => "&#x229F;";

                public static string SquaredMul => "&#x22A0;";

                public static string SquaredPoint => "&#x22A1;";

                /// <summary>
                /// Eingekreistes Pluszeichen
                /// </summary>
                public static string CircledPlus => "&#x2295;";


                public static string CircledPoint => "&#x2299;";

                public static string CircledRing => "&#x229A;";

                public static string CircledMul => "&#x229B;";

                public static string CircledEqual => "&#x229C;";
            }


            public static class CircledNumbers
            {
                public static string One => "&#x2460;";
                public static string Two => "&#x2461;";
                public static string Three => "&#x2462;";
                public static string Four => "&#x2463;";
                public static string Five => "&#x2464;";
                public static string Six => "&#x2465;";
                public static string Seven => "&#x2466;";
                public static string Eight => "&#x2467;";
                public static string Nine => "&#x2468;";
                public static string Ten => "&#x2469;";
                public static string Eleven => "&#x246A;";
                public static string Twelf => "&#x246B;";
                public static string Thirdteen => "&#x246C;";
                public static string Fourteen => "&#x246D;";
                public static string Fiveteen => "&#x246E;";
                public static string Sixteen => "&#x246F;";
                public static string Seventeen => "&#x2470;";
                public static string Eightteen => "&#x2471;";
                public static string Nineteen => "&#x2472;";
                public static string Twenty => "&#x2473;";

            }

            public static class RationalNumbers
            {
                public static string OneSeventh => "&#x2150;";
                public static string OneNinth => "&#x2151;";
                public static string OneTenth => "&#x2152;";
                public static string OneThird => "&#x2153;";
                public static string TwoThird => "&#x2154;";
                public static string OneFifth => "&#x2155;";
                public static string TwoFifth => "&#x2156;";
                public static string ThreeFifth => "&#x2157;";
                public static string FourFifth => "&#x2158;";
                public static string OneSixth => "&#x2159;";
                public static string FifeSixth => "&#x215A;";
                public static string OneEigth => "&#x215B;";
                public static string ThreeEigth => "&#x215C;";
            }

            public static class RomanNumbers
            {
                public static string One => "&#x2160;";
                public static string Two => "&#x2161;";
                public static string Three => "&#x2162;";
                public static string Four => "&#x2163;";
                public static string Five => "&#x2164;";
                public static string Six => "&#x2165;";
                public static string Seven => "&#x2166;";
                public static string Eight => "&#x2167;";
                public static string Nine => "&#x2168;";
                public static string Ten => "&#x2169;";
                public static string Eleven => "&#x216A;";
                public static string Twelve => "&#x216B;";
                public static string Fifty => "&#x216C;";
                public static string OneHundred => "&#x216D;";
                public static string FiveHundret => "&#x216E;";
                public static string Thousand => "&#x216F;";

                public static string OneSm => "&#x2170;";
                public static string TwoSm => "&#x2171;";
                public static string ThreeSm => "&#x2172;";
                public static string FourSm => "&#x2173;";
                public static string FiveSm => "&#x2174;";
                public static string SixSm => "&#x2175;";
                public static string SevenSm => "&#x2176;";
                public static string EightSm => "&#x2177;";
                public static string NineSm => "&#x2178;";
                public static string TenSm => "&#x2179;";
                public static string ElevenSm => "&#x217A;";
                public static string TwelveSm => "&#x217B;";
                public static string FiftySm => "&#x217C;";
                public static string OneHundredSm => "&#x217D;";
                public static string FiveHundredSm => "&#x217E;";
                public static string ThousandSm => "&#x217F;";

            }

            public static class Probabilistic
            {
                public static (string glyph, string html) GameDie => ("🎲", "&#x1F3B2;");

                public static (string glyph, string html) DieFace1 => ("⚀", "&#x2680;");
                public static (string glyph, string html) DieFace2 => ("⚁", "&#x2681;");
                public static (string glyph, string html) DieFace3 => ("⚂", "&#x2682;");
                public static (string glyph, string html) DieFace4 => ("⚃", "&#x2683;");
                public static (string glyph, string html) DieFace5 => ("⚄", "&#x2684;");
                public static (string glyph, string html) DieFace6 => ("⚅", "&#x2685;");
            }

        }

        public static class MechanicalEngineering
        {
            public static string Gear => Tools.Gear;

            public static string PlanetGear => "&#x1104D;";

            public static string PlanetGearSmallSatellites => "&#x1F71B;";

            public static string BigAndSmallGear => "&#x29C2;";


            public static string Transmission => "&#x1F73D;";

        }

        public static class Metrology
        {
            public static string StopWatch => "&#x23F1;";


            public static string Timer => "&#x23F2;";

            public static string Hourglass => "&#x231B;";

            public static string TriangularRuler => "&#x1F4D0;";

            public static string Promille => "&#x2030;";
            public static string Pro10000 => "&#x2031;";


            public static string PromilleArab => "&#x0609;";
            public static string Pro10000Arab => "&#x060A;";

            /// <summary>
            /// Lineal/Maßstab
            /// </summary>
            public static string Ruler => "&#x1F4CF;";

            public static string Balance => "&#x2696;";

            public static class Temperature
            {

                public static string GradCelsius => "&#x2103;";

                public static string GradFahrenheit => "&#x2109;";
            }

            public static class Mass
            {
                public static string Ounce => "&#x2125;";
            }


            public static class Length
            {
                public static string nm => "&#x339A;";
                public static string micrometer => "&#x339B;";
                public static string mm => "&#x339C;";
                public static string cm => "&#x339D;";
                public static string km => "&#x339E;";
            }

            public static class Area
            {
                public static string mm2 => "&#x339F;";
                public static string cm2 => "&#x33A0;";
                public static string m2 => "&#x33A1;";
                public static string km2 => "&#x33A2;";
            }

            public static class Volume
            {
                public static string mm3 => "&#x33A3;";
                public static string cm3 => "&#x33A4;";
                public static string m3 => "&#x33A5;";
                public static string km3 => "&#x33A6;";
            }

            public static class Time
            {
                public static string ps => "&#x33B0;";
                public static string ns => "&#x33B1;";
                public static string micros => "&#x33B2;";
                public static string ms => "&#x33B3;";
            }
        }

        public static class NamingContainers {
            /// <summary>
            /// 🄽
            /// </summary>
            public static string NamingContainer => "&#x1F13D;";

            /// <summary>
            /// 🆔
            /// </summary>
            public static string NamingId => "&#x1F194;";
        }

        public static class Navigation
        {

            public static string PrevPage => "&#x2397;";
            public static string NextPage => "&#x2398;";

            public static string GotoEnd => "&#x2B72;";
            public static string GotoStart => "&#x2B70;";

            public static string FastForward => "&#x23F5;";
            public static string FastBackward => "&#x23F4;";

            public static string SkipToBeginning => "&#x23EE;";
            public static string SkipToEnd => "&#x23ED;";

            public static string Play => "&#x23F5;";
            public static string StopPlay => "&#x23F9;";

            public static string Steuerrad => "&#x2388;";



        }

        public static class Net
        {
            public static string Cloud => Weather.Cloud;

            public static string LAN => "&#x1F5A7;";
        }

        public static class Peoples
        {
            public static string Person => "&#x1F464;";

            public static string Persons => "&#x1F465;";

            public static string Boy => "&#x1F466;";

            public static string Girl => "&#x1F467;";

            public static string Man => "&#x1F468;";

            public static string Woman => "&#x1F469;";

            public static string Family => "&#x1F46A;";

        }

        public static class Physics
        {
            public static string Atom => "&#x269B;";
        }

        public static class Places
        {
            public static string Camping => "&#x1F3D5;";
            public static string Plage => "&#x1F3D6;";
            public static string ConstructionSite => "&#x1F3D7;";
            public static string SkyLine => "&#x1F3D9;";
            public static string ClassicBuilding => "&#x1F3DB;";
            public static string Dessert => "&#x1F3DC;";
            public static string Island => "&#x1F3DD;";
            public static string Park => "&#x1F3DE;";
            public static string Stadion => "&#x1F3DF;";
            public static string Home => "&#x1F3E0;";
            public static string HomeWithGarden => "&#x1F3E1;";
            public static string Office => "&#x1F3E2;";

            public static string Bierzelt => VariousSigns.Bierzelt;

            public static string Cinema => "&#x1F3A6;";
        }

        public static class Runtime
        {
            public static string Environment => $"&#x1F733;";

            public static string NewEnvironment => "&#x2312;";

            public static string EnvironmentAsRoof => $"&#x1F702;";

            public static string Session => $"[{Session2}]";
            public static string Session2 => $"&#x1F73A;";

            public static string SessionUnderRoof => "&#x1F70E;";

            public static string SessionId => Authentication.ID;

            public static string SessionStart => $"{VariousSigns.Explosion}{Session2}"; //"&#x1F719;";

            public static string SessionEnd => $"{Session2}|"; //"&#x1F750;";


            public static string ProcessWithThread => Math.Groups.CircledPlus;

            public static string ProcessBegin => $"{VariousSigns.Explosion}{ProcessWithThread})";

            public static string ProcessEnd => $"{ProcessWithThread}{Stop}";

            public static string ProcessAndContinue => "&#x27F4;";

            public static string ProcessAndBack => "&#x2B32;";

            public static string CircularProcess => "&#x2B94;"; // "&#x27F2;";


            public static string Start => Aerospace.Rocket; // Aerospace.RocketStarts;

            public static string Progress => $"{Blocks.Left_1_2}{Blocks.Left_1_4}";

            public static string Stop => Traffic.StopSign; //"&#x1F6CB;";

            public static string RuntimeError => $"{Threads.Thread}{Weather.flash}";

            public static string Execute => Computer.EnterKey;

            public static string Cancel => "&#x2418;";
            public static string CancelX => "&#x1F5D9;";

            /// <summary>
            /// Symbolisiert einen abgebrochnen Prozess
            /// </summary>
            public static string Aborted => Threads.ThreadAbort;

            public static string Finished => Threads.ThreadStop;

            public static string Tracing => "&#x1F4DC;";

            public static string Monitoring => "&#x1F4DD;";

            public static string StartMonitoringOrTracing => "&#x1F3AC;";


            public static string Job => "&#x2752;";

            public static string Job2 => "&#x1F4E6;";

        }


        public static class Sets
        {
            public static class Operators
            {
                public static string ElementOf => "&#x2208;";
                public static string NotElementOf => "&#x2209;";

                public static string Intersection => "&#x2229;";
                public static string Union => "&#x222A;";

                public static string LeftIncludedInRight => "&#x2282;";
                public static string LeftIncludesRight => "&#x2283;";

                public static string LeftNotIncludedInRight => "&#x2284;";
                public static string LeftNotIncludesRight => "&#x2285;";


                public static string DeleteLeft => "&#x232B;";
                public static string DeleteRight => "&#x2326;";

                public static string SortAscending => "&#x2343;";
                public static string SortDescending => "&#x2344;";

            }

            /// <summary>
            /// Allgemeine Liste
            /// </summary>
            public static string List => "&#x25A4;";

            /// <summary>
            /// Tabelle, Relation
            /// </summary>
            public static string Table => "&#x25A6;";



            /// <summary>
            /// Datensatz
            /// </summary>
            public static string Records => "&#x25A5;";

            public static string Palette => "&#x1F3A8;";



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

                public static string checkedBox => "&#x1F5F9;";
                public static string checkedNegativeCheckBox => "&#x2612;";

                public static string uncheck => "&#x237B;";

                public static string checkNegativeMark => "&#x2716;";
                public static string checkNegativeMarkKurisve => "&#x2717;";




            }

            public static class Quantors
            {
                public static string Exists => "&#x2203;";
                public static string NotExists => "&#x2204;";

                public static string ForEach => "&#x2200;";

            }


        }

        public static class Shapes
        {
            public static string KaroImQuadrat => "&#x26CB;";


            public static string UpperRightShadowedWhiteSqare => "&#x2752;";

            public static string JoinedSquares => "&#x29C9;";

            public static string SquaredStar => "&#x29C6;";

            public static string SquaredCircle => "&#x29C7;";

            public static string SquaredSquare => "&#x29C8;";

            public static string SquaredSpiral => "&#xA874;";

            public static string SquaredPositionMarks => "&#x2BD0;";

            public static string SquareDiagonal => "&#x303C;";

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

        /// <summary>
        /// Allgemein Hervorhebenungen, Warnungen, Fehlermeldungen
        /// </summary>
        public static class Signalization
        {
            public static string ErrorOccured => Weather.flash;

            public static string Alarm => VariousSigns.Alarm;

            public static string Trumpet => "&#x1F3BA;";

            public static string Melody => "&#x1F3B6;";

            public static string Alarm2 => "&#x1F6A8;";

            public static string Attention => Gestures.ForeFingerUp;

            public static string Warning => VariousSigns.WarningSign;

            public static string PleaseNote => Gestures.ForefingerRight;

            public static string Danger => VariousSigns.Explosion;

            public static string WarningUnderConstruction => VariousSigns.UnderConstruction;

            public static string SupportNeeded => Shapes.squaredSOSSymbol;

            public static string NewItem => Shapes.squaredNewSymbol;

            /// <summary>
            /// Gibt es für umsonst
            /// </summary>
            public static string ItsForFree => Shapes.squaredFreeSymbol;

            public static string ItsOk => Shapes.squaredOKSymbol;

            public static string TargetReachedFlag => "&#x1F3C1;";
        }

        public static class Smileys
        {
            public static string Smiley => "&#x263A;";
            public static string SmileyBlack => "&#x263B;";

            public static string NonSmiley => "&#x2639;";
        }

        public static class Software
        {
            public static string SoftwareEngineering => "&#x29F0;";

            public static string Compiler => $"{Math.Alpha}{Math.Functions.Mapping}{Math.AFrac}";

            public static string APLFunctionUnderlined => "&#x236E;";

            public static string SyntaxError => $"&#x2376;{Weather.flash}";

            public static string Debugger => Animals.BugEgypt;

            public static string Interface => "&#x16DE;";

            public static string Interface2 => "&#x16E5;";

            public static string Assembly => Shapes.KaroImQuadrat;

            public static string Modul => "&#x16DC;";

            public static string Modular => Shapes.SquareDiagonal;

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
            public static string SPC => "&#xA0;";
            public static string SPCsmall => "&#8239;";
        }

        public static class TicketSystem
        {
            public static string Tickets => "&#x1F39F;";
        }

        public static class Threads
        {

            public static string Thread => "&#x27FF;";

            public static string ThreadStart => $"{VariousSigns.Explosion}{Thread}";

            public static string ThreadStop => $"{Thread}{Runtime.Stop}";

            public static string ThreadAbort => $"{Thread}{Runtime.CancelX}";

            public static string ThreadVertical => "&#x1D184;";

            public static string Join => "&#x2B43;";

            public static string parallelExec => "&#x2B86;";

            public static string massiveParallelExec => "&#x21F6;";
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

            public static string HammerAndHammer => "&#x2692;";

            public static string Hammer => "&#x1F528;";

            public static string Spitzhacke => "&#x26CF;";

            public static string Chains => "&#x26D3;";

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

            public static string ProhibitedSign2 => "&#x1F6C7;";

            public static string ProhibitedSign => "&#x1F6C7;";
            public static string RightOfWaySign => "&#x1F79B;";

            public static string StopSign => "&#x1F6D1;";



        }

        public static class Transactions
        {
            public static string Transaction => Runtime.Session2;

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


            public static string Rollback => "&#x238C;";

            public static string Cancel => Runtime.Aborted;

        }

        public static class Trees
        {
            public static string Tree => "&#x2F4A;"; //"&#x312D;"; "&#x2C00"; "&#x1210;";

            public static string TreeUp => "&#x15D0;";

            public static string TreeUpWithNodes => "&#x1F762;";

            public static string BranchUp => "&#x1F709;";

            public static string BranchDown => "&#x1F753;";

            public static string MultiBranchUp => "&#x10299;";


            public static string TreeDown => "&#x15D1;";

            public static string TreeLeft => "&#x15D2;";

            public static string TreeRight => "&#x15D5;";

            public static string TreeRightWithoutRoot => "&#x269F;";

            public static string TreeLeftWithoutRoot => "&#x269E;";

            public static string TridentDown => "&#x16E3;";

            public static string RoundTridentDown => "&#x16E6;";

            public static string TridentUp => "&#x16C9;";

            public static string RoundTridentUp => "&#x16D8;";

            public static string TridentUpDown => "&#x16EF;";

            public static string RoundTriBranchUp => "&#x16A0;";

            public static string Root => "&#x1430;"; //  Math.CubeRoot;
        }

        public static class Validation
        {
            public static string Check => VariousSigns.Microscope; //"&#x1FA7A;";

            public static string CheckPreCond => VariousSigns.Microscope + DateAndTime.Pre;

            public static string CheckPostCond => DateAndTime.Post + VariousSigns.Microscope;

            public static string CheckNotOutOfRange => $"{Check}: {Math.x} {Math.Sets.IsElementOf} {Math.RScript}";

            public static string CheckOutOfRange => $"{Check}: {Math.x} {Math.Sets.notIsElementOf} {Math.RScript}";

            public static string NotOutOfRange => $"{Math.x} {Math.Sets.IsElementOf} {Math.RScript}";

            public static string OutOfRange => $"{Math.x} {Math.Sets.notIsElementOf} {Math.RScript}";

            public static string NotANumber => $"{Math.x} {Math.Sets.notIsElementOf} {Math.Sets.RationalNumers}";

            public static string NotAnInt => $"{Math.x} {Math.Sets.notIsElementOf} {Math.Sets.Integers}";

            public static string Valid => "&#x1F5F8;";

            public static string Invalid => "&#x1F5F2;";


        }

        public static class VariousSigns
        {

            public static string WarningSign => "&#x26A0;";

            public static string Home => "&#x2302;";

            public static string Diameter => "&#x2300;";

            public static string Magnifier => "&#x1F50E;";
            public static string MagnifierRight => "&#x1F50D;";

            /// <summary>
            /// Radioaktivitäts- Warnzeichen
            /// </summary>
            public static string warningSignRadioactivity => "&#x2622;";

            public static string Alarm => "&#x2383;";

            /// <summary>
            /// Totenkopf 
            /// </summary>
            public static string Skull => "&#x2620;";

            public static string LatinCross => "&#x271D;";

            public static string WestSyrianCross => "&#x2670;";
            public static string EastSyrianCross => "&#x2671;";

            public static string OrthodoxCross => "&#x2626;";

            public static string LotringCross => "&#x2628;";
            public static string JerusalemCross => "&#x2629;";
            public static string DingbatCross => "&#x271E;";

            /// <summary>
            /// Wiederverwertung
            /// </summary>
            public static string Recycling => "&#x2672;";

            public static string RecyclingThin => "&#x267A;";

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

            public static string OldKey => "&#x1F5DD;";

            public static string Collision => "&#x1F4A5;";

            public static string Joker => "&#x1F0BF;";

            public static string Tsunami => "&#x1F30A;";

            public static string UnderConstruction => "&#x1F3D7;";

            public static string InfoSign => "&#x2139;";

            public static string Mountains => "&#x1A12;"; // "&#113F;";

            public static string AtomicPowerStation => "&#x15C0;";

            public static string Regenschirm => "&#x2602;";

            public static string RegenschirmMitTropfen => "&#x2614;";

            public static string Sonnenschirm => "&#x26F1;";

            public static string UnknownSign => "&#x2BD1;";

            public static string Eye => "&#x1F441;";
            public static string Eyes => "&#x1F440;";

            public static string Bizeps => "&#x1F4AA;";

            /// <summary>
            /// Bombe
            /// </summary>
            public static string Bomb => "&#x1F4A3;";

            public static string Balloon => "&#x1F388;";

            public static string Explosion => "&#x1F4A5;";

            public static string FireworkSparkler => "&#x1F387;";

            public static string Firework => "&#x1F386;";

            public static string Bell => "&#x1F56D;";
            public static string BellOff => "&#x1F515;";

            public static string SpeechBubble => "&#x1F4AC;";

            public static string Bierzelt => "&#x1F3AA;";

            public static string ChrismasTree => "&#x1F384;";

            public static string ChampagnerBottle => "&#x1F37E;";

            /// <summary>
            /// Schwarze Scheere
            /// </summary>
            public static string BlackScissors => "&#x2702;";

            public static string PrismenRaster => "&#x1246E;";

            public static string Schlange => "&#xA9AC;";

            public static string HalbblüteLinks => "&#xA9C1;";
            public static string HalbblüteRechts => "&#xA9C2;";

            public static string Einfachquadratchnecke => "&#xA874;";
            public static string Doppelquadratschnecke => "&#xA875;";


            public static string CupOfCoffee => "&#x26FE;";

            public static string Crown => "&#x1F732;";

            public static string ElectricBulb => "&#x1F4A1;";

            /// <summary>
            /// Taschenlampe
            /// </summary>
            public static string Torch => "&#x1F526;";

            public static string Microscope => "&#x1F52C;";

            public static string Brick => "&#x1F759;";

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

            public static string ProtectorWithBlackCross => "&#x26E8;";

            /// <summary>
            /// Abfangjäger
            /// </summary>
            public static string Interceptor => Aerospace.SupersonicJet;


            public static string Death => VariousSigns.Skull;

            public static string Cementary => "&#x26FC;";

            /// <summary>
            /// Star Wars Tie Jäger, weiss
            /// </summary>
            public static string TieFighter => "&#x29F2;";

            /// <summary>
            /// Star Wars Tie Jäger, schwarz
            /// </summary>
            public static string TieFighterBlack => "&#x29F3;";


            public static string Fortress => "&#x26EB;";

            public static string Castle => "&#x1F3F0;";

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

            public static string BlackSun => Astronomy.BlackSun;
            public static string WhiteSun => Astronomy.WhiteSun;
            public static string BrightSun => Astronomy.BrightSun;
        }

        public static class Workflows
        {

            public static string Start => Threads.ThreadStart;
            public static string StartRocket => Aerospace.Rocket;




            public static string Stop => $"{Threads.Thread}{Traffic.StopSign}";
            public static string Cancel => "&#x1F5D9;";

            public static string FinalState => "&#x1F3C1;";

        }

    }
}
