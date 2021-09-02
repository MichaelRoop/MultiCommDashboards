using MultiCommDashboardData.Enumerations;

namespace MultiCommDashboardData.DataModels {

    public class CodeSelectDisplayDataModel {

        public CodeFileCode Code { get; set; } = CodeFileCode.DEFINES_H;

        public string Name { get; set; } = CodeFileCode.DEFINES_H.ToFileName();

        public CodeSelectDisplayDataModel() {
        }

        public CodeSelectDisplayDataModel(CodeFileCode code) {
            this.Code = code;
            this.Name = code.ToFileName();
        }

    }
}
