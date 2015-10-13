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
    public class ProdutoController : Controller
    {


        // GET: Produto
        public ActionResult Index()
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                var produto = session.Query<Produto>().ToList();
                if (TempData["error"] != null)
                {
                    ModelState.AddModelError(string.Empty, TempData["error"].ToString());                    
                }
                var itensNoCarrinho = 0;
                Carrinho carrinho = (session.Query<Carrinho>().Where(x => x.Id == SessionCarrinho).FirstOrDefault()) ?? new Carrinho { Data = DateTime.Now };
                
                    foreach (var item in carrinho.Produtos)
                    {
                        itensNoCarrinho += item.Quantidade;
                    }
                
                ViewBag.itensCarrinho = itensNoCarrinho;
                return View(produto);
            }
        }

        public ActionResult AdicionarAoCarrinho(int id)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    Carrinho carrinho = (session.Query<Carrinho>().Where(x => x.Id == SessionCarrinho).FirstOrDefault()) ?? new Carrinho { Data = DateTime.Now };
                    
                    var produto = session.Query<Produto>().Where(x => x.Id == id).FirstOrDefault();

                    ProdutoCarrinho pc;

                    if (carrinho.Produtos != null)
                    {
                        pc = carrinho.Produtos.Where(x => x.Produto.Nome == produto.Nome).FirstOrDefault();

                        if (pc != null)
                        {
                            if ((pc.Quantidade + 1) <= pc.Produto.Estoque)
                            {
                                pc.Quantidade++;
                                pc.CalcularSubtotal();
                                session.SaveOrUpdate(pc);
                            }
                            else
                            {
                                TempData["error"] = "Não é possível incluir mais unidades desse produto no carrinho.";
                            }
                        }
                        else
                        {
                            pc = new ProdutoCarrinho
                            {
                                Produto = produto,
                                Carrinho = carrinho,
                                Quantidade = 1
                            };

                            pc.CalcularSubtotal();

                            produto.produtoCarrinho = pc.Produto.produtoCarrinho = pc;
                            carrinho.Produtos.Add(pc);
                            session.SaveOrUpdate(carrinho);
                        }
                        SessionCarrinho = carrinho.Id;
                    }
                    transaction.Commit();
                    return RedirectToAction("Index");
                }
            }
        }

        public ActionResult verCarrinho()
        {
            if (SessionCarrinho == 0)
            { 
                using (ISession session = NHibernateHelper.OpenSession())
                {
                    var carrinho = new Carrinho { Data = DateTime.Now};
                    session.SaveOrUpdate(carrinho);
                    SessionCarrinho = carrinho.Id;                    
                }
            }

            return RedirectToAction("Index", "Venda");
        }

        public int SessionCarrinho
        {
            get
            {
                return (Session["Carrinho"] == null) ? 0 : (int)Session["Carrinho"];
            }
            set
            {
                Session["Carrinho"] = value;
            }
        }
    }
}
