using Microsoft.AspNetCore.Mvc;
using SafeBreaking_Game.Models;

namespace SafeBreaking_Game.Controllers
{
    public class GameController : Controller
    {
        private readonly ILogger<HomeController> logger;
        private readonly Game game;

        public GameController(ILogger<HomeController> logger, Game game)
        {
            this.logger = logger;
            this.game = game;
        }

        public IActionResult Safe(int row, int column)
        {
            if(game.Handles == null)
                game.SetStartGame();
            else
            {
                game.ChangePositions(new Handle { Row = row, Column = column });

                if (game.GameCompletionCheck())
                    return RedirectToAction("Success");
            }
            return View(game);
        }

        [HttpGet]
        public IActionResult Settings() => View();

        [HttpPost]
        public IActionResult Settings(Game gameSettings)
        {
            if(ModelState.IsValid)
            {
                game.SaveSettings(gameSettings.Player, gameSettings.GameDifficulty);
                return RedirectToAction("Safe");
            }
            return View(game);
        }

    }
}
