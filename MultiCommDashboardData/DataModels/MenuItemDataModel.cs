using MultiCommDashboardData.Enumerations;

namespace MultiCommDashboardData.DataModels {

    /// <summary>Data to handle menu items</summary>
    public class MenuItemDataModel {

        public MenuCode Code { get; set; } = MenuCode.Language;
        public string Display { get; set; } = "NA";
        public string IconSource { get; set; } = "";
        public string Padding { get; set; } = "0";

        public MenuItemDataModel() {
        }

        public MenuItemDataModel(MenuCode code, string display, string iconSource) {
            this.Code = code;
            this.Display = display;
            this.IconSource = iconSource;
        }


        public MenuItemDataModel(MenuCode code, string display, string iconSource, string padding) :
            this(code, display, iconSource) {
            this.Padding = padding;
        }

    }

}
