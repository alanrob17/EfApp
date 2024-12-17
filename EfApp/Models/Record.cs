using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EfApp.Models
{
    [Table("Record")]
    public class Record
    {
        #region " Properties "

        public int RecordId { get; set; } // identity field

        public int ArtistId { get; set; } // relate to the artist entity

        [Required]
        public string? Name { get; set; }

        [Required]
        public string? Field { get; set; }

        [Required]
        public int Recorded { get; set; }

        [Required]
        public string? Label { get; set; }

        [Required]
        public string? Pressing { get; set; }

        [Required]
        public string? Rating { get; set; }

        [Required]
        public int Discs { get; set; }

        [Required]
        public string? Media { get; set; }

        public DateTime? Bought { get; set; }

        public decimal? Cost { get; set; }

        public string? CoverName { get; set; }

        public string? Review { get; set; }

        public virtual Artist ArtistAsset { get; set; } = new Artist();

        public override string ToString()
        {
            return $"Id: {RecordId}, ArtistId: {ArtistId}, Recorded: {Recorded}, Record: {Name}, Rating: {Rating}, Bought: {Bought}, Cost: ${Cost}";
        }

        #endregion
    }
}
