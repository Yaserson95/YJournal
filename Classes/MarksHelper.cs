using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YJournal.Models;

namespace YJournal.Classes
{
    public class MarksHelper
    {
    

    public static MvcHtmlString ListMarks(IEnumerable<Marks> marks, object htmlAttributes = null)
    {
        string[] titles = { "Студент", "Присутствие", "Оценка", "Замечание", "Активность" };

        TagBuilder MTable = new TagBuilder("table");
        MTable.MergeAttributes(HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
        typeMark typeMark = new typeMark();
        DbJournal db = new DbJournal();
        //=========================Header==============================
        TagBuilder Header = new TagBuilder("thead");
        TagBuilder HeaderTr = new TagBuilder("tr");
        //Добавление заголовков
        foreach (string title in titles)
        {
            TagBuilder thr = new TagBuilder("th");
            thr.SetInnerText(title);
            HeaderTr.InnerHtml += thr;
        }
        Header.InnerHtml += HeaderTr;
        MTable.InnerHtml += Header;

        //=========================Body================================
        TagBuilder Body = new TagBuilder("tbody");
        foreach (var mark in marks)
        {
            TagBuilder BodyTr = new TagBuilder("tr");
            //Студент:
            object idLess = new { name = "LessId", type = "hidden", value = mark.LessId.ToString() };
            object idStudent = new { name = "StudentId", type = "hidden", value = mark.StudentId.ToString() };
            object presence = new { name = "Presence", type = "checkbox", value = mark.Presence.ToString() };
            object valMark = new { name = "Mark" };
            object comment = new { name = "Comments" };
            object activity = new { name = "Activity", type = "checkbox", value = mark.Activity.ToString() };

                var studentTd = new TagBuilder("td");

            var lessId = new TagBuilder("input");
            lessId.MergeAttributes(HtmlHelper.AnonymousObjectToHtmlAttributes(idLess));
            studentTd.InnerHtml += lessId;

            var studentId = new TagBuilder("input");
            studentId.MergeAttributes(HtmlHelper.AnonymousObjectToHtmlAttributes(idStudent));
            studentTd.InnerHtml += studentId;
            var user = db.Users.Find(mark.StudentId);
            var nameP = new TagBuilder("p");
                nameP.SetInnerText(user.UserName + " " + user.Surname);
                studentTd.InnerHtml += nameP;

            BodyTr.InnerHtml += studentTd;

            //Присутствие
            var PresenceTd = new TagBuilder("td");
            var presenceInp = new TagBuilder("input");
            presenceInp.MergeAttributes(HtmlHelper.AnonymousObjectToHtmlAttributes(presence));
            if (mark.Presence.Value) { presenceInp.MergeAttribute("checked", "checked"); }
            PresenceTd.InnerHtml += presenceInp;
            BodyTr.InnerHtml += PresenceTd;

            //Оценка
            var MarkTd = new TagBuilder("td");
            var markSel = new TagBuilder("select");
            markSel.MergeAttributes(HtmlHelper.AnonymousObjectToHtmlAttributes(valMark));
            foreach (var markVal in typeMark.getTypeMark())
            {
                var opt = new TagBuilder("option");
                opt.SetInnerText(markVal.Text);
                opt.MergeAttribute("value", markVal.Value);
                if (mark.Mark.ToString() == markVal.Value)
                {
                    opt.MergeAttribute("selected", "selected");
                }
                markSel.InnerHtml += opt;
            }
            MarkTd.InnerHtml += markSel;
            BodyTr.InnerHtml += MarkTd;

            //Замечание
            var commentTd = new TagBuilder("td");
            var commentInp = new TagBuilder("textarea");
            commentInp.MergeAttributes(HtmlHelper.AnonymousObjectToHtmlAttributes(comment));
            commentInp.SetInnerText(mark.Comments);
            commentTd.InnerHtml += commentInp;
            BodyTr.InnerHtml += commentTd;

            //Активность
            var activityTd = new TagBuilder("td");
            var activityInp = new TagBuilder("input");
            activityInp.MergeAttributes(HtmlHelper.AnonymousObjectToHtmlAttributes(activity));
            if (mark.Activity.Value) { activityInp.MergeAttribute("checked", "checked"); }
            activityTd.InnerHtml += activityInp;
            BodyTr.InnerHtml += activityTd;

            Body.InnerHtml += BodyTr;
        }

        MTable.InnerHtml += Body;
        return new MvcHtmlString(MTable.ToString());
    }
    }
}