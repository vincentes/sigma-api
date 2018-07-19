// Decompiled with JetBrains decompiler
// Type: API.Controllers.ImagenController
// Assembly: API, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4B418147-8FFB-41A2-8EEF-9BE2FCA642AC
// Assembly location: C:\Users\micro\Documents\decompiling\API.dll

using API.Models;
using API.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace API.Controllers
{
    [Produces("application/json", new string[] { })]
    [Route("[controller]/[action]")]
    public class ImagenController : Controller
    {
        private readonly IRepository<Imagen> _repo;

        public ImagenController(IRepository<Imagen> repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public IEnumerable<ImagenDto> Get()
        {
            IEnumerable<Imagen> all = this._repo.GetAll();
            List<ImagenDto> imagenDtoList = new List<ImagenDto>();
            foreach (Imagen imagen in all)
                imagenDtoList.Add(this.DtoGet(imagen));
            return imagenDtoList;
        }

        [HttpGet("{id}", Name = "GetImagen")]
        public IActionResult Get(int id)
        {
            Imagen byId = this._repo.GetById(id);
            if (byId == null)
                return NotFound();
            return Ok(DtoGet(byId));
        }

        [Authorize]
        [HttpGet("{id}", Name = "DownloadImagen")]
        public IActionResult Download(int id)
        {
            Imagen byId = this._repo.GetById(id);
            if (byId == null)
                return NotFound();
            return File(System.IO.File.ReadAllBytes(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), byId.Url)), "image/jpeg");
        }

        [HttpPost]
        [Authorize]
        public IActionResult Upload(IFormFile file)
        {
            if (file == null)
                return BadRequest();
            string path2 = string.Format("Images\\{0}\\{1}", User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name").Value, (object)(DateTime.Now.ToString("yyyy-mm-dd-hh-mm-ss") + ".jpeg"));
            string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), path2);
            _repo.Add(new Imagen() { Url = path2 });
            using (MemoryStream memoryStream = new MemoryStream())
            {
                file.CopyTo(memoryStream);
                Directory.CreateDirectory(Path.GetDirectoryName(path));
                System.IO.File.WriteAllBytes(path, memoryStream.ToArray());
            }
            return Ok();
        }

        public ImagenDto DtoGet(Imagen imagen)
        {
            return new ImagenDto()
            {
                Id = imagen.Id,
                ImageUrl = imagen.Url
            };
        }

        [HttpPost]
        public IActionResult Post([FromBody] Imagen value)
        {
            if (value == null)
                return BadRequest();
            Imagen imagen = _repo.Add(value);
            return CreatedAtRoute("GetImagen", (object)new
            {
                id = imagen.Id
            }, imagen);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Imagen value)
        {
            if (value == null)
                return BadRequest();
            Imagen byId = this._repo.GetById(id);
            if (byId == null)
                return NotFound();
            byId.Id = id;
            _repo.Update(value);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Imagen byId = _repo.GetById(id);
            if (byId == null)
                return NotFound();
            _repo.Delete(byId);
            return NoContent();
        }

        public class ImagenDto
        {
            public int Id { get; set; }

            public string ImageUrl { get; set; }
        }
    }
}
