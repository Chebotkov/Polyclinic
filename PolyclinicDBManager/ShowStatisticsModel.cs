﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PolyclinicBL;

namespace PolyclinicDBManager
{
    public class ShowStatisticsModel : IShowStatisticsModel
    {
        private ICRUDMethods iCRUDMethods = new CRUDMethods();

        public ShowStatisticsModel()
        {

        }

        public ShowStatisticsModel(ICRUDMethods crudMethods)
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

        public IEnumerable GetDoctorsStatistic(int docId)
        {
            List<VisitorStatistics> list = new List<VisitorStatistics>();
            using (var context = new PolyclinicDBContext())
            {
                IQueryable<VisitorStatistics> query = context.VisitorStatistics.AsNoTracking().Where( v => v.DocId == docId);

                list = query.ToList();
            }

            return list;
        }

        public IEnumerable GetRecordedPatients(int docId, DateTime date)
        {
            throw new NotImplementedException();
        }
    }
}
