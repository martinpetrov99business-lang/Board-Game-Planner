using BoardGamePlanner.Services.Core.Contracts;
using BoardGamePlanner.ViewModels.Games;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Board_Game_Planner.Controllers
{
    [Authorize]
    public class GamesController : Controller
    {
        private readonly IGameService gameService;

        public GamesController(IGameService gameService)
        {
            this.gameService = gameService;
        }
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var model = await gameService.GetAllAsync(GetUserId());

            return View(model);
        }

        [AllowAnonymous]
        public async Task<IActionResult> Details(int id)
        {
            var model = await gameService.GetDetailsAsync(id, GetUserId());

            if (model == null)
            {
                return NotFound();
            }

            return View(model);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View(new GameFormViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(GameFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            string? userId = GetUserId();

            if (userId == null)
            {
                return Challenge();
            }

            int id = await gameService.CreateAsync(model, userId);

            return RedirectToAction(nameof(Details), new { id });
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            string? userId = GetUserId();

            if (userId == null || !await gameService.IsCreatorAsync(id, userId))
            {
                return Forbid();
            }

            var model = await gameService.GetFormAsync(id);

            if (model == null)
            {
                return NotFound();
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(GameFormViewModel model)
        {
            string? userId = GetUserId();

            if (userId == null || !await gameService.IsCreatorAsync(model.Id, userId))
            {
                return Forbid();
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            bool updated = await gameService.UpdateAsync(model);

            if (!updated)
            {
                return NotFound();
            }

            return RedirectToAction(nameof(Details), new { id = model.Id });
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            string? userId = GetUserId();

            if (userId == null || !await gameService.IsCreatorAsync(id, userId))
            {
                return Forbid();
            }

            var model = await gameService.GetDetailsAsync(id, userId);

            if (model == null)
            {
                return NotFound();
            }

            return View(model);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            string? userId = GetUserId();

            if (userId == null || !await gameService.IsCreatorAsync(id, userId))
            {
                return Forbid();
            }

            if (await gameService.IsInUseAsync(id))
            {
                TempData["Error"] = "This game is planned for a game night and cannot be deleted.";

                return RedirectToAction(nameof(Details), new { id });
            }

            bool deleted = await gameService.DeleteAsync(id);

            if (!deleted)
            {
                return NotFound();
            }

            return RedirectToAction(nameof(Index));
        }
        private string? GetUserId() => User.FindFirstValue(ClaimTypes.NameIdentifier);
    }
}