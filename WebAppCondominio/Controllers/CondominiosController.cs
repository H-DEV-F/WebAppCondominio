﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebAppCondominio.Data;
using WebAppCondominio.Models;

namespace WebAppCondominio.Controllers
{
    public class CondominiosController : Controller
    {
        private readonly WebAppCondominioContext _context;

        public CondominiosController(WebAppCondominioContext context)
        {
            _context = context;
        }

        // GET: Condominios
        public async Task<IActionResult> Index()
        {
            return View(await _context.Condominio.ToListAsync());
        }

        // GET: Condominios/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var condominio = await _context.Condominio
                .FirstOrDefaultAsync(m => m.CondominioID == id);
            if (condominio == null)
            {
                return NotFound();
            }

            return PartialView(condominio);
        }

        // GET: Condominios/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Condominios/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CondominioID,Nome,Bairro")] Condominio condominio)
        {
            if (ModelState.IsValid)
            {
                _context.Add(condominio);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(condominio);
        }

        // GET: Condominios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var condominio = await _context.Condominio.FindAsync(id);
            if (condominio == null)
            {
                return NotFound();
            }
            return View(condominio);
        }

        // POST: Condominios/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CondominioID,Nome,Bairro")] Condominio condominio)
        {
            if (id != condominio.CondominioID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(condominio);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CondominioExists(condominio.CondominioID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(condominio);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult PartialViewCreate([Bind("CondominioID,Nome,Bairro")] Condominio condominio)
        {
            if (ModelState.IsValid)
            {
                _context.Add(condominio);
                _context.SaveChanges();
            }
            return Json(condominio);
        }

        // GET: Condominios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var condominio = await _context.Condominio
                .FirstOrDefaultAsync(m => m.CondominioID == id);
            if (condominio == null)
            {
                return NotFound();
            }

            return View(condominio);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var condominio = await _context.Condominio.FindAsync(id);
            _context.Condominio.Remove(condominio);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CondominioExists(int id)
        {
            return _context.Condominio.Any(e => e.CondominioID == id);
        }
    }
}
