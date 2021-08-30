using System.Threading;
using System.Threading.Tasks;

namespace Application.Common.Interfaces
{
    /// <summary>
    /// Service for manipulating rates.
    /// </summary>
    public interface IRateService
    {
        /// <summary>
        /// Asynchronously determines whether exists element with given currency code or not.
        /// </summary>
        /// <param name="code">The currency code.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> instance.</param>
        /// <returns>True if exists currency with this code,otherwise false.</returns>
        Task<bool> ExistsAsync(string code, CancellationToken cancellationToken);
    }
}