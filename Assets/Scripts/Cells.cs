using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cells : MonoBehaviour {
  public GameObject prfbCell;
  internal Cell[,]
    hold = new Cell[4, 2],
    next = new Cell[4, 10],
    main = new Cell[12, 24];
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
    lenX = main.GetLength(0);
    lenY = main.GetLength(1);
    for (int y = 0; y < lenY; y++) {
      posY = baseY + (y * distance);
      for (int x = 0; x < lenX; x++) {
        posX = baseX + (x * distance);
        if (y >= 22) { // not visible.
          main[x, y] = new Cell(Blocks.empty, null, null);
        } else if (y==0 || x==0 || x==11) {
          main[x, y] = Wall(posX, posY);
        } else {
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
  Cell Back(float x, float y) {
    return Create(Blocks.empty, x, y, c.colors.back);
  }
  Cell Empty(float x, float y) {
    return Create(Blocks.empty, x, y, c.colors.empty);
  }
  Cell Wall(float x, float y) {
    return Create(Blocks.wall, x, y, c.colors.wall);
  }
  Cell Create(int id, float x, float y, Color clr) {
    GameObject g = Instantiate(prfbCell);
    cells.Add(g); // to destroy object later.
    SpriteRenderer s =
      g.GetComponent<SpriteRenderer>();
    s.color = clr;
    Vector2 pos = s.transform.position;
    pos.x = x; pos.y = y;
    s.transform.position = pos;
    return new Cell(id, s, c.colors);
  }
  internal void Disable() {
    foreach (GameObject o in cells) {
      Destroy(o);
    }
  }
}
