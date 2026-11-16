
using BoardGamePlanner.Services.Core.Contracts;
using BoardGamePlanner.ViewModels.GameNights;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
            var model = await gameNightService.GetAllAsync();

            return View(model);
        }

        [AllowAnonymous]
        public async Task<IActionResult> Details(int id)
        {
            var model = await gameNightService.GetDetailsAsync(id);

            if (model == null)
            {
                return NotFound();
            }

            return View(model);
        }

       
        public async Task<IActionResult> Create()
        {
            var model = await gameNightService.GetCreateModelAsync();

            return View(model);
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(GameNightFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var fresh = await gameNightService.GetCreateModelAsync();
                model.Venues = fresh.Venues;

                return View(model);
            }

            int id = await gameNightService.CreateAsync(model);

            return RedirectToAction(nameof(Details), new { id });
        }

      
        public async Task<IActionResult> Edit(int id)
        {
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

     
        public async Task<IActionResult> Delete(int id)
        {
            var model = await gameNightService.GetDetailsAsync(id);

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
            bool deleted = await gameNightService.DeleteAsync(id);

            if (!deleted)
            {
                return NotFound();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
