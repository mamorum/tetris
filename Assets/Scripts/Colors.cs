using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Colors : MonoBehaviour {
  public Color empty, wall, i, o, s, z, j, l, t;
  Dictionary<int, Color> colors = new Dictionary<int, Color>();
  internal Color Get(int id) { return colors[id]; }
  internal void Init() {
    colors.Add(Blocks.empty, empty);
    colors.Add(Blocks.wall, wall); colors.Add(Blocks.i, i);
    colors.Add(Blocks.o, o); colors.Add(Blocks.s, s);
    colors.Add(Blocks.z, z); colors.Add(Blocks.j, j);
    colors.Add(Blocks.l, l); colors.Add(Blocks.t, t);
  }
}