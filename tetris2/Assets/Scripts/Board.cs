﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour {
  //-> board cell
  static int[,] board = new int[12, 23];
  static SpriteRenderer[,] bases = new SpriteRenderer[12, 22];
  static List<SpriteRenderer> walls = new List<SpriteRenderer>();
  //-> status
  int x, y, id, rotate;
  bool moved = false;
  //-> objects
  Controller ctrl; Cell cell;
  Blocks blocks; Hold hold; Next next;
  internal void Init(Controller c) {
    ctrl = c; cell = c.cell; blocks = c.blocks;
    hold = c.hold; next = c.next;
    //-> top of the board (not displayed.)
    for (int x = 0; x < 12; x++) {
      board[x, 22] = blocks.empty;
    }
    //-> cell and board
    float posX = 0, posY;
    float baseX = -1.955f, baseY = -3.790f;
    for (int y = 0; y < 22; y++) {
      posY = baseY + (y * 0.355f);
      for (int x = 0; x < 12; x++) {
        posX = baseX + (x * 0.355f);
        bases[x, y] = cell.Empty(posX, posY);
        if (x == 11 || x == 0 || y == 0) {
          board[x, y] = blocks.wall;
          walls.Add(cell.Wall(posX, posY));
        } else if (y == 21) {
          board[x, y] = blocks.empty;
          walls.Add(cell.Wall(posX, posY));
        } else {
          board[x, y] = blocks.empty;
        }
      }
    }
    hold.Init(c);
    next.Init(c, baseY, posX);
    id = next.Id();
    PutBlock();
    FixBlock();
  }
  void PutBlock() {
    x = 5; y = 20;
    rotate = blocks.DefaultRotate(id);
  }
  void FixBlock() {
    board[x, y] = id;
    XY[] r = blocks.Relatives(id, rotate);
    int cx, cy;
    for (int i = 0; i < r.Length; i++) {
      cx = r[i].x; cy = r[i].y;
      board[x + cx, y + cy] = id;
    }
  }
  void HideBlock() {
    board[x, y] = blocks.empty;
    XY[] r = blocks.Relatives(id, rotate);
    int cx, cy;
    for (int i = 0; i < r.Length; i++) {
      cx = r[i].x; cy = r[i].y;
      board[x + cx, y + cy] = blocks.empty;
    }
  }
  bool IsEmpty(int tX, int tY, XY[] r) {
    int b = board[tX, tY];
    if (b != blocks.empty) return false;
    int rX, rY;
    for (int i = 0; i < r.Length; i++) {
      rX = r[i].x; rY = r[i].y;
      b = board[tX + rX, tY + rY];
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
    HideBlock();
    id = hold.Add(id);
    if (id == blocks.empty) {
      id = next.Id(); // first time
    }
    hold.Render();
    PutBlock();
    FixBlock();
  }
  void DeleteLine() {
    for (int y = 1; y < 22; y++) {
      bool flag = true;
      for (int x = 1; x < 11; x++) {
        if (board[x, y] == blocks.empty) {
          flag = false;
          break;
        }
      }
      if (flag) {
        for (int j = y; j < 22; j++) {
          for (int i = 1; i < 11; i++) {
            board[i, j] = board[i, j + 1];
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
    id = next.Id();
    DeleteLine();
    PutBlock();
    CheckEnd();
    FixBlock();
  }
  internal void Render() {
    //-> only inside wall
    for (int y = 1; y < 22; y++) {
      for (int x = 1; x < 11; x++) {
        int i = board[x, y];
        bases[x, y].color = blocks.colors[i];
      }
    }
    next.Render();
  }
}
