using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseManager : MonoBehaviour
{
  private void OnEnable()
  {
    PlayerController.DispatchPlayerHasFlagEvent += UpdateBaseColorGreen;
    PlayerController.DispatchPlayerDroppedFlagEvent += UpdateBaseColorRed;
  }
  private void OnDestroy()
  {
    PlayerController.DispatchPlayerHasFlagEvent -= UpdateBaseColorGreen;
    PlayerController.DispatchPlayerDroppedFlagEvent -= UpdateBaseColorRed;
  }

  private void UpdateBaseColorRed<T>(T e)
  {
    // Debug.Log("base: red");
    gameObject.GetComponent<Renderer>().material.SetColor("_Color", new Color(0.3f, 0f, 0.3f, 0.2f));
  }
  private void UpdateBaseColorGreen<T>(T e)
  {
    // Debug.Log("base: green");
    gameObject.GetComponent<Renderer>().material.SetColor("_Color", new Color(0f, 1f, 0.8f, 0.7f));
  }
}
