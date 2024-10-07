using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using infinitysky.Models;

namespace infinitysky.FavoritosCompra
{
    public class CookieFavoritosCompra
    {
        private const string CookieFavoritos = "Favoritos";
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CookieFavoritosCompra(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public void Cadastrar(Planos item)
        {
            var favoritos = Consultar();
            favoritos.Add(item);
            SalvarFavoritos(favoritos);
        }

        public List<Planos> Consultar()
        {
            if (_httpContextAccessor.HttpContext.Request.Cookies.TryGetValue(CookieFavoritos, out var json))
            {
                var favoritos = JsonConvert.DeserializeObject<List<Planos>>(json);
                return favoritos ?? new List<Planos>(); // Retorna lista vazia se nulo
            }
            return new List<Planos>(); // Retorna lista vazia se não houver cookie
        }

        public void DiminuirPlano(Planos item)
        {
            var favoritos = Consultar();
            if (favoritos != null)
            {
                var plano = favoritos.Find(p => p.IdPlano == item.IdPlano);
                if (plano != null)
                {
                    // Lógica para diminuir a quantidade ou remover se necessário
                    favoritos.Remove(plano);
                    SalvarFavoritos(favoritos);
                }
            }
        }

        public void Remover(Planos item)
        {
            var favoritos = Consultar();
            if (favoritos != null)
            {
                favoritos.RemoveAll(p => p.IdPlano == item.IdPlano);
                SalvarFavoritos(favoritos);
            }
        }

        private void SalvarFavoritos(List<Planos> favoritos)
        {
            var json = JsonConvert.SerializeObject(favoritos);
            _httpContextAccessor.HttpContext.Response.Cookies.Append(CookieFavoritos, json);
        }
    }
}