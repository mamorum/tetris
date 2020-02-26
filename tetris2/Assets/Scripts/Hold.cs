using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hold {
  int[,] grid; SpriteRenderer[,] cells;
  Blocks blocks;
  internal int id;
  internal bool used;
  internal void Init(Controller c) {
    blocks = c.blocks;
    grid = c.grids.hold;
    cells = c.grids.hCells;
    id = blocks.empty;
    used = false;
  }
  internal bool IsEmpty(int blockId) {
    return (blockId == blocks.empty);
  }
  internal int Replace(int blockId) {
    int held = id;
    id = blockId;
    return held;
  }
  internal void Render() {
    //-> reset
    for (int y = 0; y < 2; y++) {
      for (int x = 0; x < 4; x++) {
        grid[x, y] = 0;
      }
    }
    //-> put id
    int hX = 2, hY = 0;
    if (id == blocks.i) { hX--; hY++; }
    grid[hX, hY] = id;
    int routate = blocks.DefaultRotate(id);
    XY[] r = blocks.Relatives(id, routate);
    for (int j = 0; j < r.Length; j++) {
      grid[hX + r[j].x, hY + r[j].y] = id;
    }
    //-> color
    for (int y = 0; y < 2; y++) {
      for (int x = 0; x < 4; x++) {
        int i = grid[x, y];
        cells[x, y].color = blocks.colors[i];
      }
    }
  }
}
