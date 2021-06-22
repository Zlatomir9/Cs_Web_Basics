namespace Git.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Commit
    {
        public string Id { get; init; } = Guid.NewGuid().ToString();

        [Required]
        public string Description { get; init; }

        public DateTime CreatedOn { get; init; }

        [Required]
        public string CreatorId { get; init; }

        public User Creator { get; init; }

        [Required]
        public string RepositoryId { get; init; }

        public Repository Repository { get; init; }
    }
}
