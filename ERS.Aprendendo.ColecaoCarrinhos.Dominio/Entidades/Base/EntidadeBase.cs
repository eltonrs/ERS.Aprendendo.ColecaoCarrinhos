﻿namespace ERS.Aprendendo.TestesFuncionais.Dominio.Entidades.Base
{
    public class EntidadeBase
    {
        public Guid Id { get; set; }

        public EntidadeBase()
        {
            //Id = Guid.NewGuid(); // ToDo : O id é controle manual. Facilita as ligações entre tabelas.
        }
    }
}
