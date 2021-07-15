using System;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class SideSet : MonoBehaviour {
  [SerializeField]
  private List<Side> _sides;

  public int GetRandomNum() {
    List<int> nums = new List<int>();
    foreach (Side side in _sides) {
      nums.Add(side._num);
    }

    Random r = new Random();
    return nums[r.Next(0, nums.Count)];
  }
}