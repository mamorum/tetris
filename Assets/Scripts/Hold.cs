using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hold {
  SpriteRenderer[,] cells;
  Blocks blocks;
  int x, y, id, rotate;
  internal bool used;
  internal void Init(Controller c) {
    blocks = c.blocks;
    cells = c.grids.hCells;
    id = blocks.empty;
    used = false;
  }
  internal bool IsEmpty(int blockId) {
    return (blockId == blocks.empty);
  }
  internal int Replace(int blockId) {
    if (!IsEmpty(id)) Hide();
    int held = id;
    id = blockId;
    Show();
    return held;
  }
  void Hide() {
    cells[x, y].color = blocks.colors[blocks.empty];
    //-> relatives
    XY[] r = blocks.Relatives(id, rotate);
    int rx, ry;
    for (int i = 0; i < r.Length; i++) {
      rx = x + r[i].x; ry = y + r[i].y;
      cells[rx, ry].color = blocks.colors[blocks.empty];
    }
  }
  internal void Show() {
    if (id == blocks.i) { x = 1; y = 1; }
    else { x = 2; y = 0; }
    cells[x, y].color = blocks.colors[id];
    //-> relatives
    rotate = blocks.DefaultRotate(id);
    XY[] r = blocks.Relatives(id, rotate);
    int rx, ry;
    for (int j = 0; j < r.Length; j++) {
      rx = x + r[j].x; ry = y + r[j].y;
      cells[rx, ry].color = blocks.colors[id];
    }
  }
}
