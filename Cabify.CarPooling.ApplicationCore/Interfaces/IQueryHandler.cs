using System.Threading.Tasks;
using Cabify.CarPooling.Application.Query;

namespace Cabify.CarPooling.Application.Interfaces
{
    public interface IQueryHandler<in TQuery, TQueryResult>
        where TQuery : IQuery
    {
        Task<TQueryResult> Handle(TQuery query);
    }
}
