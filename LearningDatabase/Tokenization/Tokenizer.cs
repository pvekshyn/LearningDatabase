namespace LearningDatabase.Tokenization;

internal static class Tokenizer
{
    private static Dictionary<char, TokenType> reservedSymbols = new Dictionary<char, TokenType>()
        {
            { ',', TokenType.Comma },
            { '(', TokenType.OpenParens },
            { ')', TokenType.CloseParens },
            { '*', TokenType.Asterisk },
            { '=', TokenType.Equal },
            { '+', TokenType.Plus },
            { '-', TokenType.Minus },
            { '>', TokenType.Greater },
            { '<', TokenType.Less }
        };

    private static Dictionary<string, TokenType> reservedWords = new Dictionary<string, TokenType>(StringComparer.InvariantCultureIgnoreCase)
        {
            { "CREATE", TokenType.Create },
            { "TABLE", TokenType.Table },
            { "INDEX", TokenType.Index },
            { "ON", TokenType.On },
            { "INSERT", TokenType.Insert },
            { "INTO", TokenType.Into },
            { "VALUES", TokenType.Values },
            { "SELECT", TokenType.Select },
            { "FROM", TokenType.From },
            { "DROP", TokenType.Drop },
            { "WHERE", TokenType.Where },
            { "ORDER", TokenType.Order },
            { "BY", TokenType.By },
            { "AND", TokenType.And },
            { "OR", TokenType.Or },
            { "JOIN", TokenType.Join },
        };

    public static IEnumerable<Token> Tokenize(this string input)
    {
        int currentPosition = 0;
        while (currentPosition < input.Length)
        {
            var inputSpan = input.AsSpan();
            var c = inputSpan[currentPosition];
            if (char.IsWhiteSpace(c))
            {
                currentPosition++;
                continue;
            }

            var token = c switch
            {
                _ when c.IsReservedSymbol() => new Token(reservedSymbols[c], inputSpan.ReadSymbolFrom(ref currentPosition)),
                _ when c.IsBeginningOfNumber() => new Token(TokenType.Number, inputSpan.ReadNumberFrom(ref currentPosition)),
                _ when c.IsBeginningOfStringLiteral() => new Token(TokenType.StringLiteral, inputSpan.ReadStringLiteralFrom(ref currentPosition)),
                _ when c.IsBeginningOfWord() => inputSpan.ReadWordFrom(ref currentPosition) switch
                {
                    var word when reservedWords.ContainsKey(word) => new Token(reservedWords[word], word),
                    var word => new Token(TokenType.Identifier, word)
                },
                _ => throw new NotImplementedException()
            };

            yield return token;
        }
    }

    private static bool IsReservedSymbol(this char c)
    {
        return reservedSymbols.ContainsKey(c);
    }

    private static bool IsBeginningOfNumber(this char c)
    {
        return char.IsDigit(c);
    }

    private static bool IsBeginningOfStringLiteral(this char c)
    {
        return c == '\'';
    }

    private static bool IsBeginningOfWord(this char c)
    {
        return char.IsLetter(c);
    }

    private static string ReadSymbolFrom(this ReadOnlySpan<char> input, ref int currentPosition)
    {
        var c = input[currentPosition];
        currentPosition++;
        return c.ToString();
    }

    private static string ReadNumberFrom(this ReadOnlySpan<char> input, ref int currentPosition)
    {
        return input.TakeWhile(char.IsNumber, ref currentPosition);
    }

    private static string ReadWordFrom(this ReadOnlySpan<char> input, ref int currentPosition)
    {
        return input.TakeWhile(x => char.IsLetterOrDigit(x) || x == '.', ref currentPosition);
    }

    private static string ReadStringLiteralFrom(this ReadOnlySpan<char> input, ref int currentPosition)
    {
        currentPosition++;
        var result = input.TakeWhile(x => x != '\'', ref currentPosition);
        currentPosition++;
        return result;
    }

    private static string TakeWhile(this ReadOnlySpan<char> input, Func<char, bool> predicate, ref int currentPosition)
    {
        int start = currentPosition;
        while (currentPosition < input.Length && predicate(input[currentPosition]))
        {
            currentPosition++;
        }
        return input[start..currentPosition].ToString();
    }
}
