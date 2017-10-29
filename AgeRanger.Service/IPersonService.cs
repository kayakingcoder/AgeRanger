using AgeRanger.Core;
using AgeRanger.Service.DTO;
using System.Collections.Generic;

namespace AgeRanger.Service
{
    public interface IPersonService
    {
        IEnumerable<PersonDTO> Get(ListFilter filter);

        PersonDTO Add(PersonDTO person);

        PersonDTO Update(PersonDTO person);
    }
}
