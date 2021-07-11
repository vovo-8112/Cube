using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Side : MonoBehaviour {
  [SerializeField]
  private Vector3 _scaleVector3;

  [SerializeField]
  private Collider _collider;

  [SerializeField]
  private TextMeshPro _textMeshPro;

  [SerializeField]
  private MeshRenderer _meshRenderer;

  private int _num;

  private Vector3 _startPosition;
  private Vector3 _startScale;
  private Side _otherSide;

  public StateSide State;

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

  private void Awake() {
    SetText(2);
    RotationCubeController.RotationText += RotateText;
  }

  private void SetText(int val) {
    SetColor(val);
    _num = val;
    _textMeshPro.text = _num.ToString();
  }

  private void Start() {
    SetColor(_num);
    GetStartVector();
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
    transform1.localScale = _startScale;
    _collider.enabled = false;
  }

  public void Merge() {
    if (CanMerge()) {
      _otherSide._num *= 2;
      _otherSide.SetText(_otherSide._num);
      Destroy(gameObject);
    } else {
      ResetSide();
    }
  }

  private bool CanMerge() {
    if (_otherSide == null) {
      return false;
    }

    return _num == _otherSide._num;
  }

  public void SetMergeMode(Vector3 _movePoint) {
    State = StateSide.Merge;
    Transform transform1 = transform;
    transform1.localScale = _scaleVector3;
    transform1.Rotate(Vector3.zero);
    transform1.SetParent(gameObject.transform.parent.parent);
    transform1.position = _movePoint;
    _collider.enabled = true;
  }

  private void OnDestroy() {
    RotationCubeController.RotationText -= RotateText;
  }

  private void OnTriggerEnter(Collider other) {
    _otherSide = other.GetComponent<Side>();
  }
}