using _0_Framework.Domain;
using DiscountManagement.Application.Contract.ColleagueDiscount;

namespace DiscountMangement.Domain.ColleagueDiscountAgg
{
    public interface ICollegueDiscountRepository:IRepository<long,ColleagueDiscount>
    {
        EditColleagueDiscount GetDetails(long id);
        ColleagueDiscountSearchModel Search(ColleagueDiscountSearchModel searchModel);
    }
}
