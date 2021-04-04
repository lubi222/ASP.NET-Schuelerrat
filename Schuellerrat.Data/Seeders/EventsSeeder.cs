namespace Schuellerrat.Data.Seeders
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Models;

    public class EventsSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Events.Any())
            {
                return;
            }

            var act = dbContext.Events.AddAsync(new Event()
            {
                Title = "Среща през месец март",
                CreatedOn = DateTime.Now,
                EventDate = DateTime.Now,
                Paragraphs = new List<Paragraph>()
            {
                new Paragraph
                {
                    Title = "1",
                    Text = "Уведомяваме Ви, че ще се проведе събрание следващия вторник",
                },
            }

            }).Result;

            await dbContext.Events.AddAsync(new Event()
            {
                Title = "Немската чете",
                CreatedOn = DateTime.Now,
                EventDate = DateTime.Now,
                Paragraphs = new List<Paragraph>()
            {
                new Paragraph
                {
                    Title = "1",
                    Text = "Събитието ще се проведе през месец февруари.",
                },
                new Paragraph
                {
                    Title = "2",
                    Text = "Събитието ще има много посетители.",
                },
            }

            });
            await dbContext.Events.AddAsync(new Event()
            {
                Title = "Коледен базар",
                CreatedOn = DateTime.Now,
                EventDate = DateTime.Now,
                Paragraphs = new List<Paragraph>()
            {
                new Paragraph
                {
                    Title = "1",
                    Text = "С цел повдигане на духа и настроението на учениците!",
                },
                new Paragraph
                {
                    Title = "2",
                    Text = "Винаги има страхотни вкусотии, така че ще ви очакваме :)",
                },
            }

            });

            await dbContext.SaveChangesAsync();
        }
    }
}
