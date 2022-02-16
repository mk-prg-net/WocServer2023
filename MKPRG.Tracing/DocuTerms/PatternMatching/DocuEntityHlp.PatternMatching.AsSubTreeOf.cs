using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ANC = MKPRG.Naming;
using TTD = MKPRG.Naming.DocuTerms;
using TT = MKPRG.Naming.TechTerms;
using System.Diagnostics;

namespace MKPRG.Tracing.DocuTerms
{
    /// <summary>
    /// mko, 2.8.2021
    /// </summary>
    public static partial class DocuEntityHlp
    {
        /// <summary>
        /// mko, 2.8.2021
        /// </summary>
        /// <param name="pnL"></param>
        /// <param name="subTree"></param>
        /// <returns></returns>
        private static RC<(IDocuEntity subTree, IDocuEntity subTreeParent, long depth)> CreateFailedRC(IComposer pnL, IDocuEntity errorDescr)
            => RC<(IDocuEntity, IDocuEntity, long)>.Failed(
                        value: (pnL.i(TTD.Types.UndefinedDocuTerm.UID), pnL.i(TTD.Types.UndefinedDocuTerm.UID), -1),
                        ErrorDescription: errorDescr);


        /// <summary>
        /// mko, 29.3.2019
        /// Eine Baumstruktur als Teilbaum (Muster) in einem anderen Baume suchen. Wenn das Muster auf einem Zweig im anderen Baum passt, die Wurzel dieses Zweiges
        /// zurückgeben.
        /// Die Suche erfolgt top-down.
        ///         
        /// p\_     /
        /// i\p\100 / IsSubTreeOf
        /// </summary>
        /// <param name="subTreePattern"></param>
        /// <param name="treeRoot"></param>
        /// <param name="searchAnywhere">Wenn false, dann muss der Baumn mit dem Teilbaumabschnitt beginnen. sonst wird nach dem ersten Teilbaum linksrekursiv gesucht</param>
        /// <param name="PropertyValueWildCard">Bei Properties im subTreePattern mit diesem Wert muss nur der Eigenschaftsname übereinstimmen, nicht jedoch der Wert</param>
        /// <returns></returns>
        public static RC<(IDocuEntity subTree, IDocuEntity subTreeParent, long depth)> AsSubTreeOf(
            this IDocuEntity subTreePattern,
            IDocuEntity treeRoot,
            IComposer pnL,
            bool searchAnywhere = true,
            int maxLevel = int.MaxValue,
            IDocuEntity subTreeParent = null)
        {
            // Fehlermeldungen erzeugen
            Func<IDocuEntity, RC<(IDocuEntity subTree, IDocuEntity subTreeParent, long depth)>> CreateFailedRC
                = _errDescr
                    => RC<(IDocuEntity, IDocuEntity, long)>.Failed(
                        value: (pnL.i(TTD.Types.UndefinedDocuTerm.UID), pnL.i(TTD.Types.UndefinedDocuTerm.UID), -1),
                        ErrorDescription: _errDescr);


            var ret = CreateFailedRC(pnL.ReturnSearchFailsEmptyResult(pnL.EncapsulateAsPropertyValue(subTreePattern)));

            IDocuEntity matchedDocuTerm = pnL.i(TTD.Types.UndefinedDocuTerm.UID);
            IDocuEntity parentMatchedDocuTerm = pnL.i(TTD.Types.UndefinedDocuTerm.UID);
            int deepthOfMatchedDocuTerm = -1;

            Action<IDocuEntity, IDocuEntity, int> catchMatchedDocuTerm =
            (_matched, _parent, _deepth) =>
            {
                // Nur eine Struktur als ergebnis zurückliefern, die tatsächlich dem gesuchten Subtree entspricht.
                if (subTreePattern.EntityType == _matched.EntityType)
                {
                    matchedDocuTerm = _matched;
                    parentMatchedDocuTerm = _parent;
                    deepthOfMatchedDocuTerm = _deepth;
                }
            };


            // Prüfen von Vorbedingungen, die in der komplexen Umgebung des Aufrufes nicht notwendigerweise erfüllt sein müssen
            // da treeRoot und subTreePattern Ergebnisse von vorausgegangenen Berechnungen sein können.
            // Deshalb ist Debug.Assert hier nicht ausreichend.
            if (subTreePattern == null)
            {
                ret = CreateFailedRC(pnL.ReturnValidatePreconditionNotNullFailed(pnL.p(TTD.MetaData.Arg.UID, TT.Trees.SubTree.UID)));
            }
            else if (treeRoot == null)
            {
                ret = CreateFailedRC(pnL.ReturnValidatePreconditionNotNullFailed(pnL.p(ANC.DocuTerms.MetaData.Arg.UID, ANC.TechTerms.Trees.Root.UID)));
            }
            else if (IsSubTreeTestForEntityType[subTreePattern.EntityType]
                        (subTreePattern,
                         treeRoot,
                         treeRoot,
                         catchMatchedDocuTerm,
                         true,
                         searchAnywhere,
                         0,
                         maxLevel))
            {
                ret = RC<(IDocuEntity, IDocuEntity, long)>.Ok(value: (matchedDocuTerm, parentMatchedDocuTerm, deepthOfMatchedDocuTerm));
            }

            return ret;
        }

        /// <summary>
        /// mko, 1.4.2019
        /// Sucht alle Teilbäume in einem Baum mit gegebener Struktur.
        /// 
        /// mko, 6.8.2021
        /// Vollstänfdig reimplementiert
        /// </summary>
        /// <param name="subTreePattern"></param>
        /// <param name="treeRoot"></param>
        /// <param name="pnL"></param>
        /// <param name="deepth"></param>
        /// <param name="PropertyValueWildCard">Bei Properties im subTreePattern mit diesem Wert muss nur der Eigenschaftsname übereinstimmen, nicht jedoch der Wert</param>
        /// <returns></returns>
        public static RC<IEnumerable<(IDocuEntity subTree, IDocuEntity subTreeParent, long level)>> AsSubTreeOf_AllOccurrences
            (this IDocuEntity subTreePattern,
            IDocuEntity treeRoot,
            IComposer pnL,
            int maxLevel = int.MaxValue,
            IDocuEntity parent = null)
        {
            Debug.Assert(pnL != null);

            // Fehlermeldungen erzeugen
            Func<IDocuEntity, RC<IEnumerable<(IDocuEntity subTree, IDocuEntity subTreeParent, long depth)>>> CreateFailedRC
                = _errDescr
                    => RC<IEnumerable<(IDocuEntity, IDocuEntity, long)>>.Failed(
                        value: new (IDocuEntity subTree, IDocuEntity subTreeParent, long depth)[] { (pnL.i(TTD.Types.UndefinedDocuTerm.UID), pnL.i(TTD.Types.UndefinedDocuTerm.UID), -1) },
                        ErrorDescription: _errDescr);

            var matches = new List<(IDocuEntity subTree, IDocuEntity subTreeParent, long depth)>();

            Action<IDocuEntity, IDocuEntity, int> catchMatchedDocuTerm =
            (_matched, _parent, _deepth) =>
            {
                if (_matched.EntityType == subTreePattern.EntityType)
                {
                    matches.Add((_matched, _parent, _deepth));
                }
            };

            var ret = CreateFailedRC(pnL.ReturnNotCompleted(
                                        "AsSubTreeOf_AllOccurrences",
                                        pnL.KillMethodParamIf(subTreePattern == null, () => (IMethodParameter)pnL.p(TT.Trees.SubTree.UID, pnL.EncapsulateAsPropertyValue(subTreePattern))),
                                        pnL.KillMethodParamIf(treeRoot == null, () => (IMethodParameter)pnL.p(TT.Trees.Root.UID, pnL.EncapsulateAsPropertyValue(treeRoot))),
                                        pnL.p(TT.Trees.Level.UID, maxLevel)));
            try
            {
                // Prüfen von Vorbedingungen, die in der komplexen Umgebung des Aufrufes nicht notwendigerweise erfüllt sein müssen
                // da treeRoot und subTreePattern Ergebnisse von vorausgegangenen Berechnungen sein können.
                // Deshalb ist Debug.Assert hier nicht ausreichend.
                if (subTreePattern == null)
                {
                    ret = CreateFailedRC(pnL.ReturnValidatePreconditionNotNullFailed(pnL.p(TTD.MetaData.Arg.UID, TT.Trees.SubTree.UID)));
                }
                else if (treeRoot == null)
                {
                    ret = CreateFailedRC(pnL.ReturnValidatePreconditionNotNullFailed(pnL.p(TTD.MetaData.Arg.UID, TT.Trees.Root.UID)));
                }
                else
                {
                    IsSubTreeTestForEntityType[subTreePattern.EntityType]
                         (subTreePattern,
                          treeRoot,
                          treeRoot,
                          catchMatchedDocuTerm,
                          false,                 // finishAfterPatternMatched
                          true,                  //search anywhere
                          0,                     // current level
                          maxLevel);

                    if (matches.Any())
                    {
                        ret = RC<IEnumerable<(IDocuEntity, IDocuEntity, long)>>.Ok(value: matches);
                    }
                    else
                    {
                        ret = CreateFailedRC(pnL.ReturnFetchWarnEmptySet(TTD.Types.DocuTerms.UID, TTD.Types.DocuTerms.UID, pnL.EncapsulateAsPropertyValue(subTreePattern)));
                    }
                }
            }
            catch (Exception ex)
            {
                ret = CreateFailedRC(TraceHlp.FlattenExceptionMessagesPN(ex));
            }

            return ret;
        }

    }
}
