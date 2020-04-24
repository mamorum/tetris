using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key {
  static bool[] pre = new bool[6] {
    false, false, false, false, false, false };
  static bool[] now = new bool[6] {
    false, false, false, false, false, false };
  //<- 0:left, 1:right, 2:up, 3:down, 4:rotate, 5:hold
  static bool[] swap;
  static bool LeftRightKeys() {
    return Input.GetKey(KeyCode.LeftArrow)
      && Input.GetKey(KeyCode.RightArrow);
  }
  static bool UpDownKeys() {
    return Input.GetKey(KeyCode.UpArrow)
      && Input.GetKey(KeyCode.DownArrow);
  }
  internal static void Handle() {
    swap = pre; pre = now; now = swap; // swap
    if (!LeftRightKeys()) {
      now[0] = Input.GetAxisRaw("Horizontal") == -1; // left
      now[1] = Input.GetAxisRaw("Horizontal") == 1; // right
    }
    if (!UpDownKeys()) {
      now[2] = Input.GetAxisRaw("Vertical") == 1; // up
      now[3] = Input.GetAxisRaw("Vertical") == -1; // down
    }
    now[4] = Input.GetButtonDown("Fire3") ||
      Input.GetKeyDown(KeyCode.Return); // rotate
    now[5] = Input.GetButtonDown("Jump"); // hold
  }
  internal static bool Left() { return !pre[0] && now[0]; }
  internal static bool Right() { return !pre[1] && now[1]; }
  internal static bool Up() { return !pre[2] && now[2]; }
  internal static bool Down() { return !pre[3] && now[3];}
  internal static bool PressingDown() { return pre[3] && now[3]; }
  internal static bool Rotate() { return !pre[4] && now[4]; }
  internal static bool Hold() { return !pre[5] && now[5]; }
}
