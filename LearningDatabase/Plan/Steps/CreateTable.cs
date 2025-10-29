using LearningDatabase.Parsing;
using LearningDatabase.Storage;
using LearningDatabase.Tables;

namespace LearningDatabase.Plan.Steps;

internal class CreateTable(IFileManager fileManager, CreateTableASTNode node) : NonQueryPlanStep
{
    public TableMetadata Metadata { get; set; } = MapFrom(node);

    public override async Task Execute()
    {
        if (TableCache.Tables.Any(x => x.Value.Name == Metadata.Name))
            throw new Exception($"Table {Metadata.Name} already exists");

        TableCache.Tables.Add(Metadata.ObjectId, Metadata);
        await fileManager.SaveTableMetadataFile();

        fileManager.CreateDataFile(Metadata.ObjectId);
    }

    private static TableMetadata MapFrom(CreateTableASTNode nodeCreate)
    {
        var objectId = TableCache.GetMaxObjectId() + 1;

        var columns = nodeCreate.Columns
            .Select(x => new ColumnMetadata(x.Name, x.Type, x.Length))
            .ToList();

        return new TableMetadata((ushort)objectId, nodeCreate.TableName, columns);
    }
}
