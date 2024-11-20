using System.Runtime.CompilerServices;
using UnityEngine;

public class Obstacle : MonoBehaviour
{

public GameObject onDestroyEffect;
private Player player1;
private Player2 player2;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player1 = GetComponent<Player>();
        player2 = GetComponent<Player2>();

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
Debug.Log("el player1 choco con un objeto");
  }
   if(other.CompareTag("Player2")){
    //cuando el jugador entre en esta Trigger zone, se va a destruir el collectible
  Destroy(gameObject);
//para que se produzca el efecto no solo hay que agregar el archivo, sino que además hay que Instanciarlo:
Instantiate(onDestroyEffect, transform.position, transform.rotation);
Debug.Log("el player2 choco con un objeto");


  }
  
}

}

