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
    public class TexteisController : Controller
    {
    
        private readonly IRepository repository;

        public TexteisController(IRepository repository)
        {
            
            this.repository = repository;
        }

        // GET: Texteis
        public IActionResult Index()
        {
            return View(this.repository.GetTexteis());
        }

        // GET: Texteis/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var textei = this.repository.GetTextei(id.Value);
            if (textei == null)
            {
                return NotFound();
            }

            return View(textei);
        }

        // GET: Texteis/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Texteis/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,ImageFront,ImageBack,Price,Disponivel,Stock")] Textei textei)
        {
            if (ModelState.IsValid)
            {
               this.repository.AddTextei(textei);
                await this.repository.SaveAllAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(textei);
        }

        // GET: Texteis/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var textei = this.repository.GetTextei(id.Value);
            if (textei == null)
            {
                return NotFound();
            }
            return View(textei);
        }

        // POST: Texteis/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,ImageFront,ImageBack,Price,Disponivel,Stock")] Textei textei)
        {
         
            if (ModelState.IsValid)
            {
                try
                {
                    this.repository.UpdateTextei(textei);
                    await this.repository.SaveAllAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!this.repository.TexteiExists(textei.Id))
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
            return View(textei);
        }

        // GET: Texteis/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var textei = this.repository.GetTextei(id.Value);
               
            if (textei == null)
            {
                return NotFound();
            }

            return View(textei);
        }

        // POST: Texteis/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var textei = this.repository.GetTextei(id);
            this.repository.RemoveTextei(textei);
            await this.repository.SaveAllAsync();
            return  RedirectToAction(nameof(Index));
        }

    }
}
