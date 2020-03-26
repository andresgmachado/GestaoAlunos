using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GestaoAlunos.Data;
using GestaoAlunos.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GestaoAlunos.Controllers
{
    public class CursoController : Controller
    {
        private readonly EscolaContexto _context; 
        
        public CursoController(EscolaContexto contexto)
        {
            _context = contexto;
        }

        public async Task<IActionResult> Index()
        {
            return View( await _context.Cursos.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var Curso = await _context.Cursos.
                FirstOrDefaultAsync(x => x.CursoId == id);
            if (Curso.Equals(null))
            {
                return NotFound();
            }

            return View(Curso);

        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CurosId,Titulo,Creditos")] Curso curso)
        {
            if (ModelState.IsValid)
            {
                _context.Add(curso);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(curso);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if(id.Equals(null))
            {
                return NotFound();
            }
            var curso = await _context.Cursos.FindAsync(id);
            if(curso.Equals(null))
            {
                return NotFound();
            }
              
            return View(curso);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,[Bind("CursoId,Titulo,Creditoa")] Curso curso)
        {
            if (id != curso.CursoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(curso);
                    await _context.SaveChangesAsync();
                }
                catch(DbUpdateConcurrencyException)
                {
                    return NotFound();
                }
                return RedirectToAction(nameof(Index));
            }

            return View(curso);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id.Equals(null))
            {
                return NotFound();
            }

            var curso = await _context.Cursos.
                FirstOrDefaultAsync(x => x.CursoId == id);
            if (curso.Equals(null))
            {
                return NotFound();
            }

            return View(curso);
                
        }

        [HttpPost, ActionName("DeleteConfirmed")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var curso = await _context.Cursos.FindAsync(id);
            _context.Cursos.Remove(curso);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public bool CursoExistis(int id)
        {
            return _context.Cursos.Any(x => x.CursoId == id);
        }

    }
}