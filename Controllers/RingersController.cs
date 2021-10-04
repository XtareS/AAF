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
using Microsoft.AspNetCore.Authorization;

namespace AAF.Controllers
{
    public class RingersController : Controller
    {
        private readonly IRingerRepository RingerRepository;
        private readonly IUserHelper userHelper;

        public RingersController(IRingerRepository RingerRepository, IUserHelper userHelper)
        {
            this.RingerRepository = RingerRepository;
            this.userHelper = userHelper;
        }

        // GET: Ringers
        public IActionResult Index()
        {
            return View(this.RingerRepository.GetAll().OrderByDescending(p => p.Id).ToList());
        }

        [Authorize]
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
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }


        // POST: Ringers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,ImageFront,ImageBack,Price,Disponivel,Stock")] RingerViewModel view)
        {
            if (ModelState.IsValid)
            {
                var pathFront = string.Empty;
                var pathBack = string.Empty;

                if (view.ImageFileFront != null && view.ImageFileFront.Length > 0)
                {
                    var guidfront = Guid.NewGuid().ToString();
                    var fileFront = $"{guidfront}.jpg";

                    pathFront = Path.Combine(Directory.GetCurrentDirectory(),
                        "wwwroot\\images\\Ringers\\front",
                       fileFront);
                    using (var stream = new FileStream(pathFront, FileMode.Create))
                    {
                        await view.ImageFileFront.CopyToAsync(stream);
                    }
                    pathFront = $"~/images/Ringers/front/{fileFront}";
                }


                if (view.ImageFileBack != null && view.ImageFileBack.Length > 0)
                {
                    var guidback = Guid.NewGuid().ToString();
                    var fileback = $"{guidback}.jpg";

                    pathBack = Path.Combine(Directory.GetCurrentDirectory(),
                        "wwwroot\\images\\Ringers\\back",
                        fileback);
                    using (var stream = new FileStream(pathBack, FileMode.Create))
                    {
                        await view.ImageFileBack.CopyToAsync(stream);
                    }
                    pathBack = $"~/images/Ringers/back/{fileback}";
                }

                var ringer = this.ToRinger(view, pathFront, pathBack);


                //TODO: Change For the Logged User
                ringer.User = await this.userHelper.GetUserByEmailAsync("irma.mendonca.sr@gmail.com");
                await this.RingerRepository.CreateAsync(ringer);
               
                return RedirectToAction(nameof(Index));
            }
            return View(view);
        }

        private Ringer ToRinger(RingerViewModel view, string pathFront, string pathBack)
        {
            return new Ringer
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

        // GET: Ringers/Edit/5
        [Authorize]
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
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,ImageFront,ImageBack,Price,Disponivel,Stock")] Ringer ringer)
        {
        

            if (ModelState.IsValid)
            {
                try
                {
                    //TODO: Change For the Logged User
                    ringer.User = await this.userHelper.GetUserByEmailAsync("irma.mendonca.sr@gmail.com");
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
        [Authorize]
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
        [Authorize]
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
