using Core.Domain.Interfaces.Entities;
using Core.Domain.Interfaces.Repositories;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Infrastructure.GitHub.Repository
{
    public class UserGitHubReposRepository : IUserGitHubReposRepository
    {
        private readonly IHttpClientFactory _clientFactory;

        public UserGitHubReposRepository(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task<List<Root>> GetRepositoriesFromUser()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "https://api.github.com/users/Kinkami/repos");
            request.Headers.Add("Accept", "application/vnd.github.v3+json");
            request.Headers.Add("User-Agent", "HttpClientFactory-Sample");

            var client = _clientFactory.CreateClient();

            var response = await client.SendAsync(request);

            var responseStream = await response.Content.ReadAsStreamAsync();

            StreamReader reader = new StreamReader(responseStream);

            return JsonConvert.DeserializeObject<List<Root>>(reader.ReadToEnd());
        }

        public string MarkAsFavorite(int id)
        {
            try
            {
                if (id > 0)
                {
                    string path = Path.Combine(Directory.GetCurrentDirectory(), "FavoriteRepositories.txt");

                    if (!File.Exists(path))
                    {

                        using (StreamWriter sw = File.CreateText(path))
                        {
                            sw.WriteLine(id);
                        }
                    }
                    else
                    {

                        string text = File.ReadAllText(path);
                        string[] lines = text.Split(Environment.NewLine);

                        foreach (string line in lines)
                        {
                            if (line.Equals(id.ToString()))
                            {
                                return "Este repositório já está marcado como favorito!";
                            }
                        }

                        using (StreamWriter sw = File.AppendText(path))
                        {
                            sw.WriteLine(id);
                        }
                    }
                    return "OK";
                }
                else
                {
                    return "O Id do repositório deve ser preenchido";
                }
            }
            catch (System.Exception ex)
            {
                return ex.Message;
            }
        }

        public List<int> GetFavoriteRepos()
        {
            try
            {
                string fileName = Path.Combine(Directory.GetCurrentDirectory(), "FavoriteRepositories.txt");
                List<int> favoriteRepositories = new List<int>();

                if (File.Exists(fileName))
                {
                    string text = File.ReadAllText(fileName);
                    string[] lines = text.Split(Environment.NewLine);

                    foreach (string line in lines)
                    {
                        if (line.Length > 0)
                        {
                            favoriteRepositories.Add(int.Parse(line));
                        }
                    }
                }
                return favoriteRepositories;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public string RemoveFavoriteMark(int id)
        {
            try
            {
                if (id > 0)
                {
                    string fileName = Path.Combine(Directory.GetCurrentDirectory(), "FavoriteRepositories.txt");
                    List<string> lst = File.ReadAllLines(fileName).Where(arg => !string.IsNullOrWhiteSpace(arg)).ToList();
                    lst.RemoveAll(x => x.Equals(id.ToString()));
                    File.WriteAllLines(fileName, lst);

                    return "Repositório removido dos favoritos!";
                }
                else
                {
                    return "O Id do repositório deve ser preenchido";
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
