using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKPRG.Tracing.DocuTerms
{
    /// <summary>    
    /// mko, 16.6.2020
    /// 
    /// mko, 9.7.2021
    /// 
    /// *KillIfNot- Terme* werden zur *Entwurfszeit* evaluiert. Der Entstehende 
    /// **DocuTerm**- Baum enthält nur die Elemente, für die beim Erstellen die Bedingung 
    /// zutraf. 
    /// Dabei erzeugt eine Composer- Funnktion ein *KillIfNot- Objekt*, das die Schnittstelle
    /// `IKillIfNot` implementiert.
    /// Diese *KillIfNot- Objekt* sind entweder ein *Instanzmember*, ein *Methodenparameter*, ein 
    /// *Ereignisparameter* oder ein Listenelement. 
    /// Eigenschaftswerte dürfen kein IKillIfNot- 
    /// Objekt sein, da Eigenschaften mit undefinierten Werten (im Fall, die Bedingung im
    /// KillIfNot traf nicht zu), in den DocuTerms nicht definiert sind. Folglich darf es 
    /// keine Composer- Funkktion geben, die in den Eigenschaftswerten ein von 
    /// `IKillIfNot` abgeleiteten Parameter aktzeptiert.
    /// 
    /// mko, 12.7.2021
    /// Weiter ausdifferenziert durch strenge Typisierung
    /// Die Schnittstelle enthält nur noch die Eigenschaft **Condition**. 
    /// Die Ableitung von den Schnittstellen wie IListMember, IMethodParameter oder IInstanceMember
    /// findet nicht mehr statt. Diese ableitungen erfolgen in spezialisierten Schnittstellen
    /// </summary>
    public interface IKillIfNot
    {
        /// <summary>
        /// Bedingung, unter der die Löschung erfolgen soll
        /// </summary>
        bool Condition { get; }

        /// <summary>
        /// Listenelement, welches bedingt angelegt werden soll
        /// </summary>
        //IListMember DocuEntity { get; }
    }
}
