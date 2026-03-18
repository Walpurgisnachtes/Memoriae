using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Memoriae
{
    public class Piece
    {
        public string Name { get; private set; }
        public PieceStats Stats { get; private set; }

        public Piece(string name, PieceStats stats = null)
        {
            Name = name;
            Stats = stats ?? new PieceStats();
        }
    }
}