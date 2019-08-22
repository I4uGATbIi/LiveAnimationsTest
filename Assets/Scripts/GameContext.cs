using UnityEngine;

namespace Game
{
    public class GameContext : SignalContext
    {
        public GameContext(MonoBehaviour contextView) : base(contextView)
        {
        }

        protected override void mapBindings()
        {
            base.mapBindings();

            commandBinder.Bind<StartSignal>().To<StartCommand>().Once();
            commandBinder.Bind<ManagePoolSignal>().To<ManagePoolCommand>().Pooled();
            commandBinder.Bind<OnDrag>().To<OnDragCommand>().Pooled();
            commandBinder.Bind<CheckFieldSignal>().To<CheckFieldCommand>().Pooled();

            mediationBinder.Bind<TestView>().To<TestMediator>();
            
            StartManager startManager = GameObject.Find("Manager").GetComponent<StartManager>();
            FigurePoolManager figurePoolManager = GameObject.Find("Manager").GetComponent<FigurePoolManager>();
            injectionBinder.Bind<IManager>().ToValue(startManager).ToName(Managers.StartManager);
            injectionBinder.Bind<IManager>().ToValue(figurePoolManager).ToName(Managers.FigureManager);
            
            injectionBinder.Bind<FieldGrid>().ToSingleton();
            injectionBinder.Bind<FieldParams>().ToSingleton();
        }
    }

    enum Managers
    {
        StartManager,
        FigureManager
    }
}