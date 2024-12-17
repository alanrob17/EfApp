using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace EfApp.Models
{
    [Table("Artist")]
    public class Artist
    {
        public int ArtistId { get; set; }

        public string? FirstName { get; set; }

        public string LastName { get; set; } = null!;

        public string? Name { get; set; }

        public string? Biography { get; set; }

        public virtual ICollection<Record> Records { get; set; } = new List<Record>();

        public override string ToString()
        {
            var biography = string.IsNullOrEmpty(Biography) ? "No Biography" : (Biography.Length > 50 ? Biography.Substring(0, 50) + "..." : Biography);

            return $"Id: {ArtistId}, Artist: {Name}, Biography: {biography}";
        }
    }
}