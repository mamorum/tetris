using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class Status {
  internal int x, y;
  internal Type type; internal int rotate; 
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