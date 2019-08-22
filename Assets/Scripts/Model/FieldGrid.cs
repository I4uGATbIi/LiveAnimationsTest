using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class FieldGrid
    {
        public int[,] Grid { get; set; } = new int[10, 10];

//    public void ToggleCell(int row, int col)
//    {
//        Grid[row, col] = Grid[row, col] == 1 ? 0 : 1;
//    }
    }
}