using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Type {
  internal readonly static int
    empty = 0, wall = 9,
    i = 1, o = 2, s = 3, z = 4, j = 5, l = 6, t = 7;
  internal abstract int Id();
  internal abstract Block[] Blocks(int rotate);
  internal abstract int Rotate(int rotate);
  internal abstract int RotateRandom();
}
