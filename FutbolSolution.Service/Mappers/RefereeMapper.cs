using FutbolSolution.Core.DTOs.RefereeDTOs;
using FutbolSolution.Core.Mapper;
using FutbolSolution.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FutbolSolution.Service.Mappers
{
    public class RefereeMapper : IRefereeMapper
    {
        public Referee Map(BaseRefereeDTO source, object parameter)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (source is RefereeDTO refereeDTO)
            {
                return MapRefereeDTOToReferee(refereeDTO);
            }
            else
            {
                throw new InvalidOperationException("Unsupported DTO type.");
            }
        }

        public BaseRefereeDTO Map(Referee destination, object parameter)
        {
            if (destination == null)
            {
                throw new ArgumentNullException(nameof(destination));
            }

            return MapRefereeToRefereeDTO(destination);
        }

        private Referee MapRefereeDTOToReferee(RefereeDTO refereeDTO)
        {
            return new Referee
            {
                RefereeId = refereeDTO.RefereeId,
                Name = refereeDTO.Name,
                Surname = refereeDTO.Surname,
                Nationality = refereeDTO.Nationality,
                ExperienceYears = refereeDTO.ExperienceYears,
                Bias = refereeDTO.Bias
            };
        }

        private RefereeDTO MapRefereeToRefereeDTO(Referee referee)
        {
            return new RefereeDTO
            {
                RefereeId = referee.RefereeId,
                Name = referee.Name,
                Surname = referee.Surname,
                Nationality = referee.Nationality,
                ExperienceYears = referee.ExperienceYears,
                Bias = referee.Bias
            };
        }
    }


}
