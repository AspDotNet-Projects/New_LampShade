using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _0_Framework.Domain;

namespace ShopManagement.Domain.ProductPictureAgg
{
    public class ProductPicture :EntityBase
    {
        public long PictureId { get;private set; }
        public string Picture { get; private set; }
        public string PictureAlt { get; private set; }
        public string PictureTitle { get; private set; }
        public bool IsRemoved { get; private set; }

        public ProductPicture(long pictureId, string picture, string pictureAlt, string pictureTitle)
        {
            PictureId = pictureId;
            Picture = picture;
            PictureAlt = pictureAlt;
            PictureTitle = pictureTitle;
            IsRemoved = false;
        }

        public void Edit(long pictureId, string picture, string pictureAlt, string pictureTitle)
        {
            PictureId = pictureId;
            Picture = picture;
            PictureAlt = pictureAlt;
            PictureTitle = pictureTitle;
           
        }

        public void Remove()
        {
            IsRemoved = true;
        }

        public void Restore()
        {
            IsRemoved = false;
        }

    }
}
