using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SCERP.Domain;
using SCERP.Domain.DTOs;
using SCERP.Domain.Models;

namespace SCERP.Core.Interfaces
{
    public interface IPartnerService
    {

        Task<Partner> CreatePartnerAsync(CreatePartnerDto dto, string currentUser);
        Task<Partner> GetPartnerByIdAsync(int id);
        Task<List<Partner>> GetPartnersAsync(PartnerCategory? category = null);
        Task<Partner> UpdatePartnerAsync(int id, CreatePartnerDto dto, string currentUser);
        Task<bool> DeletePartnerAsync(int id, string currentUser);
    }
}
