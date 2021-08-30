using System;
using Domain.Common;

namespace Domain.Entities
{
    public class Convert : AuditableEntity
    {
        public Convert() { }

        public Convert(Currency from, Currency to, User converter, string recommenderPin, double amount, DateTime time)
        {
            From = from;
            To = to;
            Converter = converter;
            RecommenderPin = recommenderPin;
            Amount = amount;
            Time = time;
        }

        public Currency From { get; }
        public Currency To { get; }
        /// <summary>
        /// Gets or sets converter
        /// </summary>
        public User Converter { get; }
        /// <summary>
        /// Gets or sets recommender private identifier
        /// </summary>
        public string RecommenderPin { get; }
        /// <summary>
        /// Gets or sets amount to convert
        /// </summary>
        public double Amount { get; }
        /// <summary>
        /// Gets or sets convert date
        /// </summary>
        public DateTime Time { get; }
    }
}