using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Rotate2 : Type {
  internal override int Rotate(int now) {
    if (now == 0) return 1;
    else return 0;
  }
  internal override int RotateRandom() {
    return Random.Range(0, 2);
  }
}