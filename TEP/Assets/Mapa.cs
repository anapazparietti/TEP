using UnityEngine;
using UnityEngine.UI;

public class Mapa : MonoBehaviour
{
    [SerializeField] GameObject playerGo;
    [SerializeField] GameObject finishGo;

    Image progressBar;
    float maxDistance;
    

    void Start()
    {
        progressBar= GetComponent<Image>();
        maxDistance = finishGo.transform.position.z;

        progressBar.fillAmount = playerGo.transform.position.z / maxDistance;
        
    }

    void Update()
    {
        if(progressBar.fillAmount < 1)
        {
            progressBar.fillAmount = playerGo.transform.position.z / maxDistance;
        }
    }
}
