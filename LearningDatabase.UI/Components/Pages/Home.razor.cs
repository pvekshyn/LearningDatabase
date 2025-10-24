using LearningDatabase.Parsing.Parsers;
using LearningDatabase.Tokenization;

namespace LearningDatabase.UI.Components.Pages;

public partial class Home
{
    public string Sql { get; set; }
    MyDiagram ASTDiagram;

    protected override void OnInitialized()
    {
        Sql = "SELECT * FROM Users";
    }

    public async Task Submit()
    {
        var tokens = Tokenizer.Tokenize(Sql);
        var ASTNode = Parser.Parse(tokens);
        ASTDiagram.AddNodes(ASTNode);
    }
}
