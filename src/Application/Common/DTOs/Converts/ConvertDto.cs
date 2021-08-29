namespace Application.Common.DTOs.Converts
{
    public class ConvertDto
    {
        public string From { get; set; }
        public string To { get; set; }
        public string? ConverterPin { get; set;}
        public string? FirstName { get; set;}
        public string? LastName { get; set;}
        public string? RecommenderPin { get; set;}
        public double Amount { get; set;}
    }
}