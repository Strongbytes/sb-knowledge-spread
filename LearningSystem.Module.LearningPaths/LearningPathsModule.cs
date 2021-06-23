﻿using Autofac;
using LearningSystem.Module.Common.Extensions;
using LearningSystem.Module.Data;
using LearningSystem.Module.LearningPaths.Domain;
using LearningSystem.Module.LearningPaths.Domain.Repositories;
using LearningSystem.Module.LearningPaths.Infrastructure;
using LearningSystem.Module.LearningPaths.Infrastructure.Repositories;
using LearningSystem.Module.LearningPaths.Services;

namespace LearningSystem.Module.LearningPaths
{
    public class LearningPathsModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterCommandHandlersAndValidators(ThisAssembly);

            builder.RegisterProfile(ThisAssembly);

            RegisterConfigurations(builder);

            RegisterUnitOfWorkRepositories(builder);

            RegisterServices(builder);
        }

        private static void RegisterConfigurations(ContainerBuilder builder)
        {
            builder.RegisterType<LearningPathsConfiguration>()
                   .AsSelf()
                   .SingleInstance();

            builder.RegisterType<DataConfiguration>()
                   .AsSelf()
                   .SingleInstance();
        }

        private static void RegisterUnitOfWorkRepositories(ContainerBuilder builder)
        {
            builder.RegisterType<LearningPathsRepository>()
                   .As<ILearningPathsRepository>()
                   .InstancePerLifetimeScope();

            builder.RegisterType<UnitOfWork>()
                   .As<IUnitOfWork>()
                   .InstancePerLifetimeScope();
        }

        private static void RegisterServices(ContainerBuilder builder)
        {
            builder.RegisterType<ThirdPartyService>()
                   .As<IThirdPartyService>()
                   .InstancePerDependency();
        }
    }
}