using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour {
  public GameObject prfbCell;
  internal SpriteRenderer Create(float x, float y, Color c) {
    SpriteRenderer s = Instantiate(prfbCell)
      .GetComponent<SpriteRenderer>();
    Vector2 pos = s.transform.position;
    pos.x = x; pos.y = y;
    s.transform.position = pos;
    s.color = c;
    return s;
  }
}
