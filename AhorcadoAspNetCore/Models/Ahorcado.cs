using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;

public class AhorcadoModel : PageModel
{
    private static readonly Dictionary<string, string> PalabrasYPistas = new Dictionary<string, string>
    {
        { "LAGOS", "Geografía" },
        { "MUNDO", "Geografía" },
        { "LUZ", "Física" },
        { "LUNA", "Astronomía" },
        { "NIEVE", "Clima" },
        { "FLOR", "Naturaleza" },
        { "GATO", "Animales" },
        { "RATON", "Animales" },
        { "BEBE", "Familia" },
        { "SOL", "Astronomía" },
        { "LLAVE", "Objetos" },
        { "NUBE", "Clima" },
        { "CARRO", "Transporte" },
        { "CASA", "Hogar" },
        { "PERRO", "Animales" },
        { "LLAMA", "Animales" },
        { "PEZ", "Animales" },
        { "TIGRE", "Animales" },
        { "MONO", "Animales" },
        { "AVION", "Transporte" }
    };

    public string PalabraSecreta { get; private set; }
    public string Tematica { get; private set; }
    public List<char> LetrasUsadas { get; private set; } = new List<char>();
    public string PalabraMostrada { get; private set; }
    public int IntentosFallidos { get; private set; }
    public int Puntuacion { get; private set; }
    public bool HaGanado { get; private set; }
    public bool HaPerdido { get; private set; }
    public bool TecladoHabilitado => !HaGanado && !HaPerdido;
    public List<char> Alfabeto { get; } = "ABCDEFGHIJKLMNÑOPQRSTUVWXYZ".ToList();

    public void OnGet()
    {
        ReiniciarJuego(true);
    }

    public void OnPost(string letra, bool? reiniciar, bool? siguienteRonda)
    {
        if (reiniciar.HasValue && reiniciar.Value)
        {
            ReiniciarJuego(true);
        }
        else if (siguienteRonda.HasValue && siguienteRonda.Value)
        {
            ReiniciarJuego(false);
        }
        else if (!string.IsNullOrEmpty(letra) && letra.Length == 1)
        {
            ProcesarLetra(letra[0]);
        }
    }

    private void ReiniciarJuego(bool reiniciarPuntuacion)
    {
        var random = new Random();
        var clave = PalabrasYPistas.Keys.ToList()[random.Next(PalabrasYPistas.Count)];
        PalabraSecreta = clave;
        Tematica = PalabrasYPistas[clave];

        LetrasUsadas.Clear();
        IntentosFallidos = 0;
        HaGanado = false;
        HaPerdido = false;

        if (reiniciarPuntuacion)
            Puntuacion = 0;

        ActualizarPalabraMostrada();
    }

    private void ProcesarLetra(char letra)
    {
        LetrasUsadas.Add(letra);

        if (PalabraSecreta.Contains(letra))
        {
            Puntuacion++;
            ActualizarPalabraMostrada();
        }
        else
        {
            IntentosFallidos++;
            if (IntentosFallidos >= 6)
                HaPerdido = true;
        }

        if (PalabraSecreta.Replace(" ", "") == PalabraMostrada.Replace("_", "").Replace(" ", ""))
        {
            HaGanado = true;
        }
    }

    private void ActualizarPalabraMostrada()
    {
        PalabraMostrada = string.Join(" ", PalabraSecreta.Select(letra => LetrasUsadas.Contains(letra) ? letra : '_'));
    }
}
