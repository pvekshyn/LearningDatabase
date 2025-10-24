namespace LearningDatabase.Parsing;

public enum ColumnType
{
    Int,
    Char
}

public record ASTNode { }

public record SelectASTNode(List<ExpressionASTNode> OutputList, FromASTNode From) : ASTNode { }
public record FromASTNode(ConstASTNode<string> Table) : ASTNode { }

public record ExpressionASTNode : ASTNode { }
public record ConstASTNode<T>(T Value) : ExpressionASTNode { }
public record ColumnASTNode(string ColumnName) : ExpressionASTNode { }
public record AllColumnsASTNode() : ExpressionASTNode { }
