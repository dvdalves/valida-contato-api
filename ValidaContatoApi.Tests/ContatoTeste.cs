using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using ValidaContatoApi.Business.Interface;
using ValidaContatoApi.Business.ViewModels;
using ValidaContatoApi.Domain.Enum;
using ValidaContatoApi.Domain.Models;

namespace ValidaContatoApi.Tests
{
    public class ContatoTeste : TesteBase
    {
        public IContatoService _contatoService;

        public ContatoTeste()
        {
            _contatoService = _serviceProvider.GetRequiredService<IContatoService>();
        }

        [Test]
        public async Task ObterTodos_Sucesso()
        {
            //expected
            var resultadoEsperado = 200;
            var mensagemEsperada = "Sucesso";

            //arrange
            PopularBancoDeDados(ContatoMoq.ObterMoq_Para_ObterTodosContatos());

            //act
            var resultAct = await _contatoService.ObterTodos();

            //assert	
            Assert.That(resultAct.StatusCode, Is.EqualTo(resultadoEsperado));
            Assert.That(resultAct.Message, Is.EqualTo(mensagemEsperada));
            Assert.That(resultAct.Result.Any(), Is.True);
            Assert.That(resultAct.IsSuccess, Is.True);
        }

        [Test]
        public async Task ObterTodos_Falha()
        {
            // expected
            var resultadoEsperado = 204;
            var mensagemEsperada = "Nenhum registro encontrado!";

            // arrange
            await _context.Database.EnsureDeletedAsync();
            await _context.Database.EnsureCreatedAsync();

            // act
            var resultAct = await _contatoService.ObterTodos();

            // assert    
            Assert.That(resultAct.StatusCode, Is.EqualTo(resultadoEsperado));
            Assert.That(resultAct.Result, Is.Null);
            Assert.That(resultAct.Message, Is.EqualTo(mensagemEsperada));
            Assert.That(resultAct.IsSuccess, Is.False);
        }

        [Test]
        public async Task ObterPorId_Falha()
        {
            //expected
            var resultadoEsperado = 204;
            var mensagemEsperada = "Contato não encontrado!";

            //arrange
            await _context.Database.EnsureDeletedAsync();
            await _context.Database.EnsureCreatedAsync();

            //act
            var resultAct = await _contatoService.ObterPorId(Guid.NewGuid());

            //assert
            Assert.That(resultAct.StatusCode, Is.EqualTo(resultadoEsperado));
            Assert.That(resultAct.Message, Is.EqualTo(mensagemEsperada));
            Assert.That(resultAct.Result, Is.Null);
            Assert.That(resultAct.IsSuccess, Is.False);
        }

        [Test]
        public async Task ObterPorId_Sucesso()
        {
            //expected  
            var resultadoEsperado = 200;
            var mensagemEsperada = "Contato obtido com sucesso!";

            //arrange
            PopularBancoDeDados(ContatoMoq.ObterMoq_Para_ObterContatoPorId());

            //act
            var constato = await _context.Contatos.FirstOrDefaultAsync();
            var resultAct = await _contatoService.ObterPorId(constato.Id);

            //assert
            Assert.That(resultAct.StatusCode, Is.EqualTo(resultadoEsperado));
            Assert.That(resultAct.Message, Is.EqualTo(mensagemEsperada));
            Assert.That(resultAct.Result, Is.Not.Null);
            Assert.That(resultAct.IsSuccess, Is.True);
        }

        [Test]
        public async Task Adicionar_Sucesso()
        {
            //expected
            var resultadoEsperado = 200;
            var mensagemEsperada = "Contato adicionado com sucesso!";

            //arrange 
            var createContato = new CriarContatoVM
            {
                DataNascimento = DateTime.Parse("05-10-1995"),
                Sexo = (int)SexoEnum.Masculino,
                Nome = "David",
            };

            //act 
            var resultadoAcao = await _contatoService.Adicionar(createContato);
            var contatoExiste = await _context.Contatos.AnyAsync(p => p.Nome == createContato.Nome);

            //assert
            Assert.That(resultadoAcao.StatusCode, Is.EqualTo(resultadoEsperado));
            Assert.That(resultadoAcao.Message, Is.EqualTo(mensagemEsperada));
            Assert.That(resultadoAcao.Result, Is.Not.Null);
            Assert.That(resultadoAcao.Result.Nome, Is.EqualTo(createContato.Nome));
            Assert.That(contatoExiste, Is.True);
            Assert.That(resultadoAcao.IsSuccess, Is.True);
        }

