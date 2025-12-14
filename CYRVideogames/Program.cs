using System;
using System.Linq;
using CYRVideogames.Models;
using CYRVideogames.Services;

namespace CYRVideogames;

class Program
{
    static JuegoService _juegoService = new();
    static EstadisticasService _estadisticasService = new(_juegoService);
    static Random _random = new();

    static void Main(string[] args)
    {
        while (true)
        {
            MostrarMenu();
            var opcion = Console.ReadLine();

            switch (opcion)
            {
                case "1": MostrarJuegos(); break;
                case "2": MostrarEstadisticas(); break;
                case "3": MostrarRecomendacion(); break;
                case "4": AgregarJuego(); break;
                case "5": BuscarJuego(); break;
                case "6": EditarJuego(); break;
                case "7": EliminarJuego(); break;
                case "8": return;
                default: Console.WriteLine("Opción inválida"); break;
            }

            Console.WriteLine("\nPresiona Enter para continuar...");
            Console.ReadLine();
        }
    }

    static void MostrarMenu()
    {
        Console.Clear();
        Console.WriteLine(@"
    ╔══════════════════════════════════════╗
    ║         🎮 GAME TRACKER CLI          ║
    ║      Tu biblioteca de juegos         ║
    ╚══════════════════════════════════════╝");
        Console.WriteLine("\t\t======================");
        Console.WriteLine("\t\t1. Ver todos los juegos");
        Console.WriteLine("\t\t2. Ver estadísticas");
        Console.WriteLine("\t\t3. Obtener recomendación");
        Console.WriteLine("\t\t4. Agregar juego");
        Console.WriteLine("\t\t5. Buscar juego");
        Console.WriteLine("\t\t6. Editar juego");
        Console.WriteLine("\t\t7. Eliminar juego");
        Console.WriteLine("\t\t8. Salir");
        Console.Write("\t\t\t\nSelecciona: ");
    }

    static void MostrarJuegos()
    {
        var juegos = _juegoService.ObtenerTodos();

        Console.WriteLine($"\n📚 TU COLECCIÓN ({juegos.Count} juegos):");
        Console.WriteLine(new string('=', 50));

        foreach (var juego in juegos)
        {
            Console.WriteLine($"[{juego.Id}] {juego.Titulo}");
            Console.WriteLine($"   Género: {juego.Genero} | Año: {juego.Lanzamiento}");
            Console.WriteLine($"   Horas: {juego.HorasJugadas}h | Estado: {(juego.Completado ? "✅ Completado" : "⏳ Pendiente")}");
            Console.WriteLine();
        }
    }

    static void MostrarEstadisticas()
    {
        var stats = _estadisticasService.ObtenerTodasEstadisticas();

        Console.WriteLine("\n📊 ESTADÍSTICAS DETALLADAS:");
        Console.WriteLine(new string('=', 50));
        Console.WriteLine($"🎮 Total juegos: {stats["TotalJuegos"]}");
        Console.WriteLine($"⏱️  Horas totales: {stats["TotalHoras"]}h");
        Console.WriteLine($"✅ Completados: {stats["JuegosCompletados"]}");
        Console.WriteLine($"⏳ Pendientes: {stats["JuegosPendientes"]}");
        Console.WriteLine($"📊 Promedio horas/juego: {stats["PromedioHoras"]:F1}h");
        Console.WriteLine($"🔥 Juego más largo: {stats["JuegoMasLargo"]}");
        Console.WriteLine($"🏆 Género más común: {stats["GeneroMasComun"]}");
    }

    static void MostrarRecomendacion()
    {
        var pendientes = _juegoService.ObtenerPendientes();

        if (pendientes.Any())
        {
            var recomendado = pendientes[_random.Next(pendientes.Count)];

            Console.WriteLine("\n🎯 RECOMENDACIÓN INTELIGENTE:");
            Console.WriteLine(new string('=', 50));
            Console.WriteLine($"¡Deberías jugar: {recomendado.Titulo}!");
            Console.WriteLine($"• Género: {recomendado.Genero}");
            Console.WriteLine($"• Horas invertidas: {recomendado.HorasJugadas}h");
            Console.WriteLine($"• Año: {recomendado.Lanzamiento}");
            Console.WriteLine($"• Estado: ⏳ Pendiente");
        }
        else
        {
            Console.WriteLine("\n🎉 ¡Felicidades! Ya completaste todos tus juegos.");
            Console.WriteLine("¿Por qué no agregas uno nuevo?");
        }
    }

    static void AgregarJuego()
    {
        Console.WriteLine("\n➕ AGREGAR NUEVO JUEGO:");
        Console.WriteLine(new string('=', 50));

        var nuevo = new Juego();

        Console.Write("Título: ");
        nuevo.Titulo = Console.ReadLine()?.Trim() ?? "";

        Console.Write("Género: ");
        nuevo.Genero = Console.ReadLine()?.Trim() ?? "";

        Console.Write("Año de lanzamiento: ");
        _ = int.TryParse(Console.ReadLine(), out int anio);
        nuevo.Lanzamiento = anio;

        Console.Write("Horas jugadas: ");
        _ = int.TryParse(Console.ReadLine(), out int horas);
        nuevo.HorasJugadas = horas;

        Console.Write("¿Completado? (s/n): ");
        nuevo.Completado = (Console.ReadLine()?.ToLower() ?? "") == "s";

        _juegoService.Agregar(nuevo);
        Console.WriteLine($"\n✅ ¡{nuevo.Titulo} agregado exitosamente!");
    }

    static void BuscarJuego()
    {
        Console.WriteLine("\n🔍 BUSCAR JUEGO:");
        Console.WriteLine(new string('=', 50));
        Console.Write("Buscar por género: ");
        var genero = Console.ReadLine()?.Trim() ?? "";

        var resultados = _juegoService.BuscarPorGenero(genero);

        if (resultados.Any())
        {
            Console.WriteLine($"\nResultados para '{genero}':");
            foreach (var juego in resultados)
            {
                Console.WriteLine($"• {juego.Titulo} ({juego.Genero}) - {juego.HorasJugadas}h");
            }
        }
        else
        {
            Console.WriteLine($"\nNo se encontraron juegos del género '{genero}'");
        }
    }

    static void EditarJuego()
    {
        Console.WriteLine("\n✏️ EDITAR JUEGO:");
        Console.WriteLine(new string('=', 50));
        Console.Write("ID del juego a editar: ");

        if (int.TryParse(Console.ReadLine(), out int id))
        {
            var juego = _juegoService.ObtenerPorId(id);

            if (juego != null)
            {
                Console.WriteLine($"Editando: {juego.Titulo}");
                Console.WriteLine("Deja en blanco para mantener valor actual");

                Console.Write($"Título [{juego.Titulo}]: ");
                var titulo = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(titulo)) juego.Titulo = titulo;

                Console.Write($"Género [{juego.Genero}]: ");
                var genero = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(genero)) juego.Genero = genero;

                Console.Write($"Horas jugadas [{juego.HorasJugadas}]: ");
                var horasStr = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(horasStr) && int.TryParse(horasStr, out int horas))
                    juego.HorasJugadas = horas;

                Console.Write($"Completado [{(juego.Completado ? "s" : "n")}]: ");
                var completado = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(completado))
                    juego.Completado = completado.ToLower() == "s";

                _juegoService.Actualizar(juego);
                Console.WriteLine($"\n✅ ¡{juego.Titulo} actualizado!");
            }
            else
            {
                Console.WriteLine($"Juego con ID {id} no encontrado");
            }
        }
    }

    static void EliminarJuego()
    {
        Console.WriteLine("\n🗑️ ELIMINAR JUEGO:");
        Console.WriteLine(new string('=', 50));
        Console.Write("ID del juego a eliminar: ");

        if (int.TryParse(Console.ReadLine(), out int id))
        {
            var juego = _juegoService.ObtenerPorId(id);

            if (juego != null)
            {
                Console.Write($"¿Seguro que quieres eliminar '{juego.Titulo}'? (s/n): ");
                if (Console.ReadLine()?.ToLower() == "s")
                {
                    _juegoService.Eliminar(id);
                    Console.WriteLine($"✅ ¡{juego.Titulo} eliminado!");
                }
            }
            else
            {
                Console.WriteLine($"Juego con ID {id} no encontrado");
            }
        }
    }
}
