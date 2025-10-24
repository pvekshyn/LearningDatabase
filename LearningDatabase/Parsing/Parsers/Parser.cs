using LearningDatabase.Tokenization;

namespace LearningDatabase.Parsing.Parsers;

internal static class Parser
{
    public static ASTNode Parse(IEnumerable<Token> tokens)
    {
        var enumerator = tokens.GetEnumerator();

        return GetNextType(enumerator) switch
        {
            TokenType.Select => SelectParser.Parse(enumerator),
            _ => throw new NotSupportedException()
        };
    }

    private static TokenType GetNextType(IEnumerator<Token> enumerator)
    {
        enumerator.MoveNext();
        return enumerator.Current.Type;
    }
}