        [Test]
        public async Task Adicionar_IdadeInvalida_Falha()
        {
            //expected
            var resultadoEsperado = 400;
            var mensagemEsperada = "Contato não pode ser menor de idade!";

            //arrange
            var createContato = new CriarContatoVM
            {
                DataNascimento = DateTime.Now,
                Sexo = (int)SexoEnum.Masculino,
                Nome = "David",
            };

            //act
            var resultadoAcao = await _contatoService.Adicionar(createContato);
            var contatoExiste = await _context.Contatos.AnyAsync(p => p.Nome == createContato.Nome);

            //assert	
            Assert.That(resultadoAcao.StatusCode, Is.EqualTo(resultadoEsperado));
            Assert.That(resultadoAcao.Message, Is.EqualTo(mensagemEsperada));
            Assert.That(resultadoAcao.Result, Is.Null);
            Assert.That(resultadoAcao.IsSuccess, Is.False);
            Assert.That(contatoExiste, Is.False);
        }



        [Test]
        public async Task Adicionar_DataInvalida_Falha()
        {
            //expected
            var resultadoEsperado = 400;
            var mensagemEsperada = "Não pode ser selecionada data maior ou igual a atual!";

            //arrange
            var createContato = new CriarContatoVM
            {
                DataNascimento = DateTime.Now.AddDays(1),
                Sexo = (int)SexoEnum.Masculino,
                Nome = "David",
            };

            //act
            var resultadoAcao = await _contatoService.Adicionar(createContato);
            var contatoExiste = await _context.Contatos.AnyAsync(p => p.Nome == createContato.Nome);

            //assert	
            Assert.That(resultadoAcao.StatusCode, Is.EqualTo(resultadoEsperado));
            Assert.That(resultadoAcao.Message, Is.EqualTo(mensagemEsperada));
            Assert.That(resultadoAcao.Result, Is.Null);
            Assert.That(resultadoAcao.IsSuccess, Is.False);
            Assert.That(contatoExiste, Is.False);
        }

        [Test]
        public async Task AtivarDesativar_Ativar_Sucesso()
        {
            // expected
            var resultadoEsperado = 200;
            var mensagemEsperada = "Contato ativado com sucesso!";

            // arrange
            var contatoId = Guid.NewGuid();
            var contato = new Contato { Id = contatoId, Status = false };

            PopularBancoDeDados(new List<Contato> { contato });

            // act
            var result = await _contatoService.Ativar(contatoId);

            // assert
            Assert.That(result.StatusCode, Is.EqualTo(resultadoEsperado));
            Assert.That(result.Message, Is.EqualTo(mensagemEsperada));
            Assert.That(result.Result, Is.Not.Null);
            Assert.That(result.IsSuccess, Is.True);
            Assert.That(contato.Status, Is.True);
        }


        [Test]
        public async Task AtivarDesativar_Desativar_Sucesso()
        {
            // expected
            var resultadoEsperado = 200;
            var mensagemEsperada = "Contato desativado com sucesso!";

            // arrange
            var contatoId = Guid.NewGuid();
            var contato = new Contato { Id = contatoId, Status = true }; // Contato inicialmente ativado

            PopularBancoDeDados(new List<Contato> { contato });

            // act
            var result = await _contatoService.Ativar(contatoId);

            // assert
            Assert.That(result.StatusCode, Is.EqualTo(resultadoEsperado));
            Assert.That(result.Message, Is.EqualTo(mensagemEsperada));
            Assert.That(result.Result, Is.Not.Null);
            Assert.That(result.IsSuccess, Is.True);
            Assert.That(contato.Status, Is.False); // Verifica se o contato foi corretamente desativado
        }

        [Test]
        public async Task AtivarDesativar_ContatoNaoExiste_Falha()
        {
            // expected
            var resultadoEsperado = 404;
            var mensagemEsperada = "Contato não encontrado!";

            // arrange
            var contatoId = Guid.NewGuid();

            // act
            var result = await _contatoService.Ativar(contatoId);

            // assert
            Assert.That(result.StatusCode, Is.EqualTo(resultadoEsperado));
            Assert.That(result.Message, Is.EqualTo(mensagemEsperada));
            Assert.That(result.Result, Is.Null);
            Assert.That(result.IsSuccess, Is.False);
        }

        [Test]
        public async Task Atualizar_Sucesso()
        {
            PopularBancoDeDados(ContatoMoq.ObterMoq_Para_AtualizarContato());
            // expected
            var resultadoEsperado = 200;
            var mensagemEsperada = "Contato alterado com sucesso!";

            // Arrange
            var updatedContato = await _context.Contatos.FirstOrDefaultAsync();
            var contato = _mapper.Map<AtualizarContatoVM>(updatedContato);
            contato.Nome = "David 5";

            // Act
            var result = await _contatoService.Atualizar(contato);
            var verificacao = await _context.Contatos.FirstAsync(p => p.Id == contato.Id);

            // Assert
            Assert.That(result.StatusCode, Is.EqualTo(resultadoEsperado));
            Assert.That(result.Message, Is.EqualTo(mensagemEsperada));
            Assert.That(result.Result, Is.Not.Null);
            Assert.That(result.IsSuccess, Is.True);
            Assert.That(verificacao.Nome, Is.EqualTo(contato.Nome));
        }

