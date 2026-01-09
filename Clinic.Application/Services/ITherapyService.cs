using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Clinic.Application.DTOs;

namespace Clinic.Application.Services
{
    public interface ITherapyService
    {
        Task<List<TherapyResponse>> GetAllTherapiesAsync();
        Task<TherapyResponse> AddTherapyAsync(CreateTherapyRequest request);
        Task UpdateTherapyAsync(UpdateTherapyRequest request);
        Task DeleteTherapyAsync(int therapyId);
        Task ReactivateTherapyAsync(int therapyId);
    }
}
