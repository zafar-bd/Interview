﻿global using Domain.Interfaces;
global using Infrastructure.Data;
global using Infrastructure.Data.Repositories;
global using Interview.Domain.Restaurant;
global using Interview.Domain.Services.Restaurant;
global using Microsoft.EntityFrameworkCore;
global using Microsoft.Extensions.Configuration;
global using Microsoft.Extensions.DependencyInjection;
global using MediatR;
global using Microsoft.AspNetCore.Mvc;
global using System.Reflection;
global using Interview.Domain.Dto;
global using Interview.Domain.ViewModel;
global using FluentValidation;
global using FluentValidation.AspNetCore;
global using Interview.API.ModelValidations;
global using Microsoft.AspNetCore.Mvc.ModelBinding;
global using Microsoft.AspNetCore.Mvc.Filters;
global using Interview.API.Extensions;
global using Interview.API.Filters;
global using Interview.API.Middlewares;
global using System.Text.Json.Serialization;
global using System.Text.Json;
global using Interview.Domain.Helpers;