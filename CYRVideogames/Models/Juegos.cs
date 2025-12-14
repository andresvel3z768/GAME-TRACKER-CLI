using System;
using System.Linq;
using System.Collections.Generic;
using CYRVideogames;

namespace CYRVideogames.Models;

public class Juego  // Mejor singular: "Juego" no "Juegos"
{
    public int Id { get; set; }  // Cambia a público
    public string Titulo { get; set; } = "";
    public string Genero { get; set; } = "";
    public int Lanzamiento { get; set; }  // Usa mayúscula
    public string Descripcion { get; set; } = "";
    public int HorasJugadas { get; set; }
    public bool Completado { get; set; }  // Añade esto


}
