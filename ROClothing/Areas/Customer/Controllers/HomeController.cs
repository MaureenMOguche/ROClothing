﻿using Microsoft.AspNetCore.Mvc;
using ROClothing.Data.Repository.IRepository;
using ROClothing.Models;
using ROClothing.Models.ViewModels;
using System.Diagnostics;

namespace ROClothing.Areas.Customer.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		private readonly IUnitOfWork _dbContext;

		public HomeController(ILogger<HomeController> logger, IUnitOfWork dbContext)
		{
			_logger = logger;
			_dbContext = dbContext;
		}

		public IActionResult Index()
		{
			ProductProductTypeVM productProductTypeVM = new()
			{
				productTypeList = _dbContext.ProductTypeRepo.GetAll(),
				productList = _dbContext.ProductRepo.GetAll()
				.Where(x => x.Price > 10000 && x.Price < 20000).Take(8),
				StartingPrice = (decimal)_dbContext.ProductRepo.GetAll().Min(x => x.Price),
			};

			return View(productProductTypeVM);
		}



		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}