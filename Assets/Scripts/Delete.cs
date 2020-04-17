using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Delete : MonoBehaviour {
  Controller c; Board b;
  int frm; int delete = 30;
  List<int> lines = new List<int>();
  internal void Init(Controller ct) {
    c = ct; b = c.board; frm = 0;
  }
  void Update() {
    frm++;
    if (frm == delete) Complete();
    else Deleting();
  }
  void Deleting() {
    foreach (int y in lines) {
      for (int x = b.minX; x < b.maxX; x++) {
        b.cells[x, y].AddAlpha(-0.03f);
      }
    }
  }
  void Complete() {
    int line = lines.Count;
    for (int i = 0; i < line; i++) {
      for (int y = lines[i] - i; y < b.maxY; y++) {
        for (int x = b.minX; x < b.maxX; x++) {
          b.cells[x, y].id = b.cells[x, y + 1].id;
        }
      }
    }
    c.score.Add(line);
    lines.Clear();
    gameObject.SetActive(false);
    b.gameObject.SetActive(true);
    b.NextBlock();
  }
  internal void All() {
    for (int y = b.minY; y < b.maxY; y++) {
      for (int x = b.minX; x < b.maxX; x++) {
        b.cells[x, y].id = Blocks.empty;
      }
    }
  }
  internal void Check() {
    for (int y = b.minY; y < b.maxY; y++) {
      for (int x = b.minX; x < b.maxX; x++) {
        if (b.cells[x, y].id == Blocks.empty) break;
        if (x == 10) lines.Add(y);
      }
    }
    if (lines.Count == 0) {
      b.NextBlock();
    } else {
      frm = 0;
      gameObject.SetActive(true);
      b.gameObject.SetActive(false);
    }
  }
}
