using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour {
  public Camera cam; public Colors colors;
  public Cells cells; public End end;
  public Ready ready; public Score score;
  Board board = new Board();
  internal bool wait, ended, del;
  internal int frame;
  int drop = 60, delete = 30;
  void Start() {
    ResetVariables();
    colors.Init(this); cells.Init(this);
    board.Init(this); end.Init(this);
    ready.Clear(); score.Clear();
    board.Render();
  }
  void ResetVariables() {
    wait = true; ended = false;
    del = false; frame = 0;
  }
  internal void Restart() {
    ResetVariables();
    ready.Clear();
    score.Clear();
    board.Reset();
    board.Render();
  }
  void Update() {
    if (ended) {
      end.Process();
      return;
    }
    frame++;
    if (wait) Wait();
    else if (del) Delete();
    else Process();
  }
  void Wait() {
    if (frame == 60) {
      ready.Go();
    } else if (frame == 90) {
      frame = 0;
      wait = false;
      ready.Disable();
      board.Drop();
    }
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
  internal void HandleInput() {
    if (Input.GetAxisRaw("Vertical") == -1) { // Down
      if (!board.insert) frame += drop / 2; // speed up
    } else {
      board.insert = false;
    }
    if (Input.GetButtonDown("Horizontal")) {
      if (Input.GetAxisRaw("Horizontal") == -1) { // Left
        board.MoveBlock(-1, 0);
      } else if (Input.GetAxisRaw("Horizontal") == 1) { // Right
        board.MoveBlock(1, 0);
      }
    }
    if (Input.GetButtonDown("Fire3")) { // Space or 〇
      board.RotateBlock();
    }
    if (Input.GetButtonDown("Jump")) { // H or △
      board.Hold();
    }
  }
}

