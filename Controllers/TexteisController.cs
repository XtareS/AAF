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
using AAF.Models;
using System.IO;

namespace AAF.Controllers
{
    public class TexteisController : Controller
    {
        private readonly ITexteiRepository repository;
        private readonly IUserHelper userHelper;

        public TexteisController(ITexteiRepository repository, IUserHelper userHelper)
        {
            this.repository = repository;
            this.userHelper = userHelper;
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
        public async Task<IActionResult> Create([Bind("Id,Name,ImageFileFront,ImageFileBack,Price,Disponivel,Stock")] TexteiViewModel view)
        {
            if (ModelState.IsValid)
            {
                var pathFront = string.Empty;
                var pathBack = string.Empty;

                if (view.ImageFileFront !=null && view.ImageFileFront.Length>0)
                {
                    pathFront = Path.Combine(Directory.GetCurrentDirectory(),
                        "wwwroot\\images\\Texteis\\front",
                        view.ImageFileFront.FileName);
                    using (var stream = new FileStream(pathFront, FileMode.Create))
                    {
                        await view.ImageFileFront.CopyToAsync(stream);
                    }
                    pathFront = $"~/images/Texteis/front/{view.ImageFileFront.FileName}";
                }


                if (view.ImageFileBack != null && view.ImageFileBack.Length > 0)
                {
                    pathBack = Path.Combine(Directory.GetCurrentDirectory(),
                        "wwwroot\\images\\Texteis\\back",
                        view.ImageFileBack.FileName);
                    using (var stream = new FileStream(pathBack, FileMode.Create))
                    {
                        await view.ImageFileBack.CopyToAsync(stream);
                    }
                    pathBack = $"~/images/Texteis/back/{view.ImageFileBack.FileName}";
                }

                var textei = this.ToTextei(view, pathFront, pathBack);

                //TODO: Change For the Logged User
                textei.User = await this.userHelper.GetUserByEmailAsync("irma.mendonca.sr@gmail.com");
              await this.repository.CreateAsync(textei);
                return RedirectToAction(nameof(Index));
            }
            return View(view);
        }

        private Textei ToTextei(TexteiViewModel view, string pathFront, string pathBack)
        {
            return new Textei
            {
                Id = view.Id,
                Name = view.Name,
                ImageFront = pathFront,
                ImageBack = pathBack,
                Price = view.Price,
                Disponivel = view.Disponivel,
                Stock = view.Stock,
                User = view.User


            };
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

            var view = this.ToTexteiViewModel(textei);

            return View(textei);
        }

        private TexteiViewModel ToTexteiViewModel(Textei textei)
        {
            return new TexteiViewModel
            {

                Id = textei.Id,
                Name = textei.Name,
                ImageFront = textei.ImageFront,
                ImageBack = textei.ImageBack,
                Price = textei.Price,
                Disponivel= textei.Disponivel,
                Stock = textei.Stock,
                User = textei.User

            };
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
                    //TODO: Change For the Logged User
                    textei.User = await this.userHelper.GetUserByEmailAsync("irma.mendonca.sr@gmail.com");
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
