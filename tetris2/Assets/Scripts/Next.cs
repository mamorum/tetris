using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Next : MonoBehaviour {
  //-> cell
  static int[,] nexts = new int[5, 19];
  static SpriteRenderer[,] queues = new SpriteRenderer[5, 19];
  //-> block ids and queues
  int[] ids, queue1, queue2;    
  int count = 0, swap;
  Cell cell; Blocks blocks;
  internal void Init(Controller c, float baseY, float posX) {
    cell = c.cell; blocks = c.blocks;
    //-> cell
    float posY;
    float baseX = posX + 0.355f;
    for (int y = 0; y < 19; y++) {
      posY = baseY + (y * 0.355f);
      for (int x = 0; x < 5; x++) {
        posX = baseX + (x * 0.355f);
        queues[x, y] = cell.Empty(posX, posY);
      }
    }
    //-> ids, queues
    ids = new int[] {
      blocks.i, blocks.o, blocks.s, blocks.z,
      blocks.j, blocks.l, blocks.t
    };
    queue1 = new int[ids.Length];
    queue2 = new int[ids.Length];
    Shuffle(queue1);
    Shuffle(queue2);
  }
  void Shuffle(int[] queue) {
    //-> shuffle
    for (int i, j = ids.Length - 1; j > 0; j--) {
      i = Random.Range(0, j + 1);
      swap = ids[j];
      ids[j] = ids[i];
      ids[i] = swap;
    }
    //-> deep copy
    for (int i = 0; i < ids.Length; i++) {
      queue[i] = ids[i];
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
      Shuffle(queue2);
      count = 0;
    }
    return next1;
  }
  internal void Render() {
    //-> reset
    for (int y = 0; y < 19; y++) {
      for (int x = 0; x < 5; x++) {
        nexts[x, y] = 0;
      }
    }
    //-> id
    int nid, nrt, nx, ny = 17;
    for (int i = 0; i < 3; i++) {
      nid = queue1[i];
      nrt = blocks.DefaultRotate(nid);
      if (nid == blocks.i) ny++;
      if (nid == blocks.o) nx = 1;
      else nx = 2;
      nexts[nx, ny] = nid;
      XY[] r = blocks.Relatives(nid, nrt);
      for (int j = 0; j < r.Length; j++) {
        nexts[nx + r[j].x, ny + r[j].y] = nid;
      }
      ny = ny - 4;
    }
    //-> color
    for (int y = 0; y < 19; y++) {
      for (int x = 0; x < 5; x++) {
        int i = nexts[x, y];
        queues[x, y].color = blocks.colors[i];
      }
    }
  }
}
