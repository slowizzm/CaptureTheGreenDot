using UnityEngine;
using System.Collections;
public class Oscilate : MonoBehaviour
{
  public enum OscilationFunction { xAxis, zAxis }
  public bool bIsZAxis;
  public float spd;
  public void Start()
  {
    if (!bIsZAxis)
    {
      StartCoroutine(Oscillate(OscilationFunction.xAxis, spd));
    }
    else
    {
      StartCoroutine(Oscillate(OscilationFunction.zAxis, spd));
    }
  }

  private IEnumerator Oscillate(OscilationFunction method, float scalar)
  {
    while (true)
    {
      if (method == OscilationFunction.xAxis)
      {
        transform.localPosition = new Vector3(Mathf.Sin(Time.time) * scalar, transform.localPosition.y, transform.localPosition.z);
      }
      else if (method == OscilationFunction.zAxis)
      {
        transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, Mathf.Sin(Time.time) * scalar);
      }
      yield return new WaitForEndOfFrame();
    }
  }
}