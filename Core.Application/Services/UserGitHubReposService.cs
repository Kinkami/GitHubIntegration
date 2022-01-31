using Core.Domain.Interfaces.Entities;
using Core.Domain.Interfaces.Repositories;
using Core.Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Application.Services
{
    public class UserGitHubReposService : IUserGitHubReposService
    {
        private readonly IUserGitHubReposRepository _userGiHubReposRepository;

        public UserGitHubReposService(IUserGitHubReposRepository userGitHubReposRepository)
        {
            _userGiHubReposRepository = userGitHubReposRepository;
        }

        public async Task<List<Root>> GetRepositoriesFromUser() =>
            await _userGiHubReposRepository.GetRepositoriesFromUser();

        public async Task<List<Root>> GetByRepositorByName(string name)
        {
            List<Root> allReposByUser = await _userGiHubReposRepository.GetRepositoriesFromUser();

            var filteredByName = allReposByUser.Where(r => r.name.Contains(name)).ToList();

            return filteredByName;
        }

        public string MarkAsFavorite(int id)
        {
            return _userGiHubReposRepository.MarkAsFavorite(id);
        }


        public async Task<List<Root>> GetFavoriteRepos()
        {
            var favoriteReposIds = _userGiHubReposRepository.GetFavoriteRepos();

            if (favoriteReposIds.Count > 0)
            {
                var allReposByUser = await _userGiHubReposRepository.GetRepositoriesFromUser();
                List<Root> favoriteRepos = new List<Root>();
                foreach (var repo in allReposByUser)
                {
                    foreach (var idRepoFavorite in favoriteReposIds)
                    {
                        if (repo.id.Equals(idRepoFavorite))
                        {
                            favoriteRepos.Add(repo);
                        }
                    }

                }

                
                return favoriteRepos;
            }
            else
            {
                return new List<Root>();
            }
        }

        public string RemoveFavoriteMark(int id)
        {
            return _userGiHubReposRepository.RemoveFavoriteMark(id);
        }


    }
}
