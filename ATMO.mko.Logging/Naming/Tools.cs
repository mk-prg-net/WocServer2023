using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using ATMO.mko.Logging;
using ATMO.mko.Logging.PNDocuTerms.DocuEntities;

/// <summary>
/// mko, 18.2.2020
/// Liefert die Bennenung einer Entität in mehreren Sprachen.
/// Zudem erhält die Entität einen technisch eindeutigen Namen in Form einer GUID.
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
            //var tools = new Tools();
            //var pnL = new ATMO.mko.Logging.PNDocuTerms.DocuEntities.Composer();

            //var NC = tools.GetNamingDictOf("MKPRG.Naming", pnL).ValueOrException;

            //var _NamingIdForCNTName = new System.Collections.Concurrent.ConcurrentDictionary<string, string>();

            //foreach(var nc in NC)
            //{
            //    _NamingIdForCNTName[nc.Value.CNT] = nc.Value.IDAsName;
            //}

            //NamingIdForCNTNameOfDocuTerm = _NamingIdForCNTName;
        }


        /// <summary>
        /// mko, 27.2.2020
        /// Liefert alle INaming- Container, die im übergebenen Namensraum definiert sind.
        /// </summary>
        /// <param name="Namespace">Namensraum, für den die INaming- Container abgerufen werden sollen</param>
        /// <param name="pnL">Composer zum Formulieren von DocuTerms für Fehlermeldungen</param>
        /// <param name="recurseNamespaces">Wenn true, dann werden auch alle untergeordneten Namensräume nach INaming- Container abgesucht</param>
        /// <returns></returns>
        public RCV3sV<INaming[]> GetAllNamingInstancesIn(string Namespace, IComposer pnL, bool recurseNamespaces= true)
        {
            var ret = RCV3sV<INaming[]>.Failed(value: null, ErrorDescription: pnL.eNotCompleted());

            try
            {
                var namingContainerTypes = AppDomain.CurrentDomain
                                            .GetAssemblies().Where(r => r.FullName.ToUpper().Contains("NAMING"))
                                            .SelectMany(t => t.GetTypes())                                            
                                            .Where(t => t.IsClass

                                                        && t.Name.ToUpper() != "NAMINGBASE"

                                                        // Bei bedarf auch alle untergeordneten Namensräume nach Namingcontainer absuchen
                                                        && (recurseNamespaces ? t.Namespace.StartsWith(Namespace) : t.Namespace == Namespace) 

                                                        // Nur Klassen berücksichtigen, welche die Schnittstelle INaming implementieren
                                                        && t.GetInterfaces().Any(r => r.Name.ToUpper() == "INAMING"));


                // Instanzen von den Naming- containern erzeugen

                var namingContainer = namingContainerTypes.Select(r => (INaming)Activator.CreateInstance(r)).ToArray();

                ret = RCV3sV<INaming[]>.Ok(value: namingContainer);
            }
            catch (Exception ex)
            {
                ret = RCV3sV<INaming[]>.Failed(value: null, ErrorDescription: TraceHlp.FlattenExceptionMessagesPN(ex));
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
        public RCV3sV<IReadOnlyDictionary<long, INaming>> GetNamingDictOf(string Namespace, IComposer pnL, bool recurseNamespaces = true)
        {
            var ret = RCV3sV<IReadOnlyDictionary<long, INaming>>.Failed(value: null, ErrorDescription: pnL.eNotCompleted());

            var getAll = GetAllNamingInstancesIn(Namespace, pnL, recurseNamespaces);

            if (!getAll.Succeeded)
            {                
                ret = RCV3sV<IReadOnlyDictionary<long, INaming>>.Failed(
                    value: null,
                    ErrorDescription: pnL.m("GetAllNamingInstancesIn",
                                            pnL.p(DocuTerms.MetaData.NameSpace.UID, Namespace),
                                                pnL.ret(pnL.eFails(pnL.EncapsulateAsEventParameter(getAll.ToPlx())))));
            } else
            {
                ret = RCV3sV<IReadOnlyDictionary<long, INaming>>.Ok(
                    new System.Collections.ObjectModel.ReadOnlyDictionary<long, INaming>(getAll.Value.ToDictionary(r => r.ID)));
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
        public RCV3sV<System.Collections.Concurrent.ConcurrentDictionary<long, INaming>> GetNamingConcurrentDictOf(string Namespace, IComposer pnL, bool recurseNamespaces = true)
        {
            var ret = RCV3sV<System.Collections.Concurrent.ConcurrentDictionary<long, INaming>>.Failed(value: null, ErrorDescription: pnL.eNotCompleted());

            var getNamingDict = GetNamingDictOf(Namespace, pnL, recurseNamespaces);

            if (!getNamingDict.Succeeded)
            {
                ret = RCV3sV<System.Collections.Concurrent.ConcurrentDictionary<long, INaming>>.Failed(
                    value: null, 
                    ErrorDescription: getNamingDict.ToPlx());
            }
            else
            {
                ret = RCV3sV<System.Collections.Concurrent.ConcurrentDictionary<long, INaming>>.Ok(
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
            var namingContainerTypes = AppDomain.CurrentDomain
                            .GetAssemblies().Where(r => r.FullName.ToUpper().Contains("NAMING"))
                            .SelectMany(t => t.GetTypes())
                            .Where(t => t.IsClass

                                        && t.Name.ToUpper() != "NAMINGBASE"

                                        // Bei bedarf auch alle untergeordneten Namensräume nach Namingcontainer absuchen
                                        && (recurseNamespaces ? t.Namespace.StartsWith(Namespace) : t.Namespace == Namespace)

                                        // Nur Klassen berücksichtigen, welche die Schnittstelle INaming implementieren
                                        && t.GetInterfaces().Any(r => r.Name.ToUpper() == "INAMING"));

            
            // Instanzen von den Naming- containern erzeugen

            var namingContainer = namingContainerTypes.Select(r => (INaming)Activator.CreateInstance(r)).ToDictionary(r => r.ID);

            return new System.Collections.Concurrent.ConcurrentDictionary<long, INaming>(namingContainer);
        }
    }
}
