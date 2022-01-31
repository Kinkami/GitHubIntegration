using Core.Domain.Interfaces.Entities;
using Core.Domain.Interfaces.Services;
using GitHubAPI.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace API.IntegrationTests
{
    [TestClass]
    public class GitHubControllerTests
    {
        private readonly IUserGitHubReposService _userGitHubReposService;

        [TestMethod]
        [Owner("Fernando Henrique Leme")]
        public void GetAllRepositories_ShouldReturnAllUserRepositories()
        {

            var controller = new GitHubController(_userGitHubReposService);

            var result = controller.Get();

            Assert.IsNotNull(result);
        }

        [TestMethod]
        [Owner("Fernando Henrique Leme")]
        [DataRow("AppPrevisaoDoTempoReact")]
        [DataRow("angular2-loiane")]
        [DataRow("mvc5-ddd")]
        public void GetByRepositoryName_ShouldReturnAllUserRepositories(string name)
        {

            var controller = new GitHubController(_userGitHubReposService);

            var result = controller.GetByRepositoryName(name);

            Assert.IsNotNull(result);
        }

    }
}
