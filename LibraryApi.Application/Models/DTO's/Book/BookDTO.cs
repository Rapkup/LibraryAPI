using LibraryApi.Domain.Models;
using System;
using System.Net;

namespace LibraryApi.Application.Models
{
    public class BookDTO
    {
        public int Id { get; set; }
        public int ISBN { get; set; }
        public string Title { get; set; }
        public string? Genre { get; set; }
        public string? Description { get; set; }

        public string AuthorName { get; set; }
        public int AuthorId { get; set; }

        public int? TakenBy { get; set; }
        public DateOnly? TakenAt { get; set; }
        public DateOnly? ShouldBeReturnedAt { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
