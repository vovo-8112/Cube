using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class Side : MonoBehaviour {
  [SerializeField]
  private Collider _collider;

  [SerializeField]
  private TextMeshPro _textMeshPro;

  [SerializeField]
  private MeshRenderer _meshRenderer;

  [SerializeField]
  private Transform _spawnPoint;

  [SerializeField]
  private TakeAnimation _takeAnimation;

  [SerializeField]
  private ShuffleAnimation _shuffleAnimation;

  private Side _otherSide;
  public int num { get; private set; }

  public StateSide state { get; private set; }

  private Dictionary<int, string> colors = new Dictionary<int, string>() {
    {2, "#eee4da"},
    {4, "#ede0c8"},
    {8, "#f2b179"},
    {16, "#f59563"},
    {32, "#f67c5f"},
    {64, "#f95c30"},
    {128, "#edce68"},
    {256, "#eecd57"},
    {512, "#eec943"},
    {1024, "#eec62c"},
    {2048, "#eec308"}
  };

  public enum StateSide {
    Default,
    Merge
  }

  public void TakeAnimation() {
    _takeAnimation.AnimationRotation();
  }

  public void TakeAnimationStop() {
    _takeAnimation.StopAnimation();
  }

  public void MergeDenied() {
    SetStartPosition();
    _collider.enabled = false;
    state = StateSide.Default;
  }

  public void Merge(int value) {
    if (CanMerge()) {
      _otherSide.num *= 2;
      _otherSide.SetText(_otherSide.num);
      Respawn(value);
    }
  }

  public bool CanMerge() {
    if (_otherSide == null) {
      return false;
    }

    return num == _otherSide.num;
  }

  public Sequence SetStartShuffle(int val) {
    var seq = _shuffleAnimation.ShuffleAnim();
    num = val;
    seq.OnStepComplete(SetText);
    return seq;
  }

  public void UpSideValue(int value) {
    num *= value;
    SetText(num);
  }

  private void SetText() {
    SetColor(num);
    _textMeshPro.text = num.ToString();
  }

  public void SetMergeMode(Vector3 _movePoint) {
    state = StateSide.Merge;
    Transform transform1 = transform;
    transform1.Rotate(Vector3.zero);
    transform1.SetParent(gameObject.transform.parent.parent);
    transform1.position = _movePoint;
    _collider.enabled = true;
  }

  private void Awake() {
    SetText(2);
    RotationCubeController.RotationText += RotateText;
  }

  private void SetText(int val) {
    SetColor(val);
    num = val;
    _textMeshPro.text = num.ToString();
  }

  private void Start() {
    SetColor(num);
  }

  private void SetColor(int val) {
    Color col = new Color();
    ColorUtility.TryParseHtmlString("#edeae6", out col);
    string colStr = "#000000";
    if (val <= 2048)
      colStr = colors[val];
    ColorUtility.TryParseHtmlString(colStr, out col);

    _meshRenderer.material.color = col;
  }

  private void Respawn(int value) {
    gameObject.SetActive(false);
    SetText(value);
    SetStartPosition();
    _collider.enabled = false;
    state = StateSide.Default;
    Invoke(nameof(ShowGameObjectAfterRespawn), 0.01f);
  }

  private void SetStartPosition() {
    var transform1 = transform;
    transform1.position = _spawnPoint.position;
    transform1.rotation = _spawnPoint.rotation;
    transform1.localScale = Vector3.one;
  }

  private void ShowGameObjectAfterRespawn() {
    gameObject.SetActive(true);
  }

  private void RotateText() {
    _textMeshPro.transform.LookAt(Vector3.zero);
  }

  private void OnDestroy() {
    RotationCubeController.RotationText -= RotateText;
  }

  private void OnTriggerEnter(Collider other) {
    _otherSide = other.GetComponent<Side>();
  }
}