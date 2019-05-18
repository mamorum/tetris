using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class Now {
  internal int x, y, rotate;
  internal Type type;
  internal Block[] Blocks() {
    return type.Blocks(rotate);
  }
  internal Block[] RotateBlocks() {
    return type.Blocks(
      type.Rotate(rotate)
    );
  }
  internal void Rotate() {
    rotate = type.Rotate(rotate);
  }
}