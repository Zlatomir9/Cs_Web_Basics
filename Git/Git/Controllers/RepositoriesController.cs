namespace Git.Controllers
{
    using Git.Data;
    using Git.Data.Models;
    using Git.Models.Repository;
    using Git.Services;
    using MyWebServer.Controllers;
    using MyWebServer.Http;
    using System;
    using System.Linq;

    public class RepositoriesController : Controller
    {
        private readonly GitDbContext _data;
        private readonly IValidator _validator;

        public RepositoriesController(GitDbContext data, IValidator validator)
        {
            this._data = data;
            this._validator = validator;
        }

        [Authorize]
        public HttpResponse Create()
                => View();

        [HttpPost]
        [Authorize]
        public HttpResponse Create(CreateRepositoryFormModel model)
        {
            var errorModels = this._validator.ValidateRepository(model);

            if (errorModels.Any())
            {
                return Error(errorModels);
            }

            var repository = new Repository
            {
                Name = model.Name,
                IsPublic = isPublic(model.RepositoryType),
                OwnerId = this.User.Id,
                CreatedOn = DateTime.Now
            };

            this._data.Repositories.Add(repository);
            this._data.SaveChanges();

            return Redirect("/Repositories/All");
        }

        public HttpResponse All()
        {
            var repositoryQuery = this._data.Repositories.AsQueryable();

            if (this.User.IsAuthenticated)
            {
                repositoryQuery = repositoryQuery.Where(r => r.IsPublic == true || r.OwnerId == this.User.Id);
            }
            else
            {
                repositoryQuery = repositoryQuery.Where(r => r.IsPublic == true);
            }

            var repository = repositoryQuery
                .Select(x => new RepositoryListingViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Owner = x.Owner.Username,
                    CreatedOn = x.CreatedOn.ToString("R"),
                    CommitsCount = x.Commits.Count()
                })
                .ToList();

            return View(repository);
        }

        public bool isPublic(string repositoryType)
        {
            if (repositoryType == "Public")
            {
                return true;
            }

            return false;
        }
    }
}
