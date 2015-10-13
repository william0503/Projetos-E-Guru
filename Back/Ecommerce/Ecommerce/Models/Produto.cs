using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace Ecommerce.Models
{
    public class Produto
    {
        public virtual int Id { get; set; }
        public virtual string Nome { get; set; }
        [DisplayFormat(DataFormatString = "{0:C}", ApplyFormatInEditMode = true)]
        public virtual float Valor { get; set; }
        public virtual int Estoque { get; set; }
        [ScriptIgnore]
        public virtual ProdutoCarrinho produtoCarrinho { get; set; }
    }
}
