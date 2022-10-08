using Microsoft.AspNetCore.Mvc;
using SafeBreaking_Game.Models;

namespace SafeBreaking_Game.Controllers
{
    public class GameController : Controller
    {
        private readonly Game _game;

        public GameController(Game game) => _game = game;
        
        public IActionResult Safe(int row, int column)
        {
            if(_game.Handles is null)
                _game.SetStartGame();
            else
            {
                _game.TurnHandles(new Handle { Row = row, Column = column });

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
            if (!ModelState.IsValid) return View(_game);
            _game.SaveSettings(gameSettings.Player, gameSettings.GameDifficulty);
            return RedirectToAction("Safe");
        }

        public IActionResult Success()
        {
            _game.SetStartGame();
            return View(_game);
        }            
    }
}
