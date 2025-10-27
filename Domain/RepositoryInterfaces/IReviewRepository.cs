using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MatchaLatteReviews.Domain.Model;

namespace MatchaLatteReviews.Domain.RepositoryInterfaces
{
    internal interface IReviewRepository : ICrudRepository<Review>
    {
    }
}
