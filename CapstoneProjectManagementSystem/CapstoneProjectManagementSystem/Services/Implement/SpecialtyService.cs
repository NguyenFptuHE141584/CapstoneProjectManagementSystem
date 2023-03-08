using CapstoneProjectManagementSystem.Models;
using CapstoneProjectManagementSystem.Models.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CapstoneProjectManagementSystem.Services.Implement
{
    public class SpecialtyService : ISpecialtyService
    {
        public List<Specialty> getAllSpecialty(int semesterId)
        {
            return SpecialtyDao.GetAllSpecialty(semesterId);
        }
        public Specialty getSpecialtyById(int specialtyID)
        {
            return SpecialtyDao.GetSpecialtyById(specialtyID);
        }
        public Specialty GetSpecialtyByName(string specialtyFullname, int semesterId)
        {
            if (specialtyFullname == null)
            {
                specialtyFullname = "";
            }
            else
            {
                specialtyFullname = specialtyFullname.Trim().Replace(" ", "").ToUpper();
            }
            return SpecialtyDao.GetSpecialtyByName(specialtyFullname, semesterId);
        }
        public List<Specialty> getSpecialtiesByProfessionId(int professionID, int semesterId)
        {
            return SpecialtyDao.GetSpecialtiesByProfessionId(professionID,semesterId);
        }
        public int AddSpecialtyThenReturnId(Specialty specialty, int semesterId)
        {
            return SpecialtyDao.AddSpecialtyThenReturnId(specialty.SpecialtyAbbreviation, specialty.SpecialtyFullName, specialty.Profession.ProfessionID, specialty.MaxMember, specialty.CodeOfGroupName,semesterId);
        }
        public int UpdateSpecialty(Specialty specialty)
        {
            return SpecialtyDao.UpdateSpecialty(specialty.SpecialtyID,
                                                specialty.SpecialtyAbbreviation,
                                                specialty.SpecialtyFullName, 
                                                specialty.CodeOfGroupName,
                                                specialty.Profession.ProfessionID,
                                                specialty.MaxMember);
        }

        public string GetCodeOfGroupNameByGroupIdeaId(int groupIdeaId)
        {
            return SpecialtyDao.GetCodeOfGroupNameByGroupIdeaId(groupIdeaId);
        }
    }
}
