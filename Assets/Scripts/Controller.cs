using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour {
  public GameObject prfbBlock;
  static Position Pos(int x, int y) { return new Position(x, y); }
  int[,] board = new int[12, 25];
  SpriteRenderer[,] srBlock = new SpriteRenderer[12, 25];
  Block[] block = {
    new Block(1, Pos(0, 0), Pos(0, 0), Pos(0, 0)), // null
    new Block(2, Pos(0, -1), Pos(0, 1), Pos(0, 2)), // tetris
    new Block(4, Pos(0, -1), Pos(0, 1), Pos(1, 1)), // L1
    new Block(4, Pos(0, -1), Pos(0, 1), Pos(-1, 1)), // L2
    new Block(2, Pos(0, -1), Pos(1, 0), Pos(1, 1)), // Key1
    new Block(2, Pos(0, -1), Pos(-1, 0), Pos(-1, 1)), // Key2
    new Block(1, Pos(0, 1), Pos(1, 0), Pos(1, 1)), // Square
    new Block(4, Pos(0, -1), Pos(1, 0), Pos(-1, 0)) // T
  };
  Status current = new Status();

  bool PutBlock(Status s, bool action) {
    if (board[s.x, s.y] != 0) return false;
    if (action) board[s.x, s.y] = s.type;
    for (int i = 0; i < 3; i++) {
      int dx = block[s.type].p[i].x;
      int dy = block[s.type].p[i].y;
      int r = s.rotate % block[s.type].rotate;
      for (int j = 0; j < r; j++) {
        int nx, ny;
        nx = dx; ny = dy; dx = ny; dy = -nx;
      }
      if (board[s.x + dx, s.y + dy] != 0) {
        return false;
      }
      if (action) {
        board[s.x + dx, s.y + dy] = s.type;
      }
    }
    if (!action) PutBlock(s, true);
    return true;
  }

  void Start() {
    //-> Init board
    for (int x=0; x<12; x++) {
      for (int y=0; y<25; y++) {
        if (x==0 || x==11 || y==0) {
          board[x, y] = 1;
        } else {
          board[x, y] = 0;
        }
      }
    }
    //-> Init Sprite
    Vector2 pos; float nx, ny;
    for (int x = 0; x < 12; x++) {
      nx = -1.955f + (x * 0.355f);
      for (int y = 0; y < 25; y++) {
        ny = -4.5f + (y * 0.355f);
        srBlock[x, y] = Instantiate(prfbBlock).GetComponent<SpriteRenderer>();
        pos = srBlock[x, y].transform.position;
        pos.x = nx; pos.y = ny;
        srBlock[x, y].transform.position = pos;
      }
    }
    //-> Init Current
    current.x = 5;
    current.y = 21;
    current.type = Random.Range(1, 8); // 1 ～ 7
    current.rotate = Random.Range(0, 5); // 0 ～ 4
    PutBlock(current, false);
  }

  readonly int inLeft = 1, inRight = 2, inJump = 3;
  int preInput = 0;
  bool ProcessInput() {
    bool ret = false;
    Status n = current.Copy();
    if (Input.GetAxisRaw("Horizontal") == -1) { // Left
      if (preInput != inLeft) {
        preInput = inLeft;
        n.x--;
      }
    } else if (Input.GetAxisRaw("Horizontal") == 1) { // Right
      if (preInput != inRight) {
        preInput = inRight;
        n.x++;
      }
    } else if (Input.GetButtonDown("Jump")) { // Space or Y
      if (preInput != inJump) {
        preInput = inJump;
        n.rotate++;
      }
    } else if (Input.GetAxisRaw("Vertical") == -1) { // Down
      n.y--;
      ret = true;
    } else { // None
      preInput = 0;
    }
    if (n.x != current.x || n.y != current.y || n.rotate != current.rotate) {
      DeleteBlock(current);
      if (PutBlock(n, false)) current = n;
      else PutBlock(current, false);
    }
    return ret;
  }
  void BlockDown() {
    DeleteBlock(current);
    current.y--;
    if (!PutBlock(current, false)) {
      current.y++;
      PutBlock(current, false);
      DeleteLine();
      current.x = 5;
      current.y = 21;
      current.type = Random.Range(1, 8); // 1 ～ 7
      current.rotate = Random.Range(0, 5); // 0 ～ 4
      if (!PutBlock(current, false)) {
        // game over
      }
    }
  }
  void DeleteBlock(Status s) {
    board[s.x, s.y] = 0;
    for (int i = 0; i < 3; i++) {
      int dx = block[s.type].p[i].x;
      int dy = block[s.type].p[i].y;
      int r = s.rotate % block[s.type].rotate;
      for (int j = 0; j < r; j++) {
        int nx, ny;
        nx = dx; ny = dy; dx = ny; dy = -nx;
      }
      board[s.x + dx, s.y + dy] = 0;
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

  int w = 0;
  void Update() {
    ProcessInput();
    //if (w % 2 == 0) {
    //  if (ProcessInput()) {
    //    w = 0;
    //  }
    //}
    if (w == 60) {
      BlockDown();
      w = 0;
    }
    w++;
    Render();
  }
  Color c;
  void Render() {
    for (int x = 0; x < 12; x++) {
      for (int y = 0; y < 25; y++) {
        if (board[x, y] > 0) {
          c = srBlock[x, y].color;
          c.a = 1f; c.g = 1f; c.b = 1f;
          srBlock[x, y].color = c;
        } else {
          c = srBlock[x, y].color;
          c.a = 0f; c.g = 0f; c.b = 0f;
          srBlock[x, y].color = c;
        }
      }
    }
  }
}

