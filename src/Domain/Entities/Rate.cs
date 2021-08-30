using System;
using Domain.Common;

namespace Domain.Entities
{
    public class Rate : Entity
    {
        public Rate() { }
        
        public Rate(Currency from, Currency to, double sell, double buy, DateTime date) =>
            (From, To, Sell, Buy, Date) = (from, to, sell, buy, date);

        public Currency From { get; set; }
        public Currency To { get; set; }
        /// <summary>
        /// Gets or sets currency sell value
        /// </summary>
        public double Sell { get; set; }
        /// <summary>
        /// Gets or sets currency buy value
        /// </summary>
        public double Buy { get; set; }
        /// <summary>
        /// Gets or sets currency active date value
        /// </summary>
        public DateTime Date { get; set; }
    }
}