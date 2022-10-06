namespace SafeBreaking_Game.Models
{
    public class Game
    {
        public Handle[][] Handles { get; set; }
        public Handle PressedHandle { get; set; }
        public string Player { get; set; }
        public int GameDifficulty { get; set; }
        

        public virtual void SetStartGame()
        {
            if (GameDifficulty == 0)
                GameDifficulty = 4;
            
            var rnd = new Random();
            
            var points = new Handle[GameDifficulty][];
            for (int row = 0; row < points.Length; row++)
            {
                points[row] = new Handle[GameDifficulty];
                for (int col = 0; col < points[row].Length; col++)
                {
                    points[row][col] = new Handle
                    {
                        IsVertical = Convert.ToBoolean(rnd.Next(0, 2)),
                        Column = col,
                        Row = row
                    };
                }
            }
            
            Handles = points;            
        }

        public virtual void ChangePositions(Handle position)
        {
            var point = Handles[position.Row][position.Column];
            for (int row = 0; row < Handles.Length; row++)
            {
                if (row == point.Row)
                    for (int col = 0; col < Handles[row].Length; col++)
                        Handles[row][col].IsVertical = !Handles[row][col].IsVertical;
                else
                    for (int col = 0; col < Handles[row].Length; col++)
                    {
                        if (col == point.Column)
                            Handles[row][col].IsVertical = !Handles[row][col].IsVertical;                                
                    }
            }            
        }
        
        public virtual void SaveSettings(string playerName, int difficulty)
        {
            Player = playerName;
            GameDifficulty = difficulty;
            Handles = null;
        }

        public bool GameCompletionCheck() =>        
            Handles.All(arr => arr.All(el => el.IsVertical is true)) ||
            Handles.All(arr => arr.All(el => el.IsVertical is false));        
    }
}
