﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProEventos.Application.Contratos;
using Microsoft.AspNetCore.Http;
using ProEventos.Application.Dtos;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using System.Linq;
using ProEventos.API.Extensions;
using Microsoft.AspNetCore.Authorization;
using ProEventos.Persistence.Models;

namespace ProEventos.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class PalestrantesController : ControllerBase
    {
        private readonly IPalestranteService _palestranteService;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly IAccountService _accountService;

        public PalestrantesController(IPalestranteService palestranteService,
                                    IWebHostEnvironment hostEnvironment,
                                 IAccountService accountService)
        {
            _accountService = accountService;
            _hostEnvironment = hostEnvironment;
            _palestranteService = palestranteService;

        }

        [HttpGet("all")]
        public async Task<IActionResult> Get([FromQuery] PageParams pageParams)
        {
            try
            {
                var palestrantes = await _palestranteService.GetAllPalestrantesAsync(pageParams, true);
                if (palestrantes == null) return NoContent();

                Response.AddPagination(palestrantes.CurrentPage, palestrantes.PageSize, palestrantes.TotalCount, palestrantes.TotalPages);

                return Ok(palestrantes);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                $"Erro ao tentar recuperar palestrantes. Erro: {ex.Message}");
            }
        }

        [HttpGet()]
        public async Task<IActionResult> GetPalestrantes(int id)
        {
            try
            {
                var palestrante = await _palestranteService.GetPalestranteByUserIdAsync(User.GetUserId(), true);
                if (palestrante == null) return NoContent();

                return Ok(palestrante);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                $"Erro ao tentar recuperar palestrantes. Erro: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(PalestranteAddDto model)
        {
            try
            {
                var palestrante = await _palestranteService.GetPalestranteByUserIdAsync(User.GetUserId(), false);
                if (palestrante == null)
                    palestrante = await _palestranteService.AddPalestrantes(User.GetUserId(), model);

                return Ok(palestrante);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                $"Erro ao tentar adicionar palestrantes. Erro: {ex.Message}");
            }
        }

        [HttpPut]
        public async Task<IActionResult> Put(PalestranteUpdateDto model)
        {
            try
            {
                var palestrante = await _palestranteService.UpdatePalestrante(User.GetUserId(), model);
                if (palestrante == null) return NoContent();

                return Ok(palestrante);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                $"Erro ao tentar adicionar palestrantes. Erro: {ex.Message}");
            }
        }

    }
}
