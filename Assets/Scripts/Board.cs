using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board {
  //-> board cell
  int[,] grid; SpriteRenderer[,] cells;
  //-> status
  int x, y, id, rotate;
  bool moved = false;
  //-> objects
  Controller ctrl; Blocks blocks;
  Hold hold; Next next;
  internal void Init(Controller c) {
    grid = c.grids.main;
    cells = c.grids.mCells;
    ctrl = c; blocks = c.blocks;
    hold = c.hold; next = c.next;    
    hold.Init(c); next.Init(c);
    id = next.Id();
    PutBlock();
    FixBlock();
  }
  void PutBlock() {
    x = 5; y = 20;
    rotate = blocks.DefaultRotate(id);
  }
  void FixBlock() {
    grid[x, y] = id;
    XY[] r = blocks.Relatives(id, rotate);
    int cx, cy;
    for (int i = 0; i < r.Length; i++) {
      cx = r[i].x; cy = r[i].y;
      grid[x + cx, y + cy] = id;
    }
  }
  void HideBlock() {
    grid[x, y] = blocks.empty;
    XY[] r = blocks.Relatives(id, rotate);
    int cx, cy;
    for (int i = 0; i < r.Length; i++) {
      cx = r[i].x; cy = r[i].y;
      grid[x + cx, y + cy] = blocks.empty;
    }
  }
  bool IsEmpty(int tX, int tY, XY[] r) {
    int b = grid[tX, tY];
    if (b != blocks.empty) return false;
    int rX, rY;
    for (int i = 0; i < r.Length; i++) {
      rX = r[i].x; rY = r[i].y;
      b = grid[tX + rX, tY + rY];
      if (b != blocks.empty) {
        return false;
      }
    }
    return true;
  }
  internal void MoveBlock(int tX, int tY) {
    HideBlock();
    int nx = x + tX;
    int ny = y + tY;
    moved = false;
    XY[] r = blocks.Relatives(id, rotate);
    if (IsEmpty(nx, ny, r)) {
      x = nx;
      y = ny;
      moved = true;
    }
    FixBlock();
  }

  internal void RotateBlock() {
    if (id == blocks.o) return; // no rotation
    int nr = blocks.Rotate(id, rotate);
    XY[] r = blocks.Relatives(id, nr);
    HideBlock();
    if (IsEmpty(x, y, r)) rotate = nr;
    FixBlock();
  }
  internal void Hold() {
    if (hold.used) return;
    HideBlock();
    id = hold.Replace(id);
    if (hold.IsEmpty(id)) {
      id = next.Id(); // first time
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
    XY[] r = blocks.Relatives(id, rotate);
    if (!IsEmpty(x, y, r)) ctrl.end = true;
  }
  internal void Drop() {
    MoveBlock(0, -1);
    if (moved) return;
    //-> dropped
    ctrl.dropped = true;
    hold.used = false;
    id = next.Id();
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
