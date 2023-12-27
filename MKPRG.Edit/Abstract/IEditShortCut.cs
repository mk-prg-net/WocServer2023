using System;

namespace MKPRG.Edit.Abstract
{
    /// <summary>
    /// mko, 27.12.2023
    /// Registrierte Tastenkombination zum Aufruf einer Funktion oder von Sonderzeichen im Editor.
    /// </summary>
    public interface IEditShortCut
    {
        string EditShortCut { get; }

    }
}
