using strange.extensions.command.impl;

namespace Game
{
    public class ManagePoolCommand : Command
    {
        [Inject(Managers.FigureManager)]
        public IManager manager { get; set; }

        public override void Execute()
        {
            manager.Manage();
        }
    }
}