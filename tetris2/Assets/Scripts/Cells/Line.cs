using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Line : Cells {
  Cell[] rotate0 = new Cell[] {
    new Cell(0, -1), new Cell(0, 1), new Cell(0, 2)
  };
  Cell[] rotate1 = new Cell[] {
    new Cell(-1, 0), new Cell(1, 0), new Cell(2, 0)
  };
}
