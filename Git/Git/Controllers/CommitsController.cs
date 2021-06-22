namespace Git.Controllers
{
    using Git.Data;
    using Git.Data.Models;
    using Git.Models.Commit;
    using Git.Services;
    using MyWebServer.Controllers;
    using MyWebServer.Http;
    using System;
    using System.Linq;

    using static Data.DataConstants;

    public class CommitsController : Controller
    {
        private readonly GitDbContext _data;

        public CommitsController(GitDbContext data, IValidator validator) => this._data = data;

        [Authorize]
        public HttpResponse Create(string id)
        {
            var repository = this._data.Repositories
                .Where(r => r.Id == id)
                .Select(r => new CommitToRepositoryViewModel 
                {
                    Id = r.Id,
                    Name = r.Name
                })
                .FirstOrDefault();

            if (repository == null)
            {
                return BadRequest();
            }

            return View(repository);
        }

        [HttpPost]
        [Authorize]
        public HttpResponse Create(CreateCommitFormModel model)
        {
            var repository = this._data.Repositories.FirstOrDefault(x => x.Id == model.Id);

            if (repository == null)
            {
                return Error("Repository does not exist");
            }

            if (model.Description.Length < CommitDescriptionMinLenth)
            {
                return Error($"Commit description should be min {CommitDescriptionMinLenth} characters long.");
            }

            var commit = new Commit
            {
                Description = model.Description,
                CreatedOn = DateTime.Now,
                CreatorId = this.User.Id,
                RepositoryId = repository.Id
            };

            this._data.Commits.Add(commit);
            this._data.SaveChanges();

            return Redirect("/Repositories/All");
        }

        [Authorize]
        public HttpResponse All()
        {
            var commits = this._data.Commits
                .Where(c => c.CreatorId == this.User.Id)
                .Select(c => new CommitListingViewModel 
                {
                    Id = c.RepositoryId,
                    Repository = c.Repository.Name,
                    CreatedOn = c.CreatedOn.ToString("R"),
                    Description = c.Description
                })
                .ToList();

            return View(commits);
        }

        [Authorize]
        public HttpResponse Delete(string id)
        {
            var commit = this._data.Commits
                .Where(r => r.RepositoryId == id && r.CreatorId == this.User.Id)
                .FirstOrDefault();

            if (commit == null)
            {
                return BadRequest();
            }

            this._data.Commits.Remove(commit);

            this._data.SaveChanges();

            return Redirect("All");
        }
    }
}
