using System.Collections;
using System.Collections.Generic;
using Game;
using strange.extensions.mediation.impl;
using UnityEngine;

namespace Game
{
    public class StartManager : MonoBehaviour, IManager
    {
        [Inject] public FieldParams FieldParams { get; private set; }

        public StartManager()
        {
        }

        public void Manage()
        {
            InstantiateField();
            PrepareGrid();
        }

        private void InstantiateField()
        {
            if (!GameObject.FindWithTag("GameField"))
            {
                Instantiate(Resources.Load<GameObject>("Prefabs/BG"));
            }
        }

        private void PrepareGrid()
        {
            GameObject gridBlock = Resources.Load<GameObject>("Prefabs/Block");
            gridBlock.tag = "GridBlock";
            for (int row = 0; row < FieldParams.rows; row++)
            {
                for (int column = 0; column < FieldParams.columns; column++)
                {
                    GameObject instance = Instantiate(
                        gridBlock,
                        new Vector3(FieldParams.xStart + FieldParams.xStep * column,
                            FieldParams.yStart - FieldParams.yStep * row, -1),
                        Quaternion.identity,
                        GameObject.FindWithTag("Grid").transform
                    );
                    instance.name = $"{row}/{column}";
                    instance.GetComponent<SpriteRenderer>().color = Color.clear;
                }
            }
        }
    }
}