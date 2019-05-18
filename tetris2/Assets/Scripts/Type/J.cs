using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class J : Type {
  internal override int Id() { return Types.j; }
  //-> 4 rotation pattern
  static Block[] r0 = new Block[] {
    new Block(0, -1), new Block(0, 1), new Block(1, 1)
  };
  static Block[] r1 = new Block[] {
    new Block(1, -1), new Block(1, 0), new Block(-1, 0)
  };
  static Block[] r2 = new Block[] {
    new Block(-1, -1), new Block(0, -1), new Block(0, 1)
  };
  static Block[] r3 = new Block[] {
    new Block(-1, 0), new Block(1, 0), new Block(-1, 1)
  };
  static Block[][] rotations = new Block[][] { r0, r1, r2, r3 };
  internal override Block[] Blocks(int rotate) {
    return rotations[rotate];
  }
  internal override int Rotate(int now) {
    if (now == 3) return 0;
    else return now + 1;
  }
  internal override int RotateRandom() {
    return Random.Range(0, rotations.Length);
  }
}