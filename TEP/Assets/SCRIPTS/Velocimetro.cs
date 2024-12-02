using UnityEngine;
using UnityEngine.UI;


public class Velocimetro : MonoBehaviour
{
    public GameObject player;
    public float Max_speed = 300f;
    public float MinSpeedArrowAngle;
    public float MaxSpeedArrowAngle;
    public Runner runner;
 
    [Header("UO")]

    public RectTransform Arrow;
    public float speed = 0.0f;

    void Start()
    {
        runner = player.GetComponent<Runner>();
    }   
    void Update()
    { 
        speed = runner.moveSpeed; 

        if(Arrow != null)
        {
            Arrow.localEulerAngles = new Vector3(0,0, Mathf.Lerp(MinSpeedArrowAngle, MaxSpeedArrowAngle, speed / Max_speed));
        }
    }
}
