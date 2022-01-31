using Core.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;


namespace GitHubAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GitHubController : ControllerBase
    {

        private IUserGitHubReposService _userGitHubReposService;

        public GitHubController(IUserGitHubReposService userGitHubReposService) =>
        _userGitHubReposService = userGitHubReposService;

        [HttpGet]
        [Route("GetAllRepositories")]
        public async Task<IActionResult> Get()
        {
            try
            {
                var dataResponse = await _userGitHubReposService.GetRepositoriesFromUser();

                return Ok(dataResponse);
            }
            catch (Exception ex)
            {

                return BadRequest(ex);
            }
        }

        [HttpGet]
        [Route("GetByRepositoryName")]
        public async Task<IActionResult> GetByRepositoryName(string reponame)
        {
            try
            {
                if (!string.IsNullOrEmpty(reponame))
                {
                    var responseData = await _userGitHubReposService.GetByRepositorByName(reponame);
                    return Ok(responseData);
                }
                else
                {
                    return BadRequest("Favor informar o nome do repositório para busca.");
                }
            }
            catch (Exception ex)
            {

                return BadRequest(ex);
            }
        }

        [HttpGet]
        [Route("GetFavoriteRepos")]
        public async Task<IActionResult> GetFavoriteRepos()
        {
            try
            {
                var responseData = await _userGitHubReposService.GetFavoriteRepos();
                if (responseData.Count > 0)
                {
                    return Ok(responseData);
                }
                else
                {
                    return BadRequest("Nenhum repositório foi marcado como favorito.");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost]
        [Route("MarkAsFavorite")]
        public IActionResult MarkAsFavorite(int id)
        {
            try
            {
                var returnMessage = _userGitHubReposService.MarkAsFavorite(id);

                if (!String.IsNullOrEmpty(returnMessage))
                {
                    return Ok("Repositório marcado como favorito!");
                }
                else
                {
                    return BadRequest(returnMessage);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

        }

        [HttpPost]
        [Route("RemoveFavoriteMark")]
        public IActionResult RemoveFavoriteMark(int id)
        {
            try
            {
                var returnMessage = _userGitHubReposService.RemoveFavoriteMark(id);

                if (!String.IsNullOrEmpty(returnMessage))
                {
                    return Ok("Repositório removido dos favoritos!");
                }
                else
                {
                    return BadRequest(returnMessage);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }



    }
}
