using UnityEngine;
using UnityEngine.UI;

public class Fps : MonoBehaviour {
  public Text txt;
  void Start() {
    Application.targetFrameRate = 60;
    gameObject.SetActive(true);
  }
  int frm = 0;
  float sec = 0, fps = 0;
  void Update() {
    frm++;
    sec += Time.deltaTime;
    if (sec >= 1f) {
      fps = frm / sec;
      txt.text = string.Format(
        "{0:00}", fps
      ) + " fps";
      frm = 0;
      sec = 0;
    }
  }
}
