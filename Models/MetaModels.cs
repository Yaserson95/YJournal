using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace YJournal.Models
{

    public class UserAccountMeta {
        [Display(Name = "Номер пользователя")]
        public int UserId;

        [Display(Name = "Имя")]
        public string Firstname;

        [Display(Name = "Фамилия")]
        public string Surname;

        [EmailAddress]
        [Display(Name = "Электронная почта")]
        public string Email;

        [Display(Name = "Номер телефона")]
        [Phone]
        public string Phone;

        [Display(Name = "Дата рождения")]
        [DataType(DataType.Date)]
        public System.DateTime Birth;

        [Display(Name = "Тип аккаунта")]
        public string Post;
    }

    public class ChangeUserDataMeta
    {
        [StringLength(40)]
        [Required]
        [Display(Name = "Имя")]
        public string Firstname;

        [StringLength(40)]
        [Required]
        [Display(Name = "Фамилия")]
        public string Surname;

        [Display(Name = "Номер телефона")]
        [Phone]
        public string Phone;

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-mm-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Дата рождения")]
        public System.DateTime Birth;
    }

    public class UsersMeta {
        [Display(Name = "Номер пользователя")]
        public int UserID;

        [Required]
        [EmailAddress]
        [Display(Name = "Электронная почта")]
        public string Email;

        [StringLength(40)]
        [Required]
        [Display(Name = "Фамилия")]
        public string Surname;

        [StringLength(40)]
        [Required]
        [Display(Name = "Имя")]
        public string UserName;

        [Required]
        [Display(Name = "Дата рождения")]
        [DataType(DataType.Date)]
        public System.DateTime Birth;

        [Display(Name = "Номер телефона")]
        [Phone]
        public string Phone;

        [Required]
        //[se]
        [Display(Name = "Тип аккаунта")]
        public int NPost;

        [StringLength(40)]
        [Required]
        [Display(Name = "Пароль")]
        [DataType(DataType.Password)]
        public string Password;

    }

    public class UsersMetaModel
    {
        [Display(Name = "Номер пользователя")]
        public int UserID;

        [Required]
        [EmailAddress]
        [Display(Name = "Электронная почта")]
        public string Email;

        [StringLength(40)]
        [Required]
        [Display(Name = "Фамилия")]
        public string Surname;

        [StringLength(40)]
        [Required]
        [Display(Name = "Имя")]
        public string Firstname;

        [Required]
        [Display(Name = "Дата рождения")]
        [DataType(DataType.Date)]
        public System.DateTime Birth;

        [Display(Name = "Номер телефона")]
        [Phone]
        public string Phone;

        [Required]
        //[se]
        [Display(Name = "Тип аккаунта")]
        public int NPost;

        [StringLength(40)]
        [Required]
        [Display(Name = "Пароль")]
        [DataType(DataType.Password)]
        public string Password;

    }

    
    public partial class ViewTeachersMeta: UsersMetaModel
    {
        [StringLength(20)]
        [Required]
        [Display(Name = "Должность")]
        public string NamePost ;

        [Display(Name = "Курирует")]
        public string OwnGroup ;
    }
    public partial class ViewStudentsMeta : UsersMetaModel
    {
        [StringLength(20)]
        [Required]
        [Display(Name = "Группа")]
        public string GroupName;

        [Required]
        [Display(Name = "Дата поступления")]
        [DataType(DataType.Date)]
        public System.DateTime ReceiptDate ;

        [Required]
        [Display(Name = "Курс")]
        public int Course ;
    }

    public partial class TeachersMeta
    {
        [Display(Name = "Номер преподавателя")]
        public int TeacherId;

        [Display(Name = "Куратор группы")]
        public Nullable<int> GroupOwn;

        [Required]
        [Display(Name = "Должность")]
        public string NamePost;
    }

    public partial class LessonsMeta
    {
        [Display(Name = "ИД занятия")]
        public int LessId;

        [Required]
        [Display(Name = "Преподаватель")]
        public int TeacherId;

        [Required]
        [Display(Name = "Тема")]
        public string Theme;

        [Required]
        [Display(Name = "Предмет")]
        public int Edu;

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        [Display(Name = "Дата занятия")]
        public System.DateTime DateLess;

        [Display(Name = "Завершен")]
        public Nullable<bool> State;

        [Required]
        [Display(Name = "Группа")]
        public int GroupId;

        [Display(Name = "Домашнее задание")]
        [StringLength(80)]
        [DataType(DataType.MultilineText)]
        public string HomeWork;
    }

    public partial class StudentsMeta
    {
        [Display(Name = "Номер преподавателя")]
        public int StudentId;

        [Required]
        [Display(Name = "Группа")]
        public int GroupId;

        [Required]
        [Display(Name = "Дата поступления")]
        [DataType(DataType.Date)]
        public System.DateTime ReceiptDate;
    }


    public partial class MarksMeta
    {
        [Display(Name = "Занятие")]
        public int LessId;

        [Display(Name = "Студент")]
        public int StudentId;

        [Display(Name = "Оценка")]
        public Nullable<int> Mark;

        [Display(Name = "Замечание")]
        public string Comments;

       
        [Display(Name = "Проявление активности")]
        public Nullable<bool> Activity;

        [Display(Name = "Присутствие")]
        public Nullable<bool> Presence;

    }
        [MetadataType(typeof(UserAccountMeta))]
    public partial class UserAccount { }
    [MetadataType(typeof(ChangeUserDataMeta))]
    public partial class GetChUserData_Result { }
    [MetadataType(typeof(UsersMeta))]
    public partial class Users{ }
    [MetadataType(typeof(ViewTeachersMeta))]
    public partial class ViewTeachers { }
    [MetadataType(typeof(ViewStudentsMeta))]
    public partial class ViewStudents { }
    [MetadataType(typeof(TeachersMeta))]
    public partial class Teachers { }
    [MetadataType(typeof(StudentsMeta))]
    public partial class Students { }
    [MetadataType(typeof(LessonsMeta))]
    public partial class Lessons { }
    [MetadataType(typeof(MarksMeta))]
    public partial class Marks { }
}