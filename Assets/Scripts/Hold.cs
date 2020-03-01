using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hold {
  Blocks blocks;
  SpriteRenderer[,] cells;
  Status s = new Status();
  internal bool used;
  internal void Init(Controller c) {
    blocks = c.blocks;
    cells = c.grids.hCells;
    s.id = blocks.empty;
    used = false;
  }
  internal bool IsEmpty(int blockId) {
    return (blockId == blocks.empty);
  }
  internal int Replace(int blockId) {
    if (!IsEmpty(s.id)) Hide();
    int held = s.id;
    s.id = blockId;
    Show();
    return held;
  }
  void Hide() {
    cells[s.x, s.y].color = blocks.Empty();
    XY[] r = blocks.Relatives(s);
    int rx, ry;
    for (int i = 0; i < r.Length; i++) {
      rx = s.x + r[i].x; ry = s.y + r[i].y;
      cells[rx, ry].color = blocks.Empty();
    }
  }
  internal void Show() {
    if (s.id == blocks.i) s.XY(1, 1);
    else s.XY(2, 0);
    cells[s.x, s.y].color = blocks.colors[s.id];
    blocks.ResetRotate(s);
    XY[] r = blocks.Relatives(s);
    int rx, ry;
    for (int j = 0; j < r.Length; j++) {
      rx = s.x + r[j].x; ry = s.y + r[j].y;
      cells[rx, ry].color = blocks.colors[s.id];
    }
  }
}
