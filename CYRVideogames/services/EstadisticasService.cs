using System;
using System.Linq;
using System.Collections.Generic;
using CYRVideogames.Models;

namespace CYRVideogames.Services;

public class EstadisticasService
{
    private readonly JuegoService _juegoService;

    public EstadisticasService(JuegoService juegoService)
    {
        _juegoService = juegoService;
    }
    #nullable enable
    public int TotalJuegos() => _juegoService.ObtenerTodos().Count;

    public int TotalHoras() => _juegoService.ObtenerTodos().Sum(j => j.HorasJugadas);

    public int JuegosCompletados() => _juegoService.ObtenerTodos().Count(j => j.Completado);

    public Juego? JuegoMasLargo() => _juegoService.ObtenerTodos()
        .OrderByDescending(j => j.HorasJugadas)
        .FirstOrDefault();

    public string GeneroMasComun()
    {
        var grupos = _juegoService.ObtenerTodos()
            .GroupBy(j => j.Genero)
            .OrderByDescending(g => g.Count())
            .FirstOrDefault();

        return grupos?.Key ?? "N/A";
    }

    public Dictionary<string, object> ObtenerTodasEstadisticas()
    {
        var juegos = _juegoService.ObtenerTodos();

        return new Dictionary<string, object>
        {
            ["TotalJuegos"] = juegos.Count,
            ["TotalHoras"] = juegos.Sum(j => j.HorasJugadas),
            ["JuegosCompletados"] = juegos.Count(j => j.Completado),
            ["JuegosPendientes"] = juegos.Count(j => !j.Completado),
            ["PromedioHoras"] = juegos.Count > 0 ? juegos.Average(j => j.HorasJugadas) : 0,
            ["JuegoMasLargo"] = JuegoMasLargo()?.Titulo ?? "N/A",
            ["GeneroMasComun"] = GeneroMasComun()
        };
    }
}
