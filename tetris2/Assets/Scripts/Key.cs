using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key {
  Controller ctrl; Board board;
  internal void Init(Controller c, Board b) {
    ctrl = c; board = b;
  }
  readonly int left = 1, right = 2, rotate = 3;
  int pre = 0;
  internal void Process() {
    if (Input.GetAxisRaw("Horizontal") == -1) { // Left
      if (pre == left) return;
      pre = left;
      board.Move(-1, 0);
    } else if (Input.GetAxisRaw("Horizontal") == 1) { // Right
      if (pre == right) return;
      pre = right;
      board.Move(1, 0);
    } else if (Input.GetButtonDown("Jump")) { // Space or Y
      if (pre == rotate) return;
      pre = rotate;
      board.Rotate();
    } else if (Input.GetAxisRaw("Vertical") == -1) { // Down
      ctrl.SpeedUp();
    } else { // None
      pre = 0;
    }
  }
}
