using LearningDatabase.Parsing;
using LearningDatabase.Parsing.Parsers;
using LearningDatabase.Tokenization;

namespace LearningDatabase.UnitTests;
public class ParserTests
{
    [Fact]
    public void ParseSelectAllTest()
    {
        var sql = "SELECT * FROM Users";

        var tokens = Tokenizer.Tokenize(sql);
        var ASTNode = Parser.Parse(tokens);

        Assert.IsType<SelectASTNode>(ASTNode);
        var selectNode = ASTNode as SelectASTNode;
        Assert.Equal("Users", selectNode.From.Table.Value);
        Assert.Single(selectNode.OutputList);
        Assert.IsType<AllColumnsASTNode>(selectNode.OutputList.Single());
    }

    [Fact]
    public void ParseSelectTest()
    {
        var sql = "SELECT Id, Name FROM Users";

        var tokens = Tokenizer.Tokenize(sql);
        var ASTNode = Parser.Parse(tokens);

        Assert.IsType<SelectASTNode>(ASTNode);
        var selectNode = ASTNode as SelectASTNode;
        Assert.Equal("Users", selectNode.From.Table.Value);
        
        Assert.Equal(2, selectNode.OutputList.Count);
        Assert.IsType<ColumnASTNode>(selectNode.OutputList.First());
        Assert.IsType<ColumnASTNode>(selectNode.OutputList.Last());
    }
}
