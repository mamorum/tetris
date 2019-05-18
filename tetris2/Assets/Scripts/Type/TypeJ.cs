using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TypeJ : Rotate4 {
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
  internal override int Id() { return Types.j; }
}