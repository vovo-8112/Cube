using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = System.Random;

public class SideController : MonoBehaviour {
  [SerializeField]
  private List<Side> _sides;

  [SerializeField]
  private Timer _timer;

  public int GetRandomNum() {
    List<int> nums = new List<int>();
    foreach (Side side in _sides) {
      nums.Add(side.num);
    }

    _timer.RestartTimer();
    Random r = new Random();
    return nums[r.Next(0, nums.Count)];
  }

  public bool IsGameOver() {
    var nums = _sides.Select(side => side.num).ToList();

    return nums.Count == nums.Distinct().Count();
  }
}