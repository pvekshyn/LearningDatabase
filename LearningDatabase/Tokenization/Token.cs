namespace LearningDatabase.Tokenization;

internal record struct Token(TokenType Type, string Value)
{
    public int Precedence = Type switch
    {
        TokenType.Or => 1,
        TokenType.And => 2,
        TokenType.Equal => 3,
        TokenType.Greater or TokenType.Less => 4,
        TokenType.Plus or TokenType.Minus => 5,
        TokenType.Asterisk => 6,
        _ => 0
    };
}
