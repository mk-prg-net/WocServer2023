using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using NaLisp = mko.NaLisp;
using System.Text.RegularExpressions;
using MKPRG.Woc.Concrete;

namespace MKPRG.CSSQL.Test
{
    [TestClass]
    public class WhereExpressions
    {

        SQL<Author> qBG = new SQL<Author>();  
        TabAuthors tab;

        [TestInitialize]
        public void Init()
        {
            tab = new TabAuthors();
        }


    }
}
