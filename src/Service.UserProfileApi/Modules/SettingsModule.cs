using Autofac;

namespace Service.UserProfileApi.Modules
{
    public class SettingsModule : Module
    {
        protected override void Load(ContainerBuilder builder) => builder.RegisterInstance(Program.Settings).AsSelf().SingleInstance();
    }
}