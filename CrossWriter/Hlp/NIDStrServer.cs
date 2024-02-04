// mko, 20.12.2023
using MKPRG.Edit.Abstract;
using System.Text.RegularExpressions;

namespace CrossWriter.Hlp
{
    public class NamingContainerWebApiHlp
    {

        public NamingContainerWebApiHlp(MyNamingContainers myNamingContainers)
        {
            this.myNamingContainers = myNamingContainers;

            var undefNc = myNamingContainers.NC[MKPRG.Naming.DocuTerms.Types.UndefinedDocuTerm.UID];
            undefNcSimple = new NamingContainerSimple()
            {
                NIDstr = undefNc.NID.ToString("X"),
                CNT = undefNc.CNT,
                DE = undefNc is ILangDE deLng ? deLng.DE : undefNc.CNT,
                EN = undefNc is ILangEN enLng ? enLng.EN : undefNc.CNT,
            };

        }

        const string NameSpacePattern = @"^[a-zA-Z_][a-zA-Z0-9_\.]*$";
        const string NIDListPattern = @"(^0x[0-9a-fA-F]+,)+$";


        MyNamingContainers myNamingContainers;
        NamingContainerSimple undefNcSimple;



        /// <summary>
        /// mko, 28.12.2023
        /// Returns true, if queryString matches Reqular Expression of a Namespace
        /// </summary>
        /// <param name="queryString"></param>
        /// <returns></returns>
        public bool IsNameSpace(string queryString)
            => Regex.IsMatch(queryString, NameSpacePattern);

        /// <summary>
        /// mko, 28.12.2023
        /// Returns true, if queryString matches a regular Expression of a Naming ID List
        /// </summary>
        /// <param name="queryString"></param>
        /// <returns></returns>
        public bool IsNIDList(string queryString)
            => Regex.IsMatch(queryString, NIDListPattern);


        /// <summary>
        /// mko, 28.12.2023
        /// Helper
        /// </summary>
        /// <returns></returns>
        public NamingContainerSimple[] CreateNamingContainerListWithUndefNC()
        {
            var ncList = new NamingContainerSimple[] { undefNcSimple };
            return ncList;
        }

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
            var ncList = CreateNamingContainerListWithUndefNC();

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
                            NamingContainerSimple ncs = CreateNCSimple(nc);
                            return ncs;
                        }
                        else
                        {
                            return undefNcSimple;
                        }
                    }
                    else
                    {
                        return undefNcSimple;
                    }
                }).ToArray();
            }

            return ncList;
        }

        private static NamingContainerSimple CreateNCSimple(INaming nc)
        {
            var ncs = new NamingContainerSimple()
            {
                NIDstr = nc.NID.ToString("X"),
                CNT = nc.CNT,
                //CN = nc is ILangCN cnLng ? cnLng.CN : nc.CNT,
                DE = nc is ILangDE deLng ? deLng.DE : nc.CNT,
                EN = nc is ILangEN enLng ? enLng.EN : nc.CNT,
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

        /// <summary>
        /// mko, 28.12.2023
        /// Gets all Naming Containers, defined inside a given namespace.
        /// </summary>
        /// <param name="NCnamespace"></param>
        /// <returns></returns>
        public NamingContainerSimple[] FetchNamingContainersOfNamespace(string NCnamespace)
        {
            var ncList = CreateNamingContainerListWithUndefNC();

            var ncInNamespace = myNamingContainers.NC.Values.Where(r => r is IGetNameSpaceOfNamingContainer getNC && getNC.MyNamespace.Equals(NCnamespace));

            if (ncInNamespace.Any())
            {
                ncList = ncInNamespace.Select(r => CreateNCSimple(r)).ToArray();
            }

            return ncList;
        }
    }
}