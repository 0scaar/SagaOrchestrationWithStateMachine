using Autofac;
using SagaOrchestration.MoveFile.UseCases.DownloadFiles;

namespace SagaOrchestration.MoveFile.Modules
{
    public class MoveFileModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<DownloadFilesUseCase>().As<IDownloadFilesUseCase>().AsImplementedInterfaces().InstancePerLifetimeScope().AsSelf();
        }
    }
}
