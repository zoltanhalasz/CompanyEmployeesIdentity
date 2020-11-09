﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CompanyEmployees.Client.Models;
using System.Text.Json;
using System.Net.Http;

namespace CompanyEmployees.Client.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHttpClientFactory _httpClientFactory;

        public HomeController(ILogger<HomeController> logger, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public async Task<IActionResult> Companies() 
        { 
            var httpClient = _httpClientFactory.CreateClient("APIClient"); 
            var response = await httpClient.GetAsync("api/companies").ConfigureAwait(false); 
            response.EnsureSuccessStatusCode();
            var companiesString = await response.Content.ReadAsStringAsync();
            var companies = JsonSerializer.Deserialize<List<CompanyViewModel>>(companiesString, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }); 
            return View(companies); 
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
