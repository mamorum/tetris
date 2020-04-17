using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ready : MonoBehaviour {
  public Text txt;
  Controller c; int frm; string ready;
  internal void Init(Controller ctrl) {
    c = ctrl; ready = txt.text;
  }
  internal void Enable() {
    frm = 0;
    txt.text = ready;
    gameObject.SetActive(true);
  }
  void Update() {
    frm++;
    if (frm == 60) {
      txt.text = "Go!";
    } else if (frm == 90) {
      gameObject.SetActive(false);
      c.board.gameObject.SetActive(true);
      c.board.Drop();
    }
  }
}
