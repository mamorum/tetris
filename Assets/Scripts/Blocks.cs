using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XY {
  internal int x, y;
  internal XY(int X, int Y) { x = X; y = Y; }
}
public class Blocks : MonoBehaviour {
  static XY XY(int x, int y) { return new XY(x, y); }
  static XY[] XYs(XY p1, XY p2, XY p3) {
    return new XY[] { p1, p2, p3 };
  }
  //-> relative points (for rotation)
  static readonly XY[][] emptyR = null, wallR = null;
  static readonly XY[][] iR = new XY[][] {
    XYs(XY(-1, 0), XY(1, 0), XY(2, 0)),
    XYs(XY(0, -1), XY(0, 1), XY(0, 2))
  };
  static readonly XY[][] oR = new XY[][] {
    XYs(XY(1, 0), XY(0, 1), XY(1, 1))
  };
  static readonly XY[][] sR = new XY[][] {
    XYs(XY(-1, 0), XY(1, 1), XY(0, 1)),
    XYs(XY(1, -1), XY(1, 0), XY(0, 1))
  };
  static readonly XY[][] zR = new XY[][] {
    XYs(XY(1, 0), XY(-1, 1), XY(0, 1)),
    XYs(XY(0, -1), XY(1, 0), XY(1, 1))
  };
  static readonly XY[][] jR = new XY[][] {
    XYs(XY(-1, 0), XY(1, 0), XY(-1, 1)),
    XYs(XY(0, -1), XY(0, 1), XY(1, 1)),
    XYs(XY(1, -1), XY(1, 0), XY(-1, 0)),
    XYs(XY(-1, -1), XY(0, -1), XY(0, 1))
  };
  static readonly XY[][] lR = new XY[][] {
    XYs(XY(-1, 0), XY(1, 0), XY(1, 1)),
    XYs(XY(0, -1), XY(1, -1), XY(0, 1)),
    XYs(XY(-1, -1), XY(-1, 0), XY(1, 0)),
    XYs(XY(0, -1), XY(-1, 1), XY(0, 1))
  };
  static readonly XY[][] tR = new XY[][] {
    XYs(XY(-1, 0), XY(1, 0), XY(0, 1)),
    XYs(XY(0, -1), XY(1, 0), XY(0, 1)),
    XYs(XY(0, -1), XY(-1, 0), XY(1, 0)),
    XYs(XY(0, -1), XY(-1, 0), XY(0, 1))
  };
  //-> id
  internal static readonly int
    empty = 0, wall = 1,
    i = 2, o = 3, s = 4, z = 5, j = 6, l = 7, t = 8;
  internal static readonly int[] drops = { i, o, s, z, j, l, t };
  //-> color (same order as id)
  internal static Color[] Colors(
    Color empty, Color wall,
    Color i, Color o, Color s, Color z, Color j, Color l, Color t
  ) {
    return new Color[] { empty, wall, i, o, s, z, j, l, t };
  }
  //-> rotation (same order as id)
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
