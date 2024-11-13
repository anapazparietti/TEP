// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.SceneManagement; //Agregamos esto para manejar escenas (pasar de pantalla en pantalla => ganaste-perdiste)
// using UnityEngine.UI; //Agregamos esto para manejar las propiedades UI (Canvas, Text, Image, etc).
// using TMPro; //Agregamos esto para utilizar las tipograf�as m�s avanzadas.


// public class SecuenciaPasosManager : MonoBehaviour
// {

//     public GameObject[] PasosTotales;
//     [SerializeField] public Configuracion_General config;

//     public int pasoActual;
//     public int pasoFinal;
//     public GameObject ObjetoDePasos;

//     private bool pasa;
//     private float timing;

//     private string teclaApretada;
//     private int cantOpciones;

//     public int cantCorrectas;

//     [Header("Configuraci�n de elementos UI")]
//     public TMP_Text timerUI; 



//     void Start()
//     {
//         pasa = false;
//         pasoActual = 0;
//         pasoFinal = ObjetoDePasos.transform.childCount;
//         cantCorrectas = 0;
//         timing= config.tiempo;
//         teclaApretada="Null";
//     }

    
//     void Update()
//     {
        
//         if (timing > 0) {                           // Evalúa que el tiempo no se termine
//             timing -= Time.deltaTime;               // Avanza el timer
//             timerUI.text=timing.ToString("f0");     // Reloj timer en pantalla


//         if (pasoActual < pasoFinal){               // Pasa por todos los pasos (esto lo determina la cantidad de hijos que tenga el gameobject "ObjetoDePasos")
//             PasosTotales[pasoActual].SetActive(true);
//  // DEBUG        print(teclaApretada); // Tecla Apretada

//         cantOpciones = PasosTotales[pasoActual].transform.childCount; // Esto nos indicará cuantas condiciones son necesarias cumplir
       
//            if (cantOpciones > cantCorrectas){
//             for (int i=0; i < cantOpciones; i++){         

//                 if ((teclaApretada == PasosTotales[pasoActual].transform.GetChild(i).GetComponent<Elemento>().nombre)){
//                     print ("correcto, la tecla apretada fue: "+teclaApretada+".");
                    
//                     cantCorrectas=cantCorrectas+1;
//                     teclaApretada="Null";               //¡Reinicio de botón!
//                 }       
//             }
//             } else if (cantOpciones == cantCorrectas){ // Se cumplieron todas las condiciones de este paso
//                     print("se completó el paso: "+pasoActual);
//                     cantCorrectas = 0;
//                     PasosTotales[pasoActual].SetActive(false);
//                     pasoActual= pasoActual+1;


//             }

//         } else if (pasoActual == pasoFinal){ // VICTORIA

//             print ("GANASTE!");
//             SceneManager.LoadScene(config.escenaganaste);

//         }        
//     }   
// }
// public void recibirTecla(string n) {
//         teclaApretada=n;
//         //return null;
//     }

// }