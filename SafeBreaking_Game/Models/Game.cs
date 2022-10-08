using System.ComponentModel.DataAnnotations;

namespace SafeBreaking_Game.Models
{
    public class Game
    {
        public Handle[][] Handles { get; set; }

        [StringLength(15)]
        public string Player { get; set; } = string.Empty;

        public int GameDifficulty { get; set; } = 4;        

        public virtual void SetStartGame()
        {                        
            var rnd = new Random();

            Handles = new Handle[GameDifficulty][];
            for (var row = 0; row < Handles.Length; row++)
            {
                Handles[row] = new Handle[GameDifficulty];
                for (var col = 0; col < Handles[row].Length; col++)
                {
                    Handles[row][col] = new Handle
                    {
                        IsVertical = Convert.ToBoolean(rnd.Next(0, 2)),
                        Column = col,
                        Row = row
                    };
                }
            }                                    
        }

        public virtual void TurnHandles(Handle position) =>
            Handles.SelectMany(arr => arr)
                   .Where(h => h.Column == position.Column || h.Row == position.Row)
                   .ToList()
                   .ForEach(h => Handles[h.Row][h.Column].IsVertical = !h.IsVertical);


        public virtual void SaveSettings(string playerName, int difficulty)
        {
            Player = playerName;
            GameDifficulty = difficulty;
            Handles = null;
        }

        public bool CheckIsComplete() =>       
            Handles.All(arr => arr.All(el => el.IsVertical is true)) ||
            Handles.All(arr => arr.All(el => el.IsVertical is false));       
    }    
}
