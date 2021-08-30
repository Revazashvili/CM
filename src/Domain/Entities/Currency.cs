using Domain.Common;
using Domain.Enums;

namespace Domain.Entities
{
    public class Currency : Auditable
    {
        public Currency() { }

        public Currency(string? code, string name, string latinName,CurrencyStatus status = CurrencyStatus.Active) =>
            (Code, Name, LatinName,Status) = (code, name, latinName,status);

        /// <summary>
        /// Gets or sets currency code
        /// </summary>
        public string? Code { get; set; }
        /// <summary>
        /// Gets or sets currency name 
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Gets or sets currency latin name
        /// </summary>
        public string LatinName { get; set; }
        /// <summary>
        /// Gets or sets currency status
        /// </summary>
        public CurrencyStatus Status { get; set; }
    }
}