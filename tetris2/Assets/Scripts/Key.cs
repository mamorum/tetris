using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key {
  Controller ctrl; Board board;
  bool joystic = false;
  internal void Init(Controller c, Board b) {
    ctrl = c; board = b;
    if (Input.GetJoystickNames().Length == 1) {
      joystic = true;
    }
  }
  //-> Judge user input.
  bool IsDown() {
    if (joystic) return Input.GetAxisRaw("Vertical") == -1;
    else return Input.GetKey(KeyCode.DownArrow);
  }
  bool IsLeft() {
    if (joystic) return Input.GetAxisRaw("Horizontal") == -1;
    else return Input.GetKey(KeyCode.LeftArrow);
  }
  bool IsRight() {
    if (joystic) return Input.GetAxisRaw("Horizontal") == 1;
    else return Input.GetKey(KeyCode.RightArrow);
  }
  bool IsRotate() {
    return Input.GetButton("Jump");  // Space or Y
  }
  //-> Process user input.
  readonly int
    down = 1, left = 2, right = 3, rotate = 4;
  int pre = 0;
  internal bool dropped = false;
  void Down() {
    if (dropped && pre != 0) return;
    dropped = false;
    pre = down;
    ctrl.SpeedUp();
  }
  void Left() {
    if (pre != 0) return;
    pre = left;
    board.MoveBlock(-1, 0);
  }
  void Right() {
    if (pre != 0) return;
    pre = right;
    board.MoveBlock(1, 0);
  }
  void Rotate() {
    if (pre != 0) return;
    pre = rotate;
    board.RotateBlock();
  }
  internal void Process() {
    if (IsDown()) Down();
    else if (IsLeft()) Left();
    else if (IsRight()) Right();
    else if (IsRotate()) Rotate();
    else pre = 0;
  }
}
