using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using NaLisp = mko.NaLisp;
using System.Text.RegularExpressions;


namespace MKPRG.CSSQL.Test
{
    [TestClass]
    public class WhereExpressions
    {

        SQL<Woc.Author> qBG = new SQL<Woc.Author>();  
        TabAuthors tab;

        [TestInitialize]
        public void Init()
        {
            tab = new TabAuthors();
        }


    }
}
