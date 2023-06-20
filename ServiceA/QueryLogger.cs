using System;
using System.Diagnostics;
using System.Linq;
using System.Text;
using HotChocolate.Execution;
using HotChocolate.Execution.Instrumentation;
using Microsoft.Extensions.Logging;

namespace Logging
{
    public class ConsoleQueryLogger : DiagnosticEventListener
    {
        private static Stopwatch _queryTimer;
        private readonly ILogger<ConsoleQueryLogger> _logger;
        public ConsoleQueryLogger(ILogger<ConsoleQueryLogger> logger)
        {
            _logger = logger;
        }
        
        public override IDisposable ExecuteRequest(IRequestContext context)
        {
            return base.ExecuteRequest(context);
        }

        public override IDisposable ParseDocument(IRequestContext context)
        {
            return base.ParseDocument(context);
        }
    }
}

