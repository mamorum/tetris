using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Cells {
  internal abstract Cell[] Get(int rotate);
  internal abstract int Rotate(int now);
}
