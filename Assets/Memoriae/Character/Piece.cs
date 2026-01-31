using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Memoriae
{
    public class Piece
    {
        public string Name { get; private set; }

        public Piece(string name)
        {
            Name = name;
        }
    }
}