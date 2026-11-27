using Hotel.AccesoDatos.Contexto;
using Hotel.Dominio.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Hotel.AccesoDatos.Repositorios;

public class Repositorio<T> : IRepositorio<T> where T : class
{
    protected readonly HotelDbContext _db;

    public Repositorio(HotelDbContext db)
    {
        _db = db;
    }

    public virtual async Task<T?> ObtenerPorIdAsync(int id) => await _db.Set<T>().FindAsync(id);

    public virtual async Task<List<T>> ListarAsync() => await _db.Set<T>().AsNoTracking().ToListAsync();

    public async Task AgregarAsync(T entidad) => await _db.Set<T>().AddAsync(entidad);

    public void Actualizar(T entidad) => _db.Set<T>().Update(entidad);

    public void Eliminar(T entidad) => _db.Set<T>().Remove(entidad);
}
