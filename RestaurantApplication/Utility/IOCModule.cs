using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Autofac;
using RestaurantApplication.DB.IRepository;
using RestaurantApplication.DB.Models;
using RestaurantApplication.DB.Repository;

namespace RestaurantApplication.Utility
{
    public class IOCModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register<IMenuDetailRepository>(x =>
            {
                var db = new Context();
                return new MenuDetailRepository(db);
            }).InstancePerRequest();
            builder.Register<IOrderDetailsRepository>(x =>
            {
                var db = new Context();
                return new OrderDetailsRepository(db);
            }).InstancePerRequest();
            builder.Register<IBillDetailsRepository>(x =>
            {
                var db = new Context();
                return new BillDetailsRepository(db);
            }).InstancePerRequest();
            base.Load(builder);
        }
    }
}