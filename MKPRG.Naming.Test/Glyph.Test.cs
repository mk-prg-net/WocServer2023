using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics;
using System.Linq;

using System.Reflection;

namespace MKPRG.Naming.Test
{
    [TestClass]
    public class GlyphsTest
    {
        NamingHelper NH;

        [TestInitialize]
        public void Init()
        {
            NH = new NamingHelper(Tracing.RC.NC, Language.DE);
        }

        [TestMethod]
        public void CreateGlyphs()
        {
            var TGlyphs = typeof(Glyphs);

            var cats = TGlyphs.GetNestedTypes(BindingFlags.Public | BindingFlags.Static);

            Debug.WriteLine($"# Glyphs, Stand {DateTime.Now.ToLongDateString()} {DateTime.Now.ToLongTimeString()}");

            foreach(var cat in cats.OrderBy(c => c.Name))
            {

                Debug.WriteLine($"\n## {cat.Name}\n");
                Debug.WriteLine($"Symbol | HtmlEntity | Descr.");
                Debug.WriteLine($"-------|------------|-------");
                var props = cat.GetProperties(BindingFlags.Public | BindingFlags.Static);

                foreach(var prop in props.OrderBy(p => p.Name))
                {
                    // Zugriff auf statische Eigenschaften- Null :-)
                    var val = prop.GetValue(null, null) as string;
                    var sym = Glyphs.toStr(val);
                    Debug.WriteLine($"{sym} | `{val}` | {prop.Name}");
                }
            }

        }
    }
}
