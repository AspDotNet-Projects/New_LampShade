using System.Collections.Specialized;
using _0_Framework.Domain;

namespace BlogManagement.Domain.ArticleCategoryAgg
{
    public class ArticleCategory :EntityBase
    {
        public string Name { get; private set; }
        public string Picture { get; private set; }
        public string PictureAlt { get; private set; }
        public string PictureTitle { get; private set; }
        public string Description { get; private set; }
        public int ShowOrder { get; private set; }

        /// <summary>
        /// چهار مورد زیر جهت سئو سایت می باشد
        /// سلاگ جهت بالا بردن سئو با استفاده از کلمات کلیدی که آنها را به عنوان آدرس ذخیره می کننیم
        /// </summary>
        public string Slug{ get; private set; }
        /// <summary>
        /// کلمات کلیدی صفحه مثلا اگر راجع به شیشه جلو خوردو صحبت می کنیم
        /// طلقی بود،ضخامت لایه ها،بالارنگی یا ساده بودن،و ویژگی های کلیدی صحبت می کنیم
        /// </summary>
        public string Keywords { get; private set; }
        
        public string MetaDescription{ get; private set; }
        
        /// <summary>
        /// اگر در سایتمان صفحاتی با کانتنت مشابه داشته باشیم گوگل ما را جریمه
        /// خواخد کرد برای جلوگییری از این کار از کنونیکال آدرس استفاده میکنیم
        /// به این ترتیب که یا پیج آ به بی یا پیج بی به آ کنونیکال می کنیم
        /// </summary>
        public string CanonicalAddress{ get; private set; }

        public ArticleCategory(string name, string picture, 
            string pictureAlt, string pictureTitle, string description, 
            int showOrder, string slug, string keywords, string metaDescription, 
            string canonicalAddress)
        {
            Name = name;
            Picture = picture;
            PictureAlt = pictureAlt;
            PictureTitle = pictureTitle;
            Description = description;
            ShowOrder = showOrder;
            Slug = slug;
            Keywords = keywords;
            MetaDescription = metaDescription;
            CanonicalAddress = canonicalAddress;
        }
        public void Edit(string name, string picture,
            string pictureAlt, string pictureTitle, string description,
            int showOrder, string slug, string keywords, string metaDescription,
            string canonicalAddress)
        {
            Name = name;

            if(!string.IsNullOrWhiteSpace(picture))
                    Picture = picture;
            PictureAlt = pictureAlt;
            PictureTitle = PictureTitle;
            Description = description;
            ShowOrder = showOrder;
            Slug = slug;
            Keywords = keywords;
            MetaDescription = metaDescription;
            CanonicalAddress = canonicalAddress;
        }
    }
}
