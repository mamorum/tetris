using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blocks : MonoBehaviour {
  //-> relative points (last indexes are default rotation.)
  static XY[][] rI = new XY[][] {
    new XY[] { new XY(0, -1), new XY(0, 1), new XY(0, 2) },
    new XY[] { new XY(-1, 0), new XY(1, 0), new XY(2, 0) }};
  static XY[][] rO = new XY[][] {
    new XY[] { new XY(1, 0), new XY(0, 1), new XY(1, 1) }};
  static XY[][] rS = new XY[][] {
    new XY[] { new XY(1, -1), new XY(1, 0), new XY(0, 1) },
    new XY[] { new XY(-1, 0), new XY(1, 1), new XY(0, 1) }};
  static XY[][] rZ = new XY[][] {
    new XY[] { new XY(0, -1), new XY(1, 0), new XY(1, 1) },
    new XY[] { new XY(1, 0), new XY(-1, 1), new XY(0, 1) }};
  static XY[][] rJ = new XY[][] {
    new XY[] { new XY(0, -1), new XY(0, 1), new XY(1, 1) },
    new XY[] { new XY(1, -1), new XY(1, 0), new XY(-1, 0) },
    new XY[] { new XY(-1, -1), new XY(0, -1), new XY(0, 1) },
    new XY[] { new XY(-1, 0), new XY(1, 0), new XY(-1, 1) }};
  static XY[][] rL = new XY[][] {
    new XY[] { new XY(0, -1), new XY(1, -1), new XY(0, 1) },
    new XY[] { new XY(-1, -1), new XY(-1, 0), new XY(1, 0) },
    new XY[] { new XY(0, -1), new XY(-1, 1), new XY(0, 1) },
    new XY[] { new XY(-1, 0), new XY(1, 0), new XY(1, 1) }};
  static XY[][] rT = new XY[][] {
    new XY[] { new XY(0, -1), new XY(1, 0), new XY(0, 1) },
    new XY[] { new XY(0, -1), new XY(-1, 0), new XY(1, 0) },
    new XY[] { new XY(0, -1), new XY(-1, 0), new XY(0, 1) },
    new XY[] { new XY(-1, 0), new XY(1, 0), new XY(0, 1) }};
  static XY[][][] relatives = new XY[][][] {
    null, null, rI, rO, rS, rZ, rJ, rL, rT //-> empty, wall, i, o, s, z, j, l, t 
  };
  //-> id, color
  internal readonly int
    empty=0, wall=1, i=2, o=3, s=4, z=5, j=6, l=7, t=8;
  public Color[] colors; // empty, wall, i, o, s, z, j, l, t
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
