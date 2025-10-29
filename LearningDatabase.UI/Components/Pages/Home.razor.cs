using LearningDatabase.Parsing.Parsers;
using LearningDatabase.Plan;
using LearningDatabase.Tokenization;
using Microsoft.AspNetCore.Components;

namespace LearningDatabase.UI.Components.Pages;

public partial class Home
{
    [Inject]
    public Planner Planner { get; set; }

    public string Sql { get; set; }
    MyDiagram ASTDiagram;

    public string ErrorMessage { get; set; }

    protected override void OnInitialized()
    {
        Sql = "SELECT * FROM Users";
    }

    public async Task Submit()
    {
        ErrorMessage = string.Empty;
        try
        {
            var tokens = Tokenizer.Tokenize(Sql);
            var ASTNode = Parser.Parse(tokens);
            ASTDiagram.AddNodes(ASTNode);

            var plan = Planner.GetNonQueryPlan(ASTNode);
            await plan.Execute();
        }
        catch (Exception ex)
        {
            ErrorMessage = ex.Message;
        }
    }
}
