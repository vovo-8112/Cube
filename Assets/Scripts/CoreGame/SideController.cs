using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using Random = System.Random;

public class SideController : MonoBehaviour {
  [SerializeField]
  private List<Side> _sides;

  [SerializeField]
  private Transform _targetPoint;

  [SerializeField]
  private Button _shuffleButton;

  private InputController _inputController;
  private IProgressSaver _progressSaver;

  [Inject]
  public void Consturct(InputController inputController, IProgressSaver progressSaver) {
    _inputController = inputController;
    _progressSaver = progressSaver;
  }

  public void UpSides(int value) {
    foreach (var side in _sides) {
      side.UpSideValue(value);
    }
  }

  public int GetRandomNum() {
    List<int> nums = Nums(_sides);
    return RandomNum(nums);
  }

  public bool IsGameOver() {
    List<int> nums = Nums(_sides);
    _progressSaver.SaveSide(nums);
    return nums.Count == nums.Distinct().Count();
  }

  private List<int> Nums(List<Side> sides) {
    var nums = sides.Select(side => side.num).ToList();
    return nums;
  }

  private void Start() {
    _shuffleButton.onClick.AddListener(ShuffleButtonClick);
    LoadSideDate();
  }

  private void LoadSideDate() {
    List<int> sidesDate = _progressSaver.LoadSideDate();
    if (sidesDate == null) {
      return;
    }

    for (var i = 0; i < _sides.Count; i++) {
      _sides[i].SetStartShuffle(sidesDate[i]);
    }
  }

  private void ShuffleButtonClick() {
    CalculateShuffleSide();
  }

  private void CalculateShuffleSide() {
    var currentSide = FindClosestSide();
    List<Side> listSide = new List<Side>(_sides);
    listSide.Remove(currentSide);
    List<int> nums = Nums(listSide);

    _inputController.LockInput();
    var anim = currentSide.SetStartShuffle(RandomNum(nums));
    anim.OnComplete(() => _inputController.EnableInput());
  }

  private int RandomNum(List<int> nums) {
    Random r = new Random();
    return nums[r.Next(0, nums.Count)];
  }

  private Side FindClosestSide() {
    float distance = Mathf.Infinity;
    Side currentSide = null;
    Vector3 position = _targetPoint.position;
    foreach (Side go in _sides) {
      Vector3 diff = go.transform.position - position;
      float curDistance = diff.sqrMagnitude;
      if (!(curDistance < distance)) continue;
      currentSide = go;
      distance = curDistance;
    }

    return currentSide;
  }
}