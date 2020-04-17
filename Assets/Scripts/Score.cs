using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour {
  public Text txtLine, txtScore;
  int line, score;
  internal void Resets() {
    line = 0; score = 0;
    Render();
  }
  internal void Add(int lines) {
    line += lines;
    if (lines == 1) score += 40;
    else if (lines == 2) score += 100;
    else if (lines == 3) score += 300;
    else if (lines == 4) score += 1200;
    Render();
  }
  void Render() {
    txtLine.text = line.ToString();
    txtScore.text = score.ToString();
  }
}
