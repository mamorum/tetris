using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour {
  public Camera cam;
  public Colors colors;
  public Grids grids;
  internal Next next = new Next();
  internal Hold hold = new Hold();
  Main main = new Main();
  void Start() {
    colors.Init(this); grids.Init(this);
    next.Init(this); hold.Init(this);
    main.Init(this); main.Render();
  }
  internal bool
    end = false, del = false;
  internal int frame = 0;
  int drop = 60;
  void Update() {
    if (end) return;
    frame++;
    if (del) {
      if (frame == 60) main.Delete();
      else main.Deleting();
      return;
    }
    ProcessInput();
    if (frame >= drop) {
      main.Drop();
      frame = 0;
    }
    main.Render();
  }
  //-> user input
  readonly int
    left = 1, right = 2, rotate = 3, hld = 4, down = 5;
  int preInput = 0;
  internal bool dropped = false;
  internal void ProcessInput() {
    if (Input.GetAxisRaw("Horizontal") == -1) { // Left
      if (preInput == left) return;
      main.MoveBlock(-1, 0);
      preInput = left;
    } else if (Input.GetAxisRaw("Horizontal") == 1) { // Right
      if (preInput == right) return;
      main.MoveBlock(1, 0);
      preInput = right;
    } else if (Input.GetButton("Fire3")) { // Space or 〇
      if (preInput == rotate) return;
      main.RotateBlock();
      preInput = rotate;
    } else if (Input.GetButton("Jump")) { // H or △
      if (preInput == hld) return;
      main.Hold();
      preInput = hld;
    } else if (Input.GetAxisRaw("Vertical") == -1) { // Down
      if (dropped && preInput == down) return;
      frame += drop / 2; // speed up
      dropped = false;
      preInput = down;
    } else { // None
      preInput = 0;
    }
  }
}

