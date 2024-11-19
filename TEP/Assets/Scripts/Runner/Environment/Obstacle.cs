using UnityEngine;

public class Obstacle : MonoBehaviour
{

public GameObject onDestroyEffect;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

private void OnTriggerEnter(Collider other) {

  if(other.CompareTag("Player")){
    //cuando el jugador entre en esta Trigger zone, se va a destruir el collectible
  Destroy(gameObject);
//para que se produzca el efecto no solo hay que agregar el archivo, sino que además hay que Instanciarlo:
Instantiate(onDestroyEffect, transform.position, transform.rotation);
  }
   if(other.CompareTag("Player2")){
    //cuando el jugador entre en esta Trigger zone, se va a destruir el collectible
  Destroy(gameObject);
//para que se produzca el efecto no solo hay que agregar el archivo, sino que además hay que Instanciarlo:
Instantiate(onDestroyEffect, transform.position, transform.rotation);
  }
  
}

}

