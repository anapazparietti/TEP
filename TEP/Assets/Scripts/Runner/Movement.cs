using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
   public static float leftSide;
   public static float rightSide;
   public float internalLeft;
   public float internalRight;

    
    // Update is called once per frame
    void Update()
    {
        internalLeft = leftSide;
        internalRight = rightSide;
    }
}
