# 🎮 GameTracker CLI

*Tu biblioteca personal de videojuegos en la terminal*

![C#](https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=c-sharp&logoColor=white)
![.NET](https://img.shields.io/badge/.NET-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)
![Console](https://img.shields.io/badge/Console_App-4EAA25?style=for-the-badge)

## 📖 Descripción

¿Te ha pasado que no sabes qué juego jugar? ¿O que pierdes la noción de cuántas horas has invertido?  

**GameTracker CLI** es tu solución: una aplicación de consola en C# que **organiza, analiza y recomienda** tus videojuegos. Olvídate de listas desordenadas y toma el control de tu backlog.

## ✨ Características

| Función | Descripción |
|---------|-------------|
| 📥 **Gestión de colección** | Agrega, edita y elimina juegos fácilmente |
| 🎯 **Recomendaciones inteligentes** | Sugiere juegos basados en tu progreso y preferencias |
| 📊 **Dashboard de estadísticas** | Métricas detalladas de tu actividad gaming |
| 🔍 **Búsqueda avanzada** | Filtra por género, año, horas jugadas y estado |
| 💾 **Persistencia automática** | Guarda todo en formato JSON sin configuración |
| 🎨 **Interfaz intuitiva** | Menús interactivos con colores y formato |

##Estructura del proyectos
GameTrackerCLI/
├── Models/
│   └── Juego.cs          # Modelo de datos
├── Services/
│   ├── JuegoService.cs   # Lógica de negocio
│   └── EstadisticasService.cs # Cálculos
├── Program.cs            # Menú principal
└── juegos.json          # Datos persistentes

## 🚀 Instalación y uso

```bash
# 1. Clona el repositorio
git clone https://github.com/tuusuario/gametracker-cli.git
cd gametracker-cli

# 2. Ejecuta la aplicación
dotnet run
