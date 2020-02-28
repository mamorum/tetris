using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grids : MonoBehaviour {
  internal int[,]
    main = new int[12, 23],
    next = new int[4, 10],
    hold = new int[4, 2];
  internal SpriteRenderer[,]
    mCells = new SpriteRenderer[12, 22],
    nCells = new SpriteRenderer[4, 10],
    hCells = new SpriteRenderer[4, 2];
  List<SpriteRenderer> walls = new List<SpriteRenderer>();
  public GameObject prfbCell; Blocks blocks;
  internal void Init(Controller c) {
    blocks = c.blocks;
    //-> main
    float posX, posY;
    float baseX = -1.955f, baseY = -3.790f;
    for (int y = 0; y < 23; y++) {
      if (y == 22) { //-> top (not displayed.)
        for (int x = 0; x < 12; x++) {
          main[x, y] = blocks.empty;
        }
        break;
      }
      posY = baseY + (y * 0.355f);
      for (int x = 0; x < 12; x++) {
        posX = baseX + (x * 0.355f);
        mCells[x, y] = Empty(posX, posY);
        if (x == 11 || x == 0 || y == 0) {
          walls.Add(Wall(posX, posY));
          main[x, y] = blocks.wall;
        } else if (y == 21) {
          walls.Add(Wall(posX, posY));
          main[x, y] = blocks.empty;
        } else {
          main[x, y] = blocks.empty;
        }
      }
    }
    //-> next
    baseX = 2.66f; baseY = -0.595f;
    for (int y = 0; y < 10; y++) {
      posY = baseY + (y * 0.355f);
      for (int x = 0; x < 4; x++) {
        posX = baseX + (x * 0.355f);
        nCells[x, y] = Empty(posX, posY);
      }
    }
    //-> hold
    baseX = -3.73f; baseY = 2.245f;
    for (int y = 0; y < 2; y++) {
      posY = baseY + (y * 0.355f);
      for (int x = 0; x < 4; x++) {
        posX = baseX + (x * 0.355f);
        hold[x, y] = blocks.empty;
        hCells[x, y] = Empty(posX, posY);
      }
    }
  }
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
    return Create(x, y, blocks.Empty());
  }
  internal SpriteRenderer Wall(float x, float y) {
    return Create(x, y, blocks.Wall());
  }
}
