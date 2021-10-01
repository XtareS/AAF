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
        private readonly ITexteiRepository repository;

        public TexteisController(ITexteiRepository repository)
        {
            this.repository = repository;
        }

        // GET: Texteis
        public IActionResult Index()
        {
            return View(this.repository.GetAll());
        }

        // GET: Texteis/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var textei = await this.repository.GetByIdAsync(id.Value);
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
              await this.repository.CreateAsync(textei);
                return RedirectToAction(nameof(Index));
            }
            return View(textei);
        }

        // GET: Texteis/Edit/5
        public async Task <IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var textei = await this.repository.GetByIdAsync(id.Value);
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
                  await this.repository.UpdateAsync(textei);
                   
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await this.repository.ExistsAsync(textei.Id))
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
        public async Task <IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var textei = await this.repository.GetByIdAsync(id.Value);

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
            var textei = await this.repository.GetByIdAsync(id);
            await this.repository.DeleteAsync(textei);
           
            return  RedirectToAction(nameof(Index));
        }

    }
}
