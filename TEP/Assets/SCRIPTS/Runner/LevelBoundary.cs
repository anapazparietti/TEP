using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//--------LÍMITES DE LA PISTA----
public class LevelBoundary : MonoBehaviour
{
    public static float leftSide = -3;
    public static float rightSide = 3;
    public float internalLeft;
    public float internalRight;

    void Update()
    {
        internalLeft = leftSide;
        internalRight = rightSide;
        
    }
}
