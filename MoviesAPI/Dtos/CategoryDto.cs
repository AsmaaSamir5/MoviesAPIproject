﻿namespace MoviesAPI.Dtos
{
    public class CategoryDto
    {
        [MaxLength(100)]
        public string Name { get; set; }
    }
}
