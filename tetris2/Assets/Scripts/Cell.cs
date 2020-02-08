using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour {
  public GameObject prfbCell;
  Blocks blocks;
  internal void Init(Controller c) { blocks = c.blocks; }
  SpriteRenderer Create(float x, float y, Color c) {
    SpriteRenderer s = Instantiate(prfbCell)
      .GetComponent<SpriteRenderer>();
    Vector2 pos = s.transform.position;
    pos.x = x; pos.y = y;
    s.transform.position = pos;
    s.color = c;
    return s;
  }
  internal SpriteRenderer Empty(float x, float y) {
    Color c = blocks.colors[blocks.empty];
    return Create(x, y, c);
  }
  internal SpriteRenderer Wall(float x, float y) {
    Color c = blocks.colors[blocks.wall];
    return Create(x, y, c);
  }
}
