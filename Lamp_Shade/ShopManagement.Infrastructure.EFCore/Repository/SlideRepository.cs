﻿using _01_Framework.Application;
using _01_Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Application.Contracts.Slide;
using ShopManagement.Domain.SlideAgg;
using System.Globalization;

#nullable disable

namespace ShopManagement.Infrastructure.EFCore.Repository
{
    public class SlideRepository : RepositoryBase<long, Slide>, ISlideRepository
    {
        private readonly ShopContext _context;

        public SlideRepository(ShopContext context) : base(context)
        {
            _context = context;
        }

        public EditSlide GetDetails(long id)
        {
            return _context.Slides.Select(c => new EditSlide
            {
                Id = c.Id,
                BtnText = c.BtnText,
                PictureAlt = c.PictureAlt,
                PictureTitle = c.PictureTitle,
                Heading = c.Heading,
                Text = c.Text,
                Title = c.Title
            }).FirstOrDefault(z => z.Id == id);
        }

        public List<SlideViewModel> GetList()
        {
            return _context.Slides.Select(c => new SlideViewModel
            {
                Id = c.Id,
                Heading = c.Heading,
                Picture = c.Picture,
                Title = c.Title,
                IsRemoved = c.IsRemoved,
                CreationDate = c.CreationDate.ToFarsi()
            }).AsNoTracking().ToList();
        }
    }
}
