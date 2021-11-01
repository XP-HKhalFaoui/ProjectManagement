using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Layout;
using DevExpress.ExpressApp.Model.NodeGenerators;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Templates;
using DevExpress.ExpressApp.Utils;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using ProjectManagerIS.Module.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectManagerIS.Module.Controllers
{
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.
    public partial class ObjectSpaceExerciseController : ObjectViewController<ObjectView, Exercises>
    {
        public ObjectSpaceExerciseController()
        {
            InitializeComponent();
            // Target required Views (via the TargetXXX properties) and create their Actions.
        }
        protected override void OnActivated()
        {
            base.OnActivated();
            // Perform various tasks depending on the target View.
            //ObjectSpace.ObjectChanged += ObjectSpace_ObjectChanged;
        }

            

        //void ObjectSpace_ObjectChanged(object sender, ObjectChangedEventArgs e)
        //{
        //    if (View.CurrentObject == e.Object &&
        //                    e.PropertyName == "Project" &&
        //                    ObjectSpace.IsModified &&
        //                    e.NewValue != null
        //                    )
        //    {
        //        Exercises newExercise = (Exercises)e.Object;
        //        var collectionProjects = ObjectSpace.CreateCollection(typeof(Project), null);
        //        foreach (Project Projectitem in collectionProjects)
        //        {
        //            Console.WriteLine(Projectitem.Name);
        //            if ( Projectitem.Oid == newExercise.Project.Oid)
        //            {
        //                foreach (Exercises ExerciceItem in Projectitem.Exercise )
        //                {
        //                    if (ExerciceItem.Cloture == Cloture.Cloture)
        //                    {
        //                        MessageOptions options = new MessageOptions();
        //                        options.Duration = 2000;
        //                        options.Message = string.Format("{0} Project allready has an open exercise, please cloture it  ", newExercise.Project);

        //                        options.Win.Caption = "Success";
        //                        options.Win.Type = WinMessageType.Toast;
        //                        options.CancelDelegate = () => {
        //                            IObjectSpace os = Application.CreateObjectSpace(typeof(Exercises));
        //                            DetailView newTaskDetailView = Application.CreateDetailView(os, os.CreateObject<Exercises>());
        //                            Application.ShowViewStrategy.ShowViewInPopupWindow(newTaskDetailView);
        //                        };
        //                        options.Type = InformationType.Success;
        //                        options.Web.Position = InformationPosition.Right;
        //                        Application.ShowViewStrategy.ShowMessage(options);

        //                    }
        //                }
        //            }


        //        }
        //    }

        //}


        //    //IQueryable<Task> outdatedTasks = objectSpace.GetObjectsQuery<Task>().Where(t => t.DueDate < DateTime.Now);
        //    //foreach (Task task in outdatedTasks)
        //    //{
        //    //    task.AssignedTo = person;
        //    //}
        //}



        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();
            // Access and customize the target View control.
        }
        protected override void OnDeactivated()
        {
            // Unsubscribe from previously subscribed events and release other references and resources.
            base.OnDeactivated();
            //ObjectSpace.ObjectChanged += ObjectSpace_ObjectChanged;

        }
    }
}

