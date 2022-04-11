using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using MKPRG.Tracing;
using MKPRG.Tracing.DocuTerms;

/// <summary>
/// mko, 18.2.2020
/// Liefert die Bennenung einer Entität in mehreren Sprachen.
/// Zudem erhält die Entität einen technisch eindeutigen Namen in Form einer GUID.
/// 
/// mko, 18.2.2021
/// Umgezogen in MKPRG.Tracing
/// </summary>
namespace MKPRG.Naming
{
    /// <summary>
    /// mko, 27.2.2020
    /// Methoden zum Abrufen von Naming- Instanzen unte Namensräumen etc
    /// </summary>
    public class Tools
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
        /// mko, 27.2.2020
        /// Liefert alle INaming- Container, die im übergebenen Namensraum definiert sind.
        /// </summary>
        /// <param name="Namespace">Namensraum, für den die INaming- Container abgerufen werden sollen</param>
        /// <param name="pnL">Composer zum Formulieren von DocuTerms für Fehlermeldungen</param>
        /// <param name="recurseNamespaces">Wenn true, dann werden auch alle untergeordneten Namensräume nach INaming- Container abgesucht</param>
        /// <returns></returns>
        public RC<INaming[]> GetAllNamingInstancesIn(string Namespace, IComposer pnL, bool recurseNamespaces = true)
        {
            var ret = RC<INaming[]>.Failed(value: null, ErrorDescription: pnL.eNotCompleted());

            try
            {
                var namingContainerTypes = AppDomain.CurrentDomain
                            .GetAssemblies().Where(r => r.FullName.ToUpper().Contains("NAMING"))
                            .SelectMany(t => t.GetTypes())
                            .Where(t => t.IsClass

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


                // Instanzen von den Naming- containern erzeugen

                var namingContainer = namingContainerTypes.Select(r => (INaming)Activator.CreateInstance(r)).ToArray();

                ret = RC<INaming[]>.Ok(value: namingContainer);
            }
            catch (Exception ex)
            {
                ret = RC<INaming[]>.Failed(value: null, ErrorDescription: TraceHlp.FlattenExceptionMessagesPN(ex));
            }

            return ret;
        }


        /// <summary>
        /// mko, 27.2.2020
        /// Liefert eine readonly Dictionary, die jeder Naming- Id ihren Naming Container zuordnet. Kann verwendet werden, um in DocuTerm- Formattern gefundene Naming- Id's
        /// in Namen oder Werten von Docuterms gegen den bezüglich der Spracheinstellungen aktuell gültigen Bezeichner auszutauschen.
        /// </summary>
        /// <param name="Namespace"></param>
        /// <param name="pnL"></param>
        /// <param name="recurseNamespaces">Wenn true, dann werden auch alle untergeordneten Namensräume nach INaming- Container abgesucht</param>
        /// <returns></returns>
        public RC<IReadOnlyDictionary<long, INaming>> GetNamingDictOf(string Namespace, IComposer pnL, bool recurseNamespaces = true)
        {

            var ret = RC<IReadOnlyDictionary<long, INaming>>.Failed(value: null, ErrorDescription: pnL.eNotCompleted());

            var getAll = GetAllNamingInstancesIn(Namespace, pnL, recurseNamespaces);

            if (!getAll.Succeeded)
            {
                ret = RC<IReadOnlyDictionary<long, INaming>>.Failed(
                    value: null,
                    ErrorDescription: pnL.m("GetAllNamingInstancesIn",
                                            pnL.p(DocuTerms.MetaData.NameSpace.UID, Namespace),
                                                pnL.ret(pnL.eFails(pnL.EncapsulateAsEventParameter(getAll.ToPlx())))));
            }
            else
            {
                if (getAll.Value.Select(r => r.ID).Distinct().Count() < getAll.Value.Count())
                {
                    // Fall: NID's wurden versehentlich doppelt vergeben

                    var allNCs = getAll.Value.OrderBy(r => r.ID).ToArray();

                    var lastId = -1L;
                    var lstDuplicats = new Dictionary<long, INaming>();
                    for (int i = 0, c = allNCs.Count(); i < c; i++)
                    {
                        if (allNCs[i].ID == lastId && !lstDuplicats.ContainsKey(lastId))
                        {
                            lstDuplicats.Add(lastId, allNCs[i]);
                        }

                        lastId = allNCs[i].ID;
                    }

                    ret = RC<IReadOnlyDictionary<long, INaming>>.Failed(lstDuplicats, ErrorDescription: pnL.m("GetAllNamingInstancesIn", pnL.p(DocuTerms.MetaData.NameSpace.UID, Namespace), pnL.ret(pnL.eFails("Duplicates found"))));

                }
                else
                {
                    ret = RC<IReadOnlyDictionary<long, INaming>>.Ok(
                        new System.Collections.ObjectModel.ReadOnlyDictionary<long, INaming>(getAll.Value.ToDictionary(r => r.ID)));
                }
            }

            return ret;


        }

        /// <summary>
        /// mko, 12.3.2020
        /// Ruft eine NamingDictionary: UID -: INaming, aus dem gegebenen Namensraum ab, und verpackt sie in eine
        /// multithreadfeste Dictionary.
        /// </summary>
        /// <param name="Namespace"></param>
        /// <param name="pnL"></param>
        /// <param name="recurseNamespaces"></param>
        /// <returns></returns>
        public RC<System.Collections.Concurrent.ConcurrentDictionary<long, INaming>> GetNamingConcurrentDictOf(string Namespace, IComposer pnL, bool recurseNamespaces = true)
        {
            var ret = RC<System.Collections.Concurrent.ConcurrentDictionary<long, INaming>>.Failed(value: null, ErrorDescription: pnL.eNotCompleted());

            var getNamingDict = GetNamingDictOf(Namespace, pnL, recurseNamespaces);

            if (!getNamingDict.Succeeded)
            {
                ret = RC<System.Collections.Concurrent.ConcurrentDictionary<long, INaming>>.Failed(
                    value: null,
                    ErrorDescription: getNamingDict.ToPlx());
            }
            else
            {
                ret = RC<System.Collections.Concurrent.ConcurrentDictionary<long, INaming>>.Ok(
                    new System.Collections.Concurrent.ConcurrentDictionary<long, INaming>(getNamingDict.Value));

            }

            return ret;
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
        public System.Collections.Concurrent.ConcurrentDictionary<long, INaming> GetNamingContainerAsConcurrentDict(string Namespace, bool recurseNamespaces = true)
        {

            var assemblies = AppDomain.CurrentDomain.GetAssemblies();

            Debug.WriteLine("=====================================================================================================================");
            Debug.WriteLineIf(assemblies == null, "MKPRG.Naming.Tools.GetNamingContainerAsConcurrentDict(): AppDomain.CurrentDomain.GetAssembilies() → null");
            Debug.WriteLineIf(assemblies != null, $"MKPRG.Naming.Tools.GetNamingContainerAsConcurrentDict(): AppDomain.CurrentDomain.GetAssembilies().Length → {assemblies.Length}");

            Debug.WriteLine("#####################################################################################################################");
            Debug.WriteLineIf(assemblies != null, string.Join("\n", assemblies.OrderBy(r => r.FullName).Select(r => $"{r}")));
            Debug.WriteLine("#####################################################################################################################");

            var NamingAss = assemblies.First(r => r.FullName.ToUpperInvariant().Contains("NAMING,"));

            Debug.WriteLineIf(NamingAss == null, "MKPRG.Naming.Tools.GetNamingContainerAsConcurrentDict(): assemblies.First(r => r.FullName.ToUpper().Contains(\"NAMING\") → null");

            Debug.WriteLine("=====================================================================================================================");
            if (NamingAss != null)
            {
                Debug.WriteLine($"MKPRG.Naming.Tools.GetNamingContainerAsConcurrentDict(): assemblies.First(r => r.FullName.ToUpper().Contains(\"NAMING\") → {NamingAss.FullName}");

                var assTypes = NamingAss.GetTypes();

                Debug.WriteLineIf(assTypes == null, "MKPRG.Naming.Tools.GetNamingContainerAsConcurrentDict(): NamingAss.GetTypes() → null");
                Debug.WriteLineIf(assTypes != null, $"MKPRG.Naming.Tools.GetNamingContainerAsConcurrentDict(): NamingAss.GetTypes().Length → {assTypes.Length}");

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

                var namingContainer = ncTypes?.Select(r => (INaming)Activator.CreateInstance(r)).ToDictionary(r => r.ID);

                Debug.WriteLine("=====================================================================================================================");
                Debug.WriteLineIf(namingContainer == null, "MKPRG.Naming.Tools.GetNamingContainerAsConcurrentDict(): namingContainer → null");
                Debug.WriteLineIf(namingContainer != null, $"MKPRG.Naming.Tools.GetNamingContainerAsConcurrentDict(): namingContainer.Count() -> {namingContainer.Count()}");


                return new System.Collections.Concurrent.ConcurrentDictionary<long, INaming>(namingContainer);
            }
            else
            {
                throw new Exception("MKPRG.Naming.Tools.GetNamingContainerAsConcurrentDict(): Assemblies mit dem Namen *.Naming.* wurden nicht gefunden");
            }

            //var namingContainerTypes = assemblies
            //                .Where(r => r.FullName.ToUpperInvariant().Contains("NAMING"))
            //                .SelectMany(t => t.GetTypes())
            //                .Where(t => t.IsClass

            //                            && t.Name != "NamingBase"

            //                            // mko, 15.2.2021
            //                            && !t.IsAbstract

            //                            // Bei bedarf auch alle untergeordneten Namensräume nach Namingcontainer absuchen
            //                            && (recurseNamespaces ? t.Namespace?.StartsWith(Namespace) ?? false : t.Namespace == Namespace)

            //                            // Namenscontainerklassen, die dynamisch erstellt werden, um
            //                            // Fehler z.B. beim Aufbau von Sätzen zu beschreiben, vom Laden ausschließen
            //                            && !t.GetInterfaces().Any(r => r.Name == "IInterfaceConversionError")


            //                            // Nur Klassen berücksichtigen, welche die Schnittstelle INaming implementieren
            //                            && t.GetInterfaces().Any(r => r.Name == "INaming"));


            //// Instanzen von den Naming- containern erzeugen

            //Debug.WriteLine("=====================================================================================================================");
            //Debug.WriteLineIf(namingContainerTypes == null, "MKPRG.Naming.Tools.GetNamingContainerAsConcurrentDict(): namingContainerTypes → null");
            //Debug.WriteLineIf(namingContainerTypes != null, $"MKPRG.Naming.Tools.GetNamingContainerAsConcurrentDict(): namingContainerTypes.Count() → {namingContainerTypes.Count()}");

            //var namingContainer = namingContainerTypes?.Select(r => (INaming)Activator.CreateInstance(r)).ToDictionary(r => r.ID);

            //Debug.WriteLine("=====================================================================================================================");
            //Debug.WriteLineIf(namingContainer == null, "MKPRG.Naming.Tools.GetNamingContainerAsConcurrentDict(): namingContainer → null");
            //Debug.WriteLineIf(namingContainer != null, $"MKPRG.Naming.Tools.GetNamingContainerAsConcurrentDict(): namingContainer.Count() -> {namingContainer.Count()}");


            //return new System.Collections.Concurrent.ConcurrentDictionary<long, INaming>(namingContainer);

        }
    }
}
