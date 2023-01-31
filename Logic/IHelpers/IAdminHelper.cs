using Core.Models;
using Core.VeiwModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Logic.IHelpers
{
   public interface IAdminHelper
    {
        bool CreateDropdown(CreateDropDownViewModel dropdown);
        Task<Job> CreateJob(JobVeiwModel jobUpload);
        List<JobVeiwModel> GetJobs();
        bool DeList(Guid id);
        List<JobApplications> GetApplicationJobs();


    }
}
