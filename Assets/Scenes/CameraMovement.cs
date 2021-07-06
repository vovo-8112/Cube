using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CameraMovement : MonoBehaviour {
  [SerializeField]
  private Camera cam;
  [SerializeField]
  private Transform target;
  [SerializeField]
  private float distanceToTarget = 10;

  [SerializeField]
  private List<Transform> _pointsToMove;

  [SerializeField]
  private List<Vector3> _rotationCameraList;

  private Vector3 previousPosition;

  private Vector3 _rotationVector;

  private Vector3 _nextPosition;

  private void Update() {
    if (Input.GetMouseButtonDown(0)) {
      previousPosition = cam.ScreenToViewportPoint(Input.mousePosition);
    } else if (Input.GetMouseButton(0)) {
      Vector3 newPosition = cam.ScreenToViewportPoint(Input.mousePosition);
      Vector3 direction = previousPosition - newPosition;

      float rotationAroundYAxis = -direction.x * 180; // camera moves horizontally
      float rotationAroundXAxis = direction.y * 180; // camera moves vertically

      cam.transform.position = target.position;

      cam.transform.Rotate(new Vector3(1, 0, 0), rotationAroundXAxis);
      cam.transform.Rotate(new Vector3(0, 1, 0), rotationAroundYAxis, Space.World);

      cam.transform.Translate(new Vector3(0, 0, -distanceToTarget));

      previousPosition = newPosition;
    }

    if (Input.GetMouseButtonUp(0)) {
      FindPoint();
      transform.position = _nextPosition;
      SetCameraPos();
    }
  }

  private void SetCameraPos() {
    Transform transform1 = cam.transform;
    Quaternion transformRotation = transform1.rotation;
    var Vec = new Vector3(SetRotation(transformRotation.eulerAngles.x),
      SetRotation(transformRotation.eulerAngles.y), SetRotation(transformRotation.eulerAngles.z));
    transform1.eulerAngles = Vec;
  }

  private void FindPoint() {
    float distanceToClosestEnemy = Mathf.Infinity;
    Transform point = null;

    foreach (Transform currentEnemy in _pointsToMove) {
      float distanceToEnemy = (currentEnemy.transform.position - transform.position).sqrMagnitude;
      if (distanceToEnemy < distanceToClosestEnemy) {
        distanceToClosestEnemy = distanceToEnemy;
        point = currentEnemy;
        _nextPosition = point.position;
      }
    }

    if (point is { }) Debug.DrawLine(transform.position, point.transform.position);
  }

  private float SetRotation(float value) {
    var dec = value % 90;
    float res = 0;
    if (dec > 45) {
      res = value + dec;
    } else {
      res = value - dec;
    }

    return res;
  }
}