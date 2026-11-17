
using BoardGamePlanner.Services.Core.Contracts;
using BoardGamePlanner.ViewModels.GameNights;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Board_Game_Planner.Controllers
{
    [Authorize]
    public class GameNightsController : Controller
    {
        private readonly IGameNightService gameNightService;

        public GameNightsController(IGameNightService gameNightService)
        {
            this.gameNightService = gameNightService;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var model = await gameNightService.GetAllAsync(GetUserId());

            return View(model);
        }

        [AllowAnonymous]
        public async Task<IActionResult> Details(int id)
        {
            var model = await gameNightService.GetDetailsAsync(id, GetUserId());

            if (model == null)
            {
                return NotFound();
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var model = await gameNightService.GetCreateModelAsync();

            return View(model);
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(GameNightFormViewModel model)
        {
            if (!await gameNightService.FitsVenueAsync(model.VenueId, model.MaxGuests))
            {
                ModelState.AddModelError(nameof(model.MaxGuests),
                    "Max guests cannot exceed the venue's capacity.");
            }

            if (!ModelState.IsValid)
            {
                var fresh = await gameNightService.GetCreateModelAsync();
                model.Venues = fresh.Venues;
                return View(model);
            }

            string? userId = GetUserId();

            if (userId == null)
            {
                return Challenge();
            }

            int id = await gameNightService.CreateAsync(model, userId);
            return RedirectToAction(nameof(Details), new { id });
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            string? userId = GetUserId();

            if (userId == null || !await gameNightService.IsCreatorAsync(id, userId))
            {
                return Forbid();
            }
            var model = await gameNightService.GetEditModelAsync(id);

            if (model == null)
            {
                return NotFound();
            }

            return View(model);
        }

     
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(GameNightFormViewModel model)

        {
            string? userId = GetUserId();

            if (userId == null || !await gameNightService.IsCreatorAsync(model.Id, userId))
            {
                return Forbid();
            }

            if (!await gameNightService.FitsVenueAsync(model.VenueId, model.MaxGuests))
            {
                ModelState.AddModelError(nameof(model.MaxGuests),
                    "Max guests cannot exceed the venue's capacity.");
            }

            if (!ModelState.IsValid)
            {
                var fresh = await gameNightService.GetCreateModelAsync();
                model.Venues = fresh.Venues;
                return View(model);
            }

            bool updated = await gameNightService.UpdateAsync(model);

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

            if (userId == null || !await gameNightService.IsCreatorAsync(id, userId))
            {
                return Forbid();
            }

            var model = await gameNightService.GetDetailsAsync(id, null);

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

            if (userId == null || !await gameNightService.IsCreatorAsync(id, userId))
            {
                return Forbid();
            }

            bool deleted = await gameNightService.DeleteAsync(id);

            if (!deleted)
            {
                return NotFound();
            }

            return RedirectToAction(nameof(Index));
        }
        private string? GetUserId() => User.FindFirstValue(ClaimTypes.NameIdentifier);

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Join(int id, bool fromList = false)
        {
            string? userId = GetUserId();

            if (userId == null)
            {
                return Challenge();
            }

            bool joined = await gameNightService.JoinAsync(id, userId);

            if (joined)
            {
                TempData["Success"] = "You joined this game night.";
            }
            else
            {
                TempData["Error"] = "Could not join: the night is full, already started, or you already joined.";
            }

            if (fromList)
            {
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Details), new { id });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Leave(int id, bool fromList = false)
        {
            string? userId = GetUserId();

            if (userId == null)
            {
                return Challenge();
            }

            bool left = await gameNightService.LeaveAsync(id, userId);

            if (left)
            {
                TempData["Success"] = "You left this game night.";
            }
            else
            {
                TempData["Error"] = "You are not signed up for this night.";
            }

            if (fromList)
            {
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Details), new { id });
        }
    }
}
