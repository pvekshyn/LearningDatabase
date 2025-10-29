using Blazor.Diagrams;
using Blazor.Diagrams.Core.Anchors;
using Blazor.Diagrams.Core.Geometry;
using Blazor.Diagrams.Core.Models;
using Blazor.Diagrams.Core.PathGenerators;
using Blazor.Diagrams.Core.Routers;
using Blazor.Diagrams.Options;
using LearningDatabase.Parsing;

namespace LearningDatabase.UI.Components;

public partial class MyDiagram
{
    private BlazorDiagram Diagram { get; set; } = null!;

    protected override void OnInitialized()
    {
        var options = new BlazorDiagramOptions
        {
            Links =
            {
                DefaultRouter = new NormalRouter(),
                DefaultPathGenerator = new SmoothPathGenerator()
            },
        };
        Diagram = new BlazorDiagram(options);
    }

    public void AddNodes(ASTNode node)
    {
        Diagram.Nodes.Clear();
        var selectNode = node as SelectASTNode;
        if (selectNode is not null)
        {
            AddNode(selectNode, (selectNode.OutputList.Count + 1) * 100, 0);
        }
    }

    public NodeModel AddNode(ASTNode node, int x, int y)
    {
        var n = Diagram.Nodes.Add(new NodeModel(position: new Point(x, y))
        {
            Title = GetText(node)
        });

        if (node is SelectASTNode selectNode)
        {
            for (int i = 0; i < selectNode.OutputList.Count; i++)
            {
                var on = AddNode(selectNode.OutputList[i], i * 100, 100);
                Connect(n,on);
            }

            var from = AddNode(selectNode.From, (selectNode.OutputList.Count + 1) * 100, 100);
            Connect(n, from);
        }

        if (node is FromASTNode fromNode)
        {
            var table = AddNode(fromNode.Table, x, y + 100);
            Connect(n, table);
        }

        return n;
    }

    private void Connect(NodeModel from, NodeModel to)
    {
        Diagram.Links.Add(new LinkModel(new ShapeIntersectionAnchor(from), new ShapeIntersectionAnchor(to)));
    }

    public static string GetText(ASTNode node)
    {
        return node switch
        {
            SelectASTNode => "SELECT",
            FromASTNode => "FROM",
            ColumnASTNode n => $"{n.ColumnName}",
            ConstASTNode<int> n => n.Value.ToString(),
            ConstASTNode<string> n => n.Value,
            AllColumnsASTNode => "*",
            _ => node.GetType().Name,
        };
    }
}