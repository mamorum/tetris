using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cells : MonoBehaviour {
  public GameObject prfbCell;
  internal SpriteRenderer[,]
    hold = new SpriteRenderer[4, 2],
    next = new SpriteRenderer[4, 10],
    main = new SpriteRenderer[12, 22];
  internal int[,] grid = new int[12, 24]; //-> for main
  List<GameObject> cells = new List<GameObject>();
  Controller c;
  internal void Init(Controller ctrl) {
    c = ctrl;
    //-> init grid and cells
    const float
      left = -3.73f, bottom = -3.790f,
      distance = 0.355f;
    float baseX, baseY, posX, posY;
    int lenY, lenX;
    //-> init hold: left
    baseX = left;
    baseY = bottom + (17 * distance);
    lenX = hold.GetLength(0);
    lenY = hold.GetLength(1);
    for (int y = 0; y < lenY; y++) {
      posY = baseY + (y * distance);
      for (int x = 0; x < lenX; x++) {
        posX = baseX + (x * distance);
        hold[x, y] = Back(posX, posY);
      }
    }
    //-> init main: center
    lenX++; // add a cell space from hold.
    baseX = baseX + (lenX * distance);
    baseY = bottom;
    lenX = grid.GetLength(0);
    lenY = grid.GetLength(1);
    for (int y = 0; y < lenY; y++) {
      posY = baseY + (y * distance);
      for (int x = 0; x < lenX; x++) {
        posX = baseX + (x * distance);
        if (y >= 22) { // not visible.
          grid[x, y] = Blocks.empty;
        } else if (y==0 || x==0 || x==11) {
          grid[x, y] = Blocks.wall;
          main[x, y] = Wall(posX, posY);
        } else {
          grid[x, y] = Blocks.empty;
          main[x, y] = Empty(posX, posY);
        }
      }
    }
    //-> init next: right
    lenX++; // add a cell space from center.
    baseX = baseX + (lenX * distance);
    baseY = bottom + (9 * distance);
    lenX = next.GetLength(0);
    lenY = next.GetLength(1);
    for (int y = 0; y < lenY; y++) {
      posY = baseY + (y * distance);
      for (int x = 0; x < lenX; x++) {
        posX = baseX + (x * distance);
        next[x, y] = Back(posX, posY);
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
