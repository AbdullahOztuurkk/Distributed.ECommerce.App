﻿global using Autofac;
global using Autofac.Extensions.DependencyInjection;
global using CoreLib.Configurations;
global using CoreLib.DataAccess.Abstract;
global using CoreLib.DataAccess.Concrete;
global using CoreLib.Domain.Dto.EmailServiceAdapter;
global using CoreLib.Entity.Concrete;
global using CoreLib.Entity.Enums;
global using CoreLib.MapConfiguration;
global using CoreLib.ServiceAdapter.Email;
global using EmailWorkerService.Application.Consumers;
global using EmailWorkerService.Application.Extensions;
global using EmailWorkerService.Application.Services.Concrete;
global using EmailWorkerService.Application.Services.Contracts;
global using EmailWorkerService.Configurations;
global using EmailWorkerService.Domain;
global using EmailWorkerService.Domain.EmailTemplateModel;
global using MassTransit;
global using Microsoft.EntityFrameworkCore;
global using Microsoft.EntityFrameworkCore.Design;
global using Shared.Events.Mail.Base;
global using Shared.Utils.Attributes;
global using System.ComponentModel;
global using System.Reflection;
