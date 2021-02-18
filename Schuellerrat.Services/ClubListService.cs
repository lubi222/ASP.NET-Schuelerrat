namespace Schuellerrat.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Data;
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
            return this.dbContext.Clubs.ToList();
        }

        public Club GetById(int id)
        {
            return this.dbContext.Clubs.FirstOrDefault(x => x.Id == id);
        }
    }
}
