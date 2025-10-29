using LearningDatabase.Plan;
using LearningDatabase.Storage;
using Microsoft.Extensions.DependencyInjection;

namespace LearningDatabase;

public static class RegistrationExtensions
{
    public static IServiceCollection AddDatabaseDependencies(this IServiceCollection services)
    {
        return services
            .AddSingleton<IDatabase, Database>()
            .AddSingleton<Planner>()
            .AddSingleton<PlanStepFactory>()
            .AddSingleton<IFileManager, FileManager>();
    }
}
