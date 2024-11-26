using UnityEngine;
public class SincroState : MonoBehaviour
{
    public string[] syncSequence = { "W", "A", "S", "D" }; // Secuencia de teclas.
    private int currentIndex = 0;
    private PlayerStateManager stateManager;

    void Start()
    {

    }
    void Awake()
    {
        stateManager = GetComponent<PlayerStateManager>();
    }


     void Update()
     {
        
        if (Input.GetButtonDown(syncSequence[currentIndex]))
         {
            
            Debug.Log("entre");
           currentIndex++;
             if (currentIndex >= syncSequence.Length)
            {
               CompleteSequence();
          }
       }
      else if (Input.anyKeyDown)
        {
           currentIndex = 0; // Reinicia la secuencia si se presiona la tecla incorrecta.
           Debug.Log("Tecla incorrecta. Secuencia reiniciada.");
       }
   }

    private void CompleteSequence()
    {
     Debug.Log("Secuencia completada. Cambiando a Runner.");
     stateManager.SwitchToRunner();
   //esto es importante pra finalizarla
    }

}