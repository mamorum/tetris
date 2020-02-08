using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour {
  public Blocks blocks;
  public Cell cell;
  public Board board;
  public Next next;
  void Start() {
    blocks.Init();
    board.Init(this);
    board.Render();
  }
  internal bool end = false;
  int frame = 0, drop = 60;
  void Update() {
    if (end) return;
    ProcessInput();
    frame++;
    if (frame >= drop) {
      board.Drop();
      frame = 0;
    }
    board.Render();
  }
  //-> process input
  readonly int
    left = 1, right = 2, rotate = 3, down = 4;
  int preInput = 0;
  internal bool dropped = false;
  internal void ProcessInput() {
    if (Input.GetAxisRaw("Horizontal") == -1) { // Left
      if (preInput == left) return;
      board.MoveBlock(-1, 0);
      preInput = left;
    } else if (Input.GetAxisRaw("Horizontal") == 1) { // Right
      if (preInput == right) return;
      board.MoveBlock(1, 0);
      preInput = right;
    } else if (Input.GetButton("Fire3")) { // Space or 〇
      if (preInput == rotate) return;
      board.RotateBlock();
      preInput = rotate;
    } else if (Input.GetButton("Jump")) { // H or △
      // TODO: ホールドを実装
    } else if (Input.GetAxisRaw("Vertical") == -1) { // Down
      if (dropped && preInput == down) return;
      frame += drop / 2; // speed up
      dropped = false;
      preInput = down;
    } else { // None
      preInput = 0;
    }
  }
}

