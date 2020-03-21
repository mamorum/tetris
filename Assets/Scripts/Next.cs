using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Next {
  SpriteRenderer[,] cells;  
  Status[] queue1, queue2;
  int[] ids; int count = 0, swap;
  bool show = false;
  Controller c;
  internal void Init(Controller ct) {
    c = ct;
    cells = c.cells.next;
    //-> create block ids
    ids = new int[] {
      Blocks.i, Blocks.o, Blocks.s, Blocks.z,
      Blocks.j, Blocks.l, Blocks.t
    };
    //-> prepare queues
    CreateQueue();
    Shuffle(ids, queue1);
    Shuffle(ids, queue2);
  }
  void CreateQueue() {
    int len = ids.Length;
    queue1 = new Status[len];
    queue2 = new Status[len];
    for (int i = 0; i < len; i++) {
      queue1[i] = new Status();
      queue2[i] = new Status();
    }
  }
  void Shuffle(int[] from, Status[] to) {
    //-> shuffle
    for (int i, j = from.Length - 1; j > 0; j--) {
      i = Random.Range(0, j + 1);
      swap = from[j];
      from[j] = from[i];
      from[i] = swap;
    }
    //-> deep copy
    for (int i = 0; i < from.Length; i++) {
      to[i].id = from[i];
    }
  }
  int Dequeue(Status[] queue) {
    int next = queue[0].id;
    for (int i = 0; i < ids.Length - 1; i++) {
      queue[i].id = queue[i + 1].id;
    }
    return next;
  }
  void Enqueue(int i, Status[] queue) {
    queue[ids.Length - 1].id = i;
  }
  internal int Id() {
    if (show) Hide();
    else show = true;
    int next1 = Dequeue(queue1);
    int next2 = Dequeue(queue2);
    Enqueue(next2, queue1);
    Show();    
    count++;
    if (count == ids.Length) {
      Shuffle(ids, queue2);
      count = 0;
    }
    return next1;
  }
  void Hide() {
    Status s; XY[] r;
    int rx, ry;
    for (int j = 0; j < 3; j++) {
      s = queue1[j];
      cells[s.x, s.y].color = c.colors.back;
      r = Blocks.Relatives(s);
      for (int i = 0; i < r.Length; i++) {
        rx = s.x + r[i].x; ry = s.y + r[i].y;
        cells[rx, ry].color = c.colors.back;
      }
    }
  }
  void Show() {
    Status s; XY[] r;
    int rx, ry, ny = 8;
    for (int i = 0; i < 3; i++) {
      s = queue1[i];
      if (s.id == Blocks.o) s.x = 0;
      else s.x = 1;
      if (s.id == Blocks.i) ny++;
      s.y = ny;
      cells[s.x, s.y].color = c.colors.Get(s.id);
      Blocks.ResetRotate(s);
      r = Blocks.Relatives(s);
      for (int j = 0; j < r.Length; j++) {
        rx = s.x + r[j].x; ry = s.y + r[j].y;
        cells[rx, ry].color = c.colors.Get(s.id);
      }
      ny = ny - 4;
    }
  }
}
