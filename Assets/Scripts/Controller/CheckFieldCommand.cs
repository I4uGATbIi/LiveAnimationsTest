using System.Linq;
using strange.extensions.command.impl;
using UnityEngine;

namespace Game
{
    public class CheckFieldCommand : Command
    {
        [Inject] public FieldGrid FieldGrid { get; private set; }


        public override void Execute()
        {
            CheckAndDeleteHorizontal();
            CheckAndDeleteVertical();
        }

        private void CheckAndDeleteHorizontal()
        {
            bool isBreaked;
            for (int row = 0; row < 10; row++)
            {
                isBreaked = false;
                for (int col = 0; col < 10; col++)
                {
                    if (FieldGrid.Grid[row, col] == 0)
                    {
                        isBreaked = true;
                        break;
                    }
                }

                if (isBreaked)
                {
                    Debug.Log("Next Line to Check");
                    continue;
                }

                Debug.Log("Delete This line!");
                for (int col = 0; col < 10; col++)
                {
                    GameObject.Destroy(GameObject.FindGameObjectsWithTag("Block")
                        .First(block => block.name == $"{row}/{col}"));
                    FieldGrid.Grid[row, col] = 0;
                }
            }
        }

        private void CheckAndDeleteVertical()
        {
            bool isBreaked;
            for (int col = 0; col < 10; col++)
            {
                isBreaked = false;
                for (int row = 0; row < 10; row++)
                {
                    if (FieldGrid.Grid[row, col] == 0)
                    {
                        isBreaked = true;
                        break;
                    }
                }

                if (isBreaked)
                {
                    Debug.Log("Next Line to Check");
                    continue;
                }

                Debug.Log("Delete This line!");
                for (int row = 0; row < 10; row++)
                {
                    GameObject.Destroy(GameObject.FindGameObjectsWithTag("Block")
                        .First(block => block.name == $"{row}/{col}"));
                    FieldGrid.Grid[row, col] = 0;
                }
            }
        }
    }
}