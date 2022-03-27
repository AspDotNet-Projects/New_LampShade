using _0_Framework.Domain;
using DiscountManagement.Application.Contract.ColleagueDiscount;
using System.Collections.Generic;

namespace DiscountMangement.Domain.ColleagueDiscountAgg
{
    public interface ICollegueDiscountRepository:IRepository<long,ColleagueDiscount>
    {
        EditColleagueDiscount GetDetails(long id);
        List<ColleagueDiscountViewModel> Search(ColleagueDiscountSearchModel searchModel);
    }
}
