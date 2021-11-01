﻿using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Layout;
using DevExpress.ExpressApp.Model.NodeGenerators;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Templates;
using DevExpress.ExpressApp.Utils;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using ProjectManagerIS.Module.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectManagerIS.Module.Controllers
{
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.
    public partial class DemoTaskObejctSpaceControler : ObjectViewController<ObjectView, DemoTask>
    {
        public IMemberInfo IsAggregated { get; private set; }

        public DemoTaskObejctSpaceControler()
        {
            InitializeComponent();
            // Target required Views (via the TargetXXX properties) and create their Actions.


        }
        protected override void OnActivated()
        {
            base.OnActivated();
            // Perform various tasks depending on the target View.
            ObjectSpace.ObjectChanged += ObjectSpace_ObjectChanged;





        }

        void ObjectSpace_ObjectCommiting(object sender, ObjectChangedEventArgs e)
        {
            if (View.CurrentObject == e.Object && ObjectSpace.IsCommitting)
            {

            }
        }

        



        void ObjectSpace_ObjectChanged(object sender , ObjectChangedEventArgs e)
        {
            //IList<SortProperty> GetCollectionSorting(object collection);
            //object GetObjectKey(Type objectType, string objectKeyString);


            if (View.CurrentObject == e.Object &&
                e.PropertyName == "AssignedTo" &&
                ObjectSpace.IsModified &&
                e.OldValue != e.NewValue){
                DemoTask TaskChanged = (DemoTask)e.Object;
                    if (TaskChanged.AssignedTo != null)
                        {
                            TaskChanged.Description += "The task has be Assigned to " + TaskChanged.AssignedTo;
                        }
            
                }
                
                
        }

        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();
            // Access and customize the target View control.
        }
        protected override void OnDeactivated()
        {
            // Unsubscribe from previously subscribed events and release other references and resources.
            base.OnDeactivated();
            ObjectSpace.ObjectChanged += ObjectSpace_ObjectChanged;
        }

        protected override void BeginUpdate()
        {
            base.BeginUpdate();
        }

        protected override void EndUpdate()
        {
            base.EndUpdate();
        }

        protected override void OnAfterConstruction()
        {
            base.OnAfterConstruction();
        }


        protected override void OnFrameAssigned()
        {
            base.OnFrameAssigned();
        }


        protected override void OnViewChanged()
        {
            base.OnViewChanged();
        }


    }
}
