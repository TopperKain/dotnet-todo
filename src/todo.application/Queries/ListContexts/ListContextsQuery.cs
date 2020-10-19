using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Alteridem.Todo.Domain.Common;
using Alteridem.Todo.Domain.Entities;
using Alteridem.Todo.Domain.Interfaces;
using MediatR;

namespace Alteridem.Todo.Application.Queries.ListContexts
{
    public class ListContextsQuery : IRequest<string[]>
    {

        private string[] _terms;

        public string[] Terms { get => _terms ?? new string[0]; set => _terms = value; }
    }

    public sealed class ListContextsQueryHandler : IRequestHandler<ListContextsQuery, string[]>
    {
        private readonly ITaskFile _taskFile;

        public ListContextsQueryHandler(ITaskFile taskFile)
        {
            _taskFile = taskFile;
        }

        public Task<string[]> Handle(ListContextsQuery request, CancellationToken cancellationToken)
        {
            var tasks = _taskFile.LoadTasks(StandardFilenames.Todo);
            string[] positiveTerms = request.Terms
                .Where(t => t.StartsWith("@"))
                .ToArray();
            string[] negativeTerms = request.Terms
                .Where(t => t.StartsWith("-@"))
                .Select(t => t.Substring(1))
                .ToArray();
            IEnumerable<string> search = tasks
                .SelectMany(t => t.ContextTags)
                .OrderBy(p => p)
                .Distinct();
            if (positiveTerms.Length > 0)
                search = search.Where(t => positiveTerms.Contains(t));
            if (negativeTerms.Length > 0)
                search = search.Where(t => !negativeTerms.Contains(t));

            return Task.FromResult(search.ToArray());
        }
    }
}
