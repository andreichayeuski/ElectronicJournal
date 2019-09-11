using System.ComponentModel;

namespace EJ.Models.Enums
{
    public enum TeacherDegreeEnum
    {
        [Description("н/о")]
        Неизвестен = 0,
        [Description("асс.")]
        Ассистент = 1,
        [Description("проф.")]
        Профессор = 2,
        [Description("доц.")]
        Доцент = 3,
        [Description("ст.преп.")]
        СтаршийПреподаватель = 4
    }
}
