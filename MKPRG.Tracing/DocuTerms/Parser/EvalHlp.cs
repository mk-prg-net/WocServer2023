using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using mko.RPN;

using MKPRG.Tracing.DocuTerms;
using static MKPRG.Tracing.DocuTerms.ComposerSubTrees;

using ANC = MKPRG.Naming;

namespace MKPRG.Tracing.DocuTerms.Parser.Parser
{
    public static class EvalHlp
    {


        //public static DocuTerms.String EvaluateName(Stack<IToken> stack, string errorMsg)
        //{
        //    var tokName = stack.Peek();

        //    //TraceHlp.ThrowArgExIfNot(!tokName.IsFunctionName && StringToken.Test(tokName), errorMsg);

        //    // 9.5.2018: relaxed restrictions for property names: also int or numeric literals are allowed.
        //    // 2.3.2020: Doppelte Verneinung aufgehoben
        //    //TraceHlp.ThrowArgExIfNot(!tokName.IsFunctionName, errorMsg);
        //    TraceHlp.ThrowArgExIf(tokName.IsFunctionName, errorMsg);

        //    var name = stack.Pop().Value;

        //    // mko, 28.5.2020
        //    // 
        //    // Namen in CNT von DocuTerms wie Events, Instanzen etc. in die DokuTerm- Id umwandeln             
        //    if (DFC.Naming.Tools.NamingIdForCNTNameOfDocuTerm.ContainsKey(name))
        //    {
        //        name = DFC.Naming.Tools.NamingIdForCNTNameOfDocuTerm[name];
        //    }

        //    return new DocuTerms.String(name);
        //}

        /// <summary>
        /// mko, 10.6.2020
        /// Neuimplementiert unter Berücksichtigung von Namen als sprachneutrale NID's
        /// </summary>
        /// <param name="stack"></param>
        /// <param name="errorMsg"></param>
        /// <returns></returns>
        public static IPropertyValue EvaluateName(Stack<IToken> stack, IComposer pnL, long NID_DocuTermType)
        {

            IPropertyValue Name = null;

            var token = stack.Peek();

            TraceHlp.ThrowArgExIfNot(token is DocuTerms.NID || StringToken.Test(token), 
                pnL.ReturnDocuTermSyntaxError(NID_DocuTermType, ANC.DocuTerms.Parser.Errors.Name_NidOrStringTokenForNameExpected.UID));

            if (token is NID nid)
            {
                // Name liegt als sprachneutrale NID vor- vom Stapel nehmen
                stack.Pop();
                Name = nid;
            }
            else
            {
                // Name wurde als String definiert- vom Stapel nehmen
                stack.Pop();
                Name = new DocuTerms.String(token.Value);
            }

            return Name;
        }

        /// <summary>
        /// mko, 24.6.2020
        /// Anlegen von DocuTerms, die einem Namen einen Wert zuordnen.
        /// </summary>
        /// <typeparam name="TDocuTerm"></typeparam>
        /// <param name="type"></param>
        /// <param name="stack"></param>
        /// <param name="restrictions"></param>
        /// <param name="factory"></param>
        /// <param name="fmt"></param>
        //public static void EvalNameValuePair<TDocuTerm>(
        //    DocuTerms.DocuEntityTypes type,
        //    Stack<IToken> stack,
        //    Func<IToken, bool> restrictions,
        //    Func<DocuTerms.IComposer, DocuTerms.IDocuEntity, DocuTerms.IDocuEntity, TDocuTerm> factory,
        //    DocuTerms.IComposer pnL)
        //    where TDocuTerm : class, DocuTerms.IDocuEntity
        //{
        //    var name = EvalHlp.EvaluateName(stack, $"{type} without name and value");

        //    if (stack.Any() && restrictions(stack.Peek()))
        //    {
        //        var tok = stack.Peek();

        //        DocuTerms.IDocuEntity Value = null;

        //        if (tok is DocuTerms.IDocuEntity)
        //        {
        //            Value = (DocuTerms.IDocuEntity)stack.Pop();
        //        }
        //        else
        //        {
        //            stack.Pop();
        //            if (tok is BoolToken bTok)
        //            {
        //                Value = pnL.boolean(bTok.ValueAsBool);
        //            }
        //            else if (tok is IntToken iTok)
        //            {
        //                Value = pnL.integer(iTok.ValueAsInt);
        //            }
        //            else if (tok is DoubleToken dTok)
        //            {
        //                Value = pnL.dbl(dTok.ValueAsDouble);
        //            }
        //            else
        //            {
        //                Value = pnL.str(tok.Value);
        //            }
        //        }
        //        var p = factory(pnL, name, Value); // new DocuTerms.DocuEntity(fmt, type, name, Value);
        //        stack.Push(p);
        //    }
        //    else
        //    {
        //        TraceHlp.ThrowArgEx("Restrictions violated");
        //    }
        //}

