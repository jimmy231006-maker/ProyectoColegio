using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using ProyectoColegio.Modelos;

namespace ProyectoColegio.Archivos
{
    public static class ArchivoDatos
    {
        private static readonly string RutaDatos = "datos_colegio.dat";

        public static void GuardarTodo(List<Estudiante> estudiantes, List<Profesor> profesores)
        {
            try
            {
                using (BinaryWriter writer = new BinaryWriter(File.Open(RutaDatos, FileMode.Create, FileAccess.Write)))
                {
                    writer.Write(estudiantes.Count);
                    foreach (var e in estudiantes)
                    {
                        writer.Write(e.Id);
                        writer.Write(e.Cedula);
                        writer.Write(e.Nombre);
                        writer.Write(e.Apellido);
                        writer.Write(e.Edad);
                        writer.Write(e.Activo);
                    }

                    writer.Write(profesores.Count);
                    foreach (var p in profesores)
                    {
                        writer.Write(p.Id);
                        writer.Write(p.Cedula);
                        writer.Write(p.Nombre);
                        writer.Write(p.Apellido);
                        writer.Write(p.Materia);
                        writer.Write(p.Activo);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($" [!] Error al guardar datos binarios: {ex.Message}");
            }
        }

        public static void CargarTodo(out List<Estudiante> estudiantes, out List<Profesor> profesores)
        {
            estudiantes = new List<Estudiante>();
            profesores = new List<Profesor>();

            if (!File.Exists(RutaDatos)) return;

            try
            {
                using (BinaryReader reader = new BinaryReader(File.Open(RutaDatos, FileMode.Open, FileAccess.Read)))
                {
                    if (reader.BaseStream.Position < reader.BaseStream.Length)
                    {
                        int totalEstudiantes = reader.ReadInt32();
                        for (int i = 0; i < totalEstudiantes; i++)
                        {
                            estudiantes.Add(new Estudiante
                            {
                                Id = reader.ReadInt32(),
                                Cedula = reader.ReadString(),
                                Nombre = reader.ReadString(),
                                Apellido = reader.ReadString(),
                                Edad = reader.ReadInt32(),
                                Activo = reader.ReadBoolean()
                            });
                        }
                    }

                    if (reader.BaseStream.Position < reader.BaseStream.Length)
                    {
                        int totalProfesores = reader.ReadInt32();
                        for (int i = 0; i < totalProfesores; i++)
                        {
                            profesores.Add(new Profesor
                            {
                                Id = reader.ReadInt32(),
                                Cedula = reader.ReadString(),
                                Nombre = reader.ReadString(),
                                Apellido = reader.ReadString(),
                                Materia = reader.ReadString(),
                                Activo = reader.ReadBoolean()
                            });
                        }
                    }
                }
            }
            catch (EndOfStreamException) { }
            catch (Exception ex)
            {
                Console.WriteLine($" [!] Error al leer datos binarios: {ex.Message}");
            }
        }
    }
}