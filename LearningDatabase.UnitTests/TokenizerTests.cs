using LearningDatabase.Tokenization;

namespace LearningDatabase.UnitTests;

public class TokenizerTests
{
    [Fact]
    public void TokenizeSelectTest()
    {
        var sql = "SELECT * FROM Users";

        var tokens = Tokenizer.Tokenize(sql).ToList();

        Assert.Equal(4, tokens.Count);
        Assert.Equal(TokenType.Select, tokens[0].Type);
        Assert.Equal(TokenType.Asterisk, tokens[1].Type);
        Assert.Equal(TokenType.From, tokens[2].Type);
        Assert.Equal(TokenType.Identifier, tokens[3].Type);
    }
}