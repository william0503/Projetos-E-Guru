using Ecommerce.Models;
using Ecommerce.Models.Helpers;
using NHibernate;
using NHibernate.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ecommerce.Controllers
{
    public class VendaController : Controller
    {
        // GET: Venda
        public ActionResult Index()
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                int idCarrinho = (int)Session["Carrinho"];
                var carrinho = session.Query<Carrinho>().Where(x => x.Id == idCarrinho).FirstOrDefault();
                ViewBag.total = calcularTotal(carrinho).ToString("c2");
                return View("FinalizarVenda", carrinho.Produtos.ToList());
            }
        }

        public ActionResult removerQuantidade(int id)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    var produtoCarrinho = session.Query<ProdutoCarrinho>().Where(x => x.Id == id).FirstOrDefault();
                    produtoCarrinho.Quantidade--;
                    produtoCarrinho.CalcularSubtotal();
                    session.SaveOrUpdate(produtoCarrinho);
                    transaction.Commit();
                }
            }
            return RedirectToAction("Index");
        }

        public ActionResult adicionarQuantidade(int id)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    var produtoCarrinho = session.Query<ProdutoCarrinho>().Where(x => x.Id == id).FirstOrDefault();
                    produtoCarrinho.Quantidade++;
                    produtoCarrinho.CalcularSubtotal();
                    session.SaveOrUpdate(produtoCarrinho);
                    transaction.Commit();
                }
            }
            return RedirectToAction("Index");
        }

        public ActionResult removerProduto(int id)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    var produtoCarrinho = session.Query<ProdutoCarrinho>().Where(x => x.Id == id).FirstOrDefault();
                    produtoCarrinho.Carrinho.Produtos.Remove(produtoCarrinho);
                    session.SaveOrUpdate(produtoCarrinho);
                    transaction.Commit();
                }
            }
            return RedirectToAction("Index");
        }

        public ActionResult verProdutos()
        {
            return RedirectToAction("Index", "Produto");
        }

        public ActionResult VerificarVenda(FormCollection collection)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                var pagamento = getMetodoPagamento(collection["pagamento"]);
                int idCarrinho = (int)Session["Carrinho"];
                var carrinho = session.Query<Carrinho>().Where(x => x.Id == idCarrinho).FirstOrDefault();

                Venda venda = new Venda
                {
                    Carrinho = carrinho,
                    Pagamento = pagamento,
                    Total = calcularTotal(carrinho)
                };

                foreach (var item in venda.Carrinho.Produtos)
                {
                    item.Produto.Estoque -= item.Quantidade;
                }
                                
                return View(venda);

            }
        }
        public ActionResult ConfirmarVenda(MetodoPagamento pagamento)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {                    
                    int idCarrinho = (int)Session["Carrinho"];
                    var carrinho = session.Query<Carrinho>().Where(x => x.Id == idCarrinho).FirstOrDefault();

                    if (carrinho != null)
                    {
                        Venda venda = new Venda
                        {
                            Carrinho = carrinho,
                            Pagamento = pagamento,
                            Total = calcularTotal(carrinho)
                        };

                        session.SaveOrUpdate(venda);

                        foreach (var item in venda.Carrinho.Produtos)
                        {
                            item.Produto.Estoque -= item.Quantidade;
                            session.SaveOrUpdate(item);
                        }

                        Session["Carrinho"] = 0;

                        transaction.Commit();

                        return View(venda);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Produto");
                    }
                }
            }
        }

        public float calcularTotal(Carrinho carrinho)
        {
            float total = 0;
            foreach (var item in carrinho.Produtos)
            {
                total += (item.Produto.Valor * item.Quantidade);
            }
            return total;
        }

        public MetodoPagamento getMetodoPagamento(string metodo)
        {
            switch (metodo)
            {
                case "0":
                    return MetodoPagamento.Boleto;
                case "1":
                    return MetodoPagamento.Visa;
                case "2":
                    return MetodoPagamento.Mastercard;
            }

            return MetodoPagamento.Boleto;
        }
    }
}