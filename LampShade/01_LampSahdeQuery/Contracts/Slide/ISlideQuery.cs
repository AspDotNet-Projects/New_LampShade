using System.Collections.Generic;

namespace _01_LampSahdeQuery.Contracts.Slide
{
    public interface ISlideQuery
    {
         List<SlideQueryModel> GetSlides();
    }
}
