using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class O : Type {
  internal override int Id() { return o; }
  static Block[] rotate0 = new Block[] {
    new Block(1, 0), new Block(0, 1), new Block(1, 1)
  };
  internal override Block[] Blocks(int rotate) {
    return rotate0;
  }
    internal override int Rotate(int now) {
    return 0;
  }
  internal override int RotateRandom() {
    return 0;
  }
}