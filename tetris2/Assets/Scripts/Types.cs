using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Types {
  internal readonly static int
    empty = 0, wall = 9,
    i = 1, o = 2, s = 3, z = 4, j = 5, l = 6, t = 7;
  static Type[] types = new Type[] {
    new TypeI(), new TypeO(), new TypeS(),
    new TypeZ(), new TypeJ(), new TypeL(),
    new TypeT()
  };
  static int index = types.Length - 1;
  internal static Type Get() {
    index++;
    if (index == types.Length) {
      Shuffle();
      index = 0;
    }
    return types[index];
  }
  static Type swap;
  static void Shuffle() {
    int j;
    for (int i = types.Length - 1; i > 0; i--) {
      j = Random.Range(0, i + 1);
      swap = types[i];
      types[i] = types[j];
      types[j] = swap;
    }
  }
}
