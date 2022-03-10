using System.Collections.Generic;

namespace _01_LampSahdeQuery.Contracts
{
    public interface ISlideQuery
    {
         List<SlideQueryModel> GetSlides();
    }
}
