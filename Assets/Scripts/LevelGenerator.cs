using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelGenerator : MonoBehaviour
{
    private GameObject[] prefabs;
    void Awake()
    {
        Scene currentScene = SceneManager.GetSceneAt(1);
        float maxDamageChance = 0.9f;
        int gridSize = 20;
        int cellSize = 10;
        for (float z = -gridSize; z < gridSize; z += cellSize)
        {
            for (float x = -gridSize; x < gridSize; x += cellSize)
            {
                if (Random.Range(0.0f, 1.0f) < 0.2f)
                {
                    if (Random.Range(0.0f, 1.0f) < maxDamageChance)
                    {
                        GameObject go = Instantiate(Resources.Load("Prefabs/ground-damage")) as GameObject;
                        go.transform.position = new Vector3(x, 0, z);
                        SceneManager.MoveGameObjectToScene(go, SceneManager.GetSceneByName(currentScene.name));
                    }
                    else
                    {
                        GameObject go = Instantiate(Resources.Load("Prefabs/ground-heal")) as GameObject;
                        go.transform.position = new Vector3(x, 0, z);
                        SceneManager.MoveGameObjectToScene(go, SceneManager.GetSceneByName(currentScene.name));
                    }
                }
                if (Random.Range(0.0f, 1.0f) < 0.4f)
                {
                    if (Random.Range(0.0f, 1.0f) < maxDamageChance)
                    {
                        GameObject go = Instantiate(Resources.Load("Prefabs/xpath-damage")) as GameObject;
                        go.transform.position = new Vector3(x, 0, z);
                        SceneManager.MoveGameObjectToScene(go, SceneManager.GetSceneByName(currentScene.name));
                    }
                    else
                    {
                        GameObject go = Instantiate(Resources.Load("Prefabs/xpath-heal")) as GameObject;
                        go.transform.position = new Vector3(x, 0, z);
                        SceneManager.MoveGameObjectToScene(go, SceneManager.GetSceneByName(currentScene.name));
                    }
                }
                if (Random.Range(0.0f, 1.0f) < 0.7f)
                {
                    if (Random.Range(0.0f, 1.0f) < maxDamageChance)
                    {
                        GameObject go = Instantiate(Resources.Load("Prefabs/zpath-damage")) as GameObject;
                        go.transform.position = new Vector3(x, 0, z);
                        SceneManager.MoveGameObjectToScene(go, SceneManager.GetSceneByName(currentScene.name));
                    }
                    else
                    {
                        GameObject go = Instantiate(Resources.Load("Prefabs/zpath-heal")) as GameObject;
                        go.transform.position = new Vector3(x, 0, z);
                        SceneManager.MoveGameObjectToScene(go, SceneManager.GetSceneByName(currentScene.name));
                    }
                }
            }
        }
    }
}
