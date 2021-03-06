﻿using EntrevistaFuncaoInformatica.Domain.Interfaces;

namespace EntrevistaFuncaoInformatica.Domain.Bases
{
	public class LookupDto : ILookupDto
	{
		public int Key { get; set; }
		public int ParentKey { get; set; }
		public string Value { get; set; }
	}
}
