using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board {
  Controller ctrl; Key key;
  Status st = new Status();
  int[,] board = new int[12, 25];
  bool moved = false;
  SpriteRenderer[,] cells = new SpriteRenderer[12, 25];
  Color black, gray,
    blue, yellow, green, red, indigo, orange, purple;
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
          board[x, y] = Types.wall;
          if (y >= 21) { //-> hide upper wall
            Color(x, y, black);
          } else { //-> wall
            Color(x, y, gray);
          }
        } else if (y == 0) { //-> wall
          board[x, y] = Types.wall;
          Color(x, y, gray);
        } else {
          board[x, y] = Types.empty;
        }
      }
    }
    NextBlock();
    FixBlock();
  }
  void NextBlock() {
    st.x = 5; st.y = 20;
    st.type = Types.Get();
    st.rotate = st.type.DefaultRotate();
  }
  void FixBlock() {
    board[st.x, st.y] = st.Type();
    Point[] b = st.Blocks();
    int cx, cy;
    for (int i = 0; i < b.Length; i++) {
      cx = b[i].x; cy = b[i].y;
      board[st.x + cx, st.y + cy] = st.Type();
    }
  }
  void HideBlock() {
    board[st.x, st.y] = Types.empty;
    Point[] b = st.Blocks();
    int cx, cy;
    for (int i = 0; i < b.Length; i++) {
      cx = b[i].x; cy = b[i].y;
      board[st.x + cx, st.y + cy] = Types.empty;
    }
  }
  bool IsEmpty(int x, int y, Point[] b) {
    if (board[x, y] != Types.empty) return false;
    int cx, cy;
    for (int i = 0; i < b.Length; i++) {
      cx = b[i].x; cy = b[i].y;
      if (board[x + cx, y + cy] != Types.empty) {
        return false;
      }
    }
    return true;
  }
  internal void MoveBlock(int x, int y) {
    HideBlock();
    int nx = st.x + x;
    int ny = st.y + y;
    moved = false;
    Point[] b = st.Blocks();
    if (IsEmpty(nx, ny, b)) {
      st.x = nx;
      st.y = ny;
      moved = true;
    }
    FixBlock();
  }

  internal void RotateBlock() {
    HideBlock();
    Point[] b = st.RotateBlocks();
    if (IsEmpty(st.x, st.y, b)) {
      st.Rotate();
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
        if (board[x, y] == Types.i) Color(x, y, blue);
        else if (board[x, y] == Types.o) Color(x, y, yellow);
        else if (board[x, y] == Types.s) Color(x, y, green);
        else if (board[x, y] == Types.z) Color(x, y, red);
        else if (board[x, y] == Types.j) Color(x, y, indigo);
        else if (board[x, y] == Types.l) Color(x, y, orange);
        else if (board[x, y] == Types.t) Color(x, y, purple);
        else Color(x, y, black); // empty
      }
    }
  }
  void Color(int x, int y, Color c) {
    cells[x, y].color = c;
  }
}
