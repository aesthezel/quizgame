using UnityEngine;
using TMPro;
using QuizLogic;

public class SaveSlotHandler : MonoBehaviour
{
    public SaveSlot _SaveSlot;
    public TMP_Text PlayerNameText;
    public TMP_Text MedalsText;

    void Start()
    {
        CheckSlotInfo();
    }

    void CheckSlotInfo()
    {
        if(_SaveSlot.PlayerName == "")
        {
            return;
        }
        else
        {
            PlayerNameText.text = _SaveSlot.PlayerName;
        }

        if(_SaveSlot.Medals.Count == 0)
        {
            return;
        }
        {
           MedalsText.text = _SaveSlot.Medals.Count.ToString(); 
        }
    }

    public void SetSaveSlot( SaveSlot saveSlot )
    {
        _SaveSlot = saveSlot;
    }
}
