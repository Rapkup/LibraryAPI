using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace DAL.Models
{
    public class Book
    {
        public int Id { get; set; }
        public int ISBN { get; set; } 
        public string Title { get; set; }
        public string? Genre { get; set; }
        public string? Description { get; set; }

        public string AuthorName { get; set; }
        public int AuthorId { get; set; }

        [AllowNull]
        public int? TakenBy { get; set; }
        [AllowNull]
        public DateOnly TakenAt { get; set; }
        [AllowNull]
        public DateOnly ReturnAt { get; set; }


        protected virtual Author Author { get; set; }


    }
}
