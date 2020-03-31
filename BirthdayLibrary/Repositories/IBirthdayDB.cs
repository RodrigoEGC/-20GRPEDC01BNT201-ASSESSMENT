using BirthdayLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BirthdayLibrary.Repositories
{
    public interface IBirthdayDB
    {
        IEnumerable<BirthdayModel> GetAll();
        void Insert(BirthdayModel birthdayModel);
        void Update(BirthdayModel birthdayModel);
        BirthdayModel GetById(int Id);
        void Delete(int id);
        IEnumerable<BirthdayModel> Search(string search);
    }
}
