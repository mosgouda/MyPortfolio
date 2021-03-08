using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using core.Entites;
using infrastructure;
using Web.ViewModels;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using core.interfaces;

namespace Web.controllers
{
    public class ProtifolioItemsController : Controller
    {
        private readonly IUnitOfWork<ProtifolioItem> _rotfoliopitem;
        private readonly IHostingEnvironment _hosting;

        
        public ProtifolioItemsController(IUnitOfWork<ProtifolioItem> rotfoliopitem, IHostingEnvironment Hosting )
        {
           _rotfoliopitem = rotfoliopitem;
            _hosting = Hosting;
        }

        // GET: ProtifolioItems
        public IActionResult Index()
        {
            return View(_rotfoliopitem.Entity.GetAll());
        }

        // GET: ProtifolioItems/Details/5
        public IActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var protifolioItem = _rotfoliopitem.Entity.GetById(id);
            if (protifolioItem == null)
            {
                return NotFound();
            }

            return View(protifolioItem);
        }

        // GET: ProtifolioItems/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ProtifolioItems/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(portfiolioViewModel Model)
        {
            if (ModelState.IsValid)
            {
                
                if (Model.File != null)
                {
                    string uploads = Path.Combine(_hosting.WebRootPath, @"img\portfolio");
                    string fullpath = Path.Combine(uploads, Model.File.FileName);
                    Model.File.CopyTo(new FileStream(fullpath, FileMode.Create));
                }
                

                ProtifolioItem protifolioItem = new ProtifolioItem
                {
                    Id=Model.Id,
                    ProjectName = Model.ProjectName , 
                    Description = Model.Description , 
                    ImageURL = Model.File.FileName 

                };
                _rotfoliopitem.Entity.Insert(protifolioItem);
                _rotfoliopitem.save();
               
                return RedirectToAction(nameof(Index));
            }
            return View(Model);
        }

        // GET: ProtifolioItems/Edit/5
        public IActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var protifolioItem = _rotfoliopitem.Entity.GetById(id);
            if (protifolioItem == null)
            {
                return NotFound();
            }

            portfiolioViewModel portfiolioViewModel = new portfiolioViewModel 
            {
              Id= protifolioItem.Id,
              Description = protifolioItem.Description,
              ImageURL = protifolioItem.ImageURL,
              ProjectName= protifolioItem.ProjectName

            };
            return View(portfiolioViewModel);
        }

        // POST: ProtifolioItems/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id,portfiolioViewModel model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (model.File != null)
                    {
                        string uploads = Path.Combine(_hosting.WebRootPath, @"img\portfolio");
                        string fullpath = Path.Combine(uploads, model.File.FileName);
                        model.File.CopyTo(new FileStream(fullpath, FileMode.Create));
                    }
                    

                    ProtifolioItem protifolioItem = new ProtifolioItem
                    {
                        Id =model.Id,
                        ProjectName = model.ProjectName,
                        Description = model.Description,
                        ImageURL = model.File.FileName

                    };
                    _rotfoliopitem.Entity.Update(protifolioItem);
                    _rotfoliopitem.save();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProtifolioItemExists(model.Id))
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
            return View(model);
        }

        // GET: ProtifolioItems/Delete/5
        public  IActionResult  Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var protifolioItem = _rotfoliopitem.Entity.GetById(id);
            if (protifolioItem == null)
            {
                return NotFound();
            }

            return View(protifolioItem);
        }

        // POST: ProtifolioItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            _rotfoliopitem.Entity.Delete(id);
            _rotfoliopitem.save();
            return RedirectToAction(nameof(Index));
        }

        private bool ProtifolioItemExists(Guid id)
        {
            return _rotfoliopitem.Entity.GetAll().Any(e => e.Id == id);
        }
    }
}
