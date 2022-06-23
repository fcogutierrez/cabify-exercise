using Cabify.CarPooling.Application.Interfaces;
using Cabify.CarPooling.Application.Query;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Cabify.CarPooling.Application.Handlers.Decorators
{
    public sealed class QueryHandlerLoggerDecorator<TQuery, TQueryResult>
        : IQueryHandler<TQuery, TQueryResult> where TQuery : IQuery
    {
        private readonly IQueryHandler<TQuery, TQueryResult> _queryHandler;
        private readonly ILogger<QueryHandlerLoggerDecorator<TQuery, TQueryResult>> _logger;

        public QueryHandlerLoggerDecorator(
            IQueryHandler<TQuery, TQueryResult> queryHandler, 
            ILogger<QueryHandlerLoggerDecorator<TQuery, TQueryResult>> logger)
        {
            _queryHandler = queryHandler;
            _logger = logger;
        }

        public async Task<TQueryResult> Handle(TQuery query)
        {
            _logger.LogInformation($"Processing Query {typeof(TQuery).Name}: {query}");

            try
            {
                var result = await _queryHandler.Handle(query);
                _logger.LogInformation($"Query {typeof(TQuery).Name} was successfully processed");

                return result;
            }
            catch (Exception exception)
            {
                _logger.LogError($"Unhandled exception when processing Query {typeof(TQuery).Name}: {exception}");
                throw;
            }
        }
    }
}