using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TypeO : Type {
  static Point[] rotation = new Point[] {
    new Point(1, 0), new Point(0, 1), new Point(1, 1)
  };
  internal override Point[] Blocks(int rotate) {
    return rotation;
  }
  internal override int Rotate(int now) {
    return 0;
  }
  internal override int DefaultRotate() {
    return 0;
  }
}