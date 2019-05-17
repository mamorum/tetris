using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Square : Cells {
  static Cell[] rotate0 = new Cell[] {
    new Cell(1, 0), new Cell(0, 1), new Cell(1, 1)
  };
  internal override Cell[] Get(int rotate) {
    return rotate0;
  }
  internal override int Rotate(int now) {
    return 0;
  }
}