using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PolyclinicBL;

namespace PolyclinicDBManager
{
    public class NewSpecializationModel : INewSpecializationModel
    {
        public IEnumerable GetSpecializations()
        {
            List<Specialization> specializations;
            using (var context = new PolyclinicDBContext())
            {
                IQueryable<Specialization> query = context.Specialization.AsNoTracking();
                specializations = query.ToList();
            }

            return specializations;
        }

        public IEnumerable GetSpecializationsNames()
        {
            var specializations = GetSpecializations();

            List<string> specializationsNames = new List<string>();

            foreach (Specialization specialization in specializations)
            {
                specializationsNames.Add(specialization.id + "." + specialization.SpecializationName);
            }

            return specializationsNames;
        }
    }
}
