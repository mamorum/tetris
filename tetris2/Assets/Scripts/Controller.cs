using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour {
  public Board board;
  void Start() {
    board.Init(this);
    board.Render();
  }
  static int wait = 60;
  int fWait = wait;
  void Update() {
    ProcessInput();
    fWait--;
    if (fWait <= 0) {
      board.Drop();
      fWait = wait;
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
    } else if (Input.GetButton("Jump")) { // Space or Y
      if (preInput == rotate) return;
      board.RotateBlock();
      preInput = rotate;
    } else if (Input.GetAxisRaw("Vertical") == -1) { // Down
      if (dropped && preInput == down) return;
      fWait -= wait / 2; // speed up
      dropped = false;
      preInput = down;
    } else { // None
      preInput = 0;
    }
  }
}

