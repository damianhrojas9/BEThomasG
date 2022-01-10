using BEThomasG.Data;
using BEThomasG.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BEThomasG.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArchivosController : ControllerBase
    {
        public readonly ApplicationDbContext context;
        public ArchivosController(ApplicationDbContext context)
        {
            this.context = context;
        }

        // GET: api/<ArchivosController>
        [HttpGet]

        public ActionResult Get()
        {
            try
            {
                return Ok(context.archivo.ToList());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET api/<ArchivosController>/5
        [HttpGet("{id}", Name ="GetArchivo")]
        public ActionResult Get (int id)
        {
            try
            {
                var archivo = context.archivo.FirstOrDefault(f => f.id == id);
                return Ok(archivo);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST api/<ArchivosController>
        [HttpPost]
        public ActionResult PostArchivos([FromForm]List<IFormFile> files)
        {
            List<Archivo> archivos = new List<Archivo>();
            try
            {
                if (files.Count>0)
                {
                    foreach (var file in files)
                    {
                       var filePath = "C:\\Users\\57313\\Desktop\\Prueba\\BEThomasG\\BEThomasG\\Archivos" + file.FileName;
                        using (var stream = System.IO.File.Create(filePath))
                        {
                            file.CopyToAsync(stream);
                        }
                        double tamanio = file.Length;
                        tamanio = tamanio / 1048576;
                        tamanio = Math.Round(tamanio, 2);
                        Archivo archivo = new Archivo();
                        archivo.extension = Path.GetExtension(file.FileName).Substring(1);
                        archivo.extension = Path.GetFileNameWithoutExtension(file.FileName);
                        archivo.tamanio = tamanio;
                        archivo.ubicacion = filePath;
                        Random rdn = new Random();
                        int codigo = rdn.Next(1000, 1099);
                        archivo.codigo = Convert.ToString(codigo);

                        archivos.Add(archivo);
                    }
                    context.archivo.AddRange(archivos);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
            return Ok(archivos);
        }

        // PUT api/<ArchivosController>/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] Archivo archivo)
        {
            try
            {
                if (archivo.id == id)
                {
                    context.Entry(archivo).State = EntityState.Modified;
                    context.SaveChanges();
                    return CreatedAtRoute("GetArchivo", new { id = archivo.id }, archivo);
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        // DELETE api/<ArchivosController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            try
            {
                var archivo = context.archivo.FirstOrDefault(f => f.id == id);
                if (archivo !=null)
                {
                    context.archivo.Remove(archivo);
                    context.SaveChanges();
                    return Ok(id);
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
