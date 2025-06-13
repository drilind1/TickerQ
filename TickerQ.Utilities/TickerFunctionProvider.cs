using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using TickerQ.Utilities.Enums;
using TickerQ.Utilities.Interfaces;
using TickerQ.Utilities.Models;

namespace TickerQ.Utilities
{
    public delegate Task TickerFunctionDelegate(CancellationToken cancellationToken, IServiceProvider serviceProvider,
        TickerFunctionContext context);

    public static class TickerFunctionProvider
    {
        internal static IReadOnlyDictionary<string, (string, Type)> TickerFunctionRequestTypes { get; private set; }

        public static IReadOnlyDictionary<string, (string cronExpression, TickerTaskPriority Priority,
                TickerFunctionDelegate Delegate)>
            TickerFunctions { get; private set; }

        public static void RegisterFunctions(
            IDictionary<string, (string, TickerTaskPriority, TickerFunctionDelegate)> functions)
        {
            TickerFunctions =
                new ReadOnlyDictionary<string, (string, TickerTaskPriority, TickerFunctionDelegate)>(functions);
        }

        public static void RegisterRequestType(IDictionary<string, (string, Type)> requestTypes)
        {
            TickerFunctionRequestTypes = new ReadOnlyDictionary<string, (string, Type)>(requestTypes);
        }
        
        internal static void MapCronExpressionsFromIConfigurations(IDictionary<string, (string, TickerTaskPriority, TickerFunctionDelegate)> functions)
        {
            TickerFunctions =
                new ReadOnlyDictionary<string, (string, TickerTaskPriority, TickerFunctionDelegate)>(functions);
        }
    }

    public static class TickerRequestProvider
    {
        public static async Task<T> GetRequestAsync<T>(IServiceProvider serviceProvider, Guid tickerId,
            TickerType tickerType)
        {
            var internalTickerManager = serviceProvider.GetService<IInternalTickerManager>();
            return await internalTickerManager.GetRequestAsync<T>(tickerId, tickerType);
        }
    }
}