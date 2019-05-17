using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour {
  public GameObject prfbBlock;
  SpriteRenderer[,] blocks = new SpriteRenderer[12, 25];
  int[,] board = new int[12, 25];
  Type[] types = new Type[] {
    new I(), new O(), new J()
  };
  Now now = new Now();
  void Next() {
    now.x = 5;
    now.y = 21;
    //now.type = Random.Range(1, 8); // 1 ～ 7
    //now.rotate = Random.Range(0, 5); // 0 ～ 4
    now.type = types[Random.Range(0, 3)]; // 1 ～ 2
    now.rotate = 0;
    Show(now);
  }
  void Start() {
    //-> Init board
    for (int x=0; x<12; x++) {
      for (int y=0; y<25; y++) {
        if (x==0 || x==11 || y==0) board[x, y] = Type.wall;
        else board[x, y] = Type.empty;
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
    //-> Hide Upper Wall
    for (int x = 0; x < 2; x++) {
      for (int y = 21; y < 25; y++) {
        c = blocks[x * 11, y].color;
        c.r = 0f; c.g = 0f; c.b = 0f;
        blocks[x * 11, y].color = c;
      }
    }
    Next();
    Render();
  }
  void Show(Now s) {
    board[s.x, s.y] = s.type.Id();
    Block[] b = s.type.Blocks(s.rotate);
    int cx, cy;
    for (int i = 0; i < b.Length; i++) {
      cx = b[i].x; cy = b[i].y;
      board[s.x + cx, s.y + cy] = s.type.Id();
    }
  }
  void Hide(Now s) {
    board[s.x, s.y] = Type.empty;
    Block[] b = s.type.Blocks(s.rotate);
    int cx, cy;
    for (int i = 0; i < b.Length; i++) {
      cx = b[i].x; cy = b[i].y;
      board[s.x + cx, s.y + cy] = Type.empty;
    }
  }
  bool IsEmpty(Now s, int x, int y) {
    if (board[x, y] != Type.empty) return false;
    Block[] b = s.type.Blocks(s.rotate);
    int cx, cy;
    for (int i = 0; i < b.Length; i++) {
      cx = b[i].x; cy = b[i].y;
      if (board[x + cx, y + cy] != Type.empty) {
        return false;
      }
    }
    return true;
  }
  bool moved;
  void Move(Now s, int x, int y) {
    Hide(s);
    int nx = s.x + x;
    int ny = s.y + y;
    if (IsEmpty(s, nx, ny)) {
      moved = true;
      s.x = nx;
      s.y = ny;
    } else {
      moved = false;
    }
    Show(s);
  }

  readonly int inLeft = 1, inRight = 2, inJump = 3;
  int preInput = 0;
  void ProcessInput() {
    if (Input.GetAxisRaw("Horizontal") == -1) { // Left
      if (preInput == inLeft) return;
      preInput = inLeft;
      Move(now, -1, 0);
    } else if (Input.GetAxisRaw("Horizontal") == 1) { // Right
      if (preInput == inRight) return;
      preInput = inRight;
      Move(now, 1, 0);
    } else if (Input.GetButtonDown("Jump")) { // Space or Y
      if (preInput == inJump) return;
      preInput = inJump;
      //n.rotate++;
    } else if (Input.GetAxisRaw("Vertical") == -1) { // Down
      fWait -= wait / 2;
    } else { // None
      preInput = 0;
    }
  }
  void Down() {
    Move(now, 0, -1);
    //-> ブロック落下中
    if (moved) return;
    //-> ブロック落下済
    Next();
    // TODO: 行の削除
    // TODO: GameOver判定
  }

  readonly int wait = 60;
  int fWait = 0;
  void Update() {
    ProcessInput();
    fWait--;
    if (fWait <= 0) {
      Down();
      fWait = wait;
    }
    Render();
  }
  Color c;
  void Render() {
    for (int x = 1; x < 11; x++) {
      for (int y = 1; y < 25; y++) {
        if (board[x, y] == Type.wall) {
          c = blocks[x, y].color;
          c.r = 1f; c.g = 1f; c.b = 1f;
          blocks[x, y].color = c;
        } else if (board[x, y] != Type.empty) {
          c = blocks[x, y].color;
          c.r = 0.65f; c.g = 0.65f; c.b = 0.65f;
          blocks[x, y].color = c;
        } else {
          c = blocks[x, y].color;
          c.r = 0f; c.g = 0f; c.b = 0f;
          blocks[x, y].color = c;
        }
      }
    }
  }
}

