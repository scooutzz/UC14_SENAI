using ChapterWebApi.Contexts;
using ChapterWebApi.Models;

namespace ChapterWebApi.Repositories
{
    public class LivroRepository
    {
        private readonly ChapterContext _context;

        public LivroRepository(ChapterContext context)
        {
            _context = context;
        }

        public List<Livro> Listar()
        {
            return _context.livros.ToList();
        }
    }
}