        [Test]
        public async Task Atualizar_ContatoNaoExiste_Falha()
        {
            //expected
            var resultadoEsperado = 404;
            var mensagemEsperada = "Contato não existe!";

            //arrange 
            var updateContato = new AtualizarContatoVM
            {
                Id = Guid.NewGuid(),
                DataNascimento = DateTime.Parse("05-10-1995"),
                Sexo = SexoEnum.Masculino,
                Nome = "David",
            };

            //act 
            var resultadoAcao = await _contatoService.Atualizar(updateContato);
            var contatoExiste = await _context.Contatos.AnyAsync(p => p.Id == updateContato.Id);

            //assert
            Assert.That(resultadoAcao.StatusCode, Is.EqualTo(resultadoEsperado));
            Assert.That(resultadoAcao.Message, Is.EqualTo(mensagemEsperada));
            Assert.That(resultadoAcao.Result, Is.Null);
            Assert.That(contatoExiste, Is.False);
            Assert.That(resultadoAcao.IsSuccess, Is.False);
        }

        [Test]
        public async Task Atualizar_IdadeInvalida_Falha()
        {
            //expected
            var resultadoEsperado = 400;
            var mensagemEsperada = "Contato não pode ser menor de idade!";

            //arrange
            var updatedContato = await _context.Contatos.FirstOrDefaultAsync();
            var contato = _mapper.Map<AtualizarContatoVM>(updatedContato);
            contato.DataNascimento = DateTime.Now; // Definir uma data de nascimento válida
            contato.Nome = "David 5";

            //act
            var resultadoAcao = await _contatoService.Atualizar(contato);
            var contatoAtualizado = await _context.Contatos.FirstOrDefaultAsync(p => p.Id == contato.Id);

            //assert	
            Assert.That(resultadoAcao.StatusCode, Is.EqualTo(resultadoEsperado));
            Assert.That(resultadoAcao.Message, Is.EqualTo(mensagemEsperada));
            Assert.That(resultadoAcao.Result, Is.Null);
            Assert.That(resultadoAcao.IsSuccess, Is.False);
            Assert.That(contatoAtualizado, Is.Not.Null);
            Assert.That(contatoAtualizado.Nome, Is.Not.EqualTo(contato.Nome));
        }

        [Test]
        public async Task Atualizar_DataInvalida_Falha()
        {
            //expected
            var resultadoEsperado = 400;
            var mensagemEsperada = "Não pode ser selecionada data maior ou igual que a atual!";

            // Arrange
            var updatedContato = await _context.Contatos.FirstOrDefaultAsync();
            var contato = _mapper.Map<AtualizarContatoVM>(updatedContato);
            contato.DataNascimento = DateTime.Now.AddDays(1); // Definir uma data futura inválida
            contato.Nome = "David 5";

            // Act
            var resultadoAcao = await _contatoService.Atualizar(contato);
            var contatoAtualizado = await _context.Contatos.FirstOrDefaultAsync(p => p.Id == contato.Id);

            // Assert
            Assert.That(resultadoAcao.IsSuccess, Is.False);
            Assert.That(resultadoAcao.StatusCode, Is.EqualTo(resultadoEsperado));
            Assert.That(resultadoAcao.Message, Is.EqualTo(mensagemEsperada));
            Assert.That(resultadoAcao.Result, Is.Null);
            Assert.That(contatoAtualizado, Is.Not.Null);
            Assert.That(contatoAtualizado.Nome, Is.Not.EqualTo(contato.Nome));
        }

        [Test]
        public async Task Remover_ContatoNaoExiste_Falha()
        {
            // expected
            var resultadoEsperado = 404;
            var mensagemEsperada = "Contato não encontrado!";

            // arrange
            var contatoId = Guid.NewGuid();

            // act
            var result = await _contatoService.Remover(contatoId);

            // assert
            Assert.That(result.StatusCode, Is.EqualTo(resultadoEsperado));
            Assert.That(result.Message, Is.EqualTo(mensagemEsperada));
            Assert.That(result.Result, Is.Null);
            Assert.That(result.IsSuccess, Is.False);
        }

        [Test]
        public async Task Remover_ContatoExiste_Sucesso()
        {
            // expected
            var resultadoEsperado = 200;
            var mensagemEsperada = "Contato removido com sucesso!";

            // arrange
            var contatoId = Guid.NewGuid();
            var contato = new Contato { Id = contatoId };

            PopularBancoDeDados(new List<Contato> { contato });

            // act
            var result = await _contatoService.Remover(contatoId);

            // assert
            Assert.That(result.StatusCode, Is.EqualTo(resultadoEsperado));
            Assert.That(result.Message, Is.EqualTo(mensagemEsperada));
            Assert.That(result.Result, Is.Null);
            Assert.That(result.IsSuccess, Is.True);
        }
    }
}
