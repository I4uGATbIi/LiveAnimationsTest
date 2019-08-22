using UnityEngine;

namespace Game
{
    public class FieldParams
    {
        public float xStart { get; } = -6.15f;
        public float yStart { get; } = 9f;
        public float xStep { get; } = 1.375f;
        public float yStep { get; } = 1.38f;
        public int rows { get; } = 10;
        public int columns { get; } = 10;
    }
}