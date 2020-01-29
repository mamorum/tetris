using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board {
  SpriteRenderer[,] cells = new SpriteRenderer[12, 25];
  int[,] board = new int[12, 25];
  Status st = new Status();
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
  internal void Init(Controller c) {
    InitColor();
    //-> Init cells and board
    Vector2 pos; float nx, ny;
    for (int x = 0; x < 12; x++) {
      nx = -1.955f + (x * 0.355f);
      for (int y = 0; y < 25; y++) {
        ny = -3.790f + (y * 0.355f);
        cells[x, y] = c.Cell();
        pos = cells[x, y].transform.position;
        pos.x = nx; pos.y = ny;
        cells[x, y].transform.position = pos;
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
    Reset(st);
    Refresh();
    Render();
  }
  void Reset(Status s) {
    s.x = 5; s.y = 20;
    s.type = Types.Get();
    s.rotate = s.type.DefaultRotate();
  }
  void Refresh() {
    board[st.x, st.y] = st.Type();
    Point[] b = st.Blocks();
    int cx, cy;
    for (int i = 0; i < b.Length; i++) {
      cx = b[i].x; cy = b[i].y;
      board[st.x + cx, st.y + cy] = st.Type();
    }
  }
  void Hide() {
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
  internal bool Move(int x, int y) {
    Hide();
    int nx = st.x + x;
    int ny = st.y + y;
    bool moved = false;
    Point[] b = st.Blocks();
    if (IsEmpty(nx, ny, b)) {
      st.x = nx;
      st.y = ny;
      moved = true;
    }
    Refresh();
    return moved;
  }

  internal void Rotate() {
    Hide();
    Point[] b = st.RotateBlocks();
    if (IsEmpty(st.x, st.y, b)) {
      st.Rotate();
    }
    Refresh();
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
    DeleteLine();
    Reset(st);
    Refresh();
    // TODO: GameOver判定
  }
  internal void Drop() {
    if (!Move(0, -1)) Dropped();
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
