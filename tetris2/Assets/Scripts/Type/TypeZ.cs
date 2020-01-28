using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TypeZ : Rotate2 {
  static Point[][] rotations = new Point[][] {
    new Point[] {
      new Point(0, -1), new Point(1, 0), new Point(1, 1)
    },
    new Point[] {
      new Point(0, -1), new Point(1, -1), new Point(-1, 0)
    }
  };
  internal override Point[] Blocks(int rotate) {
    return rotations[rotate];
  }
  internal override int Id() { return Types.z; }
}
