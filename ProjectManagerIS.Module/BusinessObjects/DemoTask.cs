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
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.AuditTrail;


namespace ProjectManagerIS.Module.BusinessObjects
{
    [Appearance("FontColorGreen", AppearanceItemType = "ViewItem", TargetItems = "*", Context = "ListView",
    Criteria = "Status ='Completed'", FontColor = "Green")]

    [Appearance("FontColorRed", AppearanceItemType = "ViewItem", TargetItems = "*", Context = "ListView",
    Criteria = "Status ='NotStarted'", FontColor = "Red")]

   

    [DefaultClassOptions]
    [ModelDefault("Caption", "Task")]
    public class DemoTask : Task 
    {
        public DemoTask(Session session) : base(session) { }
        

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            Priority = Priority.Normal;

        }




        [Association("Contact-DemoTask")]
        public XPCollection<Employee> Contacts
        {
            get
            {
                return GetCollection<Employee>(nameof(Contacts));
            }
        }

        private Priority priority;
        public Priority Priority
        {
            get { return priority; }
            set
            {
                SetPropertyValue(nameof(Priority), ref priority, value);
            }
        }

        [Action(Caption = "Postpone", TargetObjectsCriteria = "[TaskStatus] Is not [Completed]")]
        public void Postpone(PostponeParametersObject parameters)
        {
            if ((parameters.PostponeForDays > 0))
            {
                DueDate = DueDate + TimeSpan.FromDays(parameters.PostponeForDays);
                Description += String.Format("Postponed for {0} days, new deadline is {1:d}\r\n{2}\r\n",
                parameters.PostponeForDays, DueDate, parameters.Comment);
                
                Status = DevExpress.Persistent.Base.General.TaskStatus.Deferred;
            }
        }

        [DevExpress.Xpo.Aggregated, Association("DemoTask-PortfolioFileData")]
        [DevExpress.Xpo.DisplayName("File Data")]

        public XPCollection<PortfolioFileData> Portfolio
        {
            get { return GetCollection<PortfolioFileData>(nameof(Portfolio)); }
        }


        Project projectName;
        [Association("DemoTask-Project")]
        [System.ComponentModel.DisplayName("Project")]
        [RuleRequiredField("RuleRequiredField for Task.Project", DefaultContexts.Save,
      "A Project must be specified")]
        public Project ProjectName
        {
            get
            {
                return projectName;
            }
            set
            {
                SetPropertyValue<Project>(nameof(ProjectName), ref projectName, value);
            }
        }

        Exercises exercise;
        [Association("Exercises-DemTask")]
        [DataSourceProperty("ProjectName.Exercise", DataSourcePropertyIsNullMode.SelectAll )]
        
        
       
        [ImmediatePostData]

        public Exercises Exercise
        {
            get
            {
                return exercise;
            }
            set
            {
                SetPropertyValue(nameof(Exercise), ref exercise, value);
            }
        }

        private Resolution resolutions;
        public Resolution Resolutions
        {
            get { return resolutions; }
            set { SetPropertyValue(nameof(Resolutions), ref resolutions, value); }
        }

        private XPCollection<AuditDataItemPersistent> changesHistpry;
        [CollectionOperationSet(AllowAdd = false, AllowRemove = false)]
        public XPCollection<AuditDataItemPersistent> AuditTrail
        {
            get
            {
                if (changesHistpry == null)
                {
                    changesHistpry = AuditedObjectWeakReference.GetAuditTrail(Session, this);
                }
                return changesHistpry;
            }
        }
    }

    

    public enum Resolution
    {
        [XafDisplayName("Need more information")]
        Need_More_Info = 0,
        [XafDisplayName("Not resolved")]
        Need_More_Info2 = 1,
        [XafDisplayName("pending")]
        Need_More_Info3 = 2
    }
    public enum Priority
    {
        [ImageName("State_Priority_Low")]
        Low = 0,
        [ImageName("State_Priority_Normal")]
        Normal = 1,
        [ImageName("State_Priority_High")]
        High = 2
    }
}