using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Next {
  int[] original, queue1, queue2;
  int len, count = 0;
  internal void Init(int[] ids) {
    len = ids.Length;
    original = new int[len];
    queue1 = new int[len];
    queue2 = new int[len];
    Copy(ids, original);
    Shuffle(original);
    Copy(original, queue1);
    Shuffle(original);
    Copy(original, queue2);
  }
  void Copy(int[] from, int[] to) {
    for (int i = 0; i < from.Length; i++) {
      to[i] = from[i];
    }
  }
  int swap;
  void Shuffle(int[] target) {
    int j;
    for (int i = target.Length - 1; i > 0; i--) {
      j = Random.Range(0, i + 1);
      swap = target[i];
      target[i] = target[j];
      target[j] = swap;
    }
  }
  int Dequeue(int[] queue) {
    int next = queue[0];
    for (int i = 0; i < len-1; i++) {
      queue[i] = queue[i + 1];
    }
    return next;
  }
  void Enqueue(int i, int[] queue) {
    queue[len - 1] = i;
  }
  internal int Get() {
    int next1 = Dequeue(queue1);
    int next2 = Dequeue(queue2);
    Enqueue(next2, queue1);
    count++;
    if (len == count) {
      Shuffle(original);
      Copy(original, queue2);
      count = 0;
    }
    return next1;
  }
}
