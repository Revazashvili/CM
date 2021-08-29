using System.Threading.Tasks;
using Domain.Entities;

namespace Application.Common.Interfaces
{
    /// <summary>
    /// Calculates currency related stuff.
    /// </summary>
    public interface ICurrencyCalculator
    {
        /// <summary>
        /// Calculates giving amount based on given sell rate.
        /// </summary>
        /// <param name="from">The currency from where will be converted.</param>
        /// <param name="to">The currency where will be converted.</param>
        /// <param name="receiveAmount">The amount of money to convert.</param>
        /// <returns></returns>
        ValueTask<double> AmountForGiving(string from,string to, double receiveAmount);
    }
}