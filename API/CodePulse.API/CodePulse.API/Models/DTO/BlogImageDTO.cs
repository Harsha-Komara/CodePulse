﻿namespace CodePulse.API.Models.DTO
{
    public class BlogImageDTO
    {
        public Guid Id { get; set; }
        public required string FileName { get; set; }
        public required string FileExtension { get; set; }
        public required string Title { get; set; }
        public required string Url { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
