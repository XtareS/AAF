using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AAF.Data;
using AAF.Data.Entities;

namespace AAF.Controllers
{
    public class CraftsController : Controller
    {
        private readonly IRepository repository;

        public CraftsController(IRepository repository)
        {
          
            this.repository = repository;
        }

        // GET: Crafts
        public IActionResult Index()
        {
            return View(this.repository.GetCrafts());
        }

        // GET: Crafts/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var craft = this.repository.GetCraft(id.Value);
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
                this.repository.AddCraft(craft);
                await this.repository.SaveAllAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(craft);
        }

        // GET: Crafts/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var craft = this.repository.GetCraft(id.Value);
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
                    this.repository.UpdateCraft(craft);
                    await this.repository.SaveAllAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!this.repository.CraftExists(craft.Id))
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
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var craft = this.repository.GetCraft(id.Value);
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
            var craft = this.repository.GetCraft(id);
            this.repository.RemoveCraft(craft);
            await this.repository.SaveAllAsync();
            return RedirectToAction(nameof(Index));
        }

    }
}
