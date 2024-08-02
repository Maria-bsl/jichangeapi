using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JichangeApi.Models
{
    public class ItemListModelResponse
    {
        public List<ItemListModel> Response { get; set; }
        public string Message { get; set; }
    }
}