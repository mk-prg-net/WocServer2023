using MKPRG.Naming;
using TryOut.Models;
using TryOut.MySingeltons;

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
            CNT = undefNc.CNT,
            CN = undefNc is ILangCN lngCn ? lngCn.CN : undefNc.CNT,
            DE = undefNc is ILangDE lngDe ? lngDe.DE : undefNc.CNT,
            EN = undefNc is ILangEN lngEn ? lngEn.EN : undefNc.CNT,
            ES = undefNc is ILangES lngEs ? lngEs.ES : undefNc.CNT
        };

        var ncList = nidString.Split(new char[] { ',' }).Select(r =>
        {
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
                        CNT = nc.CNT,
                        CN = nc is ILangCN lngCn ? lngCn.CN : nc.CNT,
                        DE = nc is ILangDE lngDe ? lngDe.DE : nc.CNT,
                        EN = nc is ILangEN lngEn ? lngEn.EN : nc.CNT,
                        ES = nc is ILangES lngEs ? lngEs.ES : nc.CNT
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

        return ncList;
    }

}

