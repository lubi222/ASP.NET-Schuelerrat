namespace Schuellerrat.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using InputModels;
    using Models;
    using ViewModels;

    public interface IClubsService
    {
        public ICollection<ClubViewModel> GetAll();

        public Club GetById(int id);

        public Task AddClubAsync(AddClubInputModel input, string basePath);

        public Task EditClubAsync(EditClubInputModel input, string basePath);

        public ICollection<AllContentViewModel> GetClubsOnAllPage();

        public Task DeleteClubAsync(int id);
    }
}
