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
    public class RingersController : Controller
    {
       
        private readonly IRepository repository;

        public RingersController(IRepository repository)
        {
         
            this.repository = repository;
        }

        // GET: Ringers
        public IActionResult Index()
        {
            return View(this.repository.GetRingers());
        }

        // GET: Ringers/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ringer = this.repository.GetRinger(id.Value);
            if (ringer == null)
            {
                return NotFound();
            }

            return View(ringer);
        }

        // GET: Ringers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Ringers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,ImageFront,ImageBack,Price,Disponivel,Stock")] Ringer ringer)
        {
            if (ModelState.IsValid)
            {
                this.repository.AddRinger(ringer);
                await this.repository.SaveAllAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(ringer);
        }

        // GET: Ringers/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ringer = this.repository.GetRinger(id.Value);
            if (ringer == null)
            {
                return NotFound();
            }
            return View(ringer);
        }

        // POST: Ringers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,ImageFront,ImageBack,Price,Disponivel,Stock")] Ringer ringer)
        {
        

            if (ModelState.IsValid)
            {
                try
                {
                    this.repository.UpdateRinger(ringer);
                    await this.repository.SaveAllAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!this.repository.RingerExists(ringer.Id))
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
            return View(ringer);
        }

        // GET: Ringers/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ringer = this.repository.GetRinger(id.Value);
            if (ringer == null)
            {
                return NotFound();
            }

            return View(ringer);
        }

        // POST: Ringers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ringer = this.repository.GetRinger(id);
            this.repository.RemoveRinger(ringer);
            await this.repository.SaveAllAsync();
            return RedirectToAction(nameof(Index));
        }

    }
}
