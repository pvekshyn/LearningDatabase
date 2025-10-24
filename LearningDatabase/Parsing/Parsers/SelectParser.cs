using LearningDatabase.Tokenization;

namespace LearningDatabase.Parsing.Parsers;

internal static class SelectParser
{
    public static ASTNode Parse(IEnumerator<Token> enumerator)
    {
        enumerator.MoveNext();
        var outputList = ParseOutputList(enumerator);
        var from = ParseFrom(enumerator);
        return new SelectASTNode(outputList, from);
    }

    private static List<ExpressionASTNode> ParseOutputList(IEnumerator<Token> enumerator)
    {
        var outputList = new List<ExpressionASTNode>();
        while (enumerator.Current.Type != TokenType.From)
        {
            if(enumerator.Current.Type == TokenType.Comma)
                enumerator.MoveNext();

            var token = enumerator.Current;
            ExpressionASTNode node = token.Type switch
            {
                TokenType.Number => new ConstASTNode<int>(int.Parse(token.Value)),
                TokenType.StringLiteral => new ConstASTNode<string>(token.Value),
                TokenType.Identifier => new ColumnASTNode(token.Value),
                TokenType.Asterisk => new AllColumnsASTNode(),
                _ => throw new NotSupportedException()
            };
            outputList.Add(node);
            enumerator.MoveNext();
        }
        return outputList;
    }

    private static FromASTNode ParseFrom(IEnumerator<Token> enumerator)
    {
        enumerator.MoveNext(); //table name
        var table = new ConstASTNode<string>(enumerator.Current.Value);
        return new FromASTNode(table);
    }
}
