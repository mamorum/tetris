using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour {
  public GameObject prfbBlock;
  internal SpriteRenderer Cell() {
    return Instantiate(prfbBlock)
      .GetComponent<SpriteRenderer>();
  }
  Board board = new Board();
  Key key = new Key();
  void Start() {
    board.Init(this);
    key.Init(this, board);
  }
  static int wait = 60;
  int fWait = wait;
  void Update() {
    key.Process();
    fWait--;
    if (fWait <= 0) {
      board.Drop();
      fWait = wait;
    }
    board.Render();
  }
  internal void SpeedUp() {
    fWait -= wait / 2;
  }
}

