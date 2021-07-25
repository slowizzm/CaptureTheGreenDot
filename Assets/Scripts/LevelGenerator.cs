using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelGenerator : MonoBehaviour
{
  // level components
  private GameObject[] prefabs;

  // generate the level on awake
  void Awake()
  {
    GenerateTiles();

  }
  // create the level - 10print style
  public void GenerateTiles()
  {
    // get current scene/level
    Scene currentScene = SceneManager.GetSceneAt(1);
    // grid size
    int gridSize = 20;
    int cellSize = 10;

    // prefab paths
    string[] tilePaths = { "Prefabs/ground-heal", "Prefabs/ground-damage", "Prefabs/xpath-damage", "Prefabs/xpath-heal", "Prefabs/zpath-heal", "Prefabs/zpath-damage", "Prefabs/xpath-pushers", "Prefabs/zpath-pushers" };

    // create grid
    for (float z = -gridSize; z < gridSize; z += cellSize)
    {
      // create sqr
      for (float x = -gridSize; x < gridSize; x += cellSize)
      {
        GenerateTile(tilePaths[(int)Random.Range(0f, tilePaths.Length)], x, z, currentScene);
      }
    }
  }
  // instantiate a tile
  private void GenerateTile(string tile, float x, float z, Scene scene)
  {
    GameObject go = Instantiate(Resources.Load(tile)) as GameObject;
    go.transform.position = new Vector3(x, 0, z);
    // make sure objects are in level
    SceneManager.MoveGameObjectToScene(go, SceneManager.GetSceneByName(scene.name));
    // set levels empty as parent
    go.transform.parent = gameObject.transform;
  }
}
