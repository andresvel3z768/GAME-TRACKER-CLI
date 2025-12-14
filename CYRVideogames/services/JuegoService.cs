using System;
using System.Linq;
using System.Collections.Generic;
using CYRVideogames.Models;
using System.Text.Json;
using System.IO;

namespace CYRVideogames.Services;

public class JuegoService
{
    private List<Juego> _juegos;
    private readonly string _dataFile = "juegos.json";

    public JuegoService()
    {
        _juegos = CargarDatos();
        if (_juegos.Count == 0)
        {
            CargarDatosIniciales();
        }
    }

    private List<Juego> CargarDatos()
    {
        if (File.Exists(_dataFile))
        {
            var json = File.ReadAllText(_dataFile);
            return JsonSerializer.Deserialize<List<Juego>>(json) ?? new List<Juego>();
        }
        return new List<Juego>();
    }

    private void GuardarDatos()
    {
        var json = JsonSerializer.Serialize(_juegos, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(_dataFile, json);
    }

    private void CargarDatosIniciales()
    {
        _juegos = new List<Juego>
        {
            new Juego { Id = 1, Titulo = "The Witcher 3", Genero = "RPG", Lanzamiento = 2015, HorasJugadas = 120, Completado = true },
            new Juego { Id = 2, Titulo = "Celeste", Genero = "Plataformas", Lanzamiento = 2018, HorasJugadas = 15, Completado = false },
            new Juego { Id = 3, Titulo = "Hades", Genero = "Roguelike", Lanzamiento = 2020, HorasJugadas = 65, Completado = true },
            new Juego { Id = 4, Titulo = "Elden Ring", Genero = "Souls-like", Lanzamiento = 2022, HorasJugadas = 95, Completado = false },
            new Juego { Id = 5, Titulo = "Stardew Valley", Genero = "Simulación", Lanzamiento = 2016, HorasJugadas = 60, Completado = true }
        };
        GuardarDatos();
    }

    public List<Juego> ObtenerTodos() => _juegos;

    public Juego? ObtenerPorId(int id) => _juegos.FirstOrDefault(j => j.Id == id);

    public void Agregar(Juego juego)
    {
        juego.Id = _juegos.Count > 0 ? _juegos.Max(j => j.Id) + 1 : 1;
        _juegos.Add(juego);
        GuardarDatos();
    }

    public void Actualizar(Juego juego)
    {
        var existente = ObtenerPorId(juego.Id);
        if (existente != null)
        {
            existente.Titulo = juego.Titulo;
            existente.Genero = juego.Genero;
            existente.Lanzamiento = juego.Lanzamiento;
            existente.HorasJugadas = juego.HorasJugadas;
            existente.Completado = juego.Completado;
            existente.Descripcion = juego.Descripcion;
            GuardarDatos();
        }
    }

    public bool Eliminar(int id)
    {
        var juego = ObtenerPorId(id);
        if (juego != null)
        {
            _juegos.Remove(juego);
            GuardarDatos();
            return true;
        }
        return false;
    }

    public List<Juego> BuscarPorGenero(string genero) =>
        _juegos.Where(j => j.Genero.Contains(genero, StringComparison.OrdinalIgnoreCase)).ToList();

    public List<Juego> ObtenerPendientes() => _juegos.Where(j => !j.Completado).ToList();
}
