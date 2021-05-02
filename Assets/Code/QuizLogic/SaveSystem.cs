using UnityEngine;

namespace QuizLogic
{
    public class SaveSystem : MonoBehaviour
    {
        public static readonly string QUIZ_GAME_KEY = "NIVELQUIZ";
        public static readonly int SAVE_SLOT_MAXIMUM = 3;

        public SaveGame _SaveGame;

        public int ActualSaveSlot = -1;

        public void SetActualSlot(int position)
        {
            ActualSaveSlot = position;
        }

        public SaveSlot GetSaveSlot( int position )
        {
            if (position > _SaveGame.SaveSlots.Length - 1)
            {
                Debug.Log("No se pudo conseguir el SaveSlot con el index " + position);
                return null;
            }

            return _SaveGame.SaveSlots[position];
        }

        public void SetSaveGameToPrefs()
        {
            string slotsToJson = JsonUtility.ToJson(_SaveGame);
            Debug.Log(slotsToJson);
            PlayerPrefs.SetString(QUIZ_GAME_KEY, slotsToJson);
        }

        public void GetSaveGameFromPrefs()
        {
            if(!PlayerPrefs.HasKey(QUIZ_GAME_KEY))
            {
                SaveSlot[] SaveSlots = new SaveSlot[3];
                SetSaveGameToPrefs();
            } 
            else 
            {
                string slotFromJson = PlayerPrefs.GetString(QUIZ_GAME_KEY);
                _SaveGame = JsonUtility.FromJson<SaveGame>(slotFromJson);
            }
        }

        [System.Serializable]
        public class SaveGame
        {
            public SaveSlot[] SaveSlots;
        }
    }
}


