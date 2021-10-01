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
        public IRingerRepository RingerRepository { get; }

        public RingersController(IRingerRepository RingerRepository)
        {
            this.RingerRepository = RingerRepository;
        }

        // GET: Ringers
        public IActionResult Index()
        {
            return View(this.RingerRepository.GetAll());
        }

        // GET: Ringers/Details/5
        public async Task <IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ringer = await this.RingerRepository.GetByIdAsync(id.Value);
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
                await this.RingerRepository.CreateAsync(ringer);
               
                return RedirectToAction(nameof(Index));
            }
            return View(ringer);
        }

        // GET: Ringers/Edit/5
        public async Task <IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ringer = await this.RingerRepository.GetByIdAsync(id.Value);
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
                    await this.RingerRepository.UpdateAsync(ringer);
                   
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await this.RingerRepository.ExistsAsync(ringer.Id))
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
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ringer = await this.RingerRepository.GetByIdAsync(id.Value);
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
            var ringer = await this.RingerRepository.GetByIdAsync(id);
            await this.RingerRepository.DeleteAsync(ringer);
            
            return RedirectToAction(nameof(Index));
        }

    }
}
