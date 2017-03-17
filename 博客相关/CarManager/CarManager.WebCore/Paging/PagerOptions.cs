using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;

namespace CarManager.WebCore.Paging
{
  ///<summary>
  /// Options for configuring the output of <see cref = "CarManager.WebCore.Paging.PagerExtensions" />.
  ///</summary>
  public class PagerOptions
  {
    ///<summary>
    /// The default settings render all navigation links and no descriptive text.
    ///</summary>
    public PagerOptions()
    {
      DisplayLinkToFirstPage = PagerBehavior.IfNeeded;
      DisplayLinkToLastPage = PagerBehavior.IfNeeded;
      DisplayLinkToPreviousPage = PagerBehavior.IfNeeded;
      DisplayLinkToNextPage = PagerBehavior.IfNeeded;
      DisplayLinkToIndividualPages = true;
      DisplayPageCountAndCurrentLocation = false;
      MaximumPageNumbersToDisplay = 10;
      DisplayEllipsesWhenNotShowingAllPageNumbers = true;
      EllipsesFormat = "&#8230;";
      LinkToFirstPageFormat = "<<";
      LinkToPreviousPageFormat = "<";
      LinkToIndividualPageFormat = "{0}";
      LinkToNextPageFormat = ">";
      LinkToLastPageFormat = ">>";
      PageCountAndCurrentLocationFormat = "Page {0} of {1}.";
      ItemSliceAndTotalFormat = "Showing items {0} through {1} of {2}.";
      FunctionToDisplayEachPageNumber = null;
      ClassToApplyToFirstListItemInPager = null;
      ClassToApplyToLastListItemInPager = null;
      ContainerDivClasses = new[] { "pagination-container" };
      UlElementClasses = new[] { "pagination" };
      LiElementClasses = Enumerable.Empty<string>();
      PageClasses = Enumerable.Empty<string>();
      UlElementattributes = null;
      ActiveLiElementClass = "active";
      EllipsesElementClass = "PagedList-ellipses";
      PreviousElementClass = "PagedList-skipToPrevious";
      NextElementClass = "PagedList-skipToNext";
    }

    ///<summary>
    /// CSS Classes to append to the &lt;div&gt; element that wraps the paging control.
    ///</summary>
    public IEnumerable<string> ContainerDivClasses { get; set; }

    ///<summary>
    /// CSSClasses to append to the &lt;ul&gt; element in the paging control.
    ///</summary>
    public IEnumerable<string> UlElementClasses { get; set; }

    /// <summary>
    /// Attrinutes to appendto the &lt;ul&gt; element in the paging control
    /// </summary>
    public IDictionary<string, string> UlElementattributes { get; set; }

    ///<summary>
    /// CSS Classes to append to every &lt;li&gt; element in the paging control.
    ///</summary>
    public IEnumerable<string> LiElementClasses { get; set; }

    /// <summary>
    /// CSS Classes to appent to active &lt;li&gt; element in the paging control.
    /// </summary>
    public string ActiveLiElementClass { get; set; }

    ///<summary>
    /// CSS Classes to append to every &lt;a&gt; or &lt;span&gt; element that represent each page in the paging control.
    ///</summary>
    public IEnumerable<string> PageClasses { get; set; }

    ///<summary>
    /// CSS Classes to append to previous element in the paging control.
    ///</summary>
    public string PreviousElementClass { get; set; }

    ///<summary>
    /// CSS Classes to append to next element in the paging control.
    ///</summary>
    public string NextElementClass { get; set; }

    ///<summary>
    /// CSS Classes to append to Ellipses element in the paging control.
    ///</summary>
    public string EllipsesElementClass { get; set; }

    ///<summary>
    /// Specifies a CSS class to append to the first list item in the pager. If null or whitespace is defined, no additional class is added to first list item in list.
    ///</summary>
    public string ClassToApplyToFirstListItemInPager { get; set; }

    ///<summary>
    /// Specifies a CSS class to append to the last list item in the pager. If null or whitespace is defined, no additional class is added to last list item in list.
    ///</summary>
    public string ClassToApplyToLastListItemInPager { get; set; }

    /// <summary>
    /// If set to Always, always renders the paging control. If set to IfNeeded, render the paging control when there is more than one page.
    /// </summary>
    public PagerBehavior Display { get; set; }

    ///<summary>
    /// If set to Always, render a hyperlink to the first page in the list. If set to IfNeeded, render the hyperlink only when the first page isn't visible in the paging control.
    ///</summary>
    public PagerBehavior DisplayLinkToFirstPage { get; set; }

