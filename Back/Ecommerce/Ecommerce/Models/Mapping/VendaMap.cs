using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Models.Mapping
{
    public class VendaMap :ClassMap<Venda>
    {
        public VendaMap()
        {
            Id(x => x.Id).Column("Id").GeneratedBy.Native();
            Map(x => x.Pagamento);
            Map(x => x.Total);
            References(x => x.Carrinho).Cascade.All();
        }
    }
}
