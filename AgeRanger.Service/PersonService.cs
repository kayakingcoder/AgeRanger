using System.Collections.Generic;
using System.Linq;
using AgeRanger.Service.DTO;
using AgeRanger.Data;
using AgeRanger.Core;
using AgeRanger.Entity;

namespace AgeRanger.Service
{
    public class PersonService : IPersonService
    {

        AgeRangerContext context { get; set; }

        public PersonService(AgeRangerContext dbContext)
        {
            this.context = dbContext;
        }

        public IEnumerable<PersonDTO> Get(ListFilter filter)
        {
            //Must be a better way to handle themapping. AutoMapper maybe
            var result = from p in context.Person
                         from g in context.AgeGroup.Where(ag => p.Age >= ag.MinAge && p.Age < ag.MaxAge).DefaultIfEmpty()
                         select new PersonDTO()
                         {
                             Id = p.Id,
                             FirstName = p.FirstName,
                             LastName = p.LastName,
                             Age = p.Age,
                             AgeGroup = g == null ? new AgeGroupDTO() : new AgeGroupDTO()
                             {
                                 Id = g.Id,
                                 MinAge = g.MinAge,
                                 MaxAge = g.MaxAge,
                                 Description = g.Description
                             }
                         };
            if (!string.IsNullOrEmpty(filter.q))
            {
                result = result.Where(p => p.FirstName.ToLower().Contains(filter.q.ToLower()) || p.LastName.ToLower().Contains(filter.q.ToLower()));
            }
            if (filter.offset != 0)
            {
                result = result.Skip(filter.offset);
            }
            if (filter.limit != 0)
            {
                result = result.Take(filter.limit);
            }
            return result;

        }

        public PersonDTO Add(PersonDTO person)
        {
            //?? No auto increment of Person Table
            long lastId = context.Person.Max(p => ((long?)p.Id)) ?? 0;

            Person newPerson = new Person()
            {
                FirstName = person.FirstName,
                LastName = person.LastName,
                Age = person.Age,
                Id = lastId + 1
            };
            context.Person.Add(newPerson);
            context.SaveChanges();

            var ageGroup = context.AgeGroup.FirstOrDefault(g => (g.MinAge != null && g.MinAge <= person.Age && g.MaxAge != null && g.MaxAge > person.Age));

            return new PersonDTO()
            {
                Id = person.Id,
                FirstName = person.FirstName,
                LastName = person.LastName,
                Age = person.Age,
                AgeGroup = ageGroup == null ? new AgeGroupDTO() : new AgeGroupDTO()
                {
                    Id = ageGroup.Id,
                    MinAge = ageGroup.MinAge,
                    MaxAge = ageGroup.MaxAge,
                    Description = ageGroup.Description
                }
            };
        }

        public PersonDTO Update(PersonDTO person)
        {
            var dbPerson = context.Person.FirstOrDefault(p => p.Id == person.Id);

            if (dbPerson == null)
            {
                throw new KeyNotFoundException();
            }
            else
            {
                dbPerson.FirstName = person.FirstName;
                dbPerson.LastName = person.LastName;
                dbPerson.Age = person.Age;

                var ageGroup = context.AgeGroup.FirstOrDefault(g => (g.MinAge != null && g.MinAge <= person.Age && g.MaxAge != null && g.MaxAge > person.Age));

                return new PersonDTO()
                {
                    Id = dbPerson.Id,
                    FirstName = dbPerson.FirstName,
                    LastName = dbPerson.LastName,
                    Age = dbPerson.Age,
                    AgeGroup = ageGroup == null ? new AgeGroupDTO() : new AgeGroupDTO()
                    {
                        Id = ageGroup.Id,
                        MinAge = ageGroup.MinAge,
                        MaxAge = ageGroup.MaxAge,
                        Description = ageGroup.Description
                    }
                };
            }
        }
    }
}
