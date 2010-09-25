using System;
using System.Collections.Generic;
using System.Text;
using LightCore;
using LightCore.Lifecycle;
using TheBox.Common;
using TheBox.Forms.ProfileWizard;
using TheBox.Forms;

namespace TheBox
{
    public class LightCoreBuilder
    {
        public LightCoreBuilder()
        {

        }

        public IContainer BuildContainer()
        {
            ContainerBuilder builder = new ContainerBuilder();

            builder.Register<ProfileManager>().ControlledBy<SingletonLifecycle>();
            builder.Register<StartingContext>().ControlledBy<SingletonLifecycle>();

            // GUI
            builder.Register<ISplash, Splash>().ControlledBy<SingletonLifecycle>();
            builder.Register<ILanguageSelector, LanguageSelector>();
            builder.Register<IProfileChooser, ProfileChooser>();
            builder.Register<IBoxForm, Box>();


            return builder.Build();
        }
    }
}
