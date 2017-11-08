﻿using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using xpi_versions_app.AMO;
using xpi_versions_app.Extended;

namespace xpi_versions_app.Models {
	public class AddonModel {
		public Addon Addon { get; private set; }

		public IEnumerable<FlatVersion> Versions { get; private set; }

		//public int Page { get; set; }

		//public int PageSize { get; set; }

		private AddonModel() { }

		public static async Task<AddonModel> CreateAsync(string id, int page, int page_size, string platform, string lang) {
			var t1 = Core.GetAddon(id, lang);
			var versions = await Core.GetVersions(id, page, page_size, lang);
			var addon = await t1;

			var flat = await Task.WhenAll(versions.results.Select(v => FlatVersion.GetAsync(addon, v, platform)));

			return new AddonModel {
				Addon = addon,
				Versions = flat
			};
		}
	}
}
