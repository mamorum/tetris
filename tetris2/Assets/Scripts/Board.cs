using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board {
  //-> definitions
  static Color[] colors;
  static Color black, gray,
    blue, yellow, green, red, indigo, orange, purple;
  static int empty = 0, wall = 1,
     i = 2, o = 3, s = 4, z = 5, j = 6, l = 7, t = 8;
  static Type[] types = new Type[] {
    null, null, //-> empty, wall
    new I(), new TypeO(), new TypeS(), new TypeZ(),
    new TypeJ(), new TypeL(), new TypeT() };
  static int[] blocks = new int[] { i, o, s, z, j, l, t };
  //-> current status
  int[,] board = new int[12, 25];
  SpriteRenderer[,] cells = new SpriteRenderer[12, 25];
  int x, y, id, rotate; bool moved = false;
  Next next = new Next();
  Controller ctrl; Key key;
  void InitColor() {
    ColorUtility.TryParseHtmlString("#000000", out black);
    ColorUtility.TryParseHtmlString("#e6e6e6", out gray);
    ColorUtility.TryParseHtmlString("#03a9f4", out blue);
    ColorUtility.TryParseHtmlString("#ffd83b", out yellow);
    ColorUtility.TryParseHtmlString("#4caf50", out green);
    ColorUtility.TryParseHtmlString("#f44336", out red);
    ColorUtility.TryParseHtmlString("#3f51b5", out indigo);
    ColorUtility.TryParseHtmlString("#ff9800", out orange);
    ColorUtility.TryParseHtmlString("#b53dc4", out purple);
    colors = new Color[] {
      black, gray, blue, yellow, green, red, indigo, orange, purple
    };
  }
  void InitCell(int x, int y, float posX, float posY) {
    cells[x, y] = ctrl.Cell();
    Vector2 pos = cells[x, y].transform.position;
    pos.x = posX; pos.y = posY;
    cells[x, y].transform.position = pos;
  }
  internal void Init(Controller c, Key k) {
    ctrl = c; key = k;
    InitColor();
    //-> Init cells and board
    float posX, posY;
    for (int x = 0; x < 12; x++) {
      posX = -1.955f + (x * 0.355f);
      for (int y = 0; y < 25; y++) {
        posY = -3.790f + (y * 0.355f);
        InitCell(x, y, posX, posY);
        if (x == 0 || x == 11) {
          board[x, y] = wall;
          if (y >= 21) { //-> hide upper wall
            cells[x, y].color = black;
          } else { //-> wall
            cells[x, y].color = gray;
          }
        } else if (y == 0) { //-> wall
          board[x, y] = wall;
          cells[x, y].color = gray;
        } else {
          board[x, y] = empty;
        }
      }
    }
    next.Init(blocks);
    NextBlock();
    FixBlock();
  }
  void NextBlock() {
    x = 5; y = 20;
    id = next.Get();
    rotate = types[id].DefaultRotate();
  }
  void FixBlock() {
    board[x, y] = id;
    Point[] b = types[id].Blocks(rotate);
    int cx, cy;
    for (int i = 0; i < b.Length; i++) {
      cx = b[i].x; cy = b[i].y;
      board[x + cx, y + cy] = id;
    }
  }
  void HideBlock() {
    board[x, y] = empty;
    Point[] b = types[id].Blocks(rotate);
    int cx, cy;
    for (int i = 0; i < b.Length; i++) {
      cx = b[i].x; cy = b[i].y;
      board[x + cx, y + cy] = empty;
    }
  }
  bool IsEmpty(int tX, int tY, Point[] r) {
    if (board[tX, tY] != empty) return false;
    int rX, rY;
    for (int i = 0; i < r.Length; i++) {
      rX = r[i].x; rY = r[i].y;
      if (board[tX + rX, tY + rY] != empty) {
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
    Point[] r = types[id].Blocks(rotate);
    if (IsEmpty(nx, ny, r)) {
      x = nx;
      y = ny;
      moved = true;
    }
    FixBlock();
  }

  internal void RotateBlock() {
    HideBlock();
    Point[] r = types[id].Blocks(rotate);
    if (IsEmpty(x, y, r)) {
      rotate = types[id].Rotate(rotate);
    }
    FixBlock();
  }
  void DeleteLine() {
    for (int y = 1; y < 23; y++) {
      bool flag = true;
      for (int x = 1; x <= 10; x++) {
        if (board[x, y] == 0) {
          flag = false;
        }
      }
      if (flag) {
        for (int j = y; j < 23; j++) {
          for (int i = 1; i <= 10; i++) {
            board[i, j] = board[i, j + 1];
          }
        }
        y--;
      }
    }
  }
  void Dropped() {
    key.dropped = true;
    DeleteLine();
    NextBlock();
    FixBlock();
    // TODO: GameOver判定
  }
  internal void Drop() {
    MoveBlock(0, -1);
    if (!moved) Dropped();
  }
  internal void Render() {
    //-> 壁の内側が対象
    for (int x = 1; x < 11; x++) {
      for (int y = 1; y < 25; y++) {
        int i = board[x, y];
        cells[x, y].color = colors[i];
      }
    }
  }
}
