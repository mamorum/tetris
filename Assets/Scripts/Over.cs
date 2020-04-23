using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Over : MonoBehaviour {
  public Text title; public Text[] menu;
  Color titleColor; Color focusColor, color;
  FontStyle focusStyle, style; int focusSize, size;
  int selected; int frm; Controller c;
  internal void Init(Controller ctrl) {
    c = ctrl;
    titleColor = title.color;
    focusSize = menu[0].fontSize;
    focusStyle = menu[0].fontStyle;
    focusColor = menu[0].color;
    size = menu[1].fontSize;
    style = menu[1].fontStyle;
    color = menu[1].color;
  }
  void Delay() {
    frm++;
    if (frm < 40) return;
    if (frm < 90) {
      titleColor.a = titleColor.a - 0.02f;
      title.color = titleColor;
    } else if (frm == 90) {
      titleColor.a = 0;
      title.color = titleColor;
    } else if (frm == 100) {
      titleColor.a = 1;
      title.color = titleColor;
      title.rectTransform.localPosition = up;
      menu[0].gameObject.SetActive(true);
      menu[1].gameObject.SetActive(true);
    }
  }
  void Update() {
    if (frm <= 100) {
      Delay();
    } else {
      if (Key.Rotate()) {
        if (selected == 0) Restart();
        else c.Quit();
      }
      if (Key.Up()) {
        Focus(0); Unfocus(1);
      } else if (Key.Down()) {
        Focus(1); Unfocus(0);
      }
    }
  }
  Vector3 center = new Vector3(0, 0, 0);
  Vector3 up = new Vector3(0, 74.5f, 0);
  internal void Enable() {
    frm = 0;
    title.rectTransform.localPosition = center;
    menu[0].gameObject.SetActive(false);
    menu[1].gameObject.SetActive(false);
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
}
