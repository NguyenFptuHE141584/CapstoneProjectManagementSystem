using CapstoneProjectManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CapstoneProjectManagementSystem.Services
{
    public interface IProfessionService
    {
        List<Profession> getAllProfession(int semesterId);
        Profession getProfessionById(int professionId);
        Profession GetProfessionByName(string professionFullname, int semesterId);
        int AddProfessionThenReturnId(Profession profession, int semesterId);
        int UpdateProfession(Profession profession);
        int DeleteProfession(int id);
    }
}
