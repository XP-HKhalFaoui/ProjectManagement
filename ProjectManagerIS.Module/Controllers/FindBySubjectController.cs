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
    public partial class FindBySubjectController : ViewController
    {
        public FindBySubjectController()
        {
            InitializeComponent();
            TargetViewType = ViewType.ListView;
            TargetViewNesting = Nesting.Root;
            TargetObjectType = typeof(DemoTask);

            ParametrizedAction findBySubjectAction =
                new ParametrizedAction(this, "FindBySubjectAction", PredefinedCategory.View, typeof(string))
                {
                    Caption = "Find Task By Subject",
                    NullValuePrompt = "Find Task By Subject",
                    ImageName = "Action_Search"
                };
            findBySubjectAction.Execute += FindBySubjectAction_Execute;
        }
        private void FindBySubjectAction_Execute(object sender, ParametrizedActionExecuteEventArgs e)
        {
            var objectType = ((ListView)View).ObjectTypeInfo.Type;
            IObjectSpace objectSpace = Application.CreateObjectSpace(objectType);
            string paramValue = e.ParameterCurrentValue as string;
            object obj = objectSpace.FindObject(objectType,
                CriteriaOperator.Parse("Contains([Subject], ?)", paramValue));
            if (obj != null)
            {
                DetailView detailView = Application.CreateDetailView(objectSpace, obj);
                detailView.ViewEditMode = DevExpress.ExpressApp.Editors.ViewEditMode.Edit;
                e.ShowViewParameters.CreatedView = detailView;
            }


        }
        // ...


    }
}
