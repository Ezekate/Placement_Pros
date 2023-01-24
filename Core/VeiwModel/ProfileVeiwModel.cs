using System;
using System.Collections.Generic;
using System.Text;

namespace Core.VeiwModel
{
    public class ProfileVeiwModel
    {

        public List<SkillViewModel>? Skills { get; set; }
        public SkillViewModel Skill { get; set; }
        public List<WorkExperienceViewModel>? WorkExperiences { get; set; }
        public WorkExperienceViewModel WorkExperience { get; set; }
        public List<EducationalQualificationVeiwModel>? EducationalQualifications { get; set; }
        public EducationalQualificationVeiwModel EducationalQualification { get; set; }
        public PersonalInfoViewModel PersonalInfo { get; set; }

    }
}
