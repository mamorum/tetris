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
  bool IsDown() {
    if (joystic) return Input.GetAxisRaw("Vertical") == -1;
    else return Input.GetKey(KeyCode.DownArrow);
  }
  //-> Process user input.
  readonly int
    left = 1, right = 2, rotate = 3, down = 4;
  int pre = 0;
  internal bool dropped = false;
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
  void Down() {
    if (dropped && pre != 0) return;
    dropped = false;
    pre = down;
    ctrl.SpeedUp();
  }
  internal void Process() {
    if (IsLeft()) Left();
    else if (IsRight()) Right();
    else if (IsRotate()) Rotate();
    else if (IsDown()) Down();
    else pre = 0;
  }
}
