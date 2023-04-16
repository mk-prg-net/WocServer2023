using MKPRG.Naming;

// mko, 16.4.2023
namespace TryOut.MySingeltons
{
    // Liefert alle aktuell definierten Namenscontainer
    public class MyNamingContainers
    {
        IReadOnlyDictionary<long, INaming> _NC;

        public MyNamingContainers()
        {
            var tools = new Tools();

            var getNC = tools.GetNamingContainers("MKPRG.Naming");

            if (getNC.succeded)
            {
                _NC = getNC.ncDict;
            }
            else
            {
                if (getNC.duplicates.Any())
                {
                    var duplikate = string.Join(",", getNC.duplicates.Select(r => $"{r.CNT}: {r.ID}").ToArray());
                    throw new Exception($"tools.GetNamingContainers ist gescheitert. Duplikate: {duplikate}");
                }
                else
                {
                    throw new Exception($"tools.GetNamingContainers ist gescheitert. Allgemeiner Fehler");
                }
            }                
        }

        public IReadOnlyDictionary<long, INaming> NC => _NC;
    }
}
