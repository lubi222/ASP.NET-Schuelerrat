namespace Schuellerrat.Data.Seeders
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Models;

    public class LinksSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Links.Any())
            {
                return;
            }

            await dbContext.Links.AddAsync(new Link()
            {
                Title = "91. Немска езикова гимназия",
                Path = "http://91neg.bg/",
                Description = "Официален сайт на 91. Немска езикова гимназия \"проф. Константин Гълъбов\""

            });
            await dbContext.Links.AddAsync(new Link()
            {
                Title = "Немски отдел",
                Path = "https://da-galabov.eu/",
                Description = "Официален сайт на немския отдел към 91. Немска езикова гимназия \"проф. Константин Гълъбов\""

            });
            await dbContext.Links.AddAsync(new Link()
            {
                Title = "Министерство на образованието и науката",
                Path = "https://mon.bg/",
                Description = ""

            });

            await dbContext.SaveChangesAsync();
        }
    }
}
