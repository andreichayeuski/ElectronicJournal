using System.ComponentModel;

namespace EJ.Models.Enums
{
    public enum UserStatesEnum
    {
        [Description("Обучается")]
        Studying = 1,
        [Description("Отчислен")]
        Deducted = 2,
        [Description("В академическом отпуске")]
        OnAcademicLeave = 3
    }
}
