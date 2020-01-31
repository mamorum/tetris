using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blocks : MonoBehaviour {
  //-> rotations and relative points
  static XY[][] iR = new XY[][] {
    new XY[] { new XY(0, -1), new XY(0, 1), new XY(0, 2) },
    new XY[] { new XY(-1, 0), new XY(1, 0), new XY(2, 0) }}; // default
  static XY[][] iO = new XY[][] {
    new XY[] { new XY(1, 0), new XY(0, 1), new XY(1, 1) }}; // default
  static XY[][] sR = new XY[][] {
    new XY[] { new XY(1, -1), new XY(1, 0), new XY(0, 1) },
    new XY[] { new XY(-1, 0), new XY(1, 1), new XY(0, 1) }}; // default
  static XY[][] zR = new XY[][] {
    new XY[] { new XY(0, -1), new XY(1, 0), new XY(1, 1) },
    new XY[] { new XY(1, 0), new XY(-1, 1), new XY(0, 1) }}; // default
  static XY[][] jR = new XY[][] {
    new XY[] { new XY(0, -1), new XY(0, 1), new XY(1, 1) },
    new XY[] { new XY(1, -1), new XY(1, 0), new XY(-1, 0) },
    new XY[] { new XY(-1, -1), new XY(0, -1), new XY(0, 1) },
    new XY[] { new XY(-1, 0), new XY(1, 0), new XY(-1, 1) }}; // default
  static XY[][] lR = new XY[][] {
    new XY[] { new XY(0, -1), new XY(1, -1), new XY(0, 1) },
    new XY[] { new XY(-1, -1), new XY(-1, 0), new XY(1, 0) },
    new XY[] { new XY(0, -1), new XY(-1, 1), new XY(0, 1) },
    new XY[] { new XY(-1, 0), new XY(1, 0), new XY(1, 1) }}; // default
  static XY[][] tR = new XY[][] {
    new XY[] { new XY(0, -1), new XY(1, 0), new XY(0, 1) },
    new XY[] { new XY(0, -1), new XY(-1, 0), new XY(1, 0) },
    new XY[] { new XY(0, -1), new XY(-1, 0), new XY(0, 1) },
    new XY[] { new XY(-1, 0), new XY(1, 0), new XY(0, 1) }}; // default
  static XY[][][] relatives = new XY[][][] {
    null, null, //-> empty, wall
    iR, iO, sR, zR, jR, lR, tR };
  //-> id
  internal static int
    empty = 0, wall = 1,
    i = 2, o = 3, s = 4, z = 5, j = 6, l = 7, t = 8;
  internal static int[] drops = new int[] { i, o, s, z, j, l, t };
  //-> color
  public Color
    black, gray, //-> empty, wall
    blue, yellow, green, red, indigo, orange, purple; // i, o, s, z, j, l, t
  internal Color[] colors;
  internal void Init() {
    colors = new Color[] {
      black, gray, //-> empty, wall
      blue, yellow, green, red, indigo, orange, purple // i, o, s, z, j, l, t
    };
  }
  internal int LastRotate(int id) {
    XY[][] r = relatives[id];
    return r.Length - 1;
  }
  internal int Rotate(int id, int rotate) {
    int max = LastRotate(id);
    if (max == rotate) return 0; // back to first
    else return rotate + 1; // increment index
  }
  internal XY[] Relatives(int id, int rotate) {
    XY[][] r = relatives[id];
    return r[rotate];
  }
}
