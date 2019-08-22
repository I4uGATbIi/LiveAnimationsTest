using strange.extensions.mediation.impl;

namespace Game
{
    public class TestMediator : Mediator
    {
        [Inject]
        public TestView view { get; set; }
        
        [Inject]
        public ManagePoolSignal ManagePoolSignal { get; set; }

        public override void OnRegister()
        {
            view.poolIsEmpty.AddListener(ManagePoolSignal.Dispatch);
        }
    }
}