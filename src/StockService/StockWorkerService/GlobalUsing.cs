global using Autofac;
global using Autofac.Extensions.DependencyInjection;
global using CoreLib.DataAccess.Abstract;
global using CoreLib.DataAccess.Concrete;
global using CoreLib.Entity.Enums;
global using CoreLib.ResponseModel;
global using MapsterMapper;
global using MassTransit;
global using Microsoft.EntityFrameworkCore;
global using Microsoft.EntityFrameworkCore.Design;
global using Shared.Domain.Constant;
global using Shared.Domain.ValueObject;
global using Shared.Events.Stock;
global using StockWorkerService;
global using StockWorkerService.Application;
global using StockWorkerService.Application.Consumers;
global using StockWorkerService.Application.Services.Abstract;
global using StockWorkerService.Configurations;
global using StockWorkerService.Domain.Dtos;
global using StockWorkerService.Domain.Entities;