    ///<summary>
    /// If set to Always, render a hyperlink to the last page in the list. If set to IfNeeded, render the hyperlink only when the last page isn't visible in the paging control.
    ///</summary>
    public PagerBehavior DisplayLinkToLastPage { get; set; }

    ///<summary>
    /// If set to Always, render a hyperlink to the previous page of the list. If set to IfNeeded, render the hyperlink only when there is a previous page in the list.
    ///</summary>
    public PagerBehavior DisplayLinkToPreviousPage { get; set; }

    ///<summary>
    /// If set to Always, render a hyperlink to the next page of the list. If set to IfNeeded, render the hyperlink only when there is a next page in the list.
    ///</summary>
    public PagerBehavior DisplayLinkToNextPage { get; set; }

    ///<summary>
    /// When true, includes hyperlinks for each page in the list.
    ///</summary>
    public bool DisplayLinkToIndividualPages { get; set; }

    ///<summary>
    /// When true, shows the current page number and the total number of pages in the list.
    ///</summary>
    ///<example>
    /// "Page 3 of 8."
    ///</example>
    public bool DisplayPageCountAndCurrentLocation { get; set; }

    ///<summary>
    /// When true, shows the one-based index of the first and last items on the page, and the total number of items in the list.
    ///</summary>
    ///<example>
    /// "Showing items 75 through 100 of 183."
    ///</example>
    public bool DisplayItemSliceAndTotal { get; set; }

    ///<summary>
    /// The maximum number of page numbers to display. Null displays all page numbers.
    ///</summary>
    public int? MaximumPageNumbersToDisplay { get; set; }

    ///<summary>
    /// If true, adds an ellipsis where not all page numbers are being displayed.
    ///</summary>
    ///<example>
    /// "1 2 3 4 5 ...",
    /// "... 6 7 8 9 10 ...",
    /// "... 11 12 13 14 15"
    ///</example>
    public bool DisplayEllipsesWhenNotShowingAllPageNumbers { get; set; }

    ///<summary>
    /// The pre-formatted text to display when not all page numbers are displayed at once.
    ///</summary>
    ///<example>
    /// "..."
    ///</example>
    public string EllipsesFormat { get; set; }

    ///<summary>
    /// The pre-formatted text to display inside the hyperlink to the first page. The one-based index of the page (always 1 in this case) is passed into the formatting function - use {0} to reference it.
    ///</summary>
    ///<example>
    /// "&lt;&lt; First"
    ///</example>
    public string LinkToFirstPageFormat { get; set; }

    ///<summary>
    /// The pre-formatted text to display inside the hyperlink to the previous page. The one-based index of the page is passed into the formatting function - use {0} to reference it.
    ///</summary>
    ///<example>
    /// "&lt; Previous"
    ///</example>
    public string LinkToPreviousPageFormat { get; set; }

    ///<summary>
    /// The pre-formatted text to display inside the hyperlink to each individual page. The one-based index of the page is passed into the formatting function - use {0} to reference it.
    ///</summary>
    ///<example>
    /// "{0}"
    ///</example>
    public string LinkToIndividualPageFormat { get; set; }

    ///<summary>
    /// The pre-formatted text to display inside the hyperlink to the next page. The one-based index of the page is passed into the formatting function - use {0} to reference it.
    ///</summary>
    ///<example>
    /// "Next &gt;"
    ///</example>
    public string LinkToNextPageFormat { get; set; }

    ///<summary>
    /// The pre-formatted text to display inside the hyperlink to the last page. The one-based index of the page is passed into the formatting function - use {0} to reference it.
    ///</summary>
    ///<example>
    /// "Last &gt;&gt;"
    ///</example>
    public string LinkToLastPageFormat { get; set; }

    ///<summary>
    /// The pre-formatted text to display when DisplayPageCountAndCurrentLocation is true. Use {0} to reference the current page and {1} to reference the total number of pages.
    ///</summary>
    ///<example>
    /// "Page {0} of {1}."
    ///</example>
    public string PageCountAndCurrentLocationFormat { get; set; }

    ///<summary>
    /// The pre-formatted text to display when DisplayItemSliceAndTotal is true. Use {0} to reference the first item on the page, {1} for the last item on the page, and {2} for the total number of items across all pages.
    ///</summary>
    ///<example>
    /// "Showing items {0} through {1} of {2}."
    ///</example>
    public string ItemSliceAndTotalFormat { get; set; }

    /// <summary>
    /// A function that will render each page number when specified (and DisplayLinkToIndividualPages is true). If no function is specified, the LinkToIndividualPageFormat value will be used instead.
    /// </summary>
    public Func<int, string> FunctionToDisplayEachPageNumber { get; set; }

