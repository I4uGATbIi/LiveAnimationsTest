using strange.extensions.command.impl;
using UnityEngine;

namespace Game
{
    public class StartCommand : Command
    {
        [Inject(Managers.StartManager)]
        public IManager manager { get; set; }
        
        public override void Execute()
        {
            manager.Manage();
        }
    }
}