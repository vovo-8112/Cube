using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Side : MonoBehaviour {
  [SerializeField]
  private Vector3 _scaleVector3;

  [SerializeField]
  private GameObject _collider;

  [SerializeField]
  private TextMeshPro _textMeshPro;

  [SerializeField]
  private MeshRenderer _meshRenderer;

  private Vector3 _startPosition;
  private Vector3 _startScale;

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

  private void Awake() {
    GetStartVector();
    RotationCubeController.RotationText += RotateText;
  }

  private void Start() {
    SetColor(Int32.Parse(_textMeshPro.text));
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

  private void RotateText() {
    _textMeshPro.transform.LookAt(Vector3.zero);
  }

  private void GetStartVector() {
    Transform transform1 = transform;
    _startPosition = transform1.position;
    _startScale = transform1.localScale;
  }

  public void ResetSide() {
    Transform transform1 = transform;
    transform1.position = _startPosition;
    transform1.localScale = _startScale;
    _collider.gameObject.SetActive(false);
  }

  public void SetMergeMode(Vector3 _movePoint) {
    Transform transform1 = transform;
    transform1.localScale = _scaleVector3;
    transform1.SetParent(gameObject.transform.parent.parent);
    transform1.position = _movePoint;
    _collider.gameObject.SetActive(true);
  }

  private void OnDestroy() {
    RotationCubeController.RotationText -= RotateText;
  }
}