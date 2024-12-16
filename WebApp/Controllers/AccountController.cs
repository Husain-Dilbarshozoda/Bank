using Domain.Entities;
using Infratructure.Responses;
using Infratructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class AccountController(IGenericService<Account> accountService) : ControllerBase
{
    [HttpGet]
    public async Task<ApiResponse<List<Account>>> GetAll()
    {
        return await accountService.GetAll();
    }

    [HttpGet("{id:int}")]
    public async Task<ApiResponse<Account>> GetById(int id)
    {
        return await accountService.GetById(id);
    }

    [HttpPost]
    public async Task<ApiResponse<bool>> Add(Account account)
    {
        return await accountService.Add(account);
    }

    [HttpPut]
    public async Task<ApiResponse<bool>> Update(Account account)
    {
        return await accountService.Update(account);
    }

    [HttpDelete]
    public async Task<ApiResponse<bool>> Delete(int id)
    {
        return await accountService.Delete(id);
    }
}