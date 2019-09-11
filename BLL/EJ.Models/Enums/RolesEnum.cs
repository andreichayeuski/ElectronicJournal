using System.ComponentModel;

namespace EJ.Models.Enums
{
    public enum RolesEnum
    {
        [Description("Не авторизован")]
        UnAuthorized = 0,
        [Description("Студент")]
        Студент = 1,
        [Description("Заместитель старосты")]
        ЗаместительCтаросты = 2,
        [Description("Староста")]
        Староста = 3,
        [Description("Администратор")]
        Администратор = 4,
        [Description("Преподаватель")]
        Преподаватель = 5
    }
}
