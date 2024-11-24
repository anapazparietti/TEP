using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//--------LÍMITES DE LA PISTA----
public class LevelBoundary : MonoBehaviour
{
    public static float leftSide = -5;
    public static float rightSide = 5;
    public float internalLeft;
    public float internalRight;

    void Update()
    {
        internalLeft = leftSide;
        internalRight = rightSide;
        
    }
}
