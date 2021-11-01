using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace ProjectManagerIS.Module.BusinessObjects
{
    [DefaultClassOptions]

    [RuleCriteria("End_Date_Value", DefaultContexts.Save, "StartDate < EndDate", CustomMessageTemplate = "End Date Has to be Recent than Start Date", SkipNullOrEmptyValues = true)]
    public class Exercises : BaseObject
    { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
        public Exercises(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            StartDate = DateTime.Today;
            // Place your initialization code here (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112834.aspx).


        }

        public void setDataModified()
        {
            this.dateModified = DateTime.Now;

        }

        protected override void OnSaving()
        {

            base.OnSaving();
            setDataModified();



        }

        Cloture cloture;
        private DateTime dateModified;
        bool open;
        DateTime startDate;
        string exerciceName;

        [Size(SizeAttribute.DefaultStringMappingFieldSize)]

        public string ExerciceName
        {
            get
            {
                return exerciceName;
            }
            set
            {
                SetPropertyValue(nameof(ExerciceName), ref exerciceName, value);
            }
        }


        public DateTime DateModified
        {
            get
            {
                return dateModified;
            }
            set
            {
                SetPropertyValue(nameof(DateModified), ref dateModified, value);
            }
        }

        public DateTime StartDate
        {
            get
            {
                return startDate;
            }
            set
            {
                SetPropertyValue(nameof(StartDate), ref startDate, value);
            }
        }


        [DefaultValue(Cloture.NoCloture)]
        public Cloture Cloture
        {
            get => cloture;
            set => SetPropertyValue(nameof(Cloture), ref cloture, value);
        }



        //private ClotureExercise cloture;
        //public ClotureExercise Cloture
        //{
        //    get
        //    {
        //        return cloture;
        //    }
        //    set
        //    {
        //        SetPropertyValue(nameof(Cloture), ref cloture, value);
        //    }
        //}



        //public enum ClotureExercise
        //{
        //    [ImageName("Open")]
        //    Cloture  = 1,
        //    [ImageName("Cloture")]
        //    NoCloture = 0
        //}



        private DateTime endDate;
        //[VisibleInDetailView(false)]

        public DateTime EndDate
        {
            get
            {
                return endDate;
            }
            set
            {
                SetPropertyValue(nameof(EndDate), ref endDate, value);
            }
        }





    }
    public enum Cloture
    {
        [XafDisplayName("Cloturé")]
        Cloture = 0,
        [XafDisplayName("No Cloturé")]
        NoCloture = 2
    }


}