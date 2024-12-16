using Domain.Entities;
using Infratructure.Responses;
using Infratructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers;

[ApiController]
[Route("[controller]")]
public class LoanController(IGenericService<Loan> loanService):ControllerBase
{
    [HttpGet]
    public async Task<ApiResponse<List<Loan>>> GetAll()
    {
        return await loanService.GetAll();
    }
    [HttpGet("{id:int}")]
    public async Task<ApiResponse<Loan>> GetById(int id)
    {
        return await loanService.GetById(id);
    }
    [HttpPost]
    public async Task<ApiResponse<bool>> Add(Loan loan)
    {
        return await loanService.Add(loan);
    }
    [HttpPut]
    public async Task<ApiResponse<bool>> Update(Loan loan)
    {
        return await loanService.Update(loan);
    }
    [HttpDelete]
    public async Task<ApiResponse<bool>> Delete(int id)
    {
        return await loanService.Delete(id);
    }
}