using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class FlagManager : MonoBehaviour
{
    public Flag flag;
    public PlayerController player;
    [HideInInspector] public bool bIsHeld = false;
    public static FlagManager Instance { get; private set; }
    GameObject[] flagStart;
    public static event Action<FlagManager> DispatchSuicideLevelEvent = delegate { };


    private void Awake()
    {
        if (Instance == null) Instance = this;
    }
    void Start()
    {
        SetFlagStartPos();
    }
    private void Update()
    {
        // if player is has flag then update pos of flag to player pos x,z and up off the ground
        if (bIsHeld)
        {
            if (player != null) UpdateFlagPos(new Vector3(player.transform.position.x, 5, player.transform.position.z));
        }
    }
    public void SetFlagStartPos()
    {

        // get array of flag starts
        flagStart = GameObject.FindGameObjectsWithTag("flagStart");
        // Debug.Log(flagStart);
        // make sure a flag is set
        if (flagStart.Length <= 0 || flagStart == null)
        {
            DispatchSuicideLevelEvent(this);
            return;
        }

        // get random flag start and set pos
        int newIndex = UnityEngine.Random.Range(0, flagStart.Length);
        Vector3 startPosition = flagStart[newIndex].transform.position;

        // set flag pos
        UpdateFlagPos(startPosition);
    }
    public void UpdateFlagPos(Vector3 pos)
    {
        flag.transform.position = new Vector3(pos.x, pos.y, pos.z);
    }
}