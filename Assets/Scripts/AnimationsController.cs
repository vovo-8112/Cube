using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationsController : MonoBehaviour
{
    private void Awake()
    {
     GetComponent<Animation>().Play("ResetSceneButton", PlayMode.StopAll);
    }
}
