namespace Schuellerrat.Data.Seeders
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Models;

    public class ArticlesSeeder : ISeeder

    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Articles.Any())
            {
                return;
            }

            await dbContext.Articles.AddAsync(new Article()
            {
                Title = "Среща през месец март",
                Paragraphs = new List<Paragraph>()
            {
                new Paragraph
                {
                    Title = "1",
                    Content = "Уведомяваме Ви, че ще се проведе събрание следващия вторник",
                },
            }

            });
            await dbContext.Articles.AddAsync(new Article()
            {
                Title = "Немската чете",
                Paragraphs = new List<Paragraph>()
            {
                new Paragraph
                {
                    Title = "1",
                    Content = "Събитието ще се проведе през месец февруари.",
                },
                new Paragraph
                {
                    Title = "2",
                    Content = "Събитието ще има много посетители.",
                },
            }

            });
            await dbContext.Articles.AddAsync(new Article()
            {
                Title = "Коледен базар",
                Paragraphs = new List<Paragraph>()
            {
                new Paragraph
                {
                    Title = "1",
                    Content = "С цел повдигане на духа и настроението на учениците!",
                },
                new Paragraph
                {
                    Title = "2",
                    Content = "Винаги има страхотни вкусотии, така че ще ви очакваме :)",
                },
            }

            });

            await dbContext.SaveChangesAsync();
        }
    }
}
