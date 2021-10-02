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
        public async Task<IActionResult> Create([Bind("Id,Name,ImageFileFront,ImageFileBack,Price,Disponivel,Stock")] CraftViewModel view)
        {
            if (ModelState.IsValid)
            {
                var pathFront = string.Empty;
                var pathBack = string.Empty;

                if (view.ImageFileFront != null && view.ImageFileFront.Length > 0)
                {
                    pathFront = Path.Combine(Directory.GetCurrentDirectory(),
                        "wwwroot\\images\\Crafts\\front",
                        view.ImageFileFront.FileName);
                    using (var stream = new FileStream(pathFront, FileMode.Create))
                    {
                        await view.ImageFileFront.CopyToAsync(stream);
                    }
                    pathFront = $"~/images/Crafts/front/{view.ImageFileFront.FileName}";
                }


                if (view.ImageFileBack != null && view.ImageFileBack.Length > 0)
                {
                    pathBack = Path.Combine(Directory.GetCurrentDirectory(),
                        "wwwroot\\images\\Crafts\\back",
                        view.ImageFileBack.FileName);
                    using (var stream = new FileStream(pathBack, FileMode.Create))
                    {
                        await view.ImageFileBack.CopyToAsync(stream);
                    }
                    pathBack = $"~/images/Crafts/back/{view.ImageFileBack.FileName}";
                }

                var craft = this.ToCraft(view, pathFront, pathBack);


                //TODO: Change For the Logged User
                craft.User = await this.userHelper.GetUserByEmailAsync("irma.mendonca.sr@gmail.com");
                await this.CraftRepository.CreateAsync(craft);
                return RedirectToAction(nameof(Index));
            }
            return View(view);
        }

        private Craft ToCraft(CraftViewModel view, string pathFront, string pathBack)
        {
            return new Craft
            {
                Id = view.Id,
                Name = view.Name,
                ImageFront = pathFront,
                ImageBack = pathBack,
                Price = view.Price,
                Stock = view.Stock,
                User = view.User


            };
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
