using LearningDatabase.Parsing;

namespace LearningDatabase.Plan.Steps;

public abstract class NonQueryPlanStep
{
    public abstract Task Execute();
}

public abstract class QueryPlanStep
{
    public QueryPlanStep? PreviousStep { get; set; }

    public List<OutputMetadata> OutputListMetadata { get; set; }

    public abstract IAsyncEnumerable<ReadOnlyMemory<byte>> Execute();
}

public record OutputMetadata(string Name, ColumnType Type, int Length, int Offset);

