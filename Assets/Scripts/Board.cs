using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board {
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
  void DeleteLine() {
    for (int y = 1; y < 21; y++) {
      bool flag = true;
      for (int x = 1; x < 11; x++) {
        if (board[x, y] == Blocks.empty) {
          flag = false;
          break;
        }
      }
      if (flag) {
        for (int j = y; j < 21; j++) {
          for (int i = 1; i < 11; i++) {
            board[i, j] = board[i, j + 1];
          }
        }
        y--;
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
    DeleteLine();
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
