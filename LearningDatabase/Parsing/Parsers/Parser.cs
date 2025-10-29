using LearningDatabase.Tokenization;

namespace LearningDatabase.Parsing.Parsers;

internal static class Parser
{
    public static ASTNode Parse(IEnumerable<Token> tokens)
    {
        var enumerator = tokens.GetEnumerator();

        return GetNextType(enumerator) switch
        {
            TokenType.Create => GetNextType(enumerator) switch
            {
                TokenType.Table => CreateTableParser.Parse(enumerator),
                _ => throw new NotSupportedException()
            },
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
