using CapstoneProjectManagementSystem.Models;
using CapstoneProjectManagementSystem.Models.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CapstoneProjectManagementSystem.Services.Implement
{
    public class ProfessionService : IProfessionService
    {
        public List<Profession> getAllProfession(int semesterId)
        {
            return ProfessionDao.GetAllProfession(semesterId);
        }
        public Profession getProfessionById(int professionId)
        {
            return ProfessionDao.GetProfessionById(professionId);
        }
        public Profession GetProfessionByName(string professionFullname, int semesterId)
        {
            if (professionFullname == null)
            {
                professionFullname = "";
            }
            else
            {
                professionFullname = professionFullname.Trim().Replace(" ", "").ToUpper();
            }
            return ProfessionDao.GetProfessionByName(professionFullname, semesterId);
        }
        public int AddProfessionThenReturnId(Profession profession, int semesterId)
        {
            return ProfessionDao.AddProfessionThenReturnId(profession.ProfessionAbbreviation, profession.ProfessionFullName,semesterId);
        }
        public int UpdateProfession(Profession profession)
        {
            return ProfessionDao.UpdateProfession(profession.ProfessionID, profession.ProfessionAbbreviation, profession.ProfessionFullName);
        }
        public int DeleteProfession(int id)
        {
            return ProfessionDao.DeleteProfession(id);
        }
    }
}
