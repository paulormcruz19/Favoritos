using infinitysky.Models;
using infinitysky.Repository;
using infinitysky.CarrinhoCompra;
using infinitysky.FavoritosCompra;
using InfinitySky.Libraries.Login;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Newtonsoft.Json;

namespace infinitysky.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private IClienteRepository? _clienteRepositorio;
        private LoginCliente _loginCliente;
        private IPlanosRepositorio _planosRepositorio;
        private CookieCarrinhoCompra _cookieCarrinhoCompra;
        private CookieFavoritosCompra _cookieFavoritosCompra;

        public HomeController(ILogger<HomeController> logger, IClienteRepository clienteRepositorio, LoginCliente loginCliente, IPlanosRepositorio planosRepositorio, CookieCarrinhoCompra cookieCarrinhoCompra, CookieFavoritosCompra cookieFavoritosCompra)
      
        {
            _logger = logger;
            _clienteRepositorio = clienteRepositorio;
            _loginCliente = loginCliente;
            _planosRepositorio = planosRepositorio;
            _cookieCarrinhoCompra = cookieCarrinhoCompra;
            _cookieFavoritosCompra = cookieFavoritosCompra;
        }

        public IActionResult Index()
        {
            var planosAleatorios = _planosRepositorio.ObterPlanosAleatorios(9);
            return View(planosAleatorios);
        }

        public IActionResult PainelCliente()
        {
            return RedirectToAction(nameof(DadosCliente));
        }

        public IActionResult DadosCliente()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(Cliente cliente)
        {
            Cliente loginDB = _clienteRepositorio.Login(cliente.Email, cliente.Senha);

            if (loginDB.Email != null && loginDB.Senha != null)
            {
                _loginCliente.Login(loginDB);
                return RedirectToAction(nameof(DadosCliente));
            }
            else
            {
                ViewData["msg"] = "Dados errados";
                return View();
            }
        }

        public IActionResult Carrinho()
        {
            var carrinho = _cookieCarrinhoCompra.Consultar();
            return View(carrinho);
        }

        [HttpPost]
        public IActionResult AdicionarItem(Int64 id)
        {
            Planos plano = _planosRepositorio.ObterPlano(id);

            if (plano == null)
            {
                return View("NaoExisteItem");
            }
            else
            {
                var item = new Planos()
                {
                    IdPlano = plano.IdPlano,
                    QtdPlano = 1, // Você pode começar a quantidade em 1
                    image_plano = plano.image_plano, // Captura a imagem
                    Nome = plano.Nome, // Captura o nome
                    Valor = plano.Valor, // Captura o valor
                    HospedagemPlano = plano.HospedagemPlano, // Captura a hospedagem
                    CursoPlano = plano.CursoPlano, // Captura o curso
                    InstituicaoPlano = plano.InstituicaoPlano, // Captura a instituição
                    DescricaoPlano = plano.DescricaoPlano, // Captura a descrição
                    PeriodoPlano = plano.PeriodoPlano, // Captura o período
                    ParcelaPlano = plano.ParcelaPlano,
                };

                _cookieCarrinhoCompra.Cadastrar(item);
                return RedirectToAction(nameof(Carrinho));
            }
        }

        [HttpPost]
        public IActionResult AdicionarItemFavoritos(Int64 id)
        {
            Planos plano = _planosRepositorio.ObterPlano(id);

            if (plano == null)
            {
                return View("NaoExisteItem");
            }
            else
            {
                var itemfavoritos = new Planos()
                {
                    IdPlano = plano.IdPlano,
                    QtdPlano = 1, // Você pode começar a quantidade em 1
                    image_plano = plano.image_plano, // Captura a imagem
                    Nome = plano.Nome, // Captura o nome
                    Valor = plano.Valor, // Captura o valor
                    HospedagemPlano = plano.HospedagemPlano, // Captura a hospedagem
                    CursoPlano = plano.CursoPlano, // Captura o curso
                    InstituicaoPlano = plano.InstituicaoPlano, // Captura a instituição
                    DescricaoPlano = plano.DescricaoPlano, // Captura a descrição
                    PeriodoPlano = plano.PeriodoPlano, // Captura o período
                    ParcelaPlano = plano.ParcelaPlano,
                };

                _cookieFavoritosCompra.Cadastrar(itemfavoritos);
                return RedirectToAction(nameof(Favoritos));
            }
        }

        public IActionResult DiminuirItem(Int64 id)
        {
            Planos plano = _planosRepositorio.ObterPlano(id);
            if (plano == null)
            {
                return View("NaoExisteItem");
            }
            else
            {
                var item = new Planos()
                {
                    IdPlano = plano.IdPlano,
                    QtdPlano = 1, // Você pode começar a quantidade em 1
                    image_plano = plano.image_plano, // Captura a imagem
                    Nome = plano.Nome, // Captura o nome
                    Valor = plano.Valor, // Captura o valor
                    HospedagemPlano = plano.HospedagemPlano, // Captura a hospedagem
                    CursoPlano = plano.CursoPlano, // Captura o curso
                    InstituicaoPlano = plano.InstituicaoPlano, // Captura a instituição
                    DescricaoPlano = plano.DescricaoPlano, // Captura a descrição
                    PeriodoPlano = plano.PeriodoPlano, // Captura o período
                    ParcelaPlano = plano.ParcelaPlano,
                };

                _cookieCarrinhoCompra.DiminuirPlano(item);
                return RedirectToAction(nameof(Carrinho));
            }
        }

        public IActionResult DiminuirItemFavoritos(Int64 id)
        {
            Planos plano = _planosRepositorio.ObterPlano(id);
            if (plano == null)
            {
                return View("NaoExisteItem");
            }
            else
            {
                var itemfavoritos = new Planos()
                {
                    IdPlano = plano.IdPlano,
                    QtdPlano = 1, // Você pode começar a quantidade em 1
                    image_plano = plano.image_plano, // Captura a imagem
                    Nome = plano.Nome, // Captura o nome
                    Valor = plano.Valor, // Captura o valor
                    HospedagemPlano = plano.HospedagemPlano, // Captura a hospedagem
                    CursoPlano = plano.CursoPlano, // Captura o curso
                    InstituicaoPlano = plano.InstituicaoPlano, // Captura a instituição
                    DescricaoPlano = plano.DescricaoPlano, // Captura a descrição
                    PeriodoPlano = plano.PeriodoPlano, // Captura o período
                    ParcelaPlano = plano.ParcelaPlano,
                };

                _cookieFavoritosCompra.DiminuirPlano(itemfavoritos);
                return RedirectToAction(nameof(Carrinho));
            }
        }

        public IActionResult RemoverItem(Int64 id)
        {
            _cookieCarrinhoCompra.Remover(new Planos() { IdPlano = id });
            return Json(new { success = true });
        }

        public IActionResult RemoverItemFavoritos(Int64 id)
        {
            _cookieFavoritosCompra.Remover(new Planos() { IdPlano = id });
            return Json(new { success = true });
        }

        [HttpPost]
        public IActionResult CadastrarCliente(Cliente cliente)
        {
            _clienteRepositorio.Cadastrar(cliente);
            return RedirectToAction(nameof(PainelCliente));
        }

        public IActionResult Favoritos()
        {
            var favoritos = _cookieFavoritosCompra.Consultar();

            if (favoritos == null)
            {
                favoritos = new List<Planos>(); // Garante que nunca seja nulo
            }

            return View(favoritos); // Retorna para a view
        }

        public IActionResult Sobre()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
