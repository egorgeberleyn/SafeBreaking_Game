using Microsoft.AspNetCore.Mvc;
using SafeBreaking_Game.Models;

namespace SafeBreaking_Game.Controllers
{
    public class GameController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly Game _game;

        public GameController(ILogger<HomeController> logger, Game game)
        {
            _logger = logger;
            _game = game;
        }

        public IActionResult Safe(int row, int column)
        {
            if(_game.Handles is null)
                _game.SetStartGame();
            else
            {
                _game.RevertHandles(new Handle { Row = row, Column = column });

                if (_game.CheckIsComplete())
                    return RedirectToAction("Success");
            }
            return View(_game);
        }

        [HttpGet]
        public IActionResult Settings() => View(_game);

        [HttpPost]
        public IActionResult Settings(Game gameSettings)
        {
            if(ModelState.IsValid)
            {
                _game.SaveSettings(gameSettings.Player, gameSettings.GameDifficulty);
                return RedirectToAction("Safe");
            }
            return View(_game);
        }

        public IActionResult Success()
        {
            _game.Handles = null;
            return View(_game);
        }            
    }
}
