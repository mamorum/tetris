using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hold {
  Controller c; Cell[,] cells;
  Status s = new Status();
  internal bool used;
  internal void Init(Controller ct) {
    c = ct;
    cells = c.cells.hold;
    Reset();
  }
  internal void Reset() {
    used = false;
    s.id = Blocks.empty;
  }
  internal bool IsEmpty(int blockId) {
    return (blockId == Blocks.empty);
  }
  internal int Replace(int blockId) {
    if (!IsEmpty(s.id)) Hide();
    int held = s.id;
    s.id = blockId;
    Show();
    return held;
  }
  internal void Hide() {
    if (IsEmpty(s.id)) return;
    cells[s.x, s.y].ToBackground();
    XY[] r = Blocks.Relatives(s);
    int rx, ry;
    for (int i = 0; i < r.Length; i++) {
      rx = s.x + r[i].x; ry = s.y + r[i].y;
      cells[rx, ry].ToBackground();
    }
  }
  internal void Show() {
    if (s.id == Blocks.i) s.XY(1, 1);
    else s.XY(2, 0);
    cells[s.x, s.y].Color(s);
    s.ResetRotate();
    XY[] r = Blocks.Relatives(s);
    int rx, ry;
    for (int j = 0; j < r.Length; j++) {
      rx = s.x + r[j].x; ry = s.y + r[j].y;
      cells[rx, ry].Color(s);
    }
  }
}