    /// <summary>
    /// Text that will appear between each page number. If null or whitespace is specified, no delimiter will be shown.
    /// </summary>
    public string DelimiterBetweenPageNumbers { get; set; }

    /// <summary>
    /// An extension point which allows you to fully customize the anchor tags used for clickable pages, as well as navigation features such as Next, Last, etc.
    /// </summary>
    public Func<TagBuilder, TagBuilder, TagBuilder> FunctionToTransformEachPageLink { get; set; }

    /// <summary>
    /// Enables ASP.NET MVC's unobtrusive AJAX feature. An XHR request will retrieve HTML from the clicked page and replace the innerHtml of the provided element ID.
    /// </summary>
    /// <param name="options">The preferred Html.PagedList(...) style options.</param>
    /// <param name="ajaxOptions">The ajax options that will put into the link</param>
    /// <returns>The PagedListRenderOptions value passed in, with unobtrusive AJAX attributes added to the page links.</returns>
    public static PagerOptions EnableUnobtrusiveAjaxReplacing(PagerOptions options, AjaxOptions ajaxOptions)
    {
      options.FunctionToTransformEachPageLink = (liTagBuilder, aTagBuilder) =>
                                                    {
                                                      var liClass = liTagBuilder.Attributes.ContainsKey("class") ? liTagBuilder.Attributes["class"] ?? "" : "";
                                                      if (ajaxOptions != null && !liClass.Contains("disabled") && !liClass.Contains("active"))
                                                      {
                                                        foreach (var ajaxOption in ajaxOptions.ToUnobtrusiveHtmlAttributes())
                                                          aTagBuilder.Attributes.Add(ajaxOption.Key, ajaxOption.Value.ToString());
                                                      }

                                                      liTagBuilder.InnerHtml = aTagBuilder.ToString();
                                                      return liTagBuilder;
                                                    };
      return options;
    }

    /// <summary>
    /// Enables ASP.NET MVC's unobtrusive AJAX feature. An XHR request will retrieve HTML from the clicked page and replace the innerHtml of the provided element ID.
    /// </summary>
    /// <param name="id">The element ID ("my_id") of the element whose innerHtml should be replaced, if # is included at the start this will be removed.</param>
    /// <returns>A default instance of PagedListRenderOptions value passed in, with unobtrusive AJAX attributes added to the page links.</returns>
    public static PagerOptions EnableUnobtrusiveAjaxReplacing(string id)
    {

      if (id.StartsWith("#"))
        id = id.Substring(1);

      var ajaxOptions = new AjaxOptions()
      {
        HttpMethod = "GET",
        InsertionMode = InsertionMode.Replace,
        UpdateTargetId = id
      };

      return EnableUnobtrusiveAjaxReplacing(new PagerOptions(), ajaxOptions);
    }

    /// <summary>
    /// Enables ASP.NET MVC's unobtrusive AJAX feature. An XHR request will retrieve HTML from the clicked page and replace the innerHtml of the provided element ID.
    /// </summary>
    /// <param name="ajaxOptions">Ajax options that will be used to generate the unobstrusive tags on the link</param>
    /// <returns>A default instance of PagedListRenderOptions value passed in, with unobtrusive AJAX attributes added to the page links.</returns>
    public static PagerOptions EnableUnobtrusiveAjaxReplacing(AjaxOptions ajaxOptions)
    {
      return EnableUnobtrusiveAjaxReplacing(new PagerOptions(), ajaxOptions);
    }

    ///<summary>
    /// Also includes links to First and Last pages.
    ///</summary>
    public static PagerOptions Classic
    {
      get
      {
        return new PagerOptions
        {
          DisplayLinkToFirstPage = PagerBehavior.Never,
          DisplayLinkToLastPage = PagerBehavior.Never,
          DisplayLinkToPreviousPage = PagerBehavior.Always,
          DisplayLinkToNextPage = PagerBehavior.Always
        };
      }
    }

    ///<summary>
    /// Also includes links to First and Last pages.
    ///</summary>
    public static PagerOptions ClassicPlusFirstAndLast
    {
      get
      {
        return new PagerOptions
        {
          DisplayLinkToFirstPage = PagerBehavior.Always,
          DisplayLinkToLastPage = PagerBehavior.Always,
          DisplayLinkToPreviousPage = PagerBehavior.Always,
          DisplayLinkToNextPage = PagerBehavior.Always
        };
      }
    }

