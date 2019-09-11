using System.Collections.Generic;
using Trirand.Web.Core.Trirand.Web.Core;

namespace SHARED.Models
{
    public class JqGridSettingsModel
    {
        public JqGridSettingsModel()
        {
            NeedInitializedEvent = true;

            NeedAddWrappingDialog = true;
            NeedEditWrapingDialog = true;
            NeedViewWrappingDialog = true;

            NeedAddButton = true;
            NeedEditButton = true;
            NeedDeleteButton = true;
            NeedRestoreButton = false;
            NeedOpenButton = false;

            ViewAction = "View";
            EditAction = "Edit";
            DeleteAction = "Delete";
            RestoreAction = "Restore";
            OpenAction = "Open";
            ExportAction = "ExportToExcel";

            IdentificatorParamName = "id";

            ViewActionAdditionalParameters = new KeyValuePair<string, object>[0];
            EditActionAdditionalParameters = new KeyValuePair<string, object>[0];
            DeleteActionAdditionalParameters = new KeyValuePair<string, object>[0];
            RestoreActionAdditionalParameters = new KeyValuePair<string, object>[0];
            OpenActionAdditionalParameters = new KeyValuePair<string, object>[0];

            AddButtonClass = "addButton";
            ViewButtonClass = "viewButton";
            EditButtonClass = "editButton";
            DeleteButtonClass = "deleteButton";
            RestoreButtonClass = "restoreButton";
            OpenButtonClass = "openButton";

            AddButtonText = "Добавить";
            ViewButtonText = "Просмотреть";
            EditButtonText = "Редактировать";
            DeleteButtonText = "Удалить";
            RestoreButtonText = "Восстановить";
            OpenButtonText = "Открыть";

            AddDialogTitle = "Добавление";
            //EditDialogTitle = "Редактирование";
            ViewDialogTitle = "Просмотр";

            InitializationFunctionName = "gridInitialized";
            //GridReloadFunctionName = "reloadGrid";

            ViewDialogWidth = 900;
            EditDialogWidth = 900;
            MinEditDialogWidth = 0;
            DialogResizable = "true";

            AfterInitializedEvents = new string[0];
            NeedCloseEditWindowAfterSave = true;

            FormatEditFunctionName = "formatEdit";

            EditButtonCondition = "true";
            ViewButtonCondition = "true";
            DeleteButtonCondition = "true";

            AddIconClass = "fa-plus";
            ViewIconClass = "fa-eye";
            EditIconClass = "fa-edit";
            DeleteIconClass = "fa-remove";
            AlertifyMessage = "Вы действительно хотите удалить этот объект?";

            NeedSaveGridButton = true;
            NeedExportButton = false;
            ExportContainerHtmlMarkup = string.Empty;
            ExportActionAdditionalParameters = new KeyValuePair<string, object>[0];
            BeforeRequestEvents = new string[0];
            DeleteDialogText = "Вы действительно хотите удалить этот объект?";
            ShowSaveStateButton = true;
        }

        public CoreGrid JqGrid { get; set; }

        private string _jqGridName;

        public string JqGridName
        {
            get
            {
                if (!string.IsNullOrEmpty(_jqGridName))
                {
                    return _jqGridName;
                }
              //  InitializationFunctionName = "gridInitialized" + "_" + JqGrid.ID;
                return JqGrid != null ? JqGrid.ID : "JqGrid";
            }
            set
            {
                //InitializationFunctionName = "gridInitialized" + "_" + value;
                _jqGridName = value;
            }
        }
        public bool ShowSaveStateButton { get; set; }
        public bool NeedInitializedEvent { get; set; }

        public bool NeedViewWrappingDialog { get; set; }
        public bool NeedAddWrappingDialog { get; set; }
        public bool NeedEditWrapingDialog { get; set; }

        public bool NeedViewButton { get; set; }
        public bool NeedAddButton { get; set; }
        public bool NeedEditButton { get; set; }
        public bool NeedDeleteButton { get; set; }
        public bool NeedRestoreButton { get; set; }
        public bool NeedOpenButton { get; set; }


        public string ViewAction { get; set; }
        public string EditAction { get; set; }
        public string DeleteAction { get; set; }
        public string RestoreAction { get; set; }
        public string OpenAction { get; set; }

        public IEnumerable<KeyValuePair<string, object>> ViewActionAdditionalParameters { get; set; }
        public IEnumerable<KeyValuePair<string, object>> EditActionAdditionalParameters { get; set; }
        public IEnumerable<KeyValuePair<string, object>> DeleteActionAdditionalParameters { get; set; }
        public IEnumerable<KeyValuePair<string, object>> RestoreActionAdditionalParameters { get; set; }
        public IEnumerable<KeyValuePair<string, object>> OpenActionAdditionalParameters { get; set; }

        public string IdentificatorParamName { get; set; }

        public string AddButtonClass { get; set; }
        public string ViewButtonClass { get; set; }
        public string EditButtonClass { get; set; }
        public string DeleteButtonClass { get; set; }
        public string RestoreButtonClass { get; set; }
        public string OpenButtonClass { get; set; }


        public string AddButtonText { get; set; }
        public string ViewButtonText { get; set; }
        public string EditButtonText { get; set; }
        public string DeleteButtonText { get; set; }
        public string DeleteDialogText { get; set; }
        public string RestoreButtonText { get; set; }
        public string OpenButtonText { get; set; }

        public string AddDialogTitle { get; set; }
        public string ViewDialogTitle { get; set; }
        public string EditDialogTitle { get; set; }


        public string InitializationFunctionName { get; set; }
        public string GridReloadFunctionName { get; set; }

        public string Controller { get; set; }
        public string Area { get; set; }

        public int ViewDialogWidth { get; set; }
        public int EditDialogWidth { get; set; }
        public int MinEditDialogWidth { get; set; }

        public string DialogResizable { get; set; }

        public string[] AfterInitializedEvents { get; set; }

        public string SubmitClickEventBefore { get; set; }


        public string CustomViewTemplate { get; set; }
        public string CustomEditTemplate { get; set; }

        public bool NeedCustomFormatters { get; set; }

        public bool NeedCloseEditWindowAfterSave { get; set; }
        public string FormatEditFunctionName { get; set; }
        /// <summary>
        /// javascripts condition which will be used in if clause, like "rowObject[1] == 'True'", possible vars: cellValue, options, rowObject
        /// </summary>
        public string EditButtonCondition { get; set; }
        public string ViewButtonCondition { get; set; }
        public string DeleteButtonCondition { get; set; }

        public string AddIconClass { get; set; }
        public string ViewIconClass { get; set; }
        public string EditIconClass { get; set; }
        public string DeleteIconClass { get; set; }
        public string RestoreIconClass { get; set; }
        public string OpenIconClass { get; set; }
        public string AlertifyMessage { get; set; }

        public bool NeedSaveGridButton { get; set; }
        public bool NeedExportButton { get; set; }
        public string ExportAction { get; set; }
        public IEnumerable<KeyValuePair<string, object>> ExportActionAdditionalParameters { get; set; }
        public string ExportContainerHtmlMarkup { get; set; }

        public string[] BeforeRequestEvents { get; set; }
        public bool NeedStretch { get; set; }
        public string CustomEditFormatterMarkup { get; set; }
    }
}
