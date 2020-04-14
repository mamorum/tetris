using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour {
  public Camera cam; public Colors colors;
  public Cells cells; public Score score;
  public End end;
  Board board = new Board();
  void Start() {
    colors.Init(this); cells.Init(this);
    board.Init(this); end.Init(this);
    board.Render();
  }
  internal bool
    ended = false, del = false;
  internal int frame = 0;
  int drop = 60, delete = 30;
  void Update() {
    if (ended) {
      end.Process();
      return;
    }
    frame++;
    if (del) Delete();
    else Process();
  }
  void Delete() {
    if (frame == delete) {
      board.Delete();
      board.Render();
    } else {
      board.Deleting();
    }
  }
  void Process() {
    HandleInput();
    if (frame >= drop) {
      board.Drop();
    }
    board.Render();
  }
  //-> user input
  readonly int
    left = 1, right = 2, rotate = 3, hld = 4, down = 5;
  int input = 0;
  internal bool insert = false;
  internal void HandleInput() {
    if (Input.GetAxisRaw("Horizontal") == -1) { // Left
      if (input == left) return;
      board.MoveBlock(-1, 0);
      input = left;
    } else if (Input.GetAxisRaw("Horizontal") == 1) { // Right
      if (input == right) return;
      board.MoveBlock(1, 0);
      input = right;
    } else if (Input.GetButton("Fire3")) { // Space or 〇
      if (input == rotate) return;
      board.RotateBlock();
      input = rotate;
    } else if (Input.GetButton("Jump")) { // H or △
      if (input == hld) return;
      board.Hold();
      input = hld;
    } else if (Input.GetAxisRaw("Vertical") == -1) { // Down
      if (insert && input == down) return;
      frame += drop / 2; // speed up
      insert = false;
      input = down;
    } else { // None
      input = 0;
    }
  }
}

