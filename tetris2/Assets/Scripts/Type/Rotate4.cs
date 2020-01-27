using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Rotate4 : Type {
  internal override int Rotate(int now) {
    if (now == 3) return 0;
    else return now + 1;
  }
  internal override int DefaultRotate() {
    return 3;
  }
}