using UnityEngine;

public class MarkForDestroy : MonoBehaviour
{
  // register events
  private void OnEnable()
  {
    GameManager.DispatchReloadGameEvent += DestroyMe;
    GameManager.DispatchEndGameEvent += DestroyMe;
    GameManager.DispatchBackToStartEvent += DestroyMe;
  }
  // deregister events
  private void OnDestroy()
  {
    GameManager.DispatchReloadGameEvent -= DestroyMe;
    GameManager.DispatchEndGameEvent -= DestroyMe;
    GameManager.DispatchBackToStartEvent -= DestroyMe;
  }
  // destroy this on event
  public void DestroyMe<T>(T e)
  {
    if (gameObject != null) Destroy(gameObject);
  }
}
