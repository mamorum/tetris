using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board {
  bool moved = false;
  int[,] grid; SpriteRenderer[,] cells;
  Status s = new Status();
  Next next = new Next();
  Hold hold = new Hold();
  Controller c;
  internal void Init(Controller ct) {
    c = ct; next.Init(c); hold.Init(c);
    grid = c.cells.grid;
    cells = c.cells.main;
    s.id = next.Id();
    PutBlock();
    FixBlock();
  }
  void PutBlock() {
    s.XY(5, 20);
    if (s.id == Blocks.i) s.y++;
    Blocks.ResetRotate(s);
  }
  void FixBlock() {
    grid[s.x, s.y] = s.id;
    XY[] r = Blocks.Relatives(s);
    int cx, cy;
    for (int i = 0; i < r.Length; i++) {
      cx = r[i].x; cy = r[i].y;
      grid[s.x + cx, s.y + cy] = s.id;
    }
  }
  void HideBlock() {
    grid[s.x, s.y] = Blocks.empty;
    XY[] r = Blocks.Relatives(s);
    int cx, cy;
    for (int i = 0; i < r.Length; i++) {
      cx = r[i].x; cy = r[i].y;
      grid[s.x + cx, s.y + cy] = Blocks.empty;
    }
  }
  bool IsEmpty(int x, int y, XY[] r) {
    int b = grid[x, y];
    if (b != Blocks.empty) return false;
    int rX, rY;
    for (int i = 0; i < r.Length; i++) {
      rX = r[i].x; rY = r[i].y;
      b = grid[x + rX, y + rY];
      if (b != Blocks.empty) {
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
    XY[] r = Blocks.Relatives(s);
    if (IsEmpty(nx, ny, r)) {
      s.x = nx;
      s.y = ny;
      moved = true;
    }
    FixBlock();
  }

  internal void RotateBlock() {
    if (s.id == Blocks.o) return; // none
    HideBlock();
    int cr = s.rotate;
    Blocks.Rotate(s);
    XY[] r = Blocks.Relatives(s);
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
    c.frame = 0;
    hold.used = true;
    PutBlock();
    FixBlock();
  }
  List<int> delete = new List<int>();
  bool HasDelete() {
    for (int y = 1; y < 21; y++) {
      for (int x = 1; x < 11; x++) {
        if (grid[x, y] == Blocks.empty) break;
        if (x == 10) delete.Add(y);
      }
    }
    if (delete.Count != 0) {
      c.del = true;
      return true;
    } else {
      return false;
    }
  }
  internal void Delete() {
    int line = delete.Count;
    for (int i = 0; i < line; i++) {
      for (int y = delete[i] - i; y < 21; y++) {
        for (int x = 1; x < 11; x++) {
          grid[x, y] = grid[x, y + 1];
        }
      }
    }
    c.del = false;
    c.frame = 0;
    c.score.Add(line);
    delete.Clear();
    NextBlock();
  }
  internal void Deleting() {
    Color c;
    foreach (int y in delete) {
      for (int x = 1; x < 11; x++) {
        c = cells[x, y].color;
        c.a = c.a - 0.03f;
        cells[x, y].color = c;
      }
    }
  }
  void NextBlock() {
    s.id = next.Id();
    PutBlock();
    CheckEnd();
    FixBlock();
  }
  void CheckEnd() {
    XY[] r = Blocks.Relatives(s);
    if (!IsEmpty(s.x, s.y, r)) c.end = true;
  }
  internal void Drop() {
    c.frame = 0;
    MoveBlock(0, -1);
    if (moved) return;
    //-> dropped
    c.dropped = true;
    hold.used = false;
    if (HasDelete()) return;
    NextBlock();
  }
  internal void Render() {
    for (int y = 1; y < 22; y++) {
      for (int x = 1; x < 11; x++) {
        int i = grid[x, y];
        cells[x, y].color = c.colors.Get(i);
      }
    }
  }
}
