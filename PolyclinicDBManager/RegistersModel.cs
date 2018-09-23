using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PolyclinicBL;

namespace PolyclinicDBManager
{
    public class RegistersModel : IRegistersModel
    {
        private ICRUDMethods iCRUDMethods = new CRUDMethods();

        public RegistersModel()
        {

        }

        public RegistersModel(ICRUDMethods crudMethods)
        {
            if (crudMethods is null)
            {
                throw new ArgumentNullException(String.Format("{0} is null", nameof(crudMethods)));
            }

            iCRUDMethods = crudMethods;
        }

        public IEnumerable GetDoctors()
        {
            return iCRUDMethods.GetDoctors();
        }

        public IEnumerable GetPatients()
        {
            return iCRUDMethods.GetPatientsFullNames();
        }

        public IEnumerable GetRegions()
        {
            return iCRUDMethods.GetRegions();
        }

        public IEnumerable GetSpecializations()
        {
            return iCRUDMethods.GetSpecializationsNames();
        }

        public IEnumerable GetStreetsByRegionsId(int regionsId)
        {
            List<string> streets = new List<string>();
            using (var context = new PolyclinicDBContext())
            {
                IQueryable<Street> query = context.Street.AsNoTracking();

                foreach (Street street in query.ToList())
                {
                    if (street.RegionNumber == regionsId)
                    {
                        streets.Add(street.StreetName);
                    }
                }
            }

            return streets;
        }

        public void AddNewStreet(int regionsId, string street)
        {
            using (var context = new PolyclinicDBContext())
            {
                IQueryable<Street> query = context.Street;

                Street Street = new Street()
                {
                    RegionNumber = regionsId,
                    StreetName = street
                };

                context.Street.Add(Street);
                context.SaveChanges();
            }
        }

        public void AddNewRegion(int regionId, string regionName)
        {
            using (var context = new PolyclinicDBContext())
            {
                IQueryable<Region> query = context.Region;

                Region region = new Region
                {
                    RegionNumber = regionId,
                    RegionName = regionName
                };

                context.Region.Add(region);
                context.SaveChanges();
            }
        }
    }
}
