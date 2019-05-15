using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class Status {
  internal int x, y, type, rotate;
  internal Status Copy() {
    Status s = new Status();
    s.x = x; s.y = y;
    s.type = type; s.rotate = rotate;
    return s;
  }
}