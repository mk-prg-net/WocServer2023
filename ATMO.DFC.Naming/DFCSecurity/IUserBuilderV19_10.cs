using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ATMO.mko.Logging;
using ATMO.mko.Logging.PNDocuTerms.DocuEntities;


namespace DFCSecurity
{
    /// <summary>
    /// mko, 23.10.2019
    /// Klassenfabrik für DFC- Benutzerobjekte und assoziierte AccessManagment- Controller
    /// 
    /// mko, 21.1.2020
    /// Create gibt ab jetzt nur noch ein User- Objekt zurück und keinen Access- Controller mehr.
    /// Das Anlegen eines Access- Controllers ist dann Aufgabe eines AccessController Builders
    /// </summary>
    public interface IUserBuilderV19_10
    {
        /// <summary>
        /// Liefert ein Tupel, bestehend aus einem Benutzerobjekt und einem AccessController für den Benutzer.
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="pnL"></param>
        /// <param name="UseLogin"></param>
        /// <returns>Tupel, bestehend aus Benutzerobjekt mit Details zum Benutzer und Zugriffscontroller, der Methoden zum prüfen des Zugriffes auf DFC- Ressourcen anbietet</returns>
        Task<RCV3sV<IUserV19_10>> Create(string Name, IComposer pnL, bool UseLogin = false);
    }
}
