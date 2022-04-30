using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using _0_Framework.Application;
using _0_Framework.Domain;
using _0_Framework.Repository;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Application.Contract.Slide;
using ShopManagement.Domain.SlideAgg;

namespace ShopManagement.Infrastructure.EFCore.Repository
{
    public class SlideRepository :RepositoryBase<long,Slide>, ISlideRepository
    {
        private readonly ShopContext _context;

        public SlideRepository(ShopContext context) : base(context)
        {
            _context = context;
        }
        public EditSlide GetDetails(long id)
        {
            return _context.Slides.Select(x => new EditSlide
            {
                ID = x.Id,
                PictureTitle = x.PictureTitle,
                PictureAlt = x.PictureAlt,
                Title = x.Title,
                Text = x.Text,
                Link = x.Link,
                BtnText = x.BtnText,
                Btncolor = x.Btncolor,
                Heading = x.Heading,
               
            }).FirstOrDefault(x => x.ID == id);
        }

        public List<SlideViewModel> getList()
        {
            return _context.Slides.Select(x=>new SlideViewModel
                {
                    ID = x.Id,
                    Picture = x.Picture,
                    Heading = x.Heading,
                    Title = x.Title,
                    IsRemoved = x.IsRemoved,
                    CreationDate = x.CreationDate.ToFarsi()
                })
                .OrderByDescending(x=>x.ID)
                .ToList();    
        }

        
    }
}
