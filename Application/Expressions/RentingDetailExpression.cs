using Domain.Entities;
using System.Linq.Expressions;

namespace Application.Expressions
{
    public static class RentingDetailExpression
    {
        public static Expression<Func<RentingDetail, bool>> GetRentingDetailFromToOfCar(DateTime from, DateTime to, int carId)
        {
            return rd => rd.StartDate < to && from < rd.EndDate && rd.CarId == carId;
        }
    }
}
