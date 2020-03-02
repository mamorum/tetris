using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blocks : MonoBehaviour {
  //-> relative points (block rotation)
  static readonly XY[][] emptyR = null, wallR = null;
  static readonly XY[][] iR = new XY[][] {
    new XY[] { new XY(-1, 0), new XY(1, 0), new XY(2, 0) },
    new XY[] { new XY(0, -1), new XY(0, 1), new XY(0, 2) }
  };
  static readonly XY[][] oR = new XY[][] {
    new XY[] { new XY(1, 0), new XY(0, 1), new XY(1, 1) }
  };
  static readonly XY[][] sR = new XY[][] {
    new XY[] { new XY(-1, 0), new XY(1, 1), new XY(0, 1) },
    new XY[] { new XY(1, -1), new XY(1, 0), new XY(0, 1) }
  };
  static readonly XY[][] zR = new XY[][] {
    new XY[] { new XY(1, 0), new XY(-1, 1), new XY(0, 1) },
    new XY[] { new XY(0, -1), new XY(1, 0), new XY(1, 1) }
  };
  static readonly XY[][] jR = new XY[][] {
    new XY[] { new XY(-1, 0), new XY(1, 0), new XY(-1, 1) },
    new XY[] { new XY(0, -1), new XY(0, 1), new XY(1, 1) },
    new XY[] { new XY(1, -1), new XY(1, 0), new XY(-1, 0) },
    new XY[] { new XY(-1, -1), new XY(0, -1), new XY(0, 1) }
  };
  static readonly XY[][] lR = new XY[][] {
    new XY[] { new XY(-1, 0), new XY(1, 0), new XY(1, 1) },
    new XY[] { new XY(0, -1), new XY(1, -1), new XY(0, 1) },
    new XY[] { new XY(-1, -1), new XY(-1, 0), new XY(1, 0) },
    new XY[] { new XY(0, -1), new XY(-1, 1), new XY(0, 1) }
  };
  static readonly XY[][] tR = new XY[][] {
    new XY[] { new XY(-1, 0), new XY(1, 0), new XY(0, 1) },
    new XY[] { new XY(0, -1), new XY(1, 0), new XY(0, 1) },
    new XY[] { new XY(0, -1), new XY(-1, 0), new XY(1, 0) },
    new XY[] { new XY(0, -1), new XY(-1, 0), new XY(0, 1) }
  };
  //-> id
  internal static readonly int
    empty = 0, wall = 1,
    i = 2, o = 3, s = 4, z = 5, j = 6, l = 7, t = 8;
  //-> rotation
  static readonly XY[][][] rotations = new XY[][][] {
    emptyR, wallR, iR, oR, sR, zR, jR, lR, tR
  };
  internal static void ResetRotate(Status s) {
    s.rotate = 0;
  }
  internal static void Rotate(Status s) {
    int last = rotations[s.id].Length - 1;
    if (last == s.rotate) s.rotate = 0;
    else s.rotate++;
  }
  internal static XY[] Relatives(Status s) {
    XY[][] r = rotations[s.id];
    return r[s.rotate];
  }
}
