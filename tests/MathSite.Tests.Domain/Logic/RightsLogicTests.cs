using System;
using System.Threading.Tasks;
using MathSite.Domain.Logic.Rights;
using MathSite.Entities;
using Xunit;

namespace MathSite.Tests.Domain.Logic
{
	public class RightsLogicTests : DomainTestsBase
	{
		[Fact]
		public async Task CreateRightTest()
		{
			await ExecuteWithContextAsync(async context =>
			{
				var rightsLogic = new RightsLogic(context);

				const string rightAlias = "testRight-Alias";
				const string rightName = "testRight-Name";
				const string rightDesc = "testRight-Desc";

				await rightsLogic.CreateAsync(rightAlias, rightName, rightDesc);

				var right = await rightsLogic.TryGetByAliasAsync(rightAlias);
				Assert.NotNull(right);

				Assert.Equal(rightName, right.Name);
				Assert.Equal(rightDesc, right.Description);
				Assert.Equal(rightAlias, right.Alias);
			});
		}

		[Fact]
		public async Task UpdateRightTest()
		{
			await ExecuteWithContextAsync(async context =>
			{
				var rightsLogic = new RightsLogic(context);
				var right = await CreateTestRightAsync(rightsLogic);

				const string name = "testName";
				const string desc = "testDesc";

				await rightsLogic.UpdateAsync(right.Alias, name, desc);

				var updated = await rightsLogic.TryGetByAliasAsync(right.Alias);

				Assert.NotNull(updated);
				Assert.Equal(name, updated.Name);
				Assert.Equal(desc, updated.Description);
			});
		}

		[Fact]
		public async Task DeleteRightTest()
		{
			await ExecuteWithContextAsync(async context =>
			{
				var rightsLogic = new RightsLogic(context);
				var right = await CreateTestRightAsync(rightsLogic);

				await rightsLogic.DeleteAsync(right.Alias);

				var deleted = await rightsLogic.TryGetByAliasAsync(right.Alias);

				Assert.Null(deleted);
			});
		}

		[Fact]
		public async Task TryGetByAliasTest()
		{
			await ExecuteWithContextAsync(async context =>
			{
				var rightsLogic = new RightsLogic(context);
				var createdRight = await CreateTestRightAsync(rightsLogic);

				var right = await rightsLogic.TryGetByAliasAsync(createdRight.Alias);

				Assert.NotNull(right);
				Assert.Equal(createdRight.Description, right.Description);
				Assert.Equal(createdRight.Name, right.Name);
			});
		}

		private async Task<Right> CreateTestRightAsync(IRightsLogic rightsLogic)
		{
			var randomId = Guid.NewGuid();
			var rightAlias = $"testRight-Alias-{randomId}";
			var rightName = $"testRight-Name-{randomId}";
			var rightDesc = $"testRight-Desc-{randomId}";

			await rightsLogic.CreateAsync(rightAlias, rightName, rightDesc);

			return await rightsLogic.TryGetByAliasAsync(rightAlias);
		}
	}
}