# Spezielle DocuEntities- Implementierung für den Parser

In der Vorausgegangenen Implementierung waren alle **DocuEntities** Klassen von der Basisklasse `DocuEntities.DocuEntity` abgeleitet. Diese implementierte die `IDocuEntity` und die `mko.RPN.IToken` Schnittstellen.

Die `mko.RPN.IToken` Schnittstelle ist nur für den `PNDocuTems.Parser` von Bedeutung, da beim  Parsen von *DocuTermen* diese zunächst als `IToken` auf dem `Stack<IToken>` des *Parsers* zwischengespeichert werden für weitere Analysen.

Nachteil war, das auch unabhängig vom Parser generierte *DocuTerme* alle Member der `IToken` Schnittstelle implementierten, obwohl diese hier keine Bedeutung hatten. Beim Debuggen ist das verwirrend, da Eigenschaften aus der Schnittstelle `IToken`, wie z.B. `IToken.Value` mit Facheigenschaften der *DokuTerme * sich in der Benennung überschnitten.

## Entfernung der `IToken` Schnittstelle aus der allgemeine DocuTerm- Implementierung 

Es gibt zwei möglichkeiten, die `IToken` Schnittstelle aus der allgemeinen Implementierung der *DocuTerme* zu entfernen:

1. Implementieren von Dekoratoren, die die allgemeinen *DocuTerm* - Klassen erweitern um `IToken`
2. Ableiten von Klassen aus den bestehenden *DocuTerm*- Klassen, welche zusätzlich die `IToken` Schnittstele implementieren

### Variante 1: Decoratoren

Hier eine Pilotimplementierung für die *DocuTerm*- `IInstance`

``````c#
using mko.RPN;
using ATMO.mko.Logging.PNDocuTerms.DocuEntities;

namespace ATMO.mko.Logging.PNDocuTerms.Parser
{
    public class InstanceTokenDeco
        : DocuTermToken,
        IInstance

    {
        IInstance _i;

        public InstanceTokenDeco(IInstance i)
            : base(i.EntityType)
        {
            _i = i;
        }

        // Herausführen der bestehenden Eigenschaften gemäß Decorator- Pattern
        public IInstanceMember[] InstanceMembers => _i.InstanceMembers;

        // Dekorieren mit neuen Eigenschaften aus `IToken`
        public override int CountOfEvaluatedTokens
            => _i.InstanceMembers.Select(
                        r => r is IToken t ? t.CountOfEvaluatedTokens : 1
                    ).Sum() + 1;

    }
}
``````



