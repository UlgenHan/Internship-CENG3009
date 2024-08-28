using FutbolSolution.Core.DTOs.InjuresSuspensionDTOs;
using FutbolSolution.Core.DTOs.PlayerDTOs;
using FutbolSolution.Core.Mapper;
using FutbolSolution.Core.Models;
using System;

namespace FutbolSolution.Service.Mappers
{
    namespace FutbolSolution.Service.Mappers
    {
        public class InjuriesSuspensionsMapper : IMapper<BaseInjuriesSuspensionsDTO, InjuriesSuspensions>
        {
            public InjuriesSuspensions Map(BaseInjuriesSuspensionsDTO source, object parameter = null)
            {
                if (source == null) throw new ArgumentNullException(nameof(source));

                if (source is InjuriesSuspensionsDTO injuriesSuspensionsDTO)
                {
                    return MapInjuriesSuspensionsDTOToInjuriesSuspensions(injuriesSuspensionsDTO);
                }
                else
                {
                    throw new InvalidOperationException("Unsupported DTO type.");
                }
            }

            public BaseInjuriesSuspensionsDTO Map(InjuriesSuspensions source, object parameter = null)
            {
                if (source == null) throw new ArgumentNullException(nameof(source));

                if (source is InjuriesSuspensions injuriesSuspensions)
                {
                    return MapInjuriesSuspensionsToInjuriesSuspensionsDTO(injuriesSuspensions);
                }
                else
                {
                    throw new InvalidOperationException("Unsupported InjuriesSuspensions type.");
                }
            }

            private InjuriesSuspensions MapInjuriesSuspensionsDTOToInjuriesSuspensions(InjuriesSuspensionsDTO injuriesSuspensionsDTO)
            {
                return new InjuriesSuspensions
                {
                    InjurySuspensionId = injuriesSuspensionsDTO.InjurySuspensionId,
                    PlayerId = injuriesSuspensionsDTO.PlayerId,
                    Type = injuriesSuspensionsDTO.Type,
                    Description = injuriesSuspensionsDTO.Description,
                    StartDate = injuriesSuspensionsDTO.StartDate,
                    EndDate = injuriesSuspensionsDTO.EndDate
                };
            }

            private InjuriesSuspensionsDTO MapInjuriesSuspensionsToInjuriesSuspensionsDTO(InjuriesSuspensions injuriesSuspensions)
            {
                return new InjuriesSuspensionsDTO
                {
                    InjurySuspensionId = injuriesSuspensions.InjurySuspensionId,
                    PlayerId = injuriesSuspensions.PlayerId,
                    Type = injuriesSuspensions.Type,
                    Description = injuriesSuspensions.Description,
                    StartDate = injuriesSuspensions.StartDate,
                    EndDate = injuriesSuspensions.EndDate
                };
            }
        }
    }

}
