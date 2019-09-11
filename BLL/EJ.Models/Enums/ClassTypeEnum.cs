using System.ComponentModel;

namespace EJ.Models.Enums
{
    public enum ClassTypeEnum
    {
        [Description("Не определён")]
        но = 0,
        [Description("Лабораторная работа")]
        лр = 1,
        [Description("Практическое занятие")]
        пз = 2,
        [Description("Лекция")]
        лк = 3,
        [Description("Курсовой проект")]
        кп = 4
    }
}
