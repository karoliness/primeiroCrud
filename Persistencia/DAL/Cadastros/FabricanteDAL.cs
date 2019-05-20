using Persistencia.Contexts;
using System.Linq;
using System.Data.Entity;
using Modelo.Cadastros;

namespace Persistencia.DAL.Cadastros
{
    public class FabricanteDAL
    {
        private EFContext context = new EFContext();

        public IQueryable<Fabricante> ObterFabricantesClassificadosPorNome()
        {
            return context.Fabricantes.Include(c => c.).Include(p => p.Produtos).OrderBy(n => n.Nome);
        }
        
        public Fabricante ObterFabricantePorId(long? id )
        {
            return context.Fabricantes.Find(id);
        }

        public void GravarFabricante(Fabricante fabricante)
        {
                context.Fabricantes.Add(fabricante);
                context.Entry(fabricante).State = EntityState.Modified;
                context.SaveChanges();
        }

        public Fabricante EliminarFabricantePorId(long id)
        {
            Fabricante fabricante = ObterFabricantePorId(id);
            context.Fabricantes.Remove(fabricante);
            context.SaveChanges();
            return fabricante;
        }
    }
}
