using System.Collections;
using System.Collections.Generic;
using Game;
using strange.extensions.mediation.impl;
using UnityEngine;

namespace Game
{
    public class StartManager : MonoBehaviour, IManager
    {
        [SerializeField] private float xStart = -6.15f;
        [SerializeField] private float yStart = 9f;
        [SerializeField] private float xStep = 1.375f;
        [SerializeField] private float yStep = 1.38f;
        [SerializeField] private int rows = 10;
        [SerializeField] private int columns = 10;

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
            for (int row = 0; row < rows; row++)
            {
                for (int column = 0; column < columns; column++)
                {
                    GameObject instance = Instantiate(
                        gridBlock,
                        new Vector3(xStart + xStep * column, yStart - yStep * row, -1),
                        Quaternion.identity,
                        GameObject.FindWithTag("Grid").transform
                    );
                    instance.name = $"{row}/{column}";
                    instance.GetComponent<SpriteRenderer>().color = Color.clear;
                }
            }
        }

        public int GetRows()
        {
            return rows;
        }

        public int GetColums()
        {
            return columns;
        }
    }
}