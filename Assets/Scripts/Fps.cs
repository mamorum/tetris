using UnityEngine;
using UnityEngine.UI;

public class Fps : MonoBehaviour {
  public Text txt;
  internal static readonly int rate = 60;
  internal void Init() {
    Application.targetFrameRate = rate;
    gameObject.SetActive(true);
  }
  int frame = 0;
  float sec = 0, fps = 0;
  void Update() {
    frame++;
    sec += Time.deltaTime;
    if (sec >= 1f) {
      fps = frame / sec;
      txt.text = string.Format(
        "{0:00}", fps
      ) + " fps";
      frame = 0;
      sec = 0;
    }
  }
}
