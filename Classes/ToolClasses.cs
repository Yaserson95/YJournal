using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YJournal.Models;

namespace YJournal.Classes {

    public class UserInfo {
        private SelectList List_posts;
        private Users CurUser = null;
        private string curEmail = "";
        private DbJournal db = new DbJournal();
        public string[] Nposts = {
                "Главный администратор",
                "Администратор",
                "Преподаватель",
                "Студент",
                "Обычный пользователь"
        };
        public string getUserPost(int p) {
            if (p >= 0 && p < Nposts.Count()) {
                return Nposts[p];
            } return "Неизвестная категория";
        }
        public UserInfo() {
            var list = new List<SelectListItem>();
            for (int i = 0; i < Nposts.Length; i++)
            {
                //{ Text = "Homeowner", Value = ((int)1).ToString()}
                list.Add(new SelectListItem { Text = Nposts[i], Value = ((int)i).ToString() });
            }
            List_posts = new SelectList(list, "Value", "Text");

        }
        public SelectList getPostsList() {
            return List_posts;
        }
        public Users setCurentUser(string Email) {
            if (CurUser == null || curEmail != Email) {
                CurUser = db.Users.Where(p => p.Email == Email).First();
                curEmail = Email;
            }
            return CurUser;
        }


    }
    public class HtmlForms {
        public string getCheckbox(string Name, string Value, bool Checked) {
            string CheckBox = "<input type='checkbox' name='" + Name + "' value='" + Value + "'";
            if (Checked) CheckBox += "checked = 'checked'";
            CheckBox += "/>";
            return CheckBox;
        }

        //public string getDropList() {
        //    string DropList;

        //    return DropList;
        //}

    }
    public class typeMark{
        private List<SelectListItem> list = new List<SelectListItem>();
        public typeMark()
        {
            list.Add(new SelectListItem { Value = "", Text = "Нет" });
            list.Add(new SelectListItem { Value = "5", Text = "Отлично" });
            list.Add(new SelectListItem { Value = "4", Text = "Хорошо" });
            list.Add(new SelectListItem { Value = "3", Text = "Удовлетворительно" });
            list.Add(new SelectListItem { Value = "2", Text = "Неудовлетворительно" });
        }
        public List<SelectListItem> getTypeMark() {
            return list;
        }
    }
    public class tools {
        public static string getAnsw(bool b) {
            if (b) return "Да";
            else return "Нет";
        }
    }

}
 