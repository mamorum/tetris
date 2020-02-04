using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour {
  //-> board and cell
  static int[,] board = new int[12, 23];
  static SpriteRenderer[,] cells
    = new SpriteRenderer[12, 23];
  static SpriteRenderer[] border
    = new SpriteRenderer[10];
  //-> id
  static int empty = Blocks.empty;
  static int wall = Blocks.wall;
  static int o = Blocks.o;
  //-> status
  int x, y, id, rotate;
  bool moved = false;
  //-> objects
  public GameObject prfbCell;
  public Blocks blocks;
  Next next = new Next();
  Controller ctrl;
  void InitCell(int x, int y, float posX, float posY) {
    cells[x, y] = Instantiate(prfbCell)
      .GetComponent<SpriteRenderer>();
    Vector2 pos = cells[x, y].transform.position;
    pos.x = posX; pos.y = posY;
    cells[x, y].transform.position = pos;
  }
  internal void Init(Controller c) {
    blocks.Init(); next.Init(); ctrl = c;
    //-> Init cells and board
    float posX, posY;
    for (int x = 0; x < 12; x++) {
      posX = -1.955f + (x * 0.355f);
      for (int y = 0; y < 23; y++) {
        posY = -3.790f + (y * 0.355f);
        InitCell(x, y, posX, posY);
        if (y == 0) {
          board[x, y] = wall;
          cells[x, y].color = blocks.gray;
        } else if (x == 0 || x == 11) {
          board[x, y] = wall;
          if (y == 22) cells[x, y].gameObject.SetActive(false); // hide
          else cells[x, y].color = blocks.gray;
        } else {
          board[x, y] = empty;
          cells[x, y].color = blocks.black;
          if (y == 21) {
            border[x - 1] = Instantiate(prfbCell)
              .GetComponent<SpriteRenderer>();
            Vector2 pos = border[x - 1].transform.position;
            pos.x = posX; pos.y = posY;
            border[x - 1].transform.position = pos;
            border[x - 1].color = blocks.gray;
            border[x - 1].color
              = new Color(blocks.gray.r, blocks.gray.g, blocks.gray.b, 0.7f);
          } else if (y == 22) {
            cells[x, y].gameObject.SetActive(false); // hide
          }
        }
      }
    }
    NextBlock();
    FixBlock();
  }
  void NextBlock() {
    x = 5; y = 20;
    id = next.Id();
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
    board[x, y] = empty;
    XY[] r = blocks.Relatives(id, rotate);
    int cx, cy;
    for (int i = 0; i < r.Length; i++) {
      cx = r[i].x; cy = r[i].y;
      board[x + cx, y + cy] = empty;
    }
  }
  bool IsEmpty(int tX, int tY, XY[] r) {
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
    XY[] r = blocks.Relatives(id, rotate);
    if (IsEmpty(nx, ny, r)) {
      x = nx;
      y = ny;
      moved = true;
    }
    FixBlock();
  }

  internal void RotateBlock() {
    if (id == o) return; // no rotations
    int nr = blocks.Rotate(id, rotate);
    XY[] r = blocks.Relatives(id, nr);
    HideBlock();
    if (IsEmpty(x, y, r)) rotate = nr;
    FixBlock();
  }
  void DeleteLine() {
    for (int y = 1; y < 22; y++) {
      bool flag = true;
      for (int x = 1; x < 11; x++) {
        if (board[x, y] == empty) {
          flag = false;
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
    DeleteLine();
    NextBlock();
    CheckEnd();
    FixBlock();
  }
  internal void Render() {
    for (int y = 1; y < 22; y++) {
      for (int x = 1; x < 11; x++) {
        int i = board[x, y];
        cells[x, y].color = blocks.colors[i];
      }
    }
  }
}
