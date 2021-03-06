using LearningSystem.Module.Common.Infrastructure;
using LearningSystem.Module.LearningPaths.Domain.Repositories;

namespace LearningSystem.Module.LearningPaths.Domain
{
    internal interface IUnitOfWork : IBaseUnitOfWork
    {
        ILearningPathsRepository LearningPaths { get; }

        ITutorialsRepository Tutorials { get; }
    }
}