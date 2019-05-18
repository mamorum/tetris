using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class I : Type {
  internal override int Id() { return Types.i; }
  static Block[] r0 = new Block[] {
    new Block(0, -1), new Block(0, 1), new Block(0, 2)
  };
  static Block[] r1 = new Block[] {
    new Block(-1, 0), new Block(1, 0), new Block(2, 0)
  };
  static Block[][] rotations = new Block[][] { r0, r1 };
  internal override Block[] Blocks(int rotate) {
    return rotations[rotate];
  }
  internal override int Rotate(int now) {
    if (now == 0) return 1;
    else return 0;
  }
  internal override int RotateRandom() {
    return Random.Range(0, rotations.Length);
  }
}
