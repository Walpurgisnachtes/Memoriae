using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Memoriae
{
    public class GameMap
    {
        public int Width { get; private set; }
        public int Height { get; private set; }

        private readonly Piece[,] _grid;

        public GameMap(int width, int height)
        {
            Width = width;
            Height = height;
            _grid = new Piece[width, height];
        }

        public bool TryPlacePiece(Piece piece, Vector2Int position)
        {
            if (!IsWithinBounds(position)) return false;

            _grid[position.x, position.y] = piece;
            return true;
        }

        public Piece GetPieceAt(Vector2Int position)
        {
            if (!IsWithinBounds(position)) return null;
            return _grid[position.x, position.y];
        }

        private bool IsWithinBounds(Vector2Int position)
        {
            return position.x >= 0 && position.x < Width &&
                   position.y >= 0 && position.y < Height;
        }
    }
}