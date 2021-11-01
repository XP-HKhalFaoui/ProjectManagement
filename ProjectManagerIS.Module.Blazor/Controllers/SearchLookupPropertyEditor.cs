using System;
using System.Collections.Generic;
using DevExpress.Blazor;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Blazor;
using DevExpress.ExpressApp.Blazor.Components;
using DevExpress.ExpressApp.Blazor.Components.Models;
using DevExpress.ExpressApp.Blazor.Editors;
using DevExpress.ExpressApp.Blazor.Editors.Adapters;
using DevExpress.ExpressApp.Blazor.Editors.Grid;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Model;
using Microsoft.AspNetCore.Components;

namespace ProjectManagerIS.Module.Blazor.Controllers
{
    [PropertyEditor(typeof(Object), EditorAliases.LookupPropertyEditor, true)]
    public class SearchLookupPropertyEditor : BlazorPropertyEditorBase, IComplexViewItem
    {
        class InternalObjectComponentAdapter : ObjectComponentAdapter
        {
            private readonly Func<object, string> getDisplayText;
            private object value;
            private EventCallback onClick;
            public InternalObjectComponentAdapter(ObjectComponentModel componentModel, Func<object, string> getDisplayText) : base(componentModel)
            {
                this.getDisplayText = getDisplayText;
                this.onClick = componentModel.OnClick;
            }
            public override object GetValue()
            {
                return value;
            }
            public override void SetValue(object value)
            {
                this.value = value;
                ComponentModel.DisplayText = getDisplayText(value);
            }
        }
        private BlazorApplication application;
        private IObjectSpace objectSpace;
        private LookupEditorHelper helper;
        public SearchLookupPropertyEditor(Type objectType, IModelMemberViewItem model) : base(objectType, model) { }
        void IComplexViewItem.Setup(IObjectSpace objectSpace, XafApplication application)
        {
            this.objectSpace = objectSpace;
            this.application = (BlazorApplication)application;
            if (helper == null)
            {
                helper = new LookupEditorHelper(application, objectSpace, MemberInfo.MemberTypeInfo, Model);
            }
        }
        protected override IComponentAdapter CreateComponentAdapter()
        {
            return new InternalObjectComponentAdapter(new ObjectComponentModel(), propertyValue => GetDisplayText(propertyValue));
        }
        protected override void OnControlCreated()
        {
            base.OnControlCreated();
            InitControl();
        }
        protected override void OnCurrentObjectChanged()
        {
            base.OnCurrentObjectChanged();
            InitControl();
        }
        private void InitControl()
        {
            if (Control is ObjectComponentAdapter componentAdapter)
            {
                componentAdapter.ComponentModel.CssClass = "search-editor";
                SetOpenLookupHandler(componentAdapter, enabled: AllowEdit);
            }
        }
        private void SetOpenLookupHandler(ObjectComponentAdapter adapter, bool enabled)
        {
            adapter.ComponentModel.OnClick = enabled ? EventCallback.Factory.Create(this, ComponentModel_OnClick) : EventCallback.Empty;
        }
        public override void BreakLinksToControl(bool unwireEventsOnly)
        {
            if (Control is ObjectComponentAdapter componentAdapter)
            {
                SetOpenLookupHandler(componentAdapter, enabled: false);
            }
            base.BreakLinksToControl(unwireEventsOnly);
        }
        private void ComponentModel_OnClick()
        {
            ShowWindow();
        }
        private void ShowWindow()
        {
            if (!IsDisposed)
            {
                CompositeObjectSpace popupWindowObjectSpace = (CompositeObjectSpace)objectSpace.CreateNestedObjectSpace();
                if (!popupWindowObjectSpace.IsKnownType(helper.LookupObjectType))
                {
                    popupWindowObjectSpace.AdditionalObjectSpaces.Add(application.CreateObjectSpace(helper.LookupObjectType));
                    popupWindowObjectSpace.AutoDisposeAdditionalObjectSpaces = true;
                }
                helper.SetObjectSpace(popupWindowObjectSpace);
                ListView lookupListView = helper.CreateListView(helper.ObjectSpace.GetObject(CurrentObject));
                lookupListView.ControlsCreated += (s, e) => {
                    if (((ListView)s).Editor is GridListEditor gridListEditor)
                    {
                        IDxDataGridAdapter dataGridAdapter = gridListEditor.GetDataGridAdapter();
                        dataGridAdapter.DataGridModel.SelectionMode = DataGridSelectionMode.SingleSelectedDataRow;
                        dataGridAdapter.DataGridSelectionColumnModel.Visible = false;
                        if (PropertyValue != null)
                        {
                            gridListEditor.SetSelectedObjects(new List<object>() { PropertyValue }); // requires v20.2.6+
                        }
                    }
                };
                application.ShowViewStrategy.ShowViewInPopupWindow(lookupListView, () => OnPopupWindowExecute(lookupListView));
            }
        }
        private void OnPopupWindowExecute(View popupView)
        {
            PropertyValue = objectSpace.GetObject(popupView.CurrentObject);
            ReadValue();
        }
        protected override RenderFragment CreateViewComponentCore(object dataContext)
        {
            DisplayTextModel displayTextModel = new DisplayTextModel();
            displayTextModel.DisplayText = GetDisplayText(this.GetPropertyValue(dataContext));
            return DisplayTextRenderer.Create(displayTextModel);
        }
        private string GetDisplayText(object propertyValue) => helper.GetDisplayText(propertyValue, NullText, DisplayFormat);
    }
}