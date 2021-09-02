namespace MultiCommDashboardData.Enumerations {

    public enum CodeFileCode {
        HELPERS_H,
        HELPERS_CPP,
        DEFINES_H,
        ENUMERATIONS_H,
        MESSAGES_H,
        MESSAGES_CPP,
        TEMPERATURE_PROCESSOR_H,
        TEMPERATURE_PROCESSOR_CPP,
        DEMO_MSG_BINARY_INO,
    }


    public static class CodeFileCodeExtensions {

        public static string ToFileName(this CodeFileCode code) {
            switch (code) {
                case CodeFileCode.DEFINES_H:
                    return "MsgDefines.h";
                case CodeFileCode.ENUMERATIONS_H:
                    return "MsgEnumerations.h";
                case CodeFileCode.HELPERS_H:
                    return "MessageHelpers.h";
                case CodeFileCode.MESSAGES_H:
                    return "MsgMessages.h";
                case CodeFileCode.TEMPERATURE_PROCESSOR_H:
                    return "TempProcessing.h";
                case CodeFileCode.HELPERS_CPP:
                    return "MessageHelpers.cpp";
                case CodeFileCode.MESSAGES_CPP:
                    return "MsgMessages.cpp";
                case CodeFileCode.TEMPERATURE_PROCESSOR_CPP:
                    return "TempProcessing.cpp";
                case CodeFileCode.DEMO_MSG_BINARY_INO:
                    return "DemoArduinoBT_BinaryMsg.ino";
                default:
                    return "MsgDefines.h";
            }
        }
    }



}
