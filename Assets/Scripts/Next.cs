using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Next {
  int[,] grid; SpriteRenderer[,] cells;
  int[] ids, queue1, queue2;    
  int count = 0, swap;
  Blocks blocks;
  internal void Init(Controller c) {
    blocks = c.blocks;
    grid = c.grids.next;
    cells = c.grids.nCells;
    //-> create block ids
    ids = new int[] {
      blocks.i, blocks.o, blocks.s, blocks.z,
      blocks.j, blocks.l, blocks.t
    };
    //-> create queues
    queue1 = new int[ids.Length];
    queue2 = new int[ids.Length];
    Shuffle(ids, queue1);
    Shuffle(ids, queue2);
  }
  void Shuffle(int[] from, int[] to) {
    //-> shuffle
    for (int i, j = from.Length - 1; j > 0; j--) {
      i = Random.Range(0, j + 1);
      swap = from[j];
      from[j] = from[i];
      from[i] = swap;
    }
    //-> deep copy
    for (int i = 0; i < from.Length; i++) {
      to[i] = from[i];
    }
  }
  int Dequeue(int[] queue) {
    int next = queue[0];
    for (int i = 0; i < ids.Length - 1; i++) {
      queue[i] = queue[i + 1];
    }
    return next;
  }
  void Enqueue(int i, int[] queue) {
    queue[ids.Length - 1] = i;
  }
  internal int Id() {
    int next1 = Dequeue(queue1);
    int next2 = Dequeue(queue2);
    Enqueue(next2, queue1);
    count++;
    if (count == ids.Length) {
      Shuffle(ids, queue2);
      count = 0;
    }
    return next1;
  }
  internal void Render() {
    //-> reset
    for (int y = 0; y < 10; y++) {
      for (int x = 0; x < 4; x++) {
        grid[x, y] = 0;
      }
    }
    //-> id
    int nid, nrt, nx, ny = 8;
    for (int i = 0; i < 3; i++) {
      nid = queue1[i];
      nrt = blocks.DefaultRotate(nid);
      if (nid == blocks.i) ny++;
      if (nid == blocks.o) nx = 0;
      else nx = 1;
      grid[nx, ny] = nid;
      XY[] r = blocks.Relatives(nid, nrt);
      for (int j = 0; j < r.Length; j++) {
        grid[nx + r[j].x, ny + r[j].y] = nid;
      }
      ny = ny - 4;
    }
    //-> color
    for (int y = 0; y < 10; y++) {
      for (int x = 0; x < 4; x++) {
        int i = grid[x, y];
        cells[x, y].color = blocks.colors[i];
      }
    }
  }
}
