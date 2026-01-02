using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Clinic.Domain.Entities;

namespace Clinic.Application.Interfaces.Repositories
{
    public interface IEnquiryRepository
    {
        Task AddAsync(Enquiry enquiry);
        Task<List<Enquiry>> GetUnreadAsync();
    }

}
