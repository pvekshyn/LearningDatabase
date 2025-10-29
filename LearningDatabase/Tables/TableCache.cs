namespace LearningDatabase.Tables;

internal static class TableCache
{
    public static Dictionary<ushort, TableMetadata> Tables { get; set; } = new Dictionary<ushort, TableMetadata>();

    public static ushort GetMaxObjectId()
    {
        if (!Tables.Any()) return 0;

        return Tables.Max(x => x.Value.ObjectId);
    }
}
