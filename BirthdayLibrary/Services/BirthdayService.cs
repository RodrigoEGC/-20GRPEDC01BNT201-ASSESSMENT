using BirthdayLibrary.Models;
using BirthdayLibrary.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BirthdayLibrary.Services
{
    public class BirthdayService : IBirthdayService
    {
        private readonly IBirthdayDB _birthdayDB;

        public BirthdayService(IBirthdayDB birthdayDB)
        {
            _birthdayDB = birthdayDB;
        }
        public void Delete(int id)
        {
            _birthdayDB.Delete(id);
        }

        public IEnumerable<BirthdayModel> GetAll()
        {
            return _birthdayDB.GetAll();
        }

        public BirthdayModel GetById(int Id)
        {
            return _birthdayDB.GetById(Id);
        }

        public void Insert(BirthdayModel birthdayModel)
        {
            if(birthdayModel == null)
                throw new ArgumentNullException("Error");
            if (string.IsNullOrWhiteSpace(birthdayModel.Nome) || birthdayModel.Nome.Length < 3)
                throw new ArgumentException("Nome da pessoa inválido");
            _birthdayDB.Insert(birthdayModel);
        }

        public void Update(BirthdayModel birthdayModel)
        {
            _birthdayDB.Update(birthdayModel);
        }

        public IEnumerable<BirthdayModel> OrderByBirthday()
        {
            var birthdayList = _birthdayDB.GetAll();

            var listOrder = birthdayList.OrderBy(x => x.DataNascimento.Month > DateTime.Now.Month ||
            (x.DataNascimento.Month == DateTime.Now.Month &&
                x.DataNascimento.Day >= DateTime.Now.Day) ? 0 : 1)
                .ThenBy(x => x.DataNascimento.Month).Take(5);

            return listOrder;
        }

        public IEnumerable<BirthdayModel> BirthdaysOfTheDay()
        {
            var birthdayList = _birthdayDB.GetAll();
            DateTime todayBirthday = DateTime.Now;

            var birthdaysOfDay = birthdayList.Where(p =>
                p.DataNascimento.Day == todayBirthday.Day && 
                p.DataNascimento.Month == todayBirthday.Month
            ).ToList();

            return birthdaysOfDay;
        }

        public IEnumerable<BirthdayModel> Search(string search)
        {
            return _birthdayDB.Search(search);
        }
    }
}
