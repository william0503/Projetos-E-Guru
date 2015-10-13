using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace Ecommerce.Models
{
    public class Carrinho
    {
        public virtual int Id { get; set; }
        public virtual DateTime Data { get; set; }

        [ScriptIgnore]
        public virtual IList<ProdutoCarrinho> Produtos { get; set; }

        public Carrinho()
        {
            this.Produtos = new List<ProdutoCarrinho>();
        }
    }
}
