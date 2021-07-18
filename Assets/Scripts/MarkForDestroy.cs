using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarkForDestroy : MonoBehaviour
{
    private void OnEnable()
    {
        GameManager.DispatchRestartGameEvent += DestroyMe;
    }
    public void DestroyMe<T>(T e)
    {
        Destroy(gameObject);
    }
}
