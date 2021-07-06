using UnityEngine;

public class CameraControl : MonoBehaviour {
  public GameObject yAxis;
  public GameObject xAxis;
  public GameObject camera;

  public int cameraSpeed = 100;

  // Use this for initialization
  void Start() {
    yAxis.transform.eulerAngles = new Vector3(0, 270f, 0);
    xAxis.transform.eulerAngles = new Vector3(90, 270f, 0);
    cameraSpeed = 100;
  }

  // Update is called once per frame
  void Update() {
    if (Input.GetKey("w") || Input.GetKey("up")) {
      xAxis.transform.Rotate(Vector3.right * cameraSpeed / 100);
    }

    if (Input.GetKey("s") || Input.GetKey("down")) {
      xAxis.transform.Rotate(Vector3.left * cameraSpeed / 100);
    }

    if (Input.GetKey("a") || Input.GetKey("left")) {
      yAxis.transform.Rotate(Vector3.up * cameraSpeed / 100);
    }

    if (Input.GetKey("d") || Input.GetKey("right")) {
      yAxis.transform.Rotate(Vector3.down * cameraSpeed / 100);
    }
  }
}