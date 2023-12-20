// mko, 20.12.2023
namespace NYT.Hlp
{
    public class NamingContainerWebApiHlp
    {
        /// <summary>
        /// mko, 24.5.2023
        /// Lädt gemäß einer Liste von NamingId's in Stringform die Namenscontainer ein.
        /// </summary>
        /// <param name="nidString"></param>
        /// <param name="myNamingContainers"></param>
        /// <returns></returns>
        public static NamingContainerSimple[] FetchNamingContainers(string nidString, MyNamingContainers myNamingContainers)
        {
            var undefNc = myNamingContainers.NC[MKPRG.Naming.DocuTerms.Types.UndefinedDocuTerm.UID];
            var undef = new NamingContainerSimple()
            {
                NIDstr = undefNc.NID.ToString("X"),
                CN = undefNc.CN,
                CNT = undefNc.CNT,
                DE = undefNc.DE,
                EN = undefNc.EN,
                ES = undefNc.ES
            };


            var ncList = new NamingContainerSimple[] { undef };

            var nids = nidString.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

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
                            return new NamingContainerSimple()
                            {
                                NIDstr = nc.NID.ToString("X"),
                                CN = nc.CN,
                                CNT = nc.CNT,
                                DE = nc.DE,
                                EN = nc.EN,
                                ES = nc.ES
                            };
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