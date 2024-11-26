using UnityEngine;
using UnityEngine.UI;


public class Velocimetro : MonoBehaviour
{
    public Rigidbody MiDino;
    public float Max_speed = 200f;
    public float MinSpeedArrowAngle;
    public float MaxSpeedArrowAngle;

    [Header("UO")]

    public RectTransform Arrow;
    public float speed = 0.0f;

    void Update()
    {
        speed = MiDino.linearVelocity.magnitude * 3.6f;

        if(Arrow != null)
        {
            Arrow.localEulerAngles = new Vector3(0,0, Mathf.Lerp(MinSpeedArrowAngle, MaxSpeedArrowAngle, speed / Max_speed));
        }
    }
}
