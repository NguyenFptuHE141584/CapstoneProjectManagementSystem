using CapstoneProjectManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CapstoneProjectManagementSystem.Services
{
    public interface ISpecialtyService
    {
        List<Specialty> getAllSpecialty(int semesterId);
        Specialty getSpecialtyById(int specialtyId);
        Specialty GetSpecialtyByName(string specialtyFullname, int semesterId);
        List<Specialty> getSpecialtiesByProfessionId(int professionId, int semesterId);
        int AddSpecialtyThenReturnId(Specialty specialty, int semesterId);
        int UpdateSpecialty(Specialty specialty);

        string GetCodeOfGroupNameByGroupIdeaId(int groupIdeaId);
    }
}
