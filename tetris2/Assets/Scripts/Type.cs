using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Type {
  internal abstract int Id();
  internal abstract Block[] Blocks(int rotate);
  internal abstract int Rotate(int rotate);
  internal abstract int DefaultRotate();
}
