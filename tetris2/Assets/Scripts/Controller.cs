using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour {
  public GameObject prfbBlock;
  SpriteRenderer[,] blocks = new SpriteRenderer[12, 25];
  int[,] board = new int[12, 25];
  Now n = new Now();
  Color red, yellow, purple, green, indigo, orange, lblue;
  void Start() {
    //-> Init board
    for (int x=0; x<12; x++) {
      for (int y=0; y<25; y++) {
        if (x==0 || x==11 || y==0) board[x, y] = Types.wall;
        else board[x, y] = Types.empty;
      }
    }
    //-> Init Sprite
    Vector2 pos; float nx, ny;
    for (int x = 0; x < 12; x++) {
      nx = -1.955f + (x * 0.355f);
      for (int y = 0; y < 25; y++) {
        ny = -3.790f + (y * 0.355f);
        blocks[x, y] = Instantiate(prfbBlock)
          .GetComponent<SpriteRenderer>();
        pos = blocks[x, y].transform.position;
        pos.x = nx; pos.y = ny;
        blocks[x, y].transform.position = pos;
      }
    }
    //-> Init Block Colors
    ColorUtility.TryParseHtmlString("#f44336", out red);
    ColorUtility.TryParseHtmlString("#FFEB3B", out yellow);
    ColorUtility.TryParseHtmlString("#673AB7", out purple);
    ColorUtility.TryParseHtmlString("#4CAF50", out green);
    ColorUtility.TryParseHtmlString("#3F51B5", out indigo);
    ColorUtility.TryParseHtmlString("#FF9800", out orange);
    ColorUtility.TryParseHtmlString("#03A9F4", out lblue);
    //-> Init Wall
    for (int x = 0; x < 12; x++) {
      if (x == 0 || x == 11) {
        //-> hide upper wall
        for (int y = 21; y < 25; y++) {
          Color(x, y, 0, 0, 0);
        }
        for (int y = 0; y < 21; y++) {
          Color(x, y, 0.9f, 0.9f, 0.9f);
        }
      } else { //-> only bottom
        Color(x, 0, 0.9f, 0.9f, 0.9f);
      }
    }
    n.Refresh();
    Show();
    Render();
  }
  void Show() {
    board[n.x, n.y] = n.Type();
    Block[] b = n.Blocks();
    int cx, cy;
    for (int i = 0; i < b.Length; i++) {
      cx = b[i].x; cy = b[i].y;
      board[n.x + cx, n.y + cy] = n.Type();
    }
  }
  void Hide() {
    board[n.x, n.y] = Types.empty;
    Block[] b = n.Blocks();
    int cx, cy;
    for (int i = 0; i < b.Length; i++) {
      cx = b[i].x; cy = b[i].y;
      board[n.x + cx, n.y + cy] = Types.empty;
    }
  }
  bool IsEmpty(int x, int y, Block[] b) {
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
  bool Move(int x, int y) {
    Hide();
    int nx = n.x + x;
    int ny = n.y + y;
    bool moved = false;
    Block[] b = n.Blocks();
    if (IsEmpty(nx, ny, b)) {
      n.x = nx;
      n.y = ny;
      moved = true;
    }
    Show();
    return moved;
  }

  void Rotate() {
    Hide();
    Block[] b = n.RotateBlocks();
    if (IsEmpty(n.x, n.y, b)) {
      n.Rotate();
    }
    Show();
  }

  readonly int inLeft = 1, inRight = 2, inJump = 3;
  int preInput = 0;
  void ProcessInput() {
    if (Input.GetAxisRaw("Horizontal") == -1) { // Left
      if (preInput == inLeft) return;
      preInput = inLeft;
      Move(-1, 0);
    } else if (Input.GetAxisRaw("Horizontal") == 1) { // Right
      if (preInput == inRight) return;
      preInput = inRight;
      Move(1, 0);
    } else if (Input.GetButtonDown("Jump")) { // Space or Y
      if (preInput == inJump) return;
      preInput = inJump;
      Rotate();
    } else if (Input.GetAxisRaw("Vertical") == -1) { // Down
      fWait -= wait / 2;
    } else { // None
      preInput = 0;
    }
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
    n.Refresh();
    Show();
    // TODO: GameOver判定
  }
  void Drop() {
    if (!Move(0, -1)) Dropped();
  }

  readonly int wait = 60;
  int fWait = 0;
  void Update() {
    ProcessInput();
    fWait--;
    if (fWait <= 0) {
      Drop();
      fWait = wait;
    }
    Render();
  }
  void Render() {
    //-> 壁の内側が対象
    for (int x = 1; x < 11; x++) {
      for (int y = 1; y < 25; y++) {
        if (board[x, y] == Types.i) Color(x, y, red);
        else if (board[x, y] == Types.o) Color(x, y, yellow);
        else if (board[x, y] == Types.s) Color(x, y, purple);
        else if (board[x, y] == Types.z) Color(x, y, green);
        else if (board[x, y] == Types.j) Color(x, y, indigo);
        else if (board[x, y] == Types.l) Color(x, y, orange);
        else if (board[x, y] == Types.t) Color(x, y, lblue);
        else Color(x, y, 0, 0, 0); // empty
      }
    }
  }
  Color c;
  void Color(int x, int y, float r, float g, float b) {
    c = blocks[x, y].color;
    c.r = r; c.g = g; c.b = b;
    blocks[x, y].color = c;
  }
  void Color(int x, int y, Color c) {
    blocks[x, y].color = c;
  }
}

