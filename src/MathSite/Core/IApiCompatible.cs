using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace MathSite.Core
{
	public interface IApiCompatible<T>
	{
		IEnumerable<T> GetAll(int offset = 0, int count = 50);
		IActionResult SaveAll(IEnumerable<T> items);
	}
}