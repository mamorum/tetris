using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour {
  static Position Pos(int x, int y) { return new Position(x, y); }
  int[,] board = new int[12, 25];
  Block[] block = {
    new Block(1, Pos(0, 0), Pos(0, 0), Pos(0, 0)), // null
    new Block(2, Pos(0, -1), Pos(0, 1), Pos(0, 2)), // tetris
    new Block(4, Pos(0, -1), Pos(0, 1), Pos(1, 1)), // L1
    new Block(4, Pos(0, -1), Pos(0, 1), Pos(-1, 1)), // L2
    new Block(2, Pos(0, -1), Pos(1, 0), Pos(1, 1)), // Key1
    new Block(2, Pos(0, -1), Pos(-1, 0), Pos(-1, 1)), // Key2
    new Block(1, Pos(0, 1), Pos(1, 0), Pos(1, 1)), // Square
    new Block(4, Pos(0, -1), Pos(1, 0), Pos(-1, 0)), // T
  };
  Status current = new Status();

  bool PutBlock(Status s, bool action) {
    if (board[s.x, s.y] != 0) return false;
    if (action) board[s.x, s.y] = s.type;
    for (int i=0; i<3; i++) {
      int dx = block[s.type].p[i].x;
      int dy = block[s.type].p[i].y;
      int r = s.rotate % block[s.type].rotate;
      for (int j=0; j<r; j++) {
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
  bool DeleteBlock(Status s) {
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
        Debug.Log("Input: Left");
        preInput = inLeft;
        n.x--;
      }
    } else if (Input.GetAxisRaw("Horizontal") == 1) { // Right
      if (preInput != inRight) {
        Debug.Log("Input: Right");
        preInput = inRight;
        n.x++;
      }
    } else if (Input.GetButtonDown("Jump")) { // Space or Y
      if (preInput != inJump) {
        Debug.Log("Input: Jump");
        preInput = inJump;
        n.rotate++;
      }
    } else if (Input.GetAxisRaw("Vertical") == -1) { // Down
      Debug.Log("Input: Down");
      n.x++;
      ret = true;
    } else {
      preInput = 0;
    }
    if (n.x != current.x || n.y != current.y || n.rotate != current.rotate) {
      DeleteBlock(current);
      if (PutBlock(n, false)) current = n;
      else PutBlock(current, false);
    }
    return ret;
  }

  void Update() {
    ProcessInput();
  }
}

