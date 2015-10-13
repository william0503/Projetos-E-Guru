using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Models
{
    public class Venda
    {
        public virtual int Id { get; set; }        
        public virtual MetodoPagamento Pagamento  { get; set; }                
        public virtual Carrinho Carrinho { get; set; }
        [DisplayFormat(DataFormatString = "{0:C}", ApplyFormatInEditMode = true)]
        public virtual float Total { get; set; }        
    }
}
