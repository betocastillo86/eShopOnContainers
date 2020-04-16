using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.eShopOnContainers.Services.Catalog.API.Model
{
    public class Comment
    {
        public int Id { get; set; }

        public int CatalogItemId { get; set; }

        public CatalogItem CatalogItem { get; set; }

        public string Text { get; set; }

        public DateTime CreationDate { get; set; }

        public bool Deleted { get; set; }

        public string UserId { get; set; }
    }
}
