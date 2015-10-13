using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Models.Mapping
{
    public class ProdutoMap : ClassMap<Produto>
    {
        public ProdutoMap()
        {
            Id(x => x.Id).Column("Id").GeneratedBy.Native();
            Map(x => x.Nome);
            Map(x => x.Valor);
            Map(x => x.Estoque);

            HasOne(x => x.produtoCarrinho).Cascade.All().Not.LazyLoad();

            Table("Produto");
        }
    }
}
