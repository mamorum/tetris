using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class I : Type {
  internal override int Id() { return i; }
  static Block[] rotate0 = new Block[] {
    new Block(0, -1), new Block(0, 1), new Block(0, 2)
  };
  static Block[] rotate1 = new Block[] {
    new Block(-1, 0), new Block(1, 0), new Block(2, 0)
  };
  static Block[][] all = new Block[][] {
    rotate0, rotate1
  };
  internal override Block[] Blocks(int rotate) {
    return all[rotate];
  }
  internal override int Rotate(int now) {
    if (now == 0) return 1;
    else return 0;
  }
}
