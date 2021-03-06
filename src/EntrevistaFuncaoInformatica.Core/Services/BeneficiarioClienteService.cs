﻿using EntrevistaFuncaoInformatica.Core.Bases;
using EntrevistaFuncaoInformatica.Core.Interfaces;
using EntrevistaFuncaoInformatica.Core.Messages;
using EntrevistaFuncaoInformatica.Domain.Models;
using EntrevistaFuncaoInformatica.Core.Models.Results;
using EntrevistaFuncaoInformatica.Core.Repositories;
using System;
using System.Threading.Tasks;

namespace EntrevistaFuncaoInformatica.Core.Services
{
	public class ClienteSistemaService : Service, IClienteSistemaService
	{
		private readonly IClienteSistemaRepository repository;		
		private readonly IClienteSistemaValidation validator;

		public ClienteSistemaService(IClienteSistemaRepository repository, IClienteSistemaValidation validator, IUnitOfWork uow)
			: base(uow)
		{
			this.repository = repository;
			this.validator = validator;
		}

		public async Task<ISingleResult<ClienteSistema>> Incluir(ClienteSistema entity)
		{
			try
			{
				var validacao = await validator.ValidarInclusao(entity);
				if (!validacao.Sucesso)
				{
					return validacao;
				}
				entity.DataRegistro = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time"));
				await repository.Add(entity);

				_ = await Commit();
			}
			catch (Exception)
			{
				return new SingleResult<ClienteSistema>(MensagensNegocio.MSG07);
			}

			return new InclusaoResult<ClienteSistema>(entity);
		}

		public async Task<ISingleResult<ClienteSistema>> Editar(ClienteSistema entity)
		{
			try
			{
				var result = await validator.ValidarEdicao(entity);
				if (!result.Sucesso)
				{
					return result;
				}

				var obj = result.Data;

				HydrateValues(obj, entity);

				repository.Update(obj);

				_ = await Commit();
			}
			catch (Exception ex)
			{
				return new SingleResult<ClienteSistema>(ex);
			}

			return new EdicaoResult<ClienteSistema>();
		}

		public async Task<ISingleResult<ClienteSistema>> Excluir(int id)
		{
			try
			{
				var validacao = await validator.ValidarExclusao(id);
				if (!validacao.Sucesso)
				{
					return validacao;
				}

				repository.Remove(id);

				_ = await Commit();
			}
			catch (Exception)
			{
				return new SingleResult<ClienteSistema>(MensagensNegocio.MSG07);
			}

			return new ExclusaoResult<ClienteSistema>();
		}

		private void HydrateValues(ClienteSistema target, ClienteSistema source)
		{
			target.Codigo = source.Codigo;
		}
	}
}
