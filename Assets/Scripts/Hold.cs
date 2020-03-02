﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hold {
  SpriteRenderer[,] cells;
  Status s = new Status();
  internal bool used = false;
  Grids grids;
  internal void Init(Controller c) {
    grids = c.grids;
    cells = grids.hold;
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
  void Hide() {
    cells[s.x, s.y].color = grids.empty;
    XY[] r = Blocks.Relatives(s);
    int rx, ry;
    for (int i = 0; i < r.Length; i++) {
      rx = s.x + r[i].x; ry = s.y + r[i].y;
      cells[rx, ry].color = grids.empty;
    }
  }
  internal void Show() {
    if (s.id == Blocks.i) s.XY(1, 1);
    else s.XY(2, 0);
    cells[s.x, s.y].color = grids.Color(s.id);
    Blocks.ResetRotate(s);
    XY[] r = Blocks.Relatives(s);
    int rx, ry;
    for (int j = 0; j < r.Length; j++) {
      rx = s.x + r[j].x; ry = s.y + r[j].y;
      cells[rx, ry].color = grids.Color(s.id);
    }
  }
}
