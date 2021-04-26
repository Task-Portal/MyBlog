using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using MyBlog.ViewModels;

namespace MyBlog.TagHelper
{
    public class PageLinkTagHelper : Microsoft.AspNetCore.Razor.TagHelpers.TagHelper
    {
        private IUrlHelperFactory urlHelperFactory;

        [ViewContext] [HtmlAttributeNotBound] public ViewContext ViewContext { get; set; }
        public PageVM PageModel { get; set; }
        public string PageAction { get; set; }

        public PageLinkTagHelper(IUrlHelperFactory helperFactory)
        {
            urlHelperFactory = helperFactory;
        }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            var urlHelper = urlHelperFactory.GetUrlHelper(ViewContext);
            output.TagName = "div";

            // набор ссылок будет представлять список ul
            var tag = new TagBuilder("ul");
            tag.AddCssClass("pagination");

            // формируем три ссылки - на текущую, предыдущую и следующую
            var currentItem = CreateTag(PageModel.PageNumber, urlHelper);

            // создаем ссылку на предыдущую страницу, если она есть
            if (PageModel.HasPreviousPage)
            {
                var prevItem = CreateTag(PageModel.PageNumber - 1, urlHelper);
                tag.InnerHtml.AppendHtml(prevItem);
            }

            tag.InnerHtml.AppendHtml(currentItem);
            // создаем ссылку на следующую страницу, если она есть
            if (PageModel.HasNextPage)
            {
                var nextItem = CreateTag(PageModel.PageNumber + 1, urlHelper);
                tag.InnerHtml.AppendHtml(nextItem);
            }

            output.Content.AppendHtml(tag);
        }

        private TagBuilder CreateTag(int pageNumber, IUrlHelper urlHelper)
        {
            var item = new TagBuilder("li");
            var link = new TagBuilder("a");
            if (pageNumber == PageModel.PageNumber)
                item.AddCssClass("active");
            else
                link.Attributes["href"] = urlHelper.Action(PageAction, new {pageNumber});
            item.AddCssClass("page-item");
            link.AddCssClass("page-link");
            link.InnerHtml.Append(pageNumber.ToString());
            item.InnerHtml.AppendHtml(link);
            return item;
        }
    }
}
