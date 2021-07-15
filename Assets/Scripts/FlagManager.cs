using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagManager : MonoBehaviour {

    public GameObject flagPrefab;
    GameObject[] flagStart;

    public void SpawnFlag() {

        // find all flag starts
        flagStart = GameObject.FindGameObjectsWithTag("flagStart");

        // get random flag start and set spawn pos
        int newIndex = Random.Range(0, flagStart.Length);
        Vector3 spawnPosition = flagStart[newIndex].transform.position;

        // spawn flag
        GameObject flag = Instantiate(flagPrefab, spawnPosition, Quaternion.identity);
    }
}