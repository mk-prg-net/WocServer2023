using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using mko.RPN;

namespace MKPRG.Tracing.DocuTerms
{
    /// <summary>
    /// mko, 27.11.2021
    /// Aufzählung der Typen von DocuTerms
    /// </summary>
    public enum DocuEntityTypes
    {
        // if DocuEntity has a name, child of this type defines the name
        Name,

        /// <summary>
        /// Complex, named object that contains, properties, methods and events
        /// </summary>
        Instance,

        /// <summary>
        /// Defines an Attribute consisting name and value
        /// </summary>
        Property,
        PropertySet,

        /// <summary>
        /// Defines in a Instance a version number
        /// </summary>
        Version,

        /// <summary>
        /// Signals current state of an object or method call
        /// </summary>
        Event,

        /// <summary>
        /// Describes a method call and result of them 
        /// </summary>
        Method,

        // Annonymous container of doc entities
        List,

        // string !without! whitespaces
        String,

        // A list of strings. If printed, each word will be separated from the other by whitespaces
        Text,

        Date,
        Time,

        // describes a return value of a method
        ReturnValue,

        // DocuEntity, that deletes itself if condition were not met.
        // Will be executed at runtime by the composer
        KillIfNot,

        // mko, 12.11.2018
        // needed in situations (as default value) where docuEntityType is provided in nullable
        none,

        // mko, 27.2.2019
        // List of additional Childs to embed in the current docEntity.
        // Used to embed childs at runtime, not at compile time.
        // Composer resolves this list. 
        ListToEmbed,

        /// <summary>
        /// mko, 8.6.2020
        /// Dokuterm eine bool- Wertes
        /// </summary>
        Bool,

        /// <summary>
        /// mko, 8.6.2020
        /// Dokuterm eines Integer- Wertes (wird mittels long implementiert)
        /// </summary>
        Int,

        /// <summary>
        /// Dokuterm eines Gleitkomma- Wertes
        /// </summary>
        Float,

        /// <summary>
        /// Dokuterm einer ATMO.DFC.Naming Naming- ID
        /// </summary>
        NID,

        /// <summary>
        /// Dokuterm, der einen Platzhalter für den Wert eines Eigenschaftsausdruckes darstellt.
        /// Wird beim Pattern- Matching berücksichtigt.
        /// </summary>
        WildCard
    }

    /// <summary>
    /// mko, 6.3.2018
    /// 
    /// mko, 22.7.2021
    /// Brutale Entscheidung: Eigenschaft Childs entfernt. Schnittstelle IToken entfernt
    /// Damit gibt es nur noch streng typisierte DocuTerm- Strukturen (Kampf den Nullwerten)
    /// </summary>
    public interface IDocuEntity
    {
        /// <summary>
        /// Type of DocuEntity
        /// </summary>
        DocuEntityTypes EntityType { get; }

    }


}
