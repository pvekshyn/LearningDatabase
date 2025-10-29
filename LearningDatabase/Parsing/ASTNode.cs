namespace LearningDatabase.Parsing;

public enum ColumnType
{
    Int,
    Char
}

public record ASTNode { }

public record CreateTableASTNode(string TableName, List<ColumnMetadataASTNode> Columns) : ASTNode { }
public record ColumnMetadataASTNode(string Name, ColumnType Type, int Length) : ASTNode { }

public record InsertASTNode(string TableName, List<ConstASTNode> Values) : ASTNode { }

public record SelectASTNode(List<ExpressionASTNode> OutputList, FromASTNode From) : ASTNode { }
public record FromASTNode(ConstASTNode<string> Table) : ASTNode { }

public record ExpressionASTNode : ASTNode { }
public record ConstASTNode() : ExpressionASTNode { }
public record ConstASTNode<T>(T Value) : ConstASTNode { }
public record ColumnASTNode(string ColumnName) : ExpressionASTNode { }
public record AllColumnsASTNode() : ExpressionASTNode { }
