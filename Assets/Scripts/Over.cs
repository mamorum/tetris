using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Over : MonoBehaviour {
  public Text[] menu;
  int focusSize, size;
  FontStyle focusStyle, style;
  Color focusColor, color;
  int selected;
  Controller c;
  internal void Init(Controller ctrl) {
    c = ctrl;
    focusSize = menu[0].fontSize;
    focusStyle = menu[0].fontStyle;
    focusColor = menu[0].color;
    size = menu[1].fontSize;
    style = menu[1].fontStyle;
    color = menu[1].color;
  }
  internal void Enable() {
    gameObject.SetActive(true);
  }
  void Focus(int i) {
    selected = i;
    menu[i].fontSize = focusSize;
    menu[i].fontStyle = focusStyle;
    menu[i].color = focusColor;
  }
  void Unfocus(int i) {
    menu[i].fontSize = size;
    menu[i].fontStyle = style;
    menu[i].color = color;
  }
  void Restart() {
    gameObject.SetActive(false);
    c.Restart();
  }
  void Update() {
    if (Input.GetButton("Fire3")
      || Input.GetKey(KeyCode.Return)
    ) { // Space or 〇 || Enter
      if (selected == 0) Restart();
      else c.Quit();
    } else if (Input.GetButtonDown("Vertical")) {
      if (Input.GetAxisRaw("Vertical") == 1) { // Up
        Focus(0); Unfocus(1);
      } else if (Input.GetAxisRaw("Vertical") == -1) { // Down
        Focus(1); Unfocus(0);
      }
    }
  }
}
