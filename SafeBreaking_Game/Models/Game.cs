namespace SafeBreaking_Game.Models
{
    public class Game
    {
        public Handle[][] Handles { get; set; }
        public Handle PressedHandle { get; set; }
        public string Player { get; set; } = "";
        public int GameDifficulty { get; set; } = 4;        

        public virtual void SetStartGame()
        {                        
            var rnd = new Random();

            Handles = new Handle[GameDifficulty][];
            for (int row = 0; row < Handles.Length; row++)
            {
                Handles[row] = new Handle[GameDifficulty];
                for (int col = 0; col < Handles[row].Length; col++)
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

        public virtual void RevertHandles(Handle position) =>
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
