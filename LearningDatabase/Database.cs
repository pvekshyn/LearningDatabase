using LearningDatabase.Storage;
using LearningDatabase.Tables;
using System.Text.Json;

namespace LearningDatabase;

public interface IDatabase
{
    Task StartAsync();
}

public class Database(IFileManager fileManager) : IDatabase
{
    public async Task StartAsync()
    {
        var tablesString = await fileManager.ReadTableMetadataFile();
        TableCache.Tables = JsonSerializer.Deserialize<Dictionary<ushort, TableMetadata>>(tablesString);
    }
}
