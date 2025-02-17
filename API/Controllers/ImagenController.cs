﻿using API.Models;
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
        public IActionResult Upload()
        {
            IFormFileCollection files = Request.Form.Files;
            List<int> ids = new List<int>();
            foreach(IFormFile file in files)
            {
                Imagen imagen = _repo.Add(new Imagen());
                string subPath = string.Format("Images/{0}/{1}", User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name").Value, imagen.Id + ".jpeg");
                string absolutePath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), subPath);
                imagen.Url = subPath;
                _repo.Update(imagen);
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    file.CopyTo(memoryStream);
                    Directory.CreateDirectory(Path.GetDirectoryName(absolutePath));
                    System.IO.File.WriteAllBytes(absolutePath, memoryStream.ToArray());
                }
                ids.Add(imagen.Id);
            }

            return Ok(new {
                Ids = ids.ToArray()
            });
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
