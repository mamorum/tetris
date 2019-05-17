using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Line : Cells {
  static Cell[] rotate0 = new Cell[] {
    new Cell(0, -1), new Cell(0, 1), new Cell(0, 2)
  };
  static Cell[] rotate1 = new Cell[] {
    new Cell(-1, 0), new Cell(1, 0), new Cell(2, 0)
  };
  static Cell[][] all = new Cell[][] {
    rotate0, rotate1
  };
  internal override Cell[] Get(int rotate) {
    return all[rotate];
  }
  internal override int Rotate(int now) {
    if (now == 0) return 1;
    else return 0;
  }
}
