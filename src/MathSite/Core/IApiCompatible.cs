using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace MathSite.Core
{
	public interface IApiCompatible<T>
	{
		IEnumerable<T> GetAll();
		IActionResult SaveAll(IEnumerable<T> items);
	}
}