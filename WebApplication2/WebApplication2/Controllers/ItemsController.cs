namespace WebApplication2.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;

    [Authorize]
    public class ItemsController : ApiController
    {
        // GET api/Me
        public IEnumerable<Item> Get()
        {
            var data = new List<Item>()
            {
                new Item() {Name = "牛乳1", Price = 100, TypeCd = 1, Url = "/Items/Edit/1"},
                new Item() {Name = "牛乳2", Price = 123, TypeCd = 1, Url = "/Items/Edit/2"},
                new Item() {Name = "牛乳3", Price = 10, TypeCd = 2, Url = "/Items/Edit/3"},
            };

            return data;
        }

        public class Item
        {
            public string Name { set; get; }

            public decimal Price { set; get; }

            public byte TypeCd { set; get; }

            public string Url { set; get; }
        }
    }
}
