using LearningDatabase.Parsing;
using LearningDatabase.Plan.Steps;
using Microsoft.Extensions.DependencyInjection;

namespace LearningDatabase.Plan;

public class Planner(PlanStepFactory planStepFactory)
{
    public NonQueryExecutionPlan GetNonQueryPlan(ASTNode node)
    {
        return new NonQueryExecutionPlan { Step = planStepFactory.CreateNonQueryStep(node) };
    }
}

public class PlanStepFactory(IServiceProvider serviceProvider)
{
    public NonQueryPlanStep CreateNonQueryStep(ASTNode node)
    {
        return node switch
        {
            CreateTableASTNode n => ActivatorUtilities.CreateInstance<CreateTable>(serviceProvider, n),
            _ => throw new NotImplementedException()
        };
    }
}
