using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Models.Mapping
{
    class CarrinhoMap : ClassMap<Carrinho>
    {
        public CarrinhoMap()
        {
            Id(x => x.Id).Column("Id").GeneratedBy.Native();
            Map(x => x.Data);
            HasMany(x=>x.Produtos).Cascade.All().Not.LazyLoad();
            Table("Carrinho");
            
        }
    }
}
