using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ready : MonoBehaviour {
  public Text txt;
  internal void Clear() {
    gameObject.SetActive(true);
    txt.text = "Ready..";
  }
  internal void Go() {
    txt.text = "Go!";
  }
  internal void Disable() {
    gameObject.SetActive(false);
  }
}
