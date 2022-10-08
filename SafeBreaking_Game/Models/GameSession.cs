using Newtonsoft.Json;
using SafeBreaking_Game.Extensions;

namespace SafeBreaking_Game.Models
{
    public class GameSession : Game
    {
        [JsonIgnore]
        public ISession Session { get; set; }

        public static Game GetGameSession(IServiceProvider service)
        {
            ISession session = service.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;
            GameSession game = session?.GetJson<GameSession>("Game")
            ?? new GameSession();
            game.Session = session;
            return game;
        }

        public override void SetStartGame()
        {
            base.SetStartGame();
            Session.SetJson("Game", this);
        }

        public override void TurnHandles(Handle position)
        {
            base.TurnHandles(position);
            Session.SetJson("Game", this);
        }

        public override void SaveSettings(string playerName, int difficulty)
        {
            base.SaveSettings(playerName, difficulty);
            Session.SetJson("Game", this);
        }
    }
}
