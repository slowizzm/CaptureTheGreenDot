using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GI_CGD : MonoBehaviour
{
    public static int lives = 3;
    public static int level;

    public static GI_CGD Instance;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        Instance = this;
    }

}