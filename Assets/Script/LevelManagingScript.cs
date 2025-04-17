using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class LevelManagingScript : MonoBehaviour
{
    public static LevelManagingScript main;
    public Transform startPoint;
    public Transform[] path;
    private void Awake()
    {
        main = this;
    }
}
