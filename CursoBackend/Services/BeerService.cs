using CursoBackend.DTOs;
using CursoBackend.Models;
using CursoBackend.Repository;
using Microsoft.EntityFrameworkCore;

namespace CursoBackend.Services
{
    public class BeerService : ICommonService<BeerDto, BeerInsertDto, BeerUpdateDto>
    {
        private IRepository<Beer> _beerRepository;

        public BeerService(IRepository<Beer> beerRepository)
        {
            _beerRepository = beerRepository;
        }

        public async Task<IEnumerable<BeerDto>> Get()
        {
            var beers = await _beerRepository.Get();
            return beers.Select(b => new BeerDto()
            {
                Id = b.BeerID,
                Name = b.Name,
                BrandID = b.BrandID,
                Alcohol = b.Alcohol
            });
        }

        public async Task<BeerDto> GetById(int id)
        {
            var beer = await _beerRepository.GetById(id);

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

            await _beerRepository.Add(beer);
            await _beerRepository.Save();

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
            var beer = await _beerRepository.GetById(id);

            if (beer != null)
            {
                beer.Name = beerUpdateDto.Name;
                beer.Alcohol = beerUpdateDto.Alcohol;
                beer.BrandID = beerUpdateDto.BrandID;

                _beerRepository.Update(beer);
                await _beerRepository.Save();

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
            var beer = await _beerRepository.GetById(id);

            if (beer != null)
            {
                var beerDto = new BeerDto
                {
                    Id = beer.BeerID,
                    Name = beer.Name,
                    BrandID = beer.BrandID,
                    Alcohol = beer.Alcohol
                };

                _beerRepository.Delete(beer);
                await _beerRepository.Save();
               
                return beerDto;
            }

            return null;
        }
    }
}
