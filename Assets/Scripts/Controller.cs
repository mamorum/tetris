using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour {
  public Camera cam; public Canvas canvas;
  public Colors colors; public Cells cells;
  public Board board; public Score score;
  public Over over; public Ready ready;  
  void Start() {
    colors.Init(this); cells.Init(this);
    board.Init(this); over.Init(this);
    ready.Init(this); score.Resets();
    board.Render(); ready.Enable();
  }
  internal void Restart() {
    board.Resets(); score.Resets();
    board.Render(); ready.Enable();
  }
  internal void Quit() {
    cells.Disable();
    canvas.gameObject.SetActive(false);
    #if UNITY_EDITOR
      UnityEditor.EditorApplication.isPlaying = false;
    #elif UNITY_STANDALONE
      UnityEngine.Application.Quit();
    #endif
  }
}

