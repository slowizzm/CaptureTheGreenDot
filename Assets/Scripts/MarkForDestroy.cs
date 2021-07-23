using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarkForDestroy : MonoBehaviour
{
    private void OnEnable()
    {
        GameManager.DispatchReloadGameEvent += DestroyMe;
    }
    private void OnDestroy()
    {
        GameManager.DispatchReloadGameEvent -= DestroyMe;
    }
    public void DestroyMe<T>(T e)
    {
        if (gameObject != null) Destroy(gameObject);
    }
}
