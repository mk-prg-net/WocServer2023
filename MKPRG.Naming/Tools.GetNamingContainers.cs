using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        /// Lädt den Naming- Container ohne abhängigkeit von einem DocuTerm- Composer. In allen Situationen nutzbar, wo kein 
        /// Composer bereitsteht.
        /// </summary>
        /// <param name="Namespace"></param>
        /// <param name="recurseNamespaces"></param>
        /// <returns></returns>
        public (bool succeded, System.Collections.Concurrent.ConcurrentDictionary<long, INaming> ncDict, string[] includedAssemblies, INaming[] duplicates) GetNamingContainers
                    (string Namespace, 
                    bool recurseNamespaces = true)
        {
            (bool succeded, System.Collections.Concurrent.ConcurrentDictionary<long, INaming> ncDict, string[] includedAssemblies, INaming[] duplicates) ret =
                (false,
                 new System.Collections.Concurrent.ConcurrentDictionary<long, INaming>() { },
                 new string[] { },
                 new INaming[] { }
            );

            var assemblies = AppDomain.CurrentDomain.GetAssemblies();

            if (assemblies == null)
            {
                Debug.WriteLine("MKPRG.Naming.Tools.GetNamingContainerAsConcurrentDict(): AppDomain.CurrentDomain.GetAssembilies() → null");
            }
            else
            {
                Debug.WriteLine($"MKPRG.Naming.Tools.GetNamingContainerAsConcurrentDict(): AppDomain.CurrentDomain.GetAssembilies().Length → {assemblies.Length}");                                 
                Debug.WriteLine(string.Join("\n", assemblies.OrderBy(r => r.FullName).Select(r => $"{r}")));               

                // Assemblies herausfiltern, deren Namen den Bezeichner mit .Naming endet
                var NamingAssemblies = assemblies.Where(r => r.GetName().Name.ToUpperInvariant().EndsWith(".NAMING"));
                var duplicates = new List<INaming>();
                var dict = new Dictionary<long, INaming>();

                if (NamingAssemblies.Any())
                {
                    foreach (var NamingAss in NamingAssemblies)
                    {
                        Debug.WriteLine($"Get all Naming Containers of {NamingAss.FullName}");

                        var assTypes = NamingAss.GetTypes();

                        var ncTypes = assTypes.Where(t => t.IsClass

                                                && t.Name != "NamingBase"

                                                // mko, 15.2.2021
                                                && !t.IsAbstract

                                                // Bei bedarf auch alle untergeordneten Namensräume nach Namingcontainer absuchen
                                                && (recurseNamespaces ? t.Namespace?.StartsWith(Namespace) ?? false : t.Namespace == Namespace)

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
                            if (dict.ContainsKey(nc.ID))
                            {
                                // Duplicate bezüglich der Naming- ID werden nicht ein zweites Mal erfasst. 
                                // Die Duplikate werden in dem Array `duplicates` protokolliert
                                duplicates.Add(nc);
                            }
                            else
                            {
                                dict[nc.ID] = nc;
                            }
                        }
                    }

                    var namingContainer = dict;

                    Debug.WriteLineIf(namingContainer == null, "MKPRG.Naming.Tools.GetNamingContainerAsConcurrentDict(): namingContainer → null");
                    Debug.WriteLineIf(namingContainer != null, $"MKPRG.Naming.Tools.GetNamingContainerAsConcurrentDict(): namingContainer.Count() -> {namingContainer.Count()}");

                    var succeded = dict.Any() && !duplicates.Any();


                    return (succeded, 
                            new System.Collections.Concurrent.ConcurrentDictionary<long, INaming>(namingContainer), 
                            NamingAssemblies.Select(a => a.FullName).ToArray(), 
                            duplicates.ToArray());
                }
                else
                {
                    // Keine einzige Naming- Assembly wurde gefunden.

                    ret = (false,
                     new System.Collections.Concurrent.ConcurrentDictionary<long, INaming>() { },
                     new string[] { },
                     new INaming[] { });

                    Debug.WriteLine("MKPRG.Naming.Tools.GetNamingContainerAsConcurrentDict(): Assemblie, deren Namen auf .Naming endet, wurden nicht gefunden");
                }
            }

            return ret;
        }
    }
}
