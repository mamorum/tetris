using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main {
  bool moved = false;
  int[,] board; SpriteRenderer[,] cells;
  Status s = new Status();
  Controller c;
  internal void Init(Controller ct) {
    c = ct;
    board = c.grids.board;
    cells = c.grids.main;
    s.id = c.next.Id();
    PutBlock();
    FixBlock();
  }
  void PutBlock() {
    s.XY(5, 20);
    if (s.id == Blocks.i) s.y++;
    Blocks.ResetRotate(s);
  }
  void FixBlock() {
    board[s.x, s.y] = s.id;
    XY[] r = Blocks.Relatives(s);
    int cx, cy;
    for (int i = 0; i < r.Length; i++) {
      cx = r[i].x; cy = r[i].y;
      board[s.x + cx, s.y + cy] = s.id;
    }
  }
  void HideBlock() {
    board[s.x, s.y] = Blocks.empty;
    XY[] r = Blocks.Relatives(s);
    int cx, cy;
    for (int i = 0; i < r.Length; i++) {
      cx = r[i].x; cy = r[i].y;
      board[s.x + cx, s.y + cy] = Blocks.empty;
    }
  }
  bool IsEmpty(int x, int y, XY[] r) {
    int b = board[x, y];
    if (b != Blocks.empty) return false;
    int rX, rY;
    for (int i = 0; i < r.Length; i++) {
      rX = r[i].x; rY = r[i].y;
      b = board[x + rX, y + rY];
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
    if (c.hold.used) return;
    HideBlock();
    s.id = c.hold.Replace(s.id);
    if (c.hold.IsEmpty(s.id)) {
      s.id = c.next.Id(); // first time
    }
    c.frame = 0;
    c.hold.used = true;
    PutBlock();
    FixBlock();
  }
  List<int> delete = new List<int>();
  bool HasDelete() {
    for (int y = 1; y < 21; y++) {
      for (int x = 1; x < 11; x++) {
        if (board[x, y] == Blocks.empty) break;
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
    for (int i = 0; i < delete.Count; i++) {
      for (int y = delete[i] - i; y < 21; y++) {
        for (int x = 1; x < 11; x++) {
          board[x, y] = board[x, y + 1];
        }
      }
    }
    // TODO
    //  - 色（透過）を元に戻す
    c.del = false;
    c.frame = 0;
    delete.Clear();
    PutBlock();
    CheckEnd();
    FixBlock();
  }
  internal void Deleting() {
    Color c;
    foreach (int y in delete) {
      for (int x = 1; x < 11; x++) {
        c = cells[x, y].color;
        cells[x, y].color = new Color(c.r, c.g, c.b, c.a - 0.05f);
      }
    }
  }
  void CheckEnd() {
    XY[] r = Blocks.Relatives(s);
    if (!IsEmpty(s.x, s.y, r)) c.end = true;
  }
  internal void Drop() {
    MoveBlock(0, -1);
    if (moved) return;
    //-> dropped
    c.dropped = true;
    c.hold.used = false;
    s.id = c.next.Id();
    if (HasDelete()) return;
    PutBlock();
    CheckEnd();
    FixBlock();
  }
  internal void Render() {
    for (int y = 1; y < 22; y++) {
      for (int x = 1; x < 11; x++) {
        int i = board[x, y];
        cells[x, y].color = c.colors.Get(i);
      }
    }
  }
}
