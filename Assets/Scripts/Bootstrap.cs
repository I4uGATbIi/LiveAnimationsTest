using Game;
using strange.extensions.context.impl;

namespace Game
{
    public class Bootstrap : ContextView
    {
        private void Awake()
        {
            this.context = new GameContext(this);
        }
    }
}