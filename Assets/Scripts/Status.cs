using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Status {
  internal int x, y, id, rotate;
  internal void XY(int x, int y) {
    this.x = x; this.y = y;
  }
  internal void ResetRotate() {
    rotate = 0;
  }
  internal void Rotate() {
    if (rotate == 3) ResetRotate();
    else rotate++;
  }
}
