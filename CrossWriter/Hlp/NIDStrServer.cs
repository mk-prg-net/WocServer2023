// mko, 20.12.2023
using MKPRG.Edit.Abstract;

namespace CrossWriter.Hlp
{
    public class NamingContainerWebApiHlp
    {

        public NamingContainerWebApiHlp(MyNamingContainers myNamingContainers)
        {
            this.myNamingContainers = myNamingContainers;
        }

        MyNamingContainers myNamingContainers;

        /// <summary>
        /// mko, 24.5.2023
        /// Lädt gemäß einer Liste von NamingId's in Stringform die Namenscontainer ein.
        /// 
        /// mko, 27.12.2023
        /// Angepasst an neue Schnittstellen für Namenscontainer wie IGlyph...
        /// </summary>
        /// <param name="nidStringList">Kommaseparierte Liste mit NIDs</param>
        /// <param name="myNamingContainers"></param>
        /// <returns></returns>
        public NamingContainerSimple[] FetchNamingContainersWithNamingIds(string nidStringList)
        {
            var undefNc = myNamingContainers.NC[MKPRG.Naming.DocuTerms.Types.UndefinedDocuTerm.UID];
            var undef = new NamingContainerSimple()
            {
                NIDstr = undefNc.NID.ToString("X"),                
                CNT = undefNc.CNT,
                DE = undefNc.DE,
                EN = undefNc.EN,                
            };

            var ncList = new NamingContainerSimple[] { undef };

            var nids = nidStringList.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

            if (nids.Any())
            {
                ncList = nids.Select(r =>
                {
                    if (r.StartsWith("0x"))
                    {
                        r = r.Substring(2);
                    }
                    if (long.TryParse(r,
                                      System.Globalization.NumberStyles.HexNumber,
                                      System.Globalization.CultureInfo.InvariantCulture,
                                      out long nid))
                    {

                        if (myNamingContainers.NC.ContainsKey(nid))
                        {
                            var nc = myNamingContainers.NC[nid];
                            var ncs = new NamingContainerSimple()
                            {
                                NIDstr = nc.NID.ToString("X"),                                
                                CNT = nc.CNT,
                                DE = nc.DE,
                                EN = nc.EN,                                
                            };

                            if (nc is IGlyph g)
                            {
                                ncs.Glyph = g.Glyph;
                            }
                            else
                            {
                                ncs.Glyph = " ";
                            }

                            if (nc is IGlyphUniCode gu)
                            {
                                ncs.GlyphUniCode = gu.GlyphUniCode;
                            }
                            else
                            {
                                ncs.GlyphUniCode = "&nbsp;";
                            }

                            if (nc is IEditShortCut sc)
                            {
                                ncs.EditShortCut = sc.EditShortCut;
                            }
                            else
                            {
                                ncs.EditShortCut = ncs.NIDstr;
                            }

                            return ncs;

                        }
                        else
                        {
                            return undef;
                        }
                    }
                    else
                    {
                        return undef;
                    }
                }).ToArray();
            }

            return ncList;
        }
    }
}