using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board {
  Controller c; Cell[,] cells;
  Status s = new Status();
  Next next = new Next();
  Hold hold = new Hold();
  List<int> delete = new List<int>();
  internal bool insert = false;
  internal void Init(Controller ct) {
    c = ct; cells = c.cells.main;
    next.Init(c); hold.Init(c);
    NextBlock();
  }
  internal void Reset() {
    insert = false;
    next.Hide(); next.Reset();
    hold.Hide(); hold.Reset();
    ClearCells(); NextBlock();
  }
  void InsertBlock() {
    s.XY(5, 20); // first place
    if (s.id == Blocks.i) s.y++;
    Blocks.ResetRotate(s);
    insert = true;
  }
  void FixBlock() {
    cells[s.x, s.y].id = s.id;
    XY[] r = Blocks.Relatives(s);
    int cx, cy;
    for (int i = 0; i < r.Length; i++) {
      cx = r[i].x; cy = r[i].y;
      cells[s.x + cx, s.y + cy].id = s.id;
    }
  }
  void HideBlock() {
    cells[s.x, s.y].id = Blocks.empty;
    XY[] r = Blocks.Relatives(s);
    int cx, cy;
    for (int i = 0; i < r.Length; i++) {
      cx = r[i].x; cy = r[i].y;
      cells[s.x + cx, s.y + cy].id = Blocks.empty;
    }
  }
  bool IsEmpty(int x, int y, XY[] r) {
    int b = cells[x, y].id;
    if (b != Blocks.empty) return false;
    int rX, rY;
    for (int i = 0; i < r.Length; i++) {
      rX = r[i].x; rY = r[i].y;
      b = cells[x + rX, y + rY].id;
      if (b != Blocks.empty) {
        return false;
      }
    }
    return true;
  }
  internal bool MoveBlock(int x, int y) {
    HideBlock();
    int nx = s.x + x;
    int ny = s.y + y;
    XY[] r = Blocks.Relatives(s);
    if (IsEmpty(nx, ny, r)) {
      s.x = nx;
      s.y = ny;
      FixBlock();
      return true;
    }
    FixBlock();
    return false;
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
    InsertBlock();
    FixBlock();
  }
  void NextBlock() {
    s.id = next.Id();
    InsertBlock();
    CheckEnd();
    FixBlock();
  }
  void CheckEnd() {
    XY[] r = Blocks.Relatives(s);
    if (!IsEmpty(s.x, s.y, r)) {
      c.ended = true;
      c.end.gameObject.SetActive(true);
    }
  }
  internal void Drop() {
    c.frame = 0;
    if (MoveBlock(0, -1)) return;
    //-> dropped. no space to move.
    hold.used = false;
    CheckDelete();
  }
  void ClearCells() {
    for (int y = 1; y < 22; y++) {
      for (int x = 1; x < 11; x++) {
        cells[x, y].id = Blocks.empty;
      }
    }
  }
  void CheckDelete() {
    for (int y = 1; y < 22; y++) {
      for (int x = 1; x < 11; x++) {
        if (cells[x, y].id == Blocks.empty) break;
        if (x == 10) delete.Add(y);
      }
    }
    if (delete.Count == 0) NextBlock();
    else c.del = true;
  }
  internal void Delete() {
    int line = delete.Count;
    for (int i = 0; i < line; i++) {
      for (int y = delete[i] - i; y < 22; y++) {
        for (int x = 1; x < 11; x++) {
          cells[x, y].id = cells[x, y + 1].id;
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
    foreach (int y in delete) {
      for (int x = 1; x < 11; x++) {
        cells[x, y].AddAlpha(-0.03f);
      }
    }
  }
  internal void Render() {
    for (int y = 1; y < 22; y++) {
      for (int x = 1; x < 11; x++) {
        cells[x, y].Render();
      }
    }
  }
}
