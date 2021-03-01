using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MKPRG.CSSQL
{
    /// <summary>
    /// mko, 5.6.2018
    /// Collection of tools for column table access.
    /// </summary>
    public static class TabColAccess
    {
        /// <summary>
        /// Save Access to nullable columns
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="v"></param>
        /// <param name="Default"></param>
        /// <returns></returns>
        public static T GetSave<T>(object v, T Default)
        {
            if (v == null || v is System.DBNull)
            {
                return Default;
            }
            else
            {
                return (T)v;
            }
        }
    }
}