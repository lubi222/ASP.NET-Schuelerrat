namespace Schuellerrat.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Data;
    using Microsoft.EntityFrameworkCore;
    using Models;

    public class ClubListService : IClubListService
    {
        private readonly ApplicationDbContext dbContext;

        public ClubListService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public ICollection<Club> GetAll()
        {
            return this.dbContext.Clubs.Include(x => x.Article).Include(x => x.Article.Paragraphs).Include(x => x.Article.Images).ToList();
        }

        public Club GetById(int id)
        {
            return this.dbContext.Clubs.FirstOrDefault(x => x.Id == id);
        }
    }
}
