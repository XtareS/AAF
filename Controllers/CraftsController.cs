using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AAF.Data;
using AAF.Data.Entities;
using AAF.Helpers;

namespace AAF.Controllers
{
    public class CraftsController : Controller
    {
        private readonly ICraftRepository CraftRepository;
        private readonly IUserHelper userHelper;

        public CraftsController(ICraftRepository CraftRepository, IUserHelper userHelper)
        {
            this.CraftRepository = CraftRepository;
            this.userHelper = userHelper;
        }

        // GET: Crafts
        public IActionResult Index()
        {
            return View(this.CraftRepository.GetAll());
        }

        // GET: Crafts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var craft = await this.CraftRepository.GetByIdAsync(id.Value);
            if (craft == null)
            {
                return NotFound();
            }

            return View(craft);
        }

        // GET: Crafts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Crafts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,ImageFront,ImageBack,Price,Disponivel,Stock")] Craft craft)
        {
            if (ModelState.IsValid)
            {
                //TODO: Change For the Logged User
                craft.User = await this.userHelper.GetUserByEmailAsync("irma.mendonca.sr@gmail.com");
                await this.CraftRepository.CreateAsync(craft);
                return RedirectToAction(nameof(Index));
            }
            return View(craft);
        }

        // GET: Crafts/Edit/5
        public async Task <IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var craft = await this.CraftRepository.GetByIdAsync(id.Value);
            if (craft == null)
            {
                return NotFound();
            }
            return View(craft);
        }

        // POST: Crafts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,ImageFront,ImageBack,Price,Disponivel,Stock")] Craft craft)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    //TODO: Change For the Logged User
                    craft.User = await this.userHelper.GetUserByEmailAsync("irma.mendonca.sr@gmail.com");
                    await this.CraftRepository.UpdateAsync(craft);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await this.CraftRepository.ExistsAsync(craft.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(craft);
        }

        // GET: Crafts/Delete/5
        public async Task <IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var craft = await this.CraftRepository.GetByIdAsync(id.Value);
            if (craft == null)
            {
                return NotFound();
            }

            return View(craft);
        }

        // POST: Crafts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var craft = await this.CraftRepository.GetByIdAsync(id);
            await this.CraftRepository.DeleteAsync(craft);
            return RedirectToAction(nameof(Index));
        }

    }
}
