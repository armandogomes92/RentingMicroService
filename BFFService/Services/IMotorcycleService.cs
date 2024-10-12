﻿using BFFService.Models;
using Refit;

namespace BFFService.Services;

public interface IMotorcycleService
{
    [Post("motos")]
    Task<HttpResponseMessage> Post([Body] Moto moto);

    [Get("motos")]
    Task<HttpResponseMessage> GetMotos();

    [Put("motos/{id}/placa")]
    Task<HttpResponseMessage> Put([AliasAs("id")] string id, [Body] string placa);

    [Get("motos/{id}")]
    Task<HttpResponseMessage> GetById([AliasAs("id")] string id);

    [Delete("motos/{id}")]
    Task<HttpResponseMessage> Delete([AliasAs("id")] string id);
}