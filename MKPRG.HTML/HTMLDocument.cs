using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Diagnostics;

using MKPRG.Tracing.DocuTerms;

using TT = MKPRG.Naming.TechTerms;
using TTD = MKPRG.Naming.DocuTerms;


namespace ATMO.mko.Logging.HTML
{
    /// <summary>
    /// mko, 11.11.2020
    /// Generator für RTF- Dokumente
    /// </summary>
    public partial class HTMLDocument
    {

        /// <summary>
        /// SB für Kopfdaten eines HTML- Dokuments
        /// </summary>
        StringBuilder bldHeader = new StringBuilder();

        /// <summary>
        /// SB für HTML- Fragment/Body
        /// </summary>
        StringBuilder bldDoc = new StringBuilder();

        Stack<string> tags = new Stack<string>();
        IComposer pnL;

        /// <summary>
        /// mko, 4.1.2021
        /// </summary>
        public HTMLDocument()
        {
            pnL = new Composer();
        }

        /// <summary>
        /// mko, 4.1.2021
        /// </summary>
        /// <param name="pnL"></param>
        public HTMLDocument(IComposer pnL)
        {
            this.pnL = pnL;
        }

        /// <summary>
        /// Setz den kompletten Dokumentinhalt zurück
        /// </summary>
        public void Clear()
        {
            bldHeader.Clear();
            bldDoc.Clear();            
        }


        /// <summary>
        /// Hilfsfunktion zum Abschließen einer Builder- Sequenz
        /// </summary>
        public void build() { }

        /// <summary>
        /// Eröffnet ein neues HTML- Dokument und definiert den Header.
        /// Eine Liste von CSS- Fromatierungsregeln kann als String übergeben werden.
        /// Muss mit **CloseDoc()** abgeschlossen werden.
        /// </summary>
        /// <param name="StyleSheetTxt"></param>
        /// <returns></returns>
        public HTMLDocument createHeader(string StyleSheetTxt)
        {
            // Dokument- Start (muss am Ende mit eine } geschlossen werden)
            bldHeader.Append(@"<html><header>");

            // Stylesheets definieren
            if (!string.IsNullOrWhiteSpace(StyleSheetTxt))
            {
                bldHeader.Append("<style>");
                bldHeader.Append(StyleSheetTxt);
                bldHeader.Append(@"</style>");
            }

            bldHeader.Append(@"</header><body>");

            return this;
        }

        /// <summary>
        /// mko, 21.1.2021
        /// Eröffnet ein neues HTML- Dokument und definiert den Header.
        /// Eine Liste von CSS- Fromatierungsregeln kann als String übergeben werden.
        /// Muss mit **CloseDoc()** abgeschlossen werden.
        /// </summary>
        /// <param name="Title">Dokumenttitel- erscheint im Title- Bar des Browsers</param>
        /// <param name="StyleSheetTxt"></param>
        /// <returns></returns>
        public HTMLDocument createHeader(string Title, string StyleSheetTxt)
        {
            // Dokument- Start (muss am Ende mit eine } geschlossen werden)
            bldHeader.Append(@"<html><header>");

            bldHeader.Append($"<title>{Title}</title>");

            // Stylesheets definieren

            bldHeader.Append("<style>");
            bldHeader.Append(StyleSheetTxt);
            bldHeader.Append(@"</style>");

            bldHeader.Append(@"</header><body>");

            return this;
        }

        /// <summary>
        /// Schließt Dokument ab und ginbt den RTF- formatierten String zurück.
        /// </summary>
        /// <returns></returns>
        public string CloseDoc()
        {
            bldDoc.Append(@"</body></html>");
            return bldHeader.ToString() + bldDoc.ToString();
        }

        // HTML- Fragmente

        /// <summary>
        /// Beginnt ein HTML- Fragment. Dieses muss mit CloseFragment() abgeschlossen werden.
        /// Ein HTML- Fragment ist weder in ein **html** Tag, noch in einem **body** Tag eingeschlossen.
        /// </summary>
        public void BeginFragment()
        {
            bldDoc.Clear();
        }


        /// <summary>
        /// mko, 21.1.2021
        /// Schließt die Erzeugung eines HTML- Fragmentes ab
        /// </summary>
        /// <returns></returns>
        public string CloseFragment()
        {
            return bldDoc.ToString();
        }

        /// <summary>
        /// Zeilenumbruch
        /// </summary>
        /// <returns></returns>
        public HTMLDocument br
        {
            get
            {
                bldDoc.Append(@"</br>");
                return this;
            }
        }

        public string Br() => @"</br>";


        /// <summary>
        /// öffnendesTag
        /// </summary>
        /// <param name="tagname"></param>
        /// <returns></returns>
        public HTMLDocument t(string tagname)
        {
            tags.Push(tagname);
            bldDoc.Append($"<{tagname}>");
            return this;
        }

        /// <summary>
        /// mko, 4.1.2021
        /// </summary>
        /// <param name="tagname"></param>
        /// <param name="className"></param>
        /// <returns></returns>
        public HTMLDocument tWithClass(string tagname, string className)
        {
            tags.Push(tagname);
            bldDoc.Append($"<{tagname} class='{className}'>");

            return this;
        }

        /// <summary>
        /// mko, 4.1.2021
        /// 
        /// Eröffnet automatisiert einen HTML- Block, der attributiert ist.
        /// </summary>
        /// <param name="tagname"></param>
        /// <param name="AttributeValuePairs"></param>
        /// <returns></returns>
        public HTMLDocument tWithAttribs(string tagname, params string[] AttributeValuePairs)
        {
            tags.Push(tagname);
            bldDoc.Append($"<{tagname} ");

            foreach(var av in AttributeValuePairs)
            {
                bldDoc.Append(av);
                bldDoc.Append(" ");
            }

            bldDoc.Append(">");

            return this;
        }

        /// <summary>
        /// Schließt ein zuvor geöffnetes Tag. 
        /// Wg. dem hierarchischen Aufbau von HTML wird das zu schließende Tag aus dem TagStack geladen.
        /// </summary>
        public HTMLDocument E
        {
            get
            {
                //Debug.Assert(tags.Count != 0);
                TraceHlp.ThrowExIf(tags.Count == 0,
                    pnL.m(TT.Validation.Validate.UID,
                        pnL.p_NID(TT.Grammar.Subject.UID, TT.Markup.Markup.UID),
                        pnL.ret(pnL.eFails(TT.Markup.Html.Errors.ClosingTagIsMissing.UID))));
                    
                var tag = tags.Pop();
                bldDoc.Append($"</{tag}>");
                return this;
            }
        }

        string NormalizeTxt(string txt)
        {
            return txt.Replace("&", "&amp;").Replace("<", "&lt;").Replace(">", "&gt;").Replace("\"", "&quot;").Replace("'", "&apos;"); //.Replace(';', ' ').Replace('#', ' ');
        }

        /// <summary>
        /// mko, 4.1.2021
        /// Gibt Text in den HTML- Datenstrom aus, wobei html- Steuerzeichen wie &, < und > etc. zuvor 
        /// durch HTML- Entitäten ersetzt werden
        /// </summary>
        /// <param name="txt"></param>
        /// <returns></returns>
        public HTMLDocument txt(string txt)
        {
            bldDoc.Append(NormalizeTxt(txt));
            return this;
        }

        /// <summary>
        /// mko, 4.1.2021
        /// Gibt Test in den HTML- Datenstrom aus, wobei kein Ersatz von html Steuerzeichen stattfindet-
        /// der Text kann damit auch HTML- Markup enthalten.
        /// </summary>
        /// <param name="txt"></param>
        /// <returns></returns>
        public HTMLDocument html(string txt)
        {
            bldDoc.Append(txt);
            return this;
        }

        public HTMLDocument dec(int i)
        {
            bldDoc.Append(i);
            return this;
        }

        public HTMLDocument decFix(int i, int width)
        {
            bldDoc.Append(string.Format($"{{0:D{width}}}", i));
            return this;
        }

        public HTMLDocument fltFix(double d, int width, int accuracy)
        {
            bldDoc.Append(string.Format($"{{0,{-width}:N{accuracy}", i));
            return this;
        }



    }
}
