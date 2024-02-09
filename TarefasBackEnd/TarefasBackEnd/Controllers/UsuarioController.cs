﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TarefasBackEnd.Models;
using TarefasBackEnd.Repositories.Interfaces;

namespace TarefasBackEnd.Controllers
{
    [Route("usuario")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        [HttpPost]
        public IActionResult Create([FromBody]Usuario model, [FromServices] IUsuarioRepository repository)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            repository.Create(model);

            return Ok();
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] Usuario model, [FromServices] IUsuarioRepository repository)
        {
            if(!ModelState.IsValid) 
                return BadRequest();

            Usuario usuario = repository.Read(model.Email, model.Senha);
            if (usuario == null)
                return Unauthorized();

            usuario.Senha = "";

            return Ok(new {
                usuario = usuario,
                token = GenerateToken(usuario)
            });
        }

        private string GenerateToken(Usuario usuario)
        {
            var tokenHandler = new JwtSecurityTokenHandler();            
            var key = Encoding.UTF8.GetBytes("UmTokenMuitGrandeEDiferenteParaNinguemDescobrir");
            var descriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, usuario.Id.ToString())

                }),
                Expires = DateTime.UtcNow.AddHours(5),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature
                )
            };

            var token = tokenHandler.CreateToken(descriptor);
            return tokenHandler.WriteToken(token);
        }

    }
}
