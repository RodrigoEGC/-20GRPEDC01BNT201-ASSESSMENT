using BirthdayLibrary.Models;
using System.Collections.Generic;

namespace BirthdayLibrary.Services
{
    public interface IBirthdayService
    {
        IEnumerable<BirthdayModel> GetAll();
        void Insert(BirthdayModel birthdayModel);
        void Update(BirthdayModel birthdayModel);
        BirthdayModel GetById(int Id);
        void Delete(int id);
        IEnumerable<BirthdayModel> OrderByBirthday();

        IEnumerable<BirthdayModel> BirthdaysOfTheDay();

        IEnumerable<BirthdayModel> Search(string search);

    }
}
