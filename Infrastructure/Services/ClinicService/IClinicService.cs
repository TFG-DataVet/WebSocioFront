using SocioWeb.Entities.Dtos.AuthDto;
using SocioWeb.Entities.Dtos.ClinicApiDto;

namespace SocioWeb.Infrastructure.Services.ClinicService;

public interface IClinicService
{
    Task<ClinicResponseDto>  GetByIdAsync(string clinicId);
    Task<ClinicResponseDto>  UpdateAsync(string clinicId, UpdateClinicDto dto);
    Task<TokenResponseDto>   CompleteSetupAsync(string clinicId, CompleteSetupDto dto, string onboardingToken);
}
