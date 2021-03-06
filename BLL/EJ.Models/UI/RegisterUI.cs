using System;
using System.ComponentModel.DataAnnotations;

namespace EJ.Models.UI
{
    public class RegisterUI
    {
        [Required(ErrorMessage = "Не указан Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Не указан пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Пароль и подтверждение пароля не совпадают")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Не указано имя")]
        public string FName { get; set; }

        [Required(ErrorMessage = "Не указано отчество")]
        public string MName { get; set; }

        [Required(ErrorMessage = "Не указана фамилия")]
        public string SName { get; set; }

        [Required(ErrorMessage = "Не указана дата рождения")]
        public DateTime Birthday { get; set; }

        [Required(ErrorMessage = "Не указан персональный номер")]
        public string PersonalNumber { get; set; }

        [Required(ErrorMessage = "Не указана дата начала обучения")]
        public DateTime StartDate{ get; set; }

        [Required(ErrorMessage = "Выберите пол")]
        public bool Sex { get; set; }

        [Required(ErrorMessage = "Выберите группу")]
        public int GroupId { get; set; }
    }
}