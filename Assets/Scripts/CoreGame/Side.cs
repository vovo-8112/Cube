using System;
using System.Collections.Generic;
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

  private Side _otherSide;
  public int num { get; private set; }

  public StateSide state { get; private set; }

  private Dictionary<int, string> colors = new Dictionary<int, string>() {
    {2, "#ffbdb3"},
    {4, "#fe948d"},
    {8, "#fe7968"},
    {16, "#ec4f43"},
    {32, "#dfbc94"},
    {64, "#d4617e"},
    {128, "#b5628c"},
    {256, "#6c1343"},
    {512, "#e3dae7"},
    {1024, "#d3b3b8"},
    {2048, "#a0849d"},
    {4096, "#714d69"},
    {8192, "#917898"},
    {16384, "#4c394f"}
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
    Invoke(nameof(ShowGameObjectAfterRespawn), 0.01f);
  }

  private void SetStartPosition() {
    transform.position = _spawnPoint.position;
    transform.rotation = _spawnPoint.rotation;
    transform.localScale = Vector3.one;
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