using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grids : MonoBehaviour {
  public GameObject prfbCell;
  public Color empty, wall, i, o, s, z, j, l, t;
  internal int[,] board = new int[12, 23];
  internal SpriteRenderer[,]
    main = new SpriteRenderer[12, 22],
    next = new SpriteRenderer[4, 10],
    hold = new SpriteRenderer[4, 2];
  List<GameObject> cells = new List<GameObject>();
  Dictionary<int, Color> colors = new Dictionary<int, Color>();
  internal Color Color(int id) { return colors[id]; }
  void InitColors() {
    colors.Add(Blocks.empty, empty);
    colors.Add(Blocks.wall, wall); colors.Add(Blocks.i, i);
    colors.Add(Blocks.o, o); colors.Add(Blocks.s, s);
    colors.Add(Blocks.z, z); colors.Add(Blocks.j, j);
    colors.Add(Blocks.l, l); colors.Add(Blocks.t, t);
  }
  internal void Init() {
    InitColors();
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
        main[x, y] = Cell(cX, cY, empty);
        if (x == 11 || x == 0 || y == 0) {
          Cell(cX, cY, wall);
          board[x, y] = Blocks.wall;
        } else if (y == 21) {
          Cell(cX, cY, wall);
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
        next[x, y] = Cell(cX, cY, empty);
      }
    }
    //-> hold
    baseX = -3.73f; baseY = 2.245f;
    for (int y = 0; y < 2; y++) {
      cY = baseY + (y * 0.355f);
      for (int x = 0; x < 4; x++) {
        cX = baseX + (x * 0.355f);
        hold[x, y] = Cell(cX, cY, empty);
      }
    }
  }
  SpriteRenderer Cell(float x, float y, Color c) {
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
}
