using LearningDatabase.Plan.Steps;

namespace LearningDatabase.Plan;

public class NonQueryExecutionPlan
{
    public required NonQueryPlanStep Step { get; set; }

    public async Task Execute()
    {
        await Step.Execute();
    }
}

public class QueryExecutionPlan
{
    public required QueryPlanStep Step { get; set; }

    public IAsyncEnumerable<ReadOnlyMemory<byte>> Execute()
    {
        return Step.Execute();
    }
}