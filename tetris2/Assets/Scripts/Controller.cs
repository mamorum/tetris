using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour {
  public GameObject prfbBlock;
  SpriteRenderer[,] srBlock = new SpriteRenderer[12, 25];
  int[,] board = new int[12, 25];
  Cells[] cells = new Cells[] {
    null, new Line(), new Square(), new L1()
  };
  Status now = new Status();
  void ResetNow() {
    now.x = 5;
    now.y = 21;
    //now.type = Random.Range(1, 8); // 1 ～ 7
    //now.rotate = Random.Range(0, 5); // 0 ～ 4
    now.type = Random.Range(1, 4); // 1 ～ 3
    now.rotate = 0;
  }
  void Start() {
    //-> Init board
    for (int x=0; x<12; x++) {
      for (int y=0; y<25; y++) {
        if (x==0 || x==11 || y==0) board[x, y] = 1;
        else board[x, y] = 0;
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
    ResetNow();
    Show(now);
    Render();
    //PutBlock(current, false);
  }


  void Hide(Status s) {
    board[s.x, s.y] = 0;
    Cell[] c = cells[s.type].Get(s.rotate);
    int cx, cy;
    for (int i = 0; i < c.Length; i++) {
      cx = c[i].x; cy = c[i].y;
      board[s.x + cx, s.y + cy] = 0;
    }
  }
  void Show(Status s) { Show(s, 0, 0);  }
  void Show(Status s, int x, int y) {
    s.x += x; s.y += y;
    board[s.x, s.y] = s.type;
    Cell[] c = cells[s.type].Get(s.rotate);
    int cx, cy;
    for (int i = 0; i < c.Length; i++) {
      cx = c[i].x; cy = c[i].y;
      board[s.x + cx, s.y + cy] = s.type;
    }
  }
  bool Check(Status s, int x, int y) {
    int nx = s.x + x;
    int ny = s.y + y;
    if (board[nx, ny] != 0) return false;
    Cell[] c = cells[s.type].Get(s.rotate);
    int cx, cy;
    for (int i = 0; i < c.Length; i++) {
      cx = c[i].x; cy = c[i].y;
      if (board[nx + cx, ny + cy] != 0) {
        return false;
      }
    }
    return true;
  }

  readonly int inLeft = 1, inRight = 2, inJump = 3;
  int preInput = 0;
  bool ProcessInput() {
    bool ret = false;
    Status n = now.Copy();
    if (Input.GetAxisRaw("Horizontal") == -1) { // Left
      if (preInput == inLeft) return false;
      preInput = inLeft;
      Hide(now);
      if (Check(now, -1, 0)) Show(now, -1, 0);
      else Show(now);
    } else if (Input.GetAxisRaw("Horizontal") == 1) { // Right
      if (preInput == inRight) return false;
      preInput = inRight;
      Hide(now);
      if (Check(now, 1, 0)) Show(now, 1, 0);
      Show(now);
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
    if (n.x != now.x || n.y != now.y || n.rotate != now.rotate) {
      //DeleteBlock(current);
      //if (PutBlock(n, false)) current = n;
      //else PutBlock(current, false);
    }
    return ret;
  }
  void Down() {

  }

  int w = 0;
  void Update() {
    ProcessInput();
    if (w == 60) {
      Down();
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

