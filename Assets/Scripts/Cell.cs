using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell {
  internal int id;
  SpriteRenderer sr; Colors colors;
  internal Cell(int i, SpriteRenderer s, Colors c) {
    id = i; sr = s; colors = c;
  }
  internal void AddAlpha(float a) {
    Color c = sr.color;
    c.a = c.a + a;
    sr.color = c;
  }
  internal void Render() {
    sr.color = colors.Get(id);
  }
  internal void ToBackground() {
    sr.color = colors.back;
  }
  internal void Color(Status s) {
    sr.color = colors.Get(s.id);
  }
}
