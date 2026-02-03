using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static MKPRG.Naming.Glyphs;

/// <summary>
/// mko, 18.2.2020
/// Liefert die Bennenung einer Entität in mehreren Sprachen.
/// Zudem erhält die Entität einen technisch eindeutigen Namen in Form einer GUID.
/// 
/// mko, 18.2.2021
/// Umgezogen in MKPRG.Tracing
/// 
/// 18.12.2022
/// Abhängigkeit von MKPRG.Tracing gelöst.
/// Umgezogen von MKPRG.Tracing nach MKPRG.Naming.
/// </summary>
namespace MKPRG.Naming
{
    /// <summary>
    /// mko, 27.2.2020
    /// Methoden zum Abrufen von Naming- Instanzen unte Namensräumen etc
    /// </summary>
    public partial class Tools
    {
        /// <summary>
        /// mko, 28.5.2020
        /// Ordnet jedem culture neutral name (CNT) eines Dokuterms seine Naming- ID zu.
        /// Werden DocuTerms in Strings zwecks Datenaustausch serialisiert, und soll dabei die Lesbarkeit erhalten 
        /// bleiben, dann werden die Namen der DocuTerms in CNT ausgegeben. 
        /// Beim deserialisieren müssen die CNT- Namen wieder ID's zurückgewandelt werden, da sonst Docuterms nicht 
        /// vergleichbar sind (z.B. durch SubTree)
        /// </summary>
        //public static IReadOnlyDictionary<string, string> NamingIdForCNTNameOfDocuTerm;

        static Tools()
        {
        }

        /// <summary>
        /// mko, 10.6.2020
        /// 
        /// Lädt den Naming- Container ohne Abhängigkeit von einem DocuTerm- Composer. In allen Situationen nutzbar, wo kein 
        /// Composer bereitsteht.
        /// </summary>
        /// <param name="Namespace">Schränkt die Menge der Namenscontainer auf jene ein, die sich unterhalb des angegebenen Namensraumes befinden</param>
        /// <param name="recurseNamespaces">Wenn false, dann werden nur die unmittelbar unter dem Namensraum befindlichen Naming Container zurückgegeben, sonst alle </param>
        /// <returns></returns>
        public (bool succeded, 
                System.Collections.Concurrent.ConcurrentDictionary<long, INaming> ncDict, 
                string[] includedAssemblies, 
                INaming[] duplicates,
                string ErrorDescrIfNotSucceeded) GetNamingContainers(
                    string Namespace,
                    bool recurseNamespaces = true)
        {
            bool succeded = false;
            System.Collections.Concurrent.ConcurrentDictionary<long, INaming> ncDict = new System.Collections.Concurrent.ConcurrentDictionary<long, INaming>() { };
            string[] includedAssemblies = Array.Empty<string>();
            INaming[] duplicates = Array.Empty<INaming>();
            var ErrorDescrIfFails = "";

            var assemblies = AppDomain.CurrentDomain.GetAssemblies();

            if (assemblies == null || !assemblies.Any())
            {
                Debug.WriteLine("MKPRG.Naming.Tools.GetNamingContainer(): AppDomain.CurrentDomain.GetAssembilies() → null");
            }
            else
            {
                // Assemblies herausfiltern, deren Namen mit Naming endet.
                var assembliesWithNamingContainers = assemblies.Where(r => r.GetName().Name.ToUpperInvariant().EndsWith(".NAMING")).ToArray();
                includedAssemblies = assembliesWithNamingContainers.Select(r => r.FullName).ToArray();
                (succeded, ncDict, duplicates, ErrorDescrIfFails) = GetNamingContainers(Namespace, recurseNamespaces, assembliesWithNamingContainers);
            }

            return (succeded, ncDict, includedAssemblies, duplicates, ErrorDescrIfFails);
        }


        public (bool succeded, Assembly[] assemblySet) Add_Naming_Assemblies_to_Assembly_set(params Assembly[] assembliesWithNamingContainer)
        {
            var succeded = false;
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();

            if (assemblies == null || !assemblies.Any())
            {

                Debug.WriteLine("MKPRG.Naming.Tools.GetNamingContainer(): AppDomain.CurrentDomain.GetAssembilies() → null");

            }
            else
            {
                assemblies = assemblies.Where(r => r.GetName().Name.ToUpperInvariant()
                                       .EndsWith(".NAMING"))
                                       .Union(assembliesWithNamingContainer).ToArray();

            }

            return (succeded, assemblies);
        }

        /// <summary>
        /// mko, 3.2.2026
        /// 
        /// Lädt alle Naming Container aus den 
        /// </summary>
        /// <param name="Namespace">Schränkt die Menge der Namenscontainer auf jene ein, die sich unterhalb des angegebenen Namensraumes befinden</param>
        /// <param name="recurseNamespaces">Wenn false, dann werden nur die unmittelbar unter dem Namensraum befindlichen Naming Container zurückgegeben, sonst alle</param>
        /// <param name="assembliesWithNamingContainers">Menge aller .NET Assemblies, aus denen die Naming Container eingelesen werden sollen</param>
        /// <returns></returns>
        public (bool succeded,
                System.Collections.Concurrent.ConcurrentDictionary<long, INaming> ncDict,                
                INaming[] duplicates,
                string ErrorDescrIfFails)

                GetNamingContainers

            (string Namespace,
            bool recurseNamespaces,
            params System.Reflection.Assembly[] assembliesWithNamingContainers)
        {
            bool succeded = false;
            var ncDict = new System.Collections.Concurrent.ConcurrentDictionary<long, INaming>();
            var duplicates = new List<INaming>();
            var ErrorDescrIfFails = "";

            if (assembliesWithNamingContainers == null || !assembliesWithNamingContainers.Any())
            {
                ErrorDescrIfFails = "MKPRG.Naming.Tools.GetNamingContainerAsConcurrentDict(): AppDomain.CurrentDomain.GetAssembilies() → null";
                Debug.WriteLine(ErrorDescrIfFails);
            }
            else
            {
                Debug.WriteLine($"MKPRG.Naming.Tools.GetNamingContainerAsConcurrentDict(): AppDomain.CurrentDomain.GetAssembilies().Length → {assembliesWithNamingContainers.Length}");
                Debug.WriteLine(string.Join("\n", assembliesWithNamingContainers.OrderBy(r => r.FullName).Select(r => $"{r}")));

                Func<Type, bool> checkNameSpace;

                if (string.IsNullOrWhiteSpace(Namespace) || Namespace.Trim().ToLower() == "*")
                {
                    // Alle Namensraumprüfungen werden deaktiviert
                    checkNameSpace = (Type t) => true;
                }
                else
                {
                    // Nur Namenskontainer, die sich unterhalb eines definierten Namensraumes befinden, werden übernommen
                    checkNameSpace = (Type t) => (recurseNamespaces ? t.Namespace?.StartsWith(Namespace) ?? false : t.Namespace == Namespace);
                }

                foreach (var NamingAss in assembliesWithNamingContainers)
                {
                    Debug.WriteLine($"Get all Naming Containers of {NamingAss.FullName}");

                    var assTypes = NamingAss.GetTypes();

                    var ncTypes = assTypes.Where(t => t.IsClass

                                            && t.Name != "NamingBase"

                                            && t.Name != "NamingBase24"

                                            // mko, 15.2.2021
                                            && !t.IsAbstract

                                            // Bei bedarf auch alle untergeordneten Namensräume nach Namingcontainer absuchen
                                            && checkNameSpace(t)

                                            // Namenscontainerklassen, die dynamisch erstellt werden, um
                                            // Fehler z.B. beim Aufbau von Sätzen zu beschreiben, vom Laden ausschließen
                                            && !t.GetInterfaces().Any(r => r.Name == "IInterfaceConversionError")

                                            // Nur Klassen berücksichtigen, welche die Schnittstelle INaming implementieren
                                            && t.GetInterfaces().Any(r => r.Name == "INaming"));

                    Debug.WriteLineIf(ncTypes == null, "MKPRG.Naming.Tools.GetNamingContainerAsConcurrentDict(): NamingAss.GetTypes().FilterBy('INaming') → null");
                    Debug.WriteLineIf(ncTypes != null, $"MKPRG.Naming.Tools.GetNamingContainerAsConcurrentDict(): NamingAss.GetTypes().FilterBy('INaming').Length → {ncTypes.Count()}");

                    var _namingContainers = ncTypes?.Select(r => (INaming)Activator.CreateInstance(r));

                    foreach (var nc in _namingContainers)
                    {
                        if (ncDict.ContainsKey(nc.ID))
                        {
                            // Duplicate bezüglich der Naming- ID werden nicht ein zweites Mal erfasst. 
                            // Die Duplikate werden in dem Array `duplicates` protokolliert
                            duplicates.Add(nc);
                        }
                        else
                        {
                            ncDict[nc.ID] = nc;
                        }
                    }
                }

                Debug.WriteLine("MKPRG.Naming.Tools.GetNamingContainerAsConcurrentDict(): namingContainer → null");
                Debug.WriteLine($"MKPRG.Naming.Tools.GetNamingContainerAsConcurrentDict(): namingContainer.Count() -> {ncDict.Count()}");

                succeded = ncDict.Any() && !duplicates.Any();                
            }

            return (succeded, ncDict, duplicates.ToArray(), ErrorDescrIfFails);

        }
    }
}
