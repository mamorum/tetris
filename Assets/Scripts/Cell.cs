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
  internal void ChangeColor() {
    sr.color = colors.Get(id);
  }
  internal void ToBackColor() {
    sr.color = colors.back;
  }
}
