using System.Net;
using System.Web.Mvc;
using Modelo.Cadastros;
using Servico.Cadastros;
using Servico.Tabelas;

namespace Projeto01.Controllers
{
    public class ProdutosController : Controller
    {
        private ProdutoServico produtoServico = new ProdutoServico();
        private CategoriaServico categoriaServico = new CategoriaServico();
        private FabricanteServico fabricanteServico = new FabricanteServico();

        public ActionResult Index()
        {
            return View(produtoServico.ObterProdutosClassificadosPorNome());
        }

        private ActionResult ObterVisaoProdutoPorId(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(
                HttpStatusCode.BadRequest);
            }
            Produto produto = produtoServico.ObterProdutoPorId((long)id);
            if (produto == null)
            {
                return HttpNotFound();
            }
            return View(produto);
        }

        public ActionResult Details(long? id)
        {
            return ObterVisaoProdutoPorId(id);
        }

        public ActionResult Delete(long? id)
        {
            return ObterVisaoProdutoPorId(id);
        }

        private void PopularViewBag(Produto produto = null)
        {
            if (produto == null)
            {
                ViewBag.CategoriaId = new SelectList(categoriaServico.ObterCategoriasClassificadasPorNome(), "CategoriaId", "Nome");
                ViewBag.FabricanteId = new SelectList(fabricanteServico.ObterFabricantesClassificadosPorNome(), "FabricanteId", "Nome");
            }
            else
            {
                ViewBag.CategoriaId = new SelectList(categoriaServico.ObterCategoriasClassificadasPorNome(), "CategoriaId", "Nome", produto.CategoriaId);
                ViewBag.FabricanteId = new SelectList(fabricanteServico.ObterFabricantesClassificadosPorNome(), "FabricanteId", "Nome", produto.FabricanteId);
            }
        }

        public ActionResult Edit(long? id)
        {
            PopularViewBag(produtoServico.ObterProdutoPorId((long)id));
            return ObterVisaoProdutoPorId(id);
        }

        public ActionResult Create()
        {
            PopularViewBag();
            return View();
        }

        private ActionResult GravarProduto(Produto produto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    produtoServico.GravarProduto(produto);
                    return RedirectToAction("Index");
                }
                return View(produto);
            }
            catch
            {
                return View(produto);
            }
        }

        [HttpPost]
        public ActionResult Create(Produto produto)
        {
            return GravarProduto(produto);
        }

        [HttpPost]
        public ActionResult Edit(Produto produto)
        {
            return GravarProduto(produto);
        }

        [HttpPost]
        public ActionResult Delete(long id)
        {
            try
            {
                Produto produto = produtoServico.EliminarProdutoPorId(id);
                TempData["Message"] = "Produto " + produto.Nome.ToUpper() + " foi removido";
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        /** não vou mais utilizar este código,pois ele não tem acesso direto a persistência e sim ao serviço
        private EFContext context = new EFContext();

        // GET: Produtos
       /** public ActionResult Index()
        {
            return View(context.Produtos.OrderBy(c => c.Nome));
        }*/
        /**
        public ActionResult Index()
        {
            var produtos = context.Produtos.Include(c => c.Categoria).Include(f => f.Fabricante).OrderBy(n => n.Nome);
            return View(produtos);
        }

        // GET: Produtos/Details/5
        public ActionResult Details(long? id)
        {
            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Produto produto = context.Produtos.Where(p => p.ProdutoId == id).Include(c => c.Categoria).Include(f => f.Fabricante).First();
            if (produto == null)
            {
                return HttpNotFound();
            }
            return View(produto);
        }

        // GET: Produtos/Create
        public ActionResult Create()
        {
            ViewBag.CategoriaId = new SelectList(context.Categorias.OrderBy(b => b.Nome), "CategoriaId", "Nome");
            ViewBag.FabricanteId = new SelectList(context.Fabricantes.OrderBy(b => b.Nome), "FabricanteId", "Nome");
            return View();
        }

        // POST: Produtos/Create
        [HttpPost]
        public ActionResult Create(Produto produto)
        {
            try
            {
                context.Produtos.Add(produto);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View(produto);
            }
        }

        // GET: Produtos/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Produto produto = context.Produtos.Find(id);
            if (produto == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoriaId = new SelectList(context.Categorias.OrderBy(b => b.Nome), "CategoriaId", "Nome", produto.CategoriaId);
            ViewBag.FabricanteId = new SelectList(context.Fabricantes.OrderBy(b => b.Nome), "FabricanteId", "Nome", produto.FabricanteId);

            return View(produto);
        }

        // POST: Produtos/Edit/5
        [HttpPost]
        public ActionResult Edit(Produto produto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    context.Entry(produto).State = EntityState.Modified;
                    return RedirectToAction("Index");
                }

                return View(produto);

            }
            catch
            {
                return View(produto);
            }
        }

        // GET: Produtos/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Produto produto = context.Produtos.Where(p => p.ProdutoId == id).Include(c => c.Categoria).Include(f => f.Fabricante).First();

            if (produto == null)
            {
                return HttpNotFound();
            }
            return View(produto);
        }

        // POST: Produtos/Delete/5
        [HttpPost]
        public ActionResult Delete(long id)
        {
            try
            {
                Produto produto = context.Produtos.Find(id);
                context.Produtos.Remove(produto);
                context.SaveChanges();
                TempData["Message"] = "Produto" + produto.Nome.ToUpper() + "foi removido";
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        */
    }
}
