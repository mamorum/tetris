using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board {
  int[,] grid; SpriteRenderer[,] cells;
  Status s = new Status();
  bool moved = false;
  Controller ctrl; Blocks blocks;
  Hold hold; Next next;
  internal void Init(Controller c) {
    grid = c.grids.main;
    cells = c.grids.mCells;
    ctrl = c; blocks = c.blocks;
    hold = c.hold; next = c.next;    
    hold.Init(c); next.Init(c);
    s.id = next.Id();
    PutBlock();
    FixBlock();
  }
  void PutBlock() {
    s.XY(5, 20);
    blocks.ResetRotate(s);
  }
  void FixBlock() {
    grid[s.x, s.y] = s.id;
    XY[] r = blocks.Relatives(s);
    int cx, cy;
    for (int i = 0; i < r.Length; i++) {
      cx = r[i].x; cy = r[i].y;
      grid[s.x + cx, s.y + cy] = s.id;
    }
  }
  void HideBlock() {
    grid[s.x, s.y] = blocks.empty;
    XY[] r = blocks.Relatives(s);
    int cx, cy;
    for (int i = 0; i < r.Length; i++) {
      cx = r[i].x; cy = r[i].y;
      grid[s.x + cx, s.y + cy] = blocks.empty;
    }
  }
  bool IsEmpty(int x, int y, XY[] r) {
    int b = grid[x, y];
    if (b != blocks.empty) return false;
    int rX, rY;
    for (int i = 0; i < r.Length; i++) {
      rX = r[i].x; rY = r[i].y;
      b = grid[x + rX, y + rY];
      if (b != blocks.empty) {
        return false;
      }
    }
    return true;
  }
  internal void MoveBlock(int x, int y) {
    HideBlock();
    int nx = s.x + x;
    int ny = s.y + y;
    moved = false;
    XY[] r = blocks.Relatives(s);
    if (IsEmpty(nx, ny, r)) {
      s.x = nx;
      s.y = ny;
      moved = true;
    }
    FixBlock();
  }

  internal void RotateBlock() {
    if (s.id == blocks.o) return; // none
    HideBlock();
    int cr = s.rotate;
    blocks.Rotate(s);
    XY[] r = blocks.Relatives(s);
    if (!IsEmpty(s.x, s.y, r)) s.rotate = cr;
    FixBlock();
  }
  internal void Hold() {
    if (hold.used) return;
    HideBlock();
    s.id = hold.Replace(s.id);
    if (hold.IsEmpty(s.id)) {
      s.id = next.Id(); // first time
    }
    ctrl.frame = 0;
    hold.used = true;
    PutBlock();
    FixBlock();
  }
  void DeleteLine() {
    for (int y = 1; y < 21; y++) {
      bool flag = true;
      for (int x = 1; x < 11; x++) {
        if (grid[x, y] == blocks.empty) {
          flag = false;
          break;
        }
      }
      if (flag) {
        for (int j = y; j < 21; j++) {
          for (int i = 1; i < 11; i++) {
            grid[i, j] = grid[i, j + 1];
          }
        }
        y--;
      }
    }
  }
  void CheckEnd() {
    XY[] r = blocks.Relatives(s);
    if (!IsEmpty(s.x, s.y, r)) ctrl.end = true;
  }
  internal void Drop() {
    MoveBlock(0, -1);
    if (moved) return;
    //-> dropped
    ctrl.dropped = true;
    hold.used = false;
    s.id = next.Id();
    DeleteLine();
    PutBlock();
    CheckEnd();
    FixBlock();
  }
  internal void Render() {
    for (int y = 1; y < 22; y++) {
      for (int x = 1; x < 11; x++) {
        int i = grid[x, y];
        cells[x, y].color = blocks.colors[i];
      }
    }
  }
}
