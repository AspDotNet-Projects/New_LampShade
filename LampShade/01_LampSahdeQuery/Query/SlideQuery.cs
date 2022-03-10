using System.Collections.Generic;
using System.Linq;
using _01_LampSahdeQuery.Contracts;
using ShopManagement.Infrastructure.EFCore;

namespace _01_LampSahdeQuery.Query
{
    public class SlideQuery : ISlideQuery
    {
        private readonly ShopContext _context;

        public SlideQuery(ShopContext context)
        {
            _context = context;
        }

        public List<SlideQueryModel> GetSlides()
        {
            return _context.Slides.Where(x => x.IsRemoved == false)
                .Select(x => new SlideQueryModel
                {
                    Picture = x.Picture,
                    PictureTitle = x.PictureTitle,
                    PictureAlt = x.PictureAlt,
                    Title = x.Title,
                    Text = x.Text,
                    Link = x.Link,
                    BtnText = x.BtnText,
                    Btncolor = x.Btncolor

                }).ToList();
        }
    }
}
