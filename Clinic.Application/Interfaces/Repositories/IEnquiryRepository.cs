using Clinic.Domain.Entities;

namespace Clinic.Application.Interfaces.Repositories
{
    public interface IEnquiryRepository
    {
        Task AddAsync(Enquiry enquiry);
        Task<List<Enquiry>> GetUnreadAsync();
    }

}
