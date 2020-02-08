using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Next : MonoBehaviour {
  static int[] drops = Blocks.drops;
  static int
    len = drops.Length,
    count = 0, swap;
  static int[]
    queue1 = new int[len],
    queue2 = new int[len];
  internal void Init() {
    ShuffleTo(queue1);
    ShuffleTo(queue2);
  }
  void ShuffleTo(int[] queue) {
    //-> shuffle
    for (int i, j = drops.Length - 1; j > 0; j--) {
      i = Random.Range(0, j + 1);
      swap = drops[j];
      drops[j] = drops[i];
      drops[i] = swap;
    }
    //-> deep copy
    for (int i = 0; i < drops.Length; i++) {
      queue[i] = drops[i];
    }
  }
  int Dequeue(int[] queue) {
    int next = queue[0];
    for (int i = 0; i < len - 1; i++) {
      queue[i] = queue[i + 1];
    }
    return next;
  }
  void Enqueue(int i, int[] queue) {
    queue[len - 1] = i;
  }
  internal int Id() {
    int next1 = Dequeue(queue1);
    int next2 = Dequeue(queue2);
    Enqueue(next2, queue1);
    count++;
    if (len == count) {
      ShuffleTo(queue2);
      count = 0;
    }
    return next1;
  }
  internal int[] Queue() {
    return queue1;
  }
}
