using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using CadastroClientes.Data;
using CadastroClientes.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata.Ecma335;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace CadastroClientes.Clink
{
    public class Clientes : Controller
    {
        private readonly AppDbContexto _context;

        public Clientes (AppDbContexto context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View("../Casinha/Index", await _context.tbClientes.ToListAsync());
        }

        // Método create (GET) que exibe a view de criação de clientes
        public IActionResult Criar()
        {
            return View();
        }

        public IActionResult Cadastrar()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                _context.tbClientes.Add(cliente);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Sucesso), new { acao = "cadastrado" });
                // return RedirectToAction(nameof(Cadastrado));
            }
            return View(cliente);
        }
        
        public async Task<IActionResult> Editar(int id)
        {
            var cliente = await _context.tbClientes.FindAsync(id);
            return View(cliente);
        }

        public async Task<IActionResult> Edit(Cliente cliente)
        {
            // var cliente = await _context.tbClientes.FindAsync(id);
            if (ModelState.IsValid && cliente != null)
            {
                _context.Update(cliente);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Sucesso), new { acao = "editado" });
                // return RedirectToAction(nameof(Editado));
            }
            return RedirectToAction(nameof(Erro), new {acao = "Edit"});
            // return View();
        }

        public async Task<IActionResult> Excluir(int id)
        {
            var cliente = await _context.tbClientes.FindAsync(id);
            if (ModelState.IsValid && cliente != null)
            {
                _context.Remove(cliente);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Sucesso), new { acao = "excluido" });
                // return RedirectToAction(nameof(Excluido));
            }
            return RedirectToAction(nameof(Erro), new {acao = "Excluir"});
            // return View();
        }

        public IActionResult Excluido()
        {
            return View("../Sucesso/Excluido");
        }

        public IActionResult Cadastrado()
        {
            return View("../Sucesso/Cadastrado");
        }

        public IActionResult Editado()
        {
            return View("../Sucesso/Editado");
        }

        public IActionResult Sucesso(string acao)
        {
            // ViewBag.Message = action;
            ViewBag.Acao = acao;
            return View();
        }

        public IActionResult Erro(string acao)
        {
            ViewData["Ação"] = acao;
            return View();
        }
    }
}