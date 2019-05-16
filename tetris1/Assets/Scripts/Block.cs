using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class Block {
  internal int rotate; internal Position[] p;
  internal Block(int r, Position p0, Position p1, Position p2) {
    rotate = r; p = new Position[] { p0, p1, p2};
  }
}