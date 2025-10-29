using LearningDatabase.Tokenization;

namespace LearningDatabase.Parsing.Parsers;

internal static class CreateTableParser
{
    public static ASTNode Parse(IEnumerator<Token> enumerator)
    {
        //table name
        enumerator.MoveNext();
        var tableName = enumerator.Current.Value;

        //columns
        enumerator.MoveNext();// (

        var columns = new List<ColumnMetadataASTNode>();
        while (enumerator.Current.Type != TokenType.CloseParens)
        {
            enumerator.MoveNext();
            var columnName = enumerator.Current.Value;

            enumerator.MoveNext();
            var columnType = enumerator.Current.Value;

            if (columnType == "int")
            {
                columns.Add(new ColumnMetadataASTNode(columnName, ColumnType.Int, 4));
            }
            else if (columnType == "char")
            {
                enumerator.MoveNext();// (
                enumerator.MoveNext();
                var length = int.Parse(enumerator.Current.Value);
                columns.Add(new ColumnMetadataASTNode(columnName, ColumnType.Char, length * 2));
                enumerator.MoveNext();// )
            }
            enumerator.MoveNext();//, or )
        }

        return new CreateTableASTNode(tableName, columns);
    }
}
