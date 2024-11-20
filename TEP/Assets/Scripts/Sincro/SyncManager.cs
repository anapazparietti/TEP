using UnityEngine;
using System.Collections.Generic;
namespace Assets.Scripts.Sincro
{
public class SyncManager : MonoBehaviour
{
    [Header("Secuencias")]
    public List<string[]> sequences;

    private void Update()
    {
        foreach (var playerController in FindObjectsOfType<PlayerSyncController>())
        {
            if (playerController.IsSyncing() && Input.anyKeyDown)
            {
                foreach (KeyCode key in System.Enum.GetValues(typeof(KeyCode)))
                {
                    if (Input.GetKeyDown(key))
                    {
                        ProcessInput(playerController, key.ToString());
                        break;
                    }
                }
            }
        }
    }

    private void ProcessInput(PlayerSyncController playerController, string key)
    {
        string[] currentSequence = sequences[playerController.GetSequenceIndex()];

        if (playerController.IsCorrectStep(key, currentSequence))
        {
            playerController.AdvanceStep();
            if (playerController.IsSequenceComplete(currentSequence.Length))
            {
                playerController.OnSequenceComplete(true);
            }
        }
        else
        {
            playerController.OnSequenceComplete(false);
        }
    }
}
}