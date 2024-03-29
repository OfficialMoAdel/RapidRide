﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RapidRide;
using RapidRide.Entities;


namespace RapidRide.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalletController : ControllerBase
    {
        private readonly RapidRideDbContext _context;

        public WalletController(RapidRideDbContext context)
        {
            _context = context;
        }

        // GET: api/Wallet
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Wallet>>> GetWallets()
        {
            return await _context.Wallets.ToListAsync();
        }

        // GET: api/Wallet/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Wallet>> GetWallet(int id)
        {
            var wallet = await _context.Wallets.FindAsync(id);

            if (wallet == null)
            {
                return NotFound();
            }

            return wallet;
        }

        [HttpGet("getbyquery")]
        public ActionResult<IEnumerable<Wallet>> Get([FromQuery] int? userId, [FromQuery] bool? deposit, [FromQuery] bool? withdrawal, [FromQuery] float? balance, [FromQuery] int? walletId)
        {
            var walletsQuery = _context.Wallets.Include(w => w.User).Include(w => w.Deposits).Include(w => w.Withdrawals).AsQueryable();

            if (userId != null)
            {
                walletsQuery = walletsQuery.Where(w => w.UserId == userId);
            }

            if (deposit.HasValue && deposit.Value)
            {
                walletsQuery = walletsQuery.Where(w => w.Deposits.Any());
            }

            if (withdrawal.HasValue && withdrawal.Value)
            {
                walletsQuery = walletsQuery.Where(w => w.Withdrawals.Any());
            }

            if (balance.HasValue)
            {
                walletsQuery = walletsQuery.Where(w => w.Balance == balance);
            }

            if (walletId.HasValue)
            {
                walletsQuery = walletsQuery.Where(w => w.WalletId == walletId);
            }

            var wallets = walletsQuery.ToList();

            if (wallets == null || wallets.Count == 0)
            {
                return NotFound();
            }

            return wallets;
        }


        // PUT: api/Wallet/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutWallet(int id, Wallet wallet)
        {
            if (id != wallet.WalletId)
            {
                return BadRequest();
            }

            _context.Entry(wallet).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WalletExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Wallet
        [HttpPost]
        public async Task<ActionResult<Wallet>> PostWallet(Wallet wallet)
        {
            _context.Wallets.Add(wallet);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetWallet", new { id = wallet.WalletId }, wallet);
        }

        // DELETE: api/Wallet/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Wallet>> DeleteWallet(int id)
        {
            var wallet = await _context.Wallets.FindAsync(id);
            if (wallet == null)
            {
                return NotFound();
            }

            _context.Wallets.Remove(wallet);
            await _context.SaveChangesAsync();

            return wallet;
        }

        private bool WalletExists(int id)
        {
            return _context.Wallets.Any(e => e.WalletId == id);
        }
    }
}
