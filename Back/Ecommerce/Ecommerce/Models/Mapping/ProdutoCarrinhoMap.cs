using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Models.Mapping
{
    class ProdutoCarrinhoMap : ClassMap<ProdutoCarrinho>
    {
        public ProdutoCarrinhoMap()
        {
            Id(x=>x.Id).Column("Id").GeneratedBy.Native();            
            Map(x => x.Quantidade);
            Map(x => x.Subtotal);
            References(x => x.Produto).Cascade.All().Not.LazyLoad();
            References(x => x.Carrinho).Cascade.All().Not.LazyLoad();
            Table("ProdutoCarrinho");
        }
    }
}
