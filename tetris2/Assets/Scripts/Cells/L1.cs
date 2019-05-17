using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L1 : Cells {
  static Cell[] rotate0 = new Cell[] {
    new Cell(0, -1), new Cell(0, 1), new Cell(1, 1)
  };
  static Cell[] rotate1 = new Cell[] {
    new Cell(1, -1), new Cell(1, 0), new Cell(-1, 0)
  };
  static Cell[] rotate2 = new Cell[] {
    new Cell(-1, -1), new Cell(0, -1), new Cell(0, 1)
  };
  static Cell[] rotate3 = new Cell[] {
    new Cell(-1, 0), new Cell(1, 0), new Cell(-1, 1)
  };
  static Cell[][] all = new Cell[][] {
    rotate0, rotate1, rotate2, rotate3
  };
  internal override Cell[] Get(int rotate) {
    return all[rotate];
  }
}