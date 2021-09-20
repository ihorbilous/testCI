

using PFS.Assets.Scripts.Commands.UI;
using PFS.Assets.Scripts.Views.DebugScreen;

using strange.extensions.command.api;
using strange.extensions.command.impl;
using strange.extensions.context.api;
using strange.extensions.context.impl;
using UnityEngine;


public class MainContextRoot : MVCSContext
{
    public MainContextRoot(MonoBehaviour contextView) : base(contextView, ContextStartupFlags.MANUAL_MAPPING)
    {
    }

    // CoreComponents
    protected override void AddCoreComponents()
    {
        base.AddCoreComponents();
        injectionBinder.Unbind<ICommandBinder>(); //Unbind to avoid a conflict!
        injectionBinder.Bind<ICommandBinder>().To<EventCommandBinder>().ToSingleton();

        injectionBinder.Bind<IExecutor>().To<CoroutineExecutor>().ToSingleton();
    
        injectionBinder.Bind<IPlayerPrefsStrategy>().To<StraightPlayerPrefsStrategy>().ToSingleton().ToName("Straight");
        injectionBinder.Bind<IPlayerPrefsStrategy>().To<EncryptedPlayerPrefsStrategy>().ToSingleton().ToName("Encrypted");


       
    }

    // Commands and Bindings
    protected override void MapBindings()
    {
        base.MapBindings();


        //-----TopPanel screen
     

        //-----Debug screen
        mediationBinder.BindView<DebugView>().ToMediator<DebugMediator>();
        //-----------------



        //System Commands
        commandBinder.Bind(ContextEvent.START)

        .InSequence()
        .Once();

        commandBinder.Bind(EventGlobal.E_ShowScreen).To<UIScreenShowCommand>().Pooled();
        commandBinder.Bind(EventGlobal.E_HideScreen).To<UIScreenHideCommand>().Pooled();

    }
}