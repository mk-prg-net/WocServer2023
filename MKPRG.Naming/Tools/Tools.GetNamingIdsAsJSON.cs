using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKPRG.Naming
{
    partial class Tools
    {

        string Nid64To2x32bitPair(long nid)
        {
            long upper = nid >> 32;
            long lower = nid & 0x00000000FFFFFFFF;

            return $"[{upper}, {lower}]";
        }

        /// <summary>
        /// mko, 18.12.2022
        /// Erzeugt für einen definierten Namensraum eine JSON- Struktur, die den verschachtelten Unternamensräumen entspricht, und am Ende die 
        /// Naming- ID liefert. 
        /// Nützlich für die Programmierung in JavaScript.
        /// </summary>
        /// <param name="Namespace"></param>
        /// <returns></returns>
        public ((bool succeeded, string ErrorMsg) RC, string JsonID) GetNamingIdsAsJSON(string Namespace)
        {
            ((bool succeeded, string ErrorMsg) RC, string JsonID) ret = ((false, "not completed"), "");

            var getNamingContainers = GetNamingContainers(Namespace);

            if (!getNamingContainers.succeded && getNamingContainers.duplicates.Length > 0)
            {
                var duplicateList = string.Join(", ", getNamingContainers.duplicates.Select(nc => $"{nc.ID} {nc.CNT}"));
                ret = ((false, $"GetNamingContainers failed due to duplicates: {duplicateList}"), "");
            }
            else if (!getNamingContainers.succeded)
            {
                ret = ((false, $"GetNamingContainers failed due to general error"), "");
            }
            else
            {
                var NamingContainers = getNamingContainers.ncDict;

                var ncOrderedByNamespaces = NamingContainers.Values.Where(nc => nc is IGetNameSpaceOfNamingContainer)
                                                                    .Select(nc => (ns: (IGetNameSpaceOfNamingContainer)nc, nc: nc))
                                                                    .OrderBy(nsc => nsc.ns.MyNamespace);

                var Level = 0;
                var indentStrBld = new StringBuilder(" ");

                Func<int, string> Indent = level =>
                {
                    indentStrBld.Clear();
                    indentStrBld.Append("\n ");
                    for (int i = 0; i < level; i++)
                    {
                        indentStrBld.Append(" ");
                    }

                    return indentStrBld.ToString();
                };

                bool first = true;
                string LastNameSpace = "";
                var jsonStrBld = new StringBuilder("{");
                var ncSeparators = new char[] { '.' };

                foreach(var nc in ncOrderedByNamespaces)
                {
                    if (first) {
                        var namespaceParts = nc.ns.MyNamespace.Split(ncSeparators);

                        for(; Level < namespaceParts.Length; Level++)
                        {
                            jsonStrBld.Append($"{Indent(Level)}\"{namespaceParts[Level]}\": {{");
                        }

                        jsonStrBld.Append($"{Indent(Level + 1)}\"{nc.ns.MyNamingContainerName}\": {Nid64To2x32bitPair(nc.nc.ID)}");

                        first = false;
                        LastNameSpace = nc.ns.MyNamespace;
                    }
                    else
                    {
                        if(Level == nc.ns.MyNameSpaceLevel && LastNameSpace == nc.ns.MyNamespace)
                        {
                            jsonStrBld.Append($",{Indent(Level + 1)}\"{nc.ns.MyNamingContainerName}\": {Nid64To2x32bitPair(nc.nc.ID)}");
                        }
                        else
                        {
                            var namespaceParts = nc.ns.MyNamespace.Split(ncSeparators);
                            var LastNameSpaceParts = LastNameSpace.Split(ncSeparators);

                            var countEqualLevels = 0;

                            // Gemeinsame Wurzel bestimmen
                            while(countEqualLevels < namespaceParts.Length && countEqualLevels < LastNameSpaceParts.Length && namespaceParts[countEqualLevels] == LastNameSpaceParts[countEqualLevels])
                            {
                                countEqualLevels++;
                            }

                            // Vorausgegangenen Namespace wieder schließen
                            for(int i = 0; Level > countEqualLevels; i++, Level--)
                            {
                                jsonStrBld.Append($"{Indent(Level)}}}");
                            }

                            jsonStrBld.Append(",");

                            // Neuen Sub- Namespace öffnen
                            for (; Level < namespaceParts.Length; Level++)
                            {
                                jsonStrBld.Append($"{Indent(Level)}\"{namespaceParts[Level]}\": {{");
                            }

                            // NamensID definieren
                            jsonStrBld.Append($"{Indent(Level + 1)}\"{nc.ns.MyNamingContainerName}\": {Nid64To2x32bitPair(nc.nc.ID)}");

                            LastNameSpace = nc.ns.MyNamespace;
                        }
                    }
                }

                // Vorausgegangenen Namespace wieder schließen
                for (int i = 0; Level >= 0; i++, Level--)
                {
                    jsonStrBld.Append($"{Indent(Level)}}}");
                }

                ret = ((true, ""), jsonStrBld.ToString());               
            }
            return ret;
        }
    }
}
