using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Over : MonoBehaviour {
  public Text title; Color tColor;
  Vector3 tCenter = new Vector3(0, 0, 0);
  Vector3 tUp = new Vector3(0, 74.5f, 0);
  public Text[] menu;
  Color[] mColor = new Color[2]; // 0:focus
  FontStyle[] mStyle = new FontStyle[2]; // 0:focus
  int[] mSize = new int[2]; //0:focus
  int focus; int frm; bool delay; Controller c;
  internal void Init(Controller ctrl) {
    c = ctrl;
    tColor = title.color;
    for (int i = 0; i < mColor.Length; i++) {
      mColor[i] = menu[i].color;
      mStyle[i] = menu[i].fontStyle;
      mSize[i] = menu[i].fontSize;
    }
  }
  internal void Enable() {
    frm = 0; delay = true;
    title.rectTransform.localPosition = tCenter;
    foreach (Text t in menu) {
      t.gameObject.SetActive(false);
    }
    gameObject.SetActive(true);
  }
  void Update() {
    if (delay) { Delay(); return; }
    //-> handle user input
    if (Key.Rotate()) {
      if (focus == 0) Restart();
      else c.Quit();
    }
    if (Key.Up()) Focus(0);
    else if (Key.Down()) Focus(1);
  }
  void Delay() {
    frm++;
    if (frm < 41) return;
    else if (frm < 90) TitleAlpha(tColor.a - 0.02f);
    else if (frm == 90) TitleAlpha(0f);
    else if (frm == 100) EndDelay();
  }
  void EndDelay() {
    delay = false;
    TitleAlpha(1f);
    title.rectTransform.localPosition = tUp;
    menu[0].gameObject.SetActive(true);
    menu[1].gameObject.SetActive(true);
  }
  void Restart() {
    gameObject.SetActive(false);
    c.Restart();
  }
  void TitleAlpha(float a) {
    tColor.a = a;
    title.color = tColor;
  }
  void Focus(int next) {
    if (focus == next) return;
    Font(menu[focus], 1); // UnFocus
    focus = next;
    Font(menu[focus], 0); // Focus
  }
  void Font(Text txt, int to) {
    txt.color = mColor[to];
    txt.fontStyle = mStyle[to];
    txt.fontSize = mSize[to];
  }
}
