using System.ComponentModel.DataAnnotations;
using System.Web.Script.Serialization;

namespace Ecommerce.Models
{
    public class ProdutoCarrinho
    {
        public virtual int Id { get; set; }
        public virtual Produto Produto { get; set; }
        public virtual Carrinho Carrinho { get; set; }
        public virtual int Quantidade { get; set; }
        [DisplayFormat(DataFormatString = "{0:C}", ApplyFormatInEditMode = true)]
        public virtual float Subtotal { get; set; }

        public virtual void CalcularSubtotal()
        {
            Subtotal = Produto.Valor * Quantidade;
        }



    }
}