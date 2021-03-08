using core.Entites;
using core.interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.ViewModels;

namespace Web.controllers
{
    public class HomeController : Controller
    {
        public IUnitOfWork<Owner> _Owner;
        public IUnitOfWork<ProtifolioItem> _Portfolio;

        public HomeController(IUnitOfWork<Owner> owner , IUnitOfWork<ProtifolioItem> portfolio)
        {
            _Owner = owner;
            _Portfolio = portfolio;
        }
        public IActionResult Index()
        {
            var homeViewModel = new HomeViewModel
            {
                Owner = _Owner.Entity.GetAll().First(),
                PortfolioItems = _Portfolio.Entity.GetAll().ToList()

            };
            return View(homeViewModel);
            

        }

        public IActionResult About()
        {
            return View();
        }
    }
}
