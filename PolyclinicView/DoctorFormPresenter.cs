﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PolyclinicBL;
using PolyclinicDBManager;
using PolyclinicBL;

namespace PolyclinicView
{
    public class DoctorFormPresenter
    {
        private IDoctorView doctorView;

        public DoctorFormPresenter(IDoctorView doctorView)
        {
            if (doctorView is null)
            {
                throw new ArgumentNullException(String.Format("{0} is null", nameof(doctorView)));
            }

            this.doctorView = doctorView;

            doctorView.OpenPatientsCard_Click += DoctorView_OpenPatientsCard_Click;
            doctorView.RecordedPatients_Click += DoctorView_RecordedPatients_Click;
            doctorView.ReferenceBook_Click += DoctorView_ReferenceBook_Click;
            doctorView.Statistics_Click += DoctorView_Statistics_Click;
        }

        private void DoctorView_OpenPatientsCard_Click(object sender, EventArgs e)
        {
            MedicalCardFormPresenter medicalCardFormPresenter = new MedicalCardFormPresenter(doctorView.IMedicalCardViewRef, new PolyclinicDBManager.Models.MedicalCardModel(), new MedicalCardManager());
        }

        private void DoctorView_RecordedPatients_Click(object sender, EventArgs e)
        {
            ListRecordingFormPresenter listRecordingFormPresenter = new ListRecordingFormPresenter(doctorView.IListRecordingViewRef, new ListRecordingModel());
        }

        private void DoctorView_Statistics_Click(object sender, EventArgs e)
        {
            ShowStatisticsPresenter showStatisticsPresenter = new ShowStatisticsPresenter(doctorView.IShowStatisticRef, new ShowStatisticsModel());
        }

        private void DoctorView_ReferenceBook_Click(object sender, EventArgs e)
        {
            ReferenceBookPresenter referenceBookPresenter = new ReferenceBookPresenter(doctorView.IReferenceBookViewRef, new ReferenceBookModel());
        }
    }
}
