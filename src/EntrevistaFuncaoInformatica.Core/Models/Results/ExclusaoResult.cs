﻿using EntrevistaFuncaoInformatica.Core.Messages;
using EntrevistaFuncaoInformatica.Domain.Bases;

namespace EntrevistaFuncaoInformatica.Core.Models.Results
{
	public class ExclusaoResult<TEntity> : SingleResult<TEntity>
		where TEntity : Entity
	{
		public ExclusaoResult()
		{
			Sucesso = true;
			Mensagem = MensagensNegocio.ResourceManager.GetString("MSG03");
		}

		public ExclusaoResult(bool sucesso, string mensagem)
		{
			this.Sucesso = sucesso;
			this.Mensagem = mensagem;
		}

		public ExclusaoResult(string mensagem)
		{
			this.Sucesso = false;
			this.Mensagem = mensagem;
		}
	}
}
