using Domain.Entities;
using Infratructure.Responses;
using Infratructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers;

[ApiController]
[Route("[controller]")]
public class TransactionController(IGenericService<Transaction> tranService):ControllerBase
{
    [HttpGet]
    public async Task<ApiResponse<List<Transaction>>> GetAll()
    {
        return await tranService.GetAll();
    }
    [HttpGet("{id:int}")]
    public async Task<ApiResponse<Transaction>> GetById(int id)
    {
        return await tranService.GetById(id);
    }
    [HttpPost]
    public async Task<ApiResponse<bool>> Add(Transaction transaction)
    {
        return await tranService.Add(transaction);
    }
    [HttpPut]
    public async Task<ApiResponse<bool>> Update(Transaction transaction)
    {
        return await tranService.Update(transaction);
    }
    [HttpDelete]
    public async Task<ApiResponse<bool>> Delete(int id)
    {
        return await tranService.Delete(id);
    }
}