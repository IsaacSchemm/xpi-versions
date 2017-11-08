﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using xpi_versions_app.Models;

namespace xpi_versions_app.Controllers
{
    public class AddonController : Controller
    {
        public async Task<IActionResult> Index(string id = null, int page = 1, int page_size = 10, string lang = null)
        {
			string ua = Request.Headers["User-Agent"].ToString() ?? "";
			string platform = ua.Contains("Windows") ? "windows"
				: ua.Contains("Mac") ? "mac"
				: ua.Contains("Linux") || ua.Contains("BSD") ? "linux"
				: ua.Contains("Android") ? "android"
				: "all";
			if (id == null) {
				return View("NoId");
			} else {
				return View(await AddonModel.CreateAsync(id, page, page_size, platform, lang ?? "en-US"));
			}
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