        /// <summary>
        /// mko, 24.6.2020
        /// Anlegen von DocuTerms, die einem Namen einen Wert zuordnen. Der Wert kann auch ausfallen.
        /// </summary>
        /// <typeparam name="TDocuTerm"></typeparam>
        /// <param name="type"></param>
        /// <param name="stack"></param>
        /// <param name="restrictions"></param>
        /// <param name="factory"></param>
        /// <param name="factoryForEmptyValue"></param>
        /// <param name="fmt"></param>
        //public static void EvalNameValuePair<TDocuTerm>(
        //    DocuTerms.DocuEntityTypes type,
        //    Stack<IToken> stack,
        //    Func<IToken, bool> restrictions,
        //    Func<DocuTerms.IComposer, DocuTerms.IDocuEntity, DocuTerms.IDocuEntity, TDocuTerm> factory,
        //    Func<DocuTerms.IComposer, DocuTerms.IDocuEntity, TDocuTerm> factoryForEmptyValue,
        //    DocuTerms.IComposer pnL)
        //    where TDocuTerm : DocuTerms.IDocuEntity
        //{
        //    var name = EvalHlp.EvaluateName(stack, $"{type} without name and value");

        //    if (stack.Any() && restrictions(stack.Peek()))
        //    {
        //        var tok = stack.Peek();

        //        DocuTerms.IDocuEntity Value = null;

        //        if (tok is DocuTerms.IDocuEntity)
        //        {
        //            Value = (DocuTerms.IDocuEntity)stack.Pop();
        //        }
        //        else
        //        {
        //            stack.Pop();
        //            if (tok is BoolToken bTok)
        //            {
        //                Value = pnL.boolean(bTok.ValueAsBool);
        //            }
        //            else if (tok is IntToken iTok)
        //            {
        //                Value = pnL.integer(iTok.ValueAsInt);
        //            }
        //            else if (tok is DoubleToken dTok)
        //            {
        //                Value = pnL.dbl(dTok.ValueAsDouble);
        //            }
        //            else
        //            {
        //                Value = new DocuTerms.String(tok.Value);
        //            }
        //        }
        //        var p = factory(pnL, name, Value); // new DocuTerms.DocuEntity(fmt, type, name, Value);
        //        stack.Push(p);
        //    }
        //    else if (!stack.Any())
        //    {
        //        stack.Push(factoryForEmptyValue(pnL, name));
        //    }
        //    else
        //    {
        //        TraceHlp.ThrowArgEx("Restrictions violated");
        //    }
        //}


        //public static void EvalValue<TDocuTerm>(
        //    DocuTerms.IComposer pnL,
        //    DocuTerms.DocuEntityTypes type,
        //    Stack<IToken> stack,
        //    Func<IToken, bool> restrictions,
        //    Func<DocuTerms.IComposer, DocuTerms.IDocuEntity, TDocuTerm> factory,
        //    Func<DocuTerms.IComposer, TDocuTerm> factoryForEmptyValue)
        //    where TDocuTerm : DocuTerms.IDocuEntity
        //{
        //    if (stack.Any() && restrictions(stack.Peek()))
        //    {
        //        var tok = stack.Peek();

        //        DocuTerms.IDocuEntity Value = null;

        //        if (tok is DocuTerms.IDocuEntity)
        //        {
        //            Value = (DocuTerms.IDocuEntity)stack.Pop();
        //        }
        //        else
        //        {
        //            // mko, 21.12.2018
        //            // An Verhalten von EvalNameValuePair angeglichen
        //            stack.Pop();
        //            if (tok is BoolToken bTok)
        //            {
        //                Value = pnL.boolean(bTok.ValueAsBool);
        //            }
        //            else if (tok is IntToken iTok)
        //            {
        //                Value = pnL.integer(iTok.ValueAsInt);
        //            }
        //            else if (tok is DoubleToken dTok)
        //            {
        //                Value = pnL.dbl(dTok.ValueAsDouble);
        //            }
        //            else
        //            {
        //                Value = pnL.str(tok.Value);
        //            }
        //        }

        //        var p = factory(pnL, Value);
        //        stack.Push(p);
        //    }
        //    else if(!stack.Any())
        //    {
        //        var p = factoryForEmptyValue(pnL);
        //        stack.Push(p);
        //    }
        //    else
        //    {
        //        TraceHlp.ThrowArgEx("Restrictions violated");
        //    }
        //}

        /// <summary>
        /// mko, 21.12.2018
        /// Zur separaten Verarbeitung von Name und Parameter beim Evaluieren von Methoden entwickelt
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="fn"></param>
        /// <param name="type"></param>
        /// <param name="stack"></param>
        /// <param name="restrictions"></param>
        //public static void EvalValue(
        //    DocuTerms.IDocuEntity Name,
        //    DocuTerms.IComposer pnL,
        //    DocuTerms.DocuEntityTypes type,
        //    Stack<IToken> stack,
        //    Func<IToken, bool> restrictions)
        //{
        //    if (stack.Any() && restrictions(stack.Peek()))
        //    {
        //        var tok = stack.Peek();

        //        DocuTerms.IDocuEntity Value = null;

        //        if (tok is DocuTerms.IDocuEntity)
        //        {
        //            Value = (DocuTerms.IDocuEntity)stack.Pop();
        //        }
        //        else
        //        {
        //            // mko, 21.12.2018
        //            // An Verhalten von EvalNameValuePair angeglichen
        //            stack.Pop();

        //            Value = pnL.str(tok.Value);
        //        }
        //        var p = new DocuTerms.DocuEntity(fmt, type, Name, Value);
        //        stack.Push(p);
        //    }
        //    else
        //    {
        //        TraceHlp.ThrowArgEx("Restrictions violated");
        //    }
        //}
    }
}
