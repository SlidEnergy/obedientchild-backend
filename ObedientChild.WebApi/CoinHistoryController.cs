﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ObedientChild.App;
using ObedientChild.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ObedientChild.WebApi
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CoinHistoryController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ICoinHistoryService _service;

        public CoinHistoryController(IMapper mapper, ICoinHistoryService service)
        {
            _mapper = mapper;
            _service = service;
        }

        [HttpGet]
        [ProducesResponseType(200)]
        public async Task<ActionResult<IEnumerable<CoinHistory>>> GetList()
        {
            var list = await _service.GetListAsync();

            return list;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        public async Task<ActionResult<CoinHistory>> GetById(int id)
        {
            var item = await _service.GetByIdAsync(id);

            return item;
        }

        [HttpPut]
        public async Task Add(CoinHistory coinHistory)
        {
            if (ModelState.IsValid)
            {
                await _service.AddAsync(coinHistory);
            }
        }

        [HttpDelete("{id}")]
        public async Task Revert(int id)
        {
            await _service.RevertAsync(id);
        }
    }
}
