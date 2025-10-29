using LearningDatabase.Tables;
using System.Text.Json;

namespace LearningDatabase.Storage;

public interface IFileManager
{
    void CreateDataFile(ushort objectId);
    Task SaveTableMetadataFile();
    Task<string> ReadTableMetadataFile();
}

internal class FileManager : IFileManager
{
    private static string _tableMetadataFileName = "sys.tables.dat";

    public void CreateDataFile(ushort objectId)
    {
        var fileName = $"{TableCache.Tables[objectId].Name}.dat";
        File.Create(fileName).Dispose();
    }

    public async Task SaveTableMetadataFile()
    {
        await File.WriteAllTextAsync(_tableMetadataFileName, JsonSerializer.Serialize(TableCache.Tables));
    }

    public async Task<string> ReadTableMetadataFile()
    {
        if (File.Exists(_tableMetadataFileName))
        {
            return await File.ReadAllTextAsync(_tableMetadataFileName);
        }

        return string.Empty;
    }
}
