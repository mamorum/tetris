using UnityEngine;
using UnityEngine.UI;

public class Fps : MonoBehaviour {
  public Text txt;
  internal static readonly int rate = 60;
  internal void Init() {
    Application.targetFrameRate = rate;
    gameObject.SetActive(true);
  }
  int frameCount = 0;
  float elaspedSec = 0, fps = 0;
  void Update() {
    frameCount++;
    elaspedSec += Time.deltaTime;
    if (elaspedSec >= 1f) {
      fps = frameCount / elaspedSec;
      txt.text = string.Format(
        "{0:00}", fps
      ) + " fps";
      frameCount = 0;
      elaspedSec = 0;
    }
  }
}
