namespace Git.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using static Data.DataConstants;

    public class Repository
    {
        public string Id { get; init; } = Guid.NewGuid().ToString();

        [Required]
        [MaxLength(RepositoryNameMaxLength)]
        public string Name { get; init; }

        public DateTime CreatedOn { get; init; }

        public bool IsPublic { get; init; }

        [Required]
        public string OwnerId { get; init; }

        public User Owner { get; init; }

        public ICollection<Commit> Commits { get; init; } = new List<Commit>();
    }
}
