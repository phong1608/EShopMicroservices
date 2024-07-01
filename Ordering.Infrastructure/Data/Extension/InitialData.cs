using Ordering.Domain.Models;
using Ordering.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Infrastructure.Data.Extension
{
    public class InitialData
    {
        

       

        public static IEnumerable<Order> OrdersWithItems
        {
            get
            {
                var address1 = Address.Of("mehmet", "ozkaya", "mehmet@gmail.com","0911145094", "Bahcelievler No:4", "Turkey", "Istanbul");
                var address2 = Address.Of("john", "doe", "john@gmail.com","0798838103", "Broadway No:1", "England", "Nottingham");


                var order1 = Order.Create(
                                Guid.NewGuid(),
                                new Guid("58c49479-ec65-4de2-86e7-033c546291aa"),
                                "ORD_1",
                                shippingAddress: address1
                                );
                order1.Add(new Guid("5334c996-8457-4cf0-815c-ed2b77c4ff61"), 2, 500);
                order1.Add(new Guid("c67d6323-e8b1-4bdf-9a75-b0d0d2e7e914"), 1, 400);

                var order2 = Order.Create(
                                Guid.NewGuid(),
                                new Guid("189dc8dc-990f-48e0-a37b-e6f2b60b9d7d"),
                                "ORD_2",
                                shippingAddress: address2
                                );
                order2.Add(new Guid("4f136e9f-ff8c-4c1f-9a33-d12f689bdab8"), 1, 650);
                order2.Add(new Guid("6ec1297b-ec0a-4aa1-be25-6726e3b51a27"), 2, 450);

                return new List<Order> { order1, order2 };
            }
        }
    }
}
