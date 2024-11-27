using UnityEngine;
using UnityEngine.UI;

public class Mapa : MonoBehaviour
{
    [SerializeField] GameObject playerGo;
    [SerializeField] GameObject finishGo;

    Slider progressBar;
    float finishPos;
    float startPos;
    

    void Start()
    {

        progressBar= GetComponent<Slider>();
        finishPos = finishGo.transform.position.z;
        startPos = playerGo.transform.position.z;
        progressBar.value = Mathf.InverseLerp(startPos, finishPos, playerGo.transform.position.z);

    }

    void Update()
    {
        if(progressBar.value < 1)
        {
            progressBar.value = Mathf.InverseLerp(startPos,finishPos,playerGo.transform.position.z);
        }
    }
}