namespace Schuellerrat.Data.Seeders
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Models;

    public class ClubsSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Clubs.Any())
            {
                return;
            }

            await dbContext.Clubs.AddAsync(new Club
            {
                Title = "Jugend forscht",
                Leader = "Philipp Jansche",
                MaxClass = 11,
                MinClass = 9,
                Time = "1 път годишно",
                Article = new Article
                {
                    Title = "Jugend forscht",
                    Paragraphs = new List<Paragraph>()
                    {
                        new Paragraph
                        {
                            Title = "1",
                            Content = "Извънкласна дейност, организирана от Немския отдел. Могат да участват ученици от 10. клас нагоре.",
                        }
                    }
                }
            });

            await dbContext.SaveChangesAsync();
        }
    }
}
