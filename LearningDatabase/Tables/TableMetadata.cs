using LearningDatabase.Parsing;

namespace LearningDatabase.Tables;

internal record TableMetadata(ushort ObjectId, string Name, List<ColumnMetadata> Columns);

internal record ColumnMetadata(string Name, ColumnType Type, int Length);
