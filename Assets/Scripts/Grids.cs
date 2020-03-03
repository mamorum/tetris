using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grids : MonoBehaviour {
  public GameObject prfbCell;
  internal int[,] board = new int[12, 23];
  internal SpriteRenderer[,]
    main = new SpriteRenderer[12, 22],
    next = new SpriteRenderer[4, 10],
    hold = new SpriteRenderer[4, 2];
  List<GameObject> cells = new List<GameObject>();
  Colors colors;
  internal void Init(Controller c) {
    colors = c.colors;
    //-> main
    float cX, cY;
    float baseX = -1.955f, baseY = -3.790f;
    for (int y = 0; y < 23; y++) {
      if (y == 22) { //-> top (not displayed.)
        for (int x = 0; x < 12; x++) {
          board[x, y] = Blocks.empty;
        }
        break;
      }
      cY = baseY + (y * 0.355f);
      for (int x = 0; x < 12; x++) {
        cX = baseX + (x * 0.355f);
        main[x, y] = Empty(cX, cY);
        if (x == 11 || x == 0 || y == 0) {
          CreateWall(cX, cY);
          board[x, y] = Blocks.wall;
        } else if (y == 21) {
          //CreateWall(cX, cY);
          board[x, y] = Blocks.empty;
        } else {
          board[x, y] = Blocks.empty;
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
    cells.Add(g); // to destroy later.
    SpriteRenderer s =
      g.GetComponent<SpriteRenderer>();
    Vector2 pos = s.transform.position;
    pos.x = x; pos.y = y;
    s.transform.position = pos;
    s.color = c;
    return s;
  }
  SpriteRenderer Empty(float x, float y) {
    return Create(x, y, colors.empty);
  }
  SpriteRenderer Back(float x, float y) {
    return Create(x, y, colors.back);
  }
  void CreateWall(float x, float y) {
    Create(x, y, colors.wall);
  }
}