    ///<summary>
    /// Shows only the Previous and Next links.
    ///</summary>
    public static PagerOptions Minimal
    {
      get
      {
        return new PagerOptions
        {
          DisplayLinkToFirstPage = PagerBehavior.Never,
          DisplayLinkToLastPage = PagerBehavior.Never,
          DisplayLinkToPreviousPage = PagerBehavior.Always,
          DisplayLinkToNextPage = PagerBehavior.Always,
          DisplayLinkToIndividualPages = false
        };
      }
    }

    ///<summary>
    /// Shows Previous and Next links along with current page number and page count.
    ///</summary>
    public static PagerOptions MinimalWithPageCountText
    {
      get
      {
        return new PagerOptions
        {
          DisplayLinkToFirstPage = PagerBehavior.Never,
          DisplayLinkToLastPage = PagerBehavior.Never,
          DisplayLinkToPreviousPage = PagerBehavior.Always,
          DisplayLinkToNextPage = PagerBehavior.Always,
          DisplayLinkToIndividualPages = false,
          DisplayPageCountAndCurrentLocation = true
        };
      }
    }

    ///<summary>
    ///	Shows Previous and Next links along with index of first and last items on page and total number of items across all pages.
    ///</summary>
    public static PagerOptions MinimalWithItemCountText
    {
      get
      {
        return new PagerOptions
        {
          DisplayLinkToFirstPage = PagerBehavior.Never,
          DisplayLinkToLastPage = PagerBehavior.Never,
          DisplayLinkToPreviousPage = PagerBehavior.Always,
          DisplayLinkToNextPage = PagerBehavior.Always,
          DisplayLinkToIndividualPages = false,
          DisplayItemSliceAndTotal = true
        };
      }
    }

    ///<summary>
    ///	Shows only links to each individual page.
    ///</summary>
    public static PagerOptions PageNumbersOnly
    {
      get
      {
        return new PagerOptions
        {
          DisplayLinkToFirstPage = PagerBehavior.Never,
          DisplayLinkToLastPage = PagerBehavior.Never,
          DisplayLinkToPreviousPage = PagerBehavior.Never,
          DisplayLinkToNextPage = PagerBehavior.Never,
          DisplayEllipsesWhenNotShowingAllPageNumbers = false
        };
      }
    }

    ///<summary>
    ///	Shows Next and Previous while limiting to a max of 5 page numbers at a time.
    ///</summary>
    public static PagerOptions OnlyShowFivePagesAtATime
    {
      get
      {
        return new PagerOptions
        {
          DisplayLinkToFirstPage = PagerBehavior.Never,
          DisplayLinkToLastPage = PagerBehavior.Never,
          DisplayLinkToPreviousPage = PagerBehavior.Always,
          DisplayLinkToNextPage = PagerBehavior.Always,
          MaximumPageNumbersToDisplay = 5
        };
      }
    }

    ///<summary>
    /// Twitter Bootstrap 2's basic pager format (just Previous and Next links).
    ///</summary>
    public static PagerOptions TwitterBootstrapPager
    {
      get
      {
        return new PagerOptions
        {
          DisplayLinkToFirstPage = PagerBehavior.Never,
          DisplayLinkToLastPage = PagerBehavior.Never,
          DisplayLinkToPreviousPage = PagerBehavior.Always,
          DisplayLinkToNextPage = PagerBehavior.Always,
          DisplayLinkToIndividualPages = false,
          ContainerDivClasses = null,
          UlElementClasses = new[] { "pager" },
          ClassToApplyToFirstListItemInPager = null,
          ClassToApplyToLastListItemInPager = null,
          LinkToPreviousPageFormat = "Previous",
          LinkToNextPageFormat = "Next"
        };
      }
    }

    ///<summary>
    /// Twitter Bootstrap 2's basic pager format (just Previous and Next links), with aligned links.
    ///</summary>
    public static PagerOptions TwitterBootstrapPagerAligned
    {
      get
      {
        return new PagerOptions
        {
          DisplayLinkToFirstPage = PagerBehavior.Never,
          DisplayLinkToLastPage = PagerBehavior.Never,
          DisplayLinkToPreviousPage = PagerBehavior.Always,
          DisplayLinkToNextPage = PagerBehavior.Always,
          DisplayLinkToIndividualPages = false,
          ContainerDivClasses = null,
          UlElementClasses = new[] { "pager" },
          ClassToApplyToFirstListItemInPager = "previous",
          ClassToApplyToLastListItemInPager = "next",
          LinkToPreviousPageFormat = "&larr; Older",
          LinkToNextPageFormat = "Newer &rarr;"
        };
      }
    }
  }
}