using Microsoft.AspNetCore.Mvc;
using SafeBreaking_Game.Models;

namespace SafeBreaking_Game.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly Game _game;

        public HomeController(ILogger<HomeController> logger, Game game)
        {
            _logger = logger;
            _game = game;
        }

        public IActionResult Index() => View(_game);

        public IActionResult Rules() => View(_game);            
    }
}