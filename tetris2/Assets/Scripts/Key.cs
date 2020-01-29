using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key {
  Controller ctrl; Board board;
  internal void Init(Controller c, Board b) {
    ctrl = c; board = b;
  }
  internal bool dropped = false;
  readonly int
    left = 1, right = 2, rotate = 3, down = 4;
  int pre = 0;
  internal void Process() {
    if (Input.GetAxisRaw("Horizontal") == -1) { // Left
      if (pre == left) return;
      pre = left;
      board.MoveBlock(-1, 0);
    } else if (Input.GetAxisRaw("Horizontal") == 1) { // Right
      if (pre == right) return;
      pre = right;
      board.MoveBlock(1, 0);
    } else if (Input.GetButton("Jump")) { // Space or Y
      if (pre == rotate) return;
      pre = rotate;
      board.RotateBlock();
    } else if (Input.GetAxisRaw("Vertical") == -1) { // Down
      if (dropped && pre == down) return;
      dropped = false;
      pre = down;
      ctrl.SpeedUp();
    } else { // None
      pre = 0;
    }
  }
}