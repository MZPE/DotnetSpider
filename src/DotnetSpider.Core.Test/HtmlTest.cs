﻿using DotnetSpider.Core.Selector;
using System.Linq;
using Xunit;

namespace DotnetSpider.Core.Test
{
	public class HtmlTest
	{
		[Fact(DisplayName = "HtmlSelect")]
		public void Select()
		{
			Selectable selectable = new Selectable("aaaaaaab", "", Core.Infrastructure.ContentType.Html);
			string value = selectable.Regex("(.*)").GetValue();
			Assert.Equal("aaaaaaab", value);
		}

		[Fact(DisplayName = "DonotDetectDomain")]
		public void DonotDetectDomain()
		{
			Selectable selectable = new Selectable("<div><a href=\"www.aaaa.com\">aaaaaaab</a></div>", "", Core.Infrastructure.ContentType.Html);
			var values = selectable.XPath(".//a").GetValues();
			Assert.Equal("aaaaaaab", values.First());
		}

		[Fact(DisplayName = "DetectDomain1")]
		public void DetectDomain1()
		{
			Selectable selectable = new Selectable("<div><a href=\"www.aaaa.com\">aaaaaaab</a></div>", "", Core.Infrastructure.ContentType.Html, "www\\.aaaa\\.com");
			var values = selectable.XPath(".//a").GetValues();
			Assert.Equal("aaaaaaab", values.First());
		}

		[Fact(DisplayName = "DetectDomain2")]
		public void DetectDomain2()
		{
			Selectable selectable = new Selectable("<div><a href=\"www.aaaab.com\">aaaaaaab</a></div>", "", Core.Infrastructure.ContentType.Html, "www\\.aaaa\\.com");
			var values = selectable.XPath(".//a").GetValues();
			Assert.Empty(values);
		}
	}
}
