using System.Collections.Generic;

namespace QuizLogic
{
    [System.Serializable]
    public class SaveSlot
    {
        public string PlayerName;
        public List<Medal> Medals = new List<Medal>();

        public void AddMedal( Medal medal ) 
        {
            Medals.Add(medal);
        }

        public void EditPlayerName( string newName )
        {
            PlayerName = newName;
        }
    }
}
