using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key {
  static bool press; // temp
  static bool[] pre = {
    false, false, false, false
  }; // input -> 0:left, 1:right, 2:up, 3:down
  internal static bool Rotate() {
    return Input.GetButtonDown("Fire3")
      || Input.GetKeyDown(KeyCode.Return);
  }
  internal static bool Hold() {
    return Input.GetButtonDown("Jump");
  }
  static bool Press(int i) {
    if (press && pre[i]) return false;
    if (press) pre[i] = true;
    else pre[i] = false;
    return pre[i];
  }
  internal static bool Left() {
    press = Input.GetAxisRaw("Horizontal") == -1;
    return Press(0);
  }
  internal static bool Right() {
    press = Input.GetAxisRaw("Horizontal") == 1;
    return Press(1);
  }
  internal static bool Up() {
    press = Input.GetAxisRaw("Vertical") == 1;
    return Press(2);
  }
  internal static bool Down() {
    press = Input.GetAxisRaw("Vertical") == -1;
    return Press(3);
  }
  internal static bool PressingDown() {
    press = Input.GetAxisRaw("Vertical") == -1;
    if (press && pre[3]) return true;
    if (press) {
      pre[3] = true;
      return false;
    }
    pre[3] = false;
    return false;
  }
}
