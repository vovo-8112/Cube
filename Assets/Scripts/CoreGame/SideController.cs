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
    List<int> nums = Nums();
    _timer.RestartTimer();
    Random r = new Random();
    return nums[r.Next(0, nums.Count)];
  }

  public bool IsGameOver() {
    List<int> nums = Nums();
    return nums.Count == nums.Distinct().Count();
  }

  private List<int> Nums() {
    var nums = _sides.Select(side => side.num).ToList();
    return nums;
  }
}