using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour {
  //-> board cell
  static int[,] board = new int[12, 23];
  static SpriteRenderer[,] bases = new SpriteRenderer[12, 22];
  static List<SpriteRenderer> walls = new List<SpriteRenderer>();
  //-> next cell
  static int[,] nexts = new int[5, 19];
  static SpriteRenderer[,] queues = new SpriteRenderer[5, 19];
  //-> id
  static int empty = Blocks.empty;
  static int wall = Blocks.wall;
  static int blockI = Blocks.i;
  static int blockO = Blocks.o;
  //-> status
  int x, y, id, rotate;
  bool moved = false;
  //-> objects
  public GameObject prfbCell;
  public Blocks blocks;
  Next next = new Next();
  Controller ctrl;
  void CreateCell(
    SpriteRenderer[,] sr, int x, int y, float posX, float posY
  ) {
    sr[x, y] = Instantiate(prfbCell)
      .GetComponent<SpriteRenderer>();
    Vector2 pos = sr[x, y].transform.position;
    pos.x = posX; pos.y = posY;
    sr[x, y].transform.position = pos;
    sr[x, y].color = blocks.black;
  }
  void CreateWall(float posX, float posY) {
    SpriteRenderer sr = Instantiate(prfbCell)
      .GetComponent<SpriteRenderer>();
    Vector2 pos = sr.transform.position;
    pos.x = posX; pos.y = posY;
    sr.transform.position = pos;
    sr.color = blocks.gray;
    walls.Add(sr);
  }
  internal void Init(Controller c) {
    blocks.Init(); next.Init(); ctrl = c;
    //-> Init board and cell
    float posX = 0, posY;
    float baseX = -1.955f, baseY = -3.790f;
    for (int y = 0; y < 22; y++) {
      posY = baseY + (y * 0.355f);
      for (int x = 0; x < 12; x++) {
        posX = baseX + (x * 0.355f);
        CreateCell(bases, x, y, posX, posY);
        if (x == 11 || x == 0 || y == 0) {
          board[x, y] = wall;
          CreateWall(posX, posY);
        } else if (y == 21) {
          board[x, y] = empty;
          CreateWall(posX, posY);
        } else {
          board[x, y] = empty;
        }
      }
    }
    for (int x = 0; x < 12; x++) {
      board[x, 22] = empty; // not displayed
    }
    //-> Next
    baseX = posX + 0.355f;
    for (int y = 0; y < 19; y++) {
      posY = baseY + (y * 0.355f);
      for (int x = 0; x < 5; x++) {
        posX = baseX + (x * 0.355f);
        CreateCell(queues, x, y, posX, posY);
      }
    }
    NextBlock();
    FixBlock();
  }
  void NextBlock() {
    x = 5; y = 20;
    id = next.Id();
    rotate = blocks.DefaultRotate(id);
    Nexts();
  }
  void Nexts() {
    //-> reset
    for (int y = 0; y < 19; y++) {
      for (int x = 0; x < 5; x++) {
        nexts[x, y] = 0;
      }
    }
    //-> id
    int[] queue = next.Queue();
    int nid, nrt, nx, ny = 17;
    for (int i = 0; i < 3; i++) {
      nid = queue[i];
      nrt = blocks.DefaultRotate(nid);
      if (nid == blockI) ny++;
      if (nid == blockO) nx = 1;
      else nx = 2;
      nexts[nx, ny] = nid;
      XY[] r = blocks.Relatives(nid, nrt);
      for (int j = 0; j < r.Length; j++) {
        nexts[nx + r[j].x, ny + r[j].y] = nid;
      }
      ny = ny - 4;
    }
    //-> color
    for (int y = 0; y < 19; y++) {
      for (int x = 0; x < 5; x++) {
        int i = nexts[x, y];
        queues[x, y].color = blocks.colors[i];
      }
    }
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
    if (id == blockO) return; // no rotation
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
    DeleteLine();
    NextBlock();
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
  }
}
