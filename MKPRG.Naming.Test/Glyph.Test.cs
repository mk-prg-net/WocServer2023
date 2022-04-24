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
            Debug.WriteLine($"# Glyphs, Stand {DateTime.Now.ToLongDateString()} {DateTime.Now.ToLongTimeString()}");

            _CreateGlyphs(TGlyphs, 2);

        }

        string HLevel(int deepth)
        {
            var hl = "";

            for(int i = 0; i < deepth; i++)
            {
                hl += "#";
            }

            return hl;
        }

        public void _CreateGlyphs(Type glyphContainer, int deepth)
        {
            var cats = glyphContainer.GetNestedTypes(BindingFlags.Public | BindingFlags.Static);

            if (cats.Any())
            {
                foreach (var cat in cats.OrderBy(c => c.Name))
                {

                    Debug.WriteLine($"\n{HLevel(deepth)} {cat.Name}\n");
                    Debug.WriteLine($"Symbol | HtmlEntity | Descr.");
                    Debug.WriteLine($"-------|------------|-------");
                    var props = cat.GetProperties(BindingFlags.Public | BindingFlags.Static);

                    foreach (var prop in props.OrderBy(p => p.Name))
                    {
                        // Zugriff auf statische Eigenschaften- Null :-)
                        var val = prop.GetValue(null, null) as string;
                        var sym = Glyphs.toStr(val);
                        Debug.WriteLine($"{sym} | `{val}` | {prop.Name}");
                    }

                    // Subkategorien bearbeiten
                    _CreateGlyphs(cat, deepth + 1);
                }
            }

        }

    }
}
