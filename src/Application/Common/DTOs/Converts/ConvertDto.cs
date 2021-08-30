namespace Application.Common.DTOs.Converts
{
    public class ConvertDto
    {
        /// <summary>
        /// Gets or sets currency 
        /// </summary>
        public string From { get; set; }
        public string To { get; set; }
        /// <summary>
        /// Gets or sets converter private identifier
        /// </summary>
        public string? ConverterPin { get; set;}
        /// <summary>
        /// Gets or sets converter firstname
        /// </summary>
        public string? FirstName { get; set;}
        /// <summary>
        /// Gets or sets converter lastname
        /// </summary>
        public string? LastName { get; set;}
        /// <summary>
        /// Gets or sets recommender private identifier
        /// </summary>
        public string? RecommenderPin { get; set;}
        /// <summary>
        /// Gets or sets amount to convert
        /// </summary>
        public double Amount { get; set;}
    }
}