using CadastroUsuarios.Data;
using CadastroUsuarios.Models;
using CadastroUsuarios.Models.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CadastroUsuarios.Controllers
{
    public class FuncionariosController : Controller
    {
        private readonly MVCDataDbContext mvcDataDbContext;

        public FuncionariosController(MVCDataDbContext mvcDataDbContext)
        {
            this.mvcDataDbContext = mvcDataDbContext;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var funcionario = await mvcDataDbContext.Funcionarios.ToListAsync();
            return View(funcionario);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddFuncionarioViewModel addFuncionarioRequest)
        {
            var funcionario = new Funcionario()
            {
                Id = Guid.NewGuid(),
                Nome = addFuncionarioRequest.Nome,
                Email = addFuncionarioRequest.Email,
                Senha = addFuncionarioRequest.Senha,
                Salario = addFuncionarioRequest.Salario,
                Departamento = addFuncionarioRequest.Departamento,
                DataNascimento = addFuncionarioRequest.DataNascimento
            };

            await mvcDataDbContext.Funcionarios.AddAsync(funcionario);
            await mvcDataDbContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> View(Guid id)
        {
            var funcionario =await mvcDataDbContext.Funcionarios.FirstOrDefaultAsync(x => x.Id == id);

            if (funcionario != null)
            {
                var viewModel = new UpdateFuncionarioViewModel()
                {
                    Id = funcionario.Id,
                    Nome = funcionario.Nome,
                    Email = funcionario.Email,
                    Senha = funcionario.Senha,
                    Salario = funcionario.Salario,
                    Departamento = funcionario.Departamento,
                    DataNascimento = funcionario.DataNascimento
                };
                return await Task.Run(() =>View("View", viewModel));
            }
            
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> View(UpdateFuncionarioViewModel model)
        {
            var funcionario = await mvcDataDbContext.Funcionarios.FindAsync(model.Id);

            if (funcionario != null)
            {
                funcionario.Nome = model.Nome;
                funcionario.Email = model.Email;
                funcionario.Senha = model.Senha;
                funcionario.Salario = model.Salario;
                funcionario.DataNascimento = model.DataNascimento;
                funcionario.Departamento = model.Departamento;

                await mvcDataDbContext.SaveChangesAsync();

                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(UpdateFuncionarioViewModel model)
        {
            var funcionario = await mvcDataDbContext.Funcionarios.FindAsync(model.Id);

            if (funcionario != null)
            {
                mvcDataDbContext.Funcionarios.Remove(funcionario);
                await mvcDataDbContext.SaveChangesAsync();

                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
    }
}
