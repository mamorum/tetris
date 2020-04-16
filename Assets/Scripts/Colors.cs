using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Colors : MonoBehaviour {
  public Color empty, wall, i, o, s, z, j, l, t;
  internal Color back;
  internal void Init(Controller c) {
    back = c.cam.backgroundColor;
  }
  internal Color Get(int id) {
    if (Blocks.wall == id) return wall;
    else if (Blocks.i == id) return i;
    else if (Blocks.o == id) return o;
    else if (Blocks.s == id) return s;
    else if (Blocks.z == id) return z;
    else if (Blocks.j == id) return j;
    else if (Blocks.l == id) return l;
    else if (Blocks.t == id) return t;
    else return empty;
  }
}