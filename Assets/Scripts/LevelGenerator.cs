using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelGenerator : MonoBehaviour
{
    private GameObject[] prefabs;

    // generate the level on awake
    void Awake()
    {
        GenerateTiles();

    }
    // create the level 
    public void GenerateTiles()
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
                        GenerateTile("Prefabs/ground-heal", x, z, currentScene);
                    }
                    else
                    {
                        GenerateTile("Prefabs/ground-damage", x, z, currentScene);
                    }
                }
                else if (Random.Range(0.0f, 1.0f) < 0.4f)
                {
                    if (Random.Range(0.0f, 1.0f) < maxDamageChance)
                    {
                        GenerateTile("Prefabs/xpath-damage", x, z, currentScene);
                    }
                    else
                    {
                        GenerateTile("Prefabs/xpath-heal", x, z, currentScene);
                    }
                }
                else if (Random.Range(0.0f, 1.0f) < 0.7f)
                {
                    if (Random.Range(0.0f, 1.0f) < maxDamageChance)
                    {
                        GenerateTile("Prefabs/zpath-heal", x, z, currentScene);
                    }
                    else
                    {
                        GenerateTile("Prefabs/zpath-damage", x, z, currentScene);
                    }
                }
            }
        }
    }
    // instantiate a tile
    private void GenerateTile(string tile, float x, float z, Scene scene)
    {
        GameObject go = Instantiate(Resources.Load(tile)) as GameObject;
        go.transform.position = new Vector3(x, 0, z);
        SceneManager.MoveGameObjectToScene(go, SceneManager.GetSceneByName(scene.name));
    }
}
