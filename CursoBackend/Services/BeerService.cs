using CursoBackend.DTOs;
using CursoBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace CursoBackend.Services
{
    public class BeerService : ICommonService<BeerDto, BeerInsertDto, BeerUpdateDto>
    {
        private StoreContext _context;

        public BeerService(StoreContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<BeerDto>> Get() =>
            await _context.Beers.Select(x => new BeerDto
            {
                Id = x.BeerID,
                Name = x.Name,
                Alcohol = x.Alcohol,
                BrandID = x.BrandID
            }).ToListAsync();

        public async Task<BeerDto> GetById(int id)
        {
            var beer = await _context.Beers.FindAsync(id);

            if (beer != null)
            {
                var beerDto = new BeerDto
                {
                    Id = beer.BeerID,
                    Name = beer.Name,
                    Alcohol = beer.Alcohol,
                    BrandID = beer.BrandID
                };

                return beerDto;
            }

            return null;
        }

        public async Task<BeerDto> Add(BeerInsertDto beerInsertDto)
        {
            var beer = new Beer()
            {
                Name = beerInsertDto.Name,
                Alcohol = beerInsertDto.Alcohol,
                BrandID = beerInsertDto.BrandID
            };

            await _context.Beers.AddAsync(beer);
            await _context.SaveChangesAsync();

            var beerDto = new BeerDto
            {
                Id = beer.BeerID,
                Alcohol = beer.Alcohol,
                BrandID = beer.BrandID,
                Name = beer.Name,
            };

            return beerDto;
        }

        public async Task<BeerDto> Update(int id, BeerUpdateDto beerUpdateDto)
        {
            var beer = await _context.Beers.FindAsync(id);

            if (beer != null)
            {
                beer.Name = beerUpdateDto.Name;
                beer.Alcohol = beerUpdateDto.Alcohol;
                beer.BrandID = beerUpdateDto.BrandID;

                await _context.SaveChangesAsync();

                var beerDto = new BeerDto
                {
                    Id = beer.BeerID,
                    Name = beer.Name,
                    BrandID = beer.BrandID,
                    Alcohol = beer.Alcohol
                };

                return beerDto;
            }
          
            return null;
        }

        public async Task<BeerDto> Delete(int id)
        {
            var beer = await _context.Beers.FindAsync(id);

            if (beer != null)
            {
                var beerDto = new BeerDto
                {
                    Id = beer.BeerID,
                    Name = beer.Name,
                    BrandID = beer.BrandID,
                    Alcohol = beer.Alcohol
                };

                _context.Beers.Remove(beer);
                await _context.SaveChangesAsync();
               
                return beerDto;
            }

            return null;
        }
    }
}
