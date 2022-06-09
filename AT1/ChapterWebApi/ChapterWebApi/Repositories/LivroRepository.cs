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
            // conexão com o banco
            return _context.Livros.ToList();
        }

        // metodo retornar um livro específico
        public Livro BuscarPorId(int id)
        {
            return _context.Livros.Find(id);
        }

        // metodo de cadastro
        public void Cadastrar(Livro livro)
        {
            _context.Livros.Add(livro);

            _context.SaveChanges();
        }

        // metodo atualizar
        public void Atualizar(int id, Livro livro)
        {
            Livro livroBuscado = _context.Livros.Find(id);

            if (livroBuscado != null)
            {
                livroBuscado.Titulo = livro.Titulo;
                livroBuscado.QuantidadePaginas = livro.QuantidadePaginas;
                livroBuscado.Disponivel = livro.Disponivel;
            }

            _context.Livros.Update(livroBuscado);

            _context.SaveChanges();
        }

        // metodo deletar
        public void Deletar(int id)
        {
            Livro livro = _context.Livros.Find(id);

            _context.Livros.Remove(livro);

            _context.SaveChanges();
        }

    }
}
