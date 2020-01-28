using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class Now {
  internal int x, y;
  Type type; int rotate; 
  internal void Refresh() {
    x = 5; y = 21;
    type = Types.Get();
    rotate = type.DefaultRotate();
  }
  internal int Type() {
    return type.Id();
  }
  internal Point[] Blocks() {
    return type.Blocks(rotate);
  }
  internal Point[] RotateBlocks() {
    return type.Blocks(
      type.Rotate(rotate)
    );
  }
  internal void Rotate() {
    rotate = type.Rotate(rotate);
  }
}