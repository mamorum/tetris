using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grids : MonoBehaviour {
  public GameObject prfbCell;
  internal int[,] board = new int[12, 24];
  internal SpriteRenderer[,]
    main = new SpriteRenderer[12, 22],
    next = new SpriteRenderer[4, 10],
    hold = new SpriteRenderer[4, 2];
  List<GameObject> cells = new List<GameObject>();
  Controller c;
  internal void Init(Controller ctrl) {
    c = ctrl;
    //-> main
    float cX, cY;
    float baseX = -1.955f, baseY = -3.790f;
    for (int y = 0; y < 24; y++) {
      cY = baseY + (y * 0.355f);
      for (int x = 0; x < 12; x++) {
        cX = baseX + (x * 0.355f);
        if (y >= 22) { // not to display
          board[x, y] = Blocks.empty;
        } else if (y==0 || x==0 || x==11) {
          board[x, y] = Blocks.wall;
          main[x, y] = Wall(cX, cY);
        } else {
          board[x, y] = Blocks.empty;
          main[x, y] = Empty(cX, cY);
        }
      }
    }
    //-> next
    baseX = 2.66f; baseY = -0.595f;
    for (int y = 0; y < 10; y++) {
      cY = baseY + (y * 0.355f);
      for (int x = 0; x < 4; x++) {
        cX = baseX + (x * 0.355f);
        next[x, y] = Back(cX, cY);
      }
    }
    //-> hold
    baseX = -3.73f; baseY = 2.245f;
    for (int y = 0; y < 2; y++) {
      cY = baseY + (y * 0.355f);
      for (int x = 0; x < 4; x++) {
        cX = baseX + (x * 0.355f);
        hold[x, y] = Back(cX, cY);
      }
    }
  }
  SpriteRenderer Create(float x, float y, Color c) {
    GameObject g = Instantiate(prfbCell);
    cells.Add(g); // to destroy object later.
    SpriteRenderer s =
      g.GetComponent<SpriteRenderer>();
    Vector2 pos = s.transform.position;
    pos.x = x; pos.y = y;
    s.transform.position = pos;
    s.color = c;
    return s;
  }
  SpriteRenderer Back(float x, float y) {
    return Create(x, y, c.colors.back);
  }
  SpriteRenderer Empty(float x, float y) {
    return Create(x, y, c.colors.empty);
  }
  SpriteRenderer Wall(float x, float y) {
    return Create(x, y, c.colors.wall);
  }
}
