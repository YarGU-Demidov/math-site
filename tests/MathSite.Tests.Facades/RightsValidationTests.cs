using System;
using Xunit;

namespace MathSite.Tests.Facades
{
    public class RightsValidationTests: FacadesTestsBase
    {
        [Fact]
        public async void AllowedAccess()
        {
			await WithLogicAsync(async manger =>
			{
				// TODO: code for test validation
			});
        }
    }
}
