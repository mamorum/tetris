using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Colors : MonoBehaviour {
  public Color empty, wall, i, o, s, z, j, l, t;
  internal Color back; Color[] colors;
  internal void Init(Controller c) {
    back = c.cam.backgroundColor;
    colors = Blocks.Colors(
      empty, wall, i, o, s, z, j, l, t
    );
  }
  internal Color Get(int id) {
    return colors[id];
  }
}