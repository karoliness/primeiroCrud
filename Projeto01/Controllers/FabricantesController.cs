using Modelo.Cadastros;
using Servico.Cadastros;
using Servico.Tabelas;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace Projeto01.Controllers
{
    public class FabricantesController : Controller
    {
        private ProdutoServico produtoServico = new ProdutoServico();
        private CategoriaServico categoriaServico = new CategoriaServico();
        private FabricanteServico fabricanteServico = new FabricanteServico();

        public ActionResult Index()
        {
            return View(fabricanteServico.ObterFabricantesClassificadosPorNome());
        }

        private ActionResult ObterVisaoFabricantePorId(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(
                HttpStatusCode.BadRequest);
            }
            Fabricante fabricante = fabricanteServico.ObterFabricantePorId((long)id);
            if (fabricante == null)
            {
                return HttpNotFound();
            }
            return View(fabricante);
        }

        public ActionResult Details(long? id)
        {
            return ObterVisaoFabricantePorId(id);
        }

        public ActionResult Delete(long? id)
        {
            return ObterVisaoFabricantePorId(id);
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
            return ObterVisaoFabricantePorId(id);
        }

        public ActionResult Create()
        {
            return View();
        }

        private ActionResult GravarFabricante(Fabricante fabricante)
        {
            fabricanteServico.GravarFabricante(fabricante);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Create(Fabricante fabricante)
        {
            return GravarFabricante(fabricante);
        }

        [HttpPost]
        public ActionResult Edit(Fabricante fabricante)
        {
            return GravarFabricante(fabricante);
        }

        [HttpPost]
        public ActionResult Delete(long id)
        {
            Fabricante fabricante = fabricanteServico.EliminarFabricantePorId(id);
            TempData["Message"] = "Fabricante" + fabricante.Nome.ToUpper() + "foi excluído";
            return RedirectToAction("Index");
        }
        /**
        private EFContext context = new EFContext();

        // GET: Fabricantes
        public ActionResult Index()
        {
            return View(context.Fabricantes.OrderBy(c => c.Nome)); //só retorna os fabricante em ordem alfabética
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Fabricante fabricante)
        {
            context.Fabricantes.Add(fabricante); // está pegando o valor passado pelo usuário informando o (fabricante) depois o está adicionando na tabela Fabricantes do banco de dados
            context.SaveChanges(); //está salvando o fabricante no banco de dados
            return RedirectToAction("Index");
        }

        public ActionResult Edit(long? id)          // o ponto de interrogação na frente do long serve para que aceite valor nulo.
        {
            if ( id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Fabricante fabricante = context.Fabricantes.Where(f => f.FabricanteId == id).Include("Produtos.Categoria").First();
            if (fabricante == null)
            {
                return HttpNotFound();
            }
            return View(fabricante);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Fabricante fabricante)
        {
            if (ModelState.IsValid)
            {
                context.Entry(fabricante).State = EntityState.Modified;
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(fabricante);
        }

        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Fabricante fabricante = context.Fabricantes.Where(f => f.FabricanteId == id).Include("Produtos.Categoria").First();

            if (fabricante == null)
            {
                return HttpNotFound();
            }
            return View(fabricante);
        }

        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Fabricante fabricante = context.Fabricantes.Find(id);

            if (fabricante == null)
            {
                return HttpNotFound();
            }

            return View(fabricante);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(long id)
        {
            Fabricante fabricante = context.Fabricantes.Find(id);
            context.Fabricantes.Remove(fabricante);
            context.SaveChanges();
            TempData["Message"] = "Fabricante" + fabricante.Nome.ToUpper() + "foi excluído";
            return RedirectToAction("Index");
        }
        */
    }
}