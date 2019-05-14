using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour {
  int[,] board = new int[12, 25];
  class Block {
    int rotate; int x0, y0, x1, y1, x2, y2;
    internal Block(int r, int[] xy0, int[] xy1, int[] xy2) {
      rotate = r;
      x0 = xy0[0]; y0 = xy0[1];
      x1 = xy1[2]; y1 = xy1[3];
      x2 = xy2[4]; y2 = xy2[5];
    }
  }
  static int[] XY(int x, int y) { return new int[] { x, y }; }
  Block[] block = {
    new Block(1, XY(0, 0), XY(0, 0), XY(0, 0)), // null
    new Block(2, XY(0, -1), XY(0, 1), XY(0, 2)), // tetris
    new Block(4, XY(0, -1), XY(0, 1), XY(1, 1)), // L1
    new Block(4, XY(0, -1), XY(0, 1), XY(-1, 1)), // L2
    new Block(2, XY(0, -1), XY(1, 0), XY(1, 1)), // Key1
    new Block(2, XY(0, -1), XY(-1, 0), XY(-1, 1)), // Key2
    new Block(1, XY(0, 1), XY(1, 0), XY(1, 1)), // Square
    new Block(4, XY(0, -1), XY(1, 0), XY(-1, 0)), // T
  };
  class Status {
    int x, y, type, rotate;
  }
  Status current = new Status();

  void Start() {

  }

  void Update() {

  }
}

