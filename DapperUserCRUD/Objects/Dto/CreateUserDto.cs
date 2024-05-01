using System.ComponentModel.DataAnnotations;

namespace DapperUserCRUD.Objects.Dto
{
    public class CreateUserDto
    {
        [Required(ErrorMessage = "Укажите логин")]
        [MaxLength(100, ErrorMessage = "Логин должен быть меньше 100 символов")]
        [MinLength(4, ErrorMessage = "Логин должен быть больше 4 символов")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Введите пароль")]
        [MaxLength(100, ErrorMessage = "Пароль должен иметь длину меньше 100 символов")]
        [MinLength(5, ErrorMessage = "Пароль должен иметь длину больше 5 символов")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Подтвердите пароль")]
        public string PasswordConfirm { get; set; }

        [Required(ErrorMessage = "Введите почту")]
        [EmailAddress(ErrorMessage = "Некорректный формат почты")]
        [MaxLength(20, ErrorMessage = "Почта должна иметь длину меньше 20 символов")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Введите возраст")]
        [Range(0, 150, ErrorMessage = "Недопустимый возраст")]
        public int Age { get; set; }
    }
}
