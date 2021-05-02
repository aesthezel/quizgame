using QuizLogic;
using UnityEngine;

public class SaveSlotSetup : MonoBehaviour
{
    public GameObject SaveSlotPrefab; 

    private void Start()
    {
        Instantiate(SaveSlotPrefab);

        for (int i = 0; i < SaveSystem.SAVE_SLOT_MAXIMUM; i++)
        {
            Instantiate(SaveSlotPrefab, transform);
        }
    }
}
