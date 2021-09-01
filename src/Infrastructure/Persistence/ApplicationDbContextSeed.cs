using System.Threading.Tasks;
using Domain.Entities;

namespace Infrastructure.Persistence
{
    public static class ApplicationDbContextSeed
    {
        public static async Task SeedSampleDataAsync(ApplicationDbContext context)
        {
            await context.Currencies.AddRangeAsync(new Currency("EUR", "ევრო","Euro"),
                new Currency("USD", "აშშ დოლარი", "United States dollar"),
                new Currency("GBP", "დიდი ბრიტანეთის გირვანქა სტერლინგი", "British pound"),
                new Currency("CNY", "ჩინური იუანი", "Chinese yuan"),
                new Currency("JPY", "იაპონური იენი", "Japanese yen"));
            await context.SaveChangesAsync();
        }
    }
}