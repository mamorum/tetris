using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blocks : MonoBehaviour {
  //-> relative points (each last index is default rotation.)
  static readonly XY[][] rI = new XY[][] {
    new XY[] { new XY(0, -1), new XY(0, 1), new XY(0, 2) },
    new XY[] { new XY(-1, 0), new XY(1, 0), new XY(2, 0) }};
  static readonly XY[][] rO = new XY[][] {
    new XY[] { new XY(1, 0), new XY(0, 1), new XY(1, 1) }};
  static readonly XY[][] rS = new XY[][] {
    new XY[] { new XY(1, -1), new XY(1, 0), new XY(0, 1) },
    new XY[] { new XY(-1, 0), new XY(1, 1), new XY(0, 1) }};
  static readonly XY[][] rZ = new XY[][] {
    new XY[] { new XY(0, -1), new XY(1, 0), new XY(1, 1) },
    new XY[] { new XY(1, 0), new XY(-1, 1), new XY(0, 1) }};
  static readonly XY[][] rJ = new XY[][] {
    new XY[] { new XY(0, -1), new XY(0, 1), new XY(1, 1) },
    new XY[] { new XY(1, -1), new XY(1, 0), new XY(-1, 0) },
    new XY[] { new XY(-1, -1), new XY(0, -1), new XY(0, 1) },
    new XY[] { new XY(-1, 0), new XY(1, 0), new XY(-1, 1) }};
  static readonly XY[][] rL = new XY[][] {
    new XY[] { new XY(0, -1), new XY(1, -1), new XY(0, 1) },
    new XY[] { new XY(-1, -1), new XY(-1, 0), new XY(1, 0) },
    new XY[] { new XY(0, -1), new XY(-1, 1), new XY(0, 1) },
    new XY[] { new XY(-1, 0), new XY(1, 0), new XY(1, 1) }};
  static readonly XY[][] rT = new XY[][] {
    new XY[] { new XY(0, -1), new XY(1, 0), new XY(0, 1) },
    new XY[] { new XY(0, -1), new XY(-1, 0), new XY(1, 0) },
    new XY[] { new XY(0, -1), new XY(-1, 0), new XY(0, 1) },
    new XY[] { new XY(-1, 0), new XY(1, 0), new XY(0, 1) }};
  static readonly XY[][][] relatives = new XY[][][] {
    null, null, rI, rO, rS, rZ, rJ, rL, rT }; // empty, wall, i, o, s, z, j, l, t 
  //-> color, id, 
  public Color[] colors; // empty, wall, i, o, s, z, j, l, t
  internal readonly int
    empty=0, wall=1, i=2, o=3, s=4, z=5, j=6, l=7, t=8;
  internal Color Empty() { return colors[empty]; }
  internal Color Wall() { return colors[wall]; }
  internal int DefaultRotate(int id) {
    XY[][] r = relatives[id];
    return r.Length - 1; // last index
  }
  internal int Rotate(int id, int rotate) {
    int max = DefaultRotate(id);
    if (max == rotate) return 0; // back to first
    else return rotate + 1; // increment index
  }
  internal XY[] Relatives(int id, int rotate) {
    XY[][] r = relatives[id];
    return r[rotate];
  }
}
