using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;


namespace ATMO.mko.Logging.RTF
{
    /// <summary>
    /// mko, 11.11.2020
    /// Generator für RTF- Dokumente
    /// </summary>
    public class RTFDocument
    {
        /// <summary>
        /// Tabelle mit den im Dokument zu verwendenen Farben
        /// </summary>
        public List<Color> ColorTable { get; } = new List<Color>();

        /// <summary>
        /// Fügt eine neue Farbe der Farbtabelle hinzu, und liefert den zugeordneten Farbindex zurück
        /// </summary>
        /// <param name="color"></param>
        /// <returns></returns>
        public int AddColor(Color color)
        {
            ColorTable.Add(color);
            return ColorTable.Count - 1;
        }
        
        StringBuilder bldDoc = new StringBuilder();

        public RTFDocument createHeader()
        {
            // Dokument- Start (muss am Ende mit eine } geschlossen werden)
            bldDoc.Append(@"{\rtf1\ansi\deff0\n");

            // Font definieren
            bldDoc.Append(@"{\fonttbl {\f0 Consolas;}}\n");

            // Farbtabelle
            bldDoc.Append(@"{\colortbl;");
            foreach (var color in ColorTable)
            {
                bldDoc.Append($"\\red{color.R}\\green{color.G}\\blue{color.B};");
            }
            bldDoc.Append(@"}\n");

            // Tabulatoren (Einheit = 1/1440 Zoll)
            //bldDoc.Append(@"tx720\tx1440\tx2880\tx5760");
            bldDoc.Append(@"tx0\tx600\tx900\tx1200\tx1500\tx1800\tx2100\tx2400");

            return this;
        }

        /// <summary>
        /// Schließt Dokument ab und ginbt den RTF- formatierten String zurück.
        /// </summary>
        /// <returns></returns>
        public string CloseDoc()
        {
            bldDoc.Append("}");
            return bldDoc.ToString();
        }

        /// <summary>
        /// Zeilenumbruch
        /// </summary>
        /// <returns></returns>
        public RTFDocument BR()
        {
            bldDoc.Append(@"\line");
            return this;
        }

        public RTFDocument txt(string txt)
        {
            bldDoc.Append(txt);
            return this;
        }

        /// <summary>
        /// Einleitung fett gedruckt
        /// </summary>
        /// <returns></returns>
        public RTFDocument B_()
        {
            bldDoc.Append(@"\b1 ");
            return this;
        }

        /// <summary>
        /// Ende fett gedruckt
        /// </summary>
        /// <returns></returns>
        public RTFDocument B()
        {
            bldDoc.Append(@" \b0\n");
            return this;
        }

        /// <summary>
        /// Text fett setzen
        /// </summary>
        /// <param name="txt"></param>
        /// <returns></returns>
        public string B(string txt)
        {
            return $"\\b {txt} \\b0\\n";            
        }


        /// <summary>
        /// Einleitung kursiv drucken
        /// </summary>
        /// <returns></returns>
        public RTFDocument IT_()
        {
            bldDoc.Append(@"\i ");
            return this;
        }

        /// <summary>
        /// Beenden eines Kursiven Textes
        /// </summary>
        /// <returns></returns>
        public RTFDocument IT()
        {
            bldDoc.Append(@" \i0\n");
            return this;
        }

        /// <summary>
        /// Text kursiv setzen
        /// </summary>
        /// <param name="txt"></param>
        /// <returns></returns>
        public string IT(string txt)
        {
            return $"\\i {txt} \\i0\\n";            
        }

        /// <summary>
        /// Nachfolgender Text wird in der Farbe gedruckt, die hier in der Farbtabelle ausgewählt wurde
        /// </summary>
        /// <param name="colorTableIndex"></param>
        /// <returns></returns>
        public RTFDocument Color(int colorTableIndex)
        {
            bldDoc.Append($"\\cf{colorTableIndex}\\n");
            return this;
        }

        /// <summary>
        /// Unterstreichen
        /// </summary>
        /// <returns></returns>
        public RTFDocument UL_()
        {
            bldDoc.Append(@"\ul ");
            return this;
        }

        public RTFDocument UL()
        {
            bldDoc.Append(@" \ul0\n");
            return this;
        }

        public string UL(string txt)
        {
            return $"\\ul {txt} \\ul0\\n";            
        }

        /// <summary>
        /// Doppelt unterstreichen
        /// </summary>
        /// <returns></returns>
        public RTFDocument ULDB_()
        {
            bldDoc.Append(@"\uldb ");
            return this;
        }

        public RTFDocument ULDB()
        {
            bldDoc.Append(@" \ul0\n");
            return this;
        }

        public string ULDB(string txt)
        {
            return $"\\uldb {txt} \\ul0\\n";            
        }


        /// <summary>
        /// Mit Wellenlinie unterstreichen
        /// </summary>
        /// <returns></returns>
        public RTFDocument ULW_()
        {
            bldDoc.Append(@"\ulw ");
            return this;
        }

        public RTFDocument ULW()
        {
            bldDoc.Append(@" \ul0\n");
            return this;
        }

        public string ULW(string txt)
        {
            return $"\\ulw {txt} \\ul0\\n";            
        }

        /// <summary>
        /// Absatz
        /// </summary>
        /// <returns></returns>
        public RTFDocument P()
        {
            bldDoc.Append(@"\par ");
            return this;
        }


        /// <summary>
        /// Tabulator
        /// </summary>
        /// <returns></returns>
        public RTFDocument Tab()
        {
            bldDoc.Append(@"\tab ");
            return this;
        }


        /// <summary>
        /// Hyperlink 
        /// </summary>
        /// <param name="URL">URL, auf den verwiesen wird</param>
        /// <param name="Descr">Beschreibung des Links</param>
        /// <returns></returns>
        public RTFDocument Link(string URL, string Descr)
        {
            bldDoc.Append(@"{\field{\*\fldinst HYPERLINK ");
            bldDoc.Append($"\"{URL}\"");
            bldDoc.Append(@"}{\fldrslt ");
            bldDoc.Append(Descr);
            bldDoc.Append(@"}}");
            return this;
        }
    }
}
