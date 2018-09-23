﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PolyclinicDBManager;
using PolyclinicBL;

namespace PolyclinicView
{
    public class RegistersFormPresenter
    {
        private IRegistersView iRegistersView;
        private IRegistersModel iRegistersModel;

        public RegistersFormPresenter(IRegistersView iRegistersView, IRegistersModel iRegistersModel)
        {
            if (iRegistersView is null)
            {
                throw new ArgumentNullException(String.Format("{0} is null", nameof(iRegistersView)));
            }

            if (iRegistersModel is null)
            {
                throw new ArgumentNullException(String.Format("{0} is null", nameof(iRegistersModel)));
            }


            this.iRegistersView = iRegistersView;
            this.iRegistersModel = iRegistersModel;

            iRegistersView.NewDoctor_Click += IRegistersView_NewDoctor_Click;
            iRegistersView.NewSpecialization_Click += IRegistersView_NewSpecialization_Click;
            iRegistersView.GetEntities += IRegistersView_GetEntities;
            iRegistersView.FillStreets += IRegistersView_FillStreets;
            iRegistersView.AddNewRegion += IRegistersView_AddNewRegion;
            iRegistersView.AddNewStreet += IRegistersView_AddNewStreet;
        }

        private void IRegistersView_AddNewStreet(object sender, StreetsEventHandler e)
        {
            iRegistersModel.AddNewStreet(e.RegionsId, e.StreetName);
        }

        private void IRegistersView_AddNewRegion(object sender, RegionsEventHandler e)
        {
            iRegistersModel.AddNewRegion(e.RegionNumber, e.RegionName);
        }

        private void IRegistersView_FillStreets(object sender, StreetsEventHandler e)
        {
            iRegistersView.SetStreets(iRegistersModel.GetStreetsByRegionsId(e.RegionsId));
        }

        private void IRegistersView_GetEntities(object sender, EventArgs e)
        {
            iRegistersView.SetEntities(iRegistersModel.GetPatients(), iRegistersModel.GetDoctors(), iRegistersModel.GetSpecializations(), iRegistersModel.GetRegions());
        }

        private void IRegistersView_NewSpecialization_Click(object sender, EventArgs e)
        {
            NewSpecializationPresenter newSpecializationPresenter = new NewSpecializationPresenter(iRegistersView.INewSpecializationRef, new NewSpecializationModel());
        }

        private void IRegistersView_NewDoctor_Click(object sender, EventArgs e)
        {
            NewDoctorPresenter newDoctorPresenter = new NewDoctorPresenter(iRegistersView.INewDoctorViewRef);
        }
    }
}
