using CapstoneProjectManagementSystem.Models;
using CapstoneProjectManagementSystem.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CapstoneProjectManagementSystem.Controllers.Staff_Controller
{
    [Authorize(Roles = "Staff")]
    public class ManageMajorController : Controller
    {
        private readonly IProfessionService _professionService;
        private readonly ISpecialtyService _specialtyService;
        private readonly ISemesterService _semesterService;

        public ManageMajorController(IProfessionService professionService,
                                        ISpecialtyService specialtyService,
                                        ISemesterService semesterService)
        {
            _professionService = professionService;
            _specialtyService = specialtyService;
            _semesterService = semesterService;
        }

        public IActionResult Index()
        {
            if (_semesterService.GetCurrentSemester() != null)
            {
                int semesterId = _semesterService.GetCurrentSemester().SemesterID;
                List<Profession> professionList = _professionService.getAllProfession(semesterId);
                if (professionList is null)
                {
                    return RedirectToAction("SetupMajor", "SemesterManage");
                }
                else
                {
                    foreach (Profession pro in professionList)
                    {
                        List<Specialty> specialtyList = _specialtyService.getSpecialtiesByProfessionId(pro.ProfessionID, semesterId);
                        pro.Specialties = specialtyList;
                    }
                    ViewBag.professionList = professionList;
                    return View("/Views/Staff_View/ManageMajor/ManageMajor.cshtml");
                }
            }
            else
            {
                return RedirectToAction("Index", "SemesterManage");
            }
        }

        [HttpPost]
        public JsonResult UpdateMajor([FromBody] Profession data)
        {
            int semesterId = _semesterService.GetCurrentSemester().SemesterID;
            Profession profession = data;
            //add new Profession
            if (profession.ProfessionID == 0)
            {
                int professionId = _professionService.AddProfessionThenReturnId(profession, semesterId);
                foreach (Specialty spec in profession.Specialties)
                {
                    //add new Specialty
                    Specialty specialty = new Specialty
                    {
                        SpecialtyAbbreviation = spec.SpecialtyAbbreviation,
                        SpecialtyFullName = spec.SpecialtyFullName,
                        CodeOfGroupName = spec.CodeOfGroupName,
                        Profession = new Profession
                        {
                            ProfessionID = professionId
                        },
                        MaxMember = spec.MaxMember
                    };
                    _specialtyService.AddSpecialtyThenReturnId(specialty, semesterId);
                }
            }
            //update old Profession
            else
            {
                _professionService.UpdateProfession(profession);
                foreach (Specialty spec in profession.Specialties)
                {
                    //add new Specialty
                    if (spec.SpecialtyID == 0)
                    {
                        Specialty specialty = new Specialty
                        {
                            SpecialtyAbbreviation = spec.SpecialtyAbbreviation,
                            SpecialtyFullName = spec.SpecialtyFullName,
                            CodeOfGroupName = spec.CodeOfGroupName,
                            Profession = new Profession
                            {
                                ProfessionID = profession.ProfessionID
                            },
                            MaxMember = spec.MaxMember
                        };
                        _specialtyService.AddSpecialtyThenReturnId(specialty, semesterId);
                    }
                    //update old Specialty
                    else
                    {
                        Specialty specialty = new Specialty
                        {
                            SpecialtyID = spec.SpecialtyID,
                            SpecialtyAbbreviation = spec.SpecialtyAbbreviation,
                            SpecialtyFullName = spec.SpecialtyFullName,
                            CodeOfGroupName = spec.CodeOfGroupName,
                            Profession = new Profession
                            {
                                ProfessionID = profession.ProfessionID
                            },
                            MaxMember = spec.MaxMember
                        };
                        _specialtyService.UpdateSpecialty(specialty);
                    }
                }

            }
            return Json(true);
        }
        [HttpPost]
        public JsonResult DeleteProfession([FromBody] string data)
        {
            _professionService.DeleteProfession(Convert.ToInt32(data));
            return Json(true);
        }
    }
}
