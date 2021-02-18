namespace Schuellerrat.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Models;

    public interface IClubListService
    {
        public ICollection<Club> GetAll();
        public Club GetById(int id);
    }
}
