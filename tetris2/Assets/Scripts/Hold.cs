using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hold : MonoBehaviour {
  //-> cell
  static int[,] boardH = new int[4, 2];
  static SpriteRenderer[,] cells = new SpriteRenderer[4, 2];
  bool held = false;
  int hold; // id
  Blocks blocks; Cell cell;
  internal void Init(Controller c) {
    blocks = c.blocks; cell = c.cell;
    //-> cell
    float posX, posY;
    float baseX = -3.73f, baseY = 2.245f;
    for (int y = 0; y < 2; y++) {
      posY = baseY + (y * 0.355f);
      for (int x = 0; x < 4; x++) {
        posX = baseX + (x * 0.355f);
        cells[x, y] = cell.Empty(posX, posY);
      }
    }
  }
  internal int Add(int id) {
    int r = blocks.empty;
    if (held) {
      r = hold;
      hold = id;
    } else { // first time
      held = true;
      hold = id;
    }
    return r;
  }
  internal void Render() {
    //-> reset
    for (int y = 0; y < 2; y++) {
      for (int x = 0; x < 4; x++) {
        boardH[x, y] = 0;
      }
    }
    //-> put id
    int hX = 2, hY = 0;
    if (hold == blocks.i) { hX--; hY++; }
    boardH[hX, hY] = hold;
    int routate = blocks.DefaultRotate(hold);
    XY[] r = blocks.Relatives(hold, routate);
    for (int j = 0; j < r.Length; j++) {
      boardH[hX + r[j].x, hY + r[j].y] = hold;
    }
    //-> color
    for (int y = 0; y < 2; y++) {
      for (int x = 0; x < 4; x++) {
        int i = boardH[x, y];
        cells[x, y].color = blocks.colors[i];
      }
    }
  }
}
