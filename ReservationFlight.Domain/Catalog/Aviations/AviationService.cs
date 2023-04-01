using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using ReservationFlight.Data.EF;
using ReservationFlight.Data.Entities;
using ReservationFlight.Domain.Common;
using ReservationFlight.Model.Catalog.Aviations;
using ReservationFlight.Model.Common;
using ReservationFlight.Utility;
using System.Net.Http.Headers;

namespace ReservationFlight.Domain.Catalog.Aviations
{
    public class AviationService : IAviationService
    {
        private readonly ReservationFlightDbContext _context;
        private readonly IStorageService _storageService;

        public AviationService(
            ReservationFlightDbContext context, 
            IStorageService storageService)
        {
            _context = context;
            _storageService = storageService;
        }

        public async Task<string> Create(AviationCreateRequest request)
        {
            var aviation = new Aviation
            {
                AviationCode = request.AviationCode,
                Name = request.Name
            };

            //Save thumbnail image
            if (request.ImageAviation != null)
            {
                aviation.ImageAviation = await this.SaveFile(request.ImageAviation);
            }
            else
            {
                aviation.ImageAviation = Constants.DEFAUT_IMAGE_FILE;
            }
            _context.Aviations.Add(aviation);
            //trả về số lượng bản ghi
            await _context.SaveChangesAsync();
            return aviation.AviationCode;
        }

        public async Task<int> Delete(string aviationCode)
        {
            var aviation = await _context.Aviations.FirstOrDefaultAsync(x => x.AviationCode == aviationCode);
            if (aviation == null) throw new ReservationFlightException(
                string.Format(
                    Constants.ERR_NOT_EXIST,
                    aviationCode));

            var images = _context.Aviations.Where(x => x.AviationCode == aviationCode).FirstOrDefault();

            _context.Aviations.Remove(aviation);
            
            if (images != null && images.ImageAviation.Equals(Constants.DEFAUT_IMAGE_FILE) == false)
            {
                await _storageService.DeleteFileAsync(images.ImageAviation);
            }

            return await _context.SaveChangesAsync();
        }

        public async Task<List<AviationViewModel>> GetAll()
        {
            var query = from c in _context.Aviations
                        select new { c };

            return await query.Select(x => new AviationViewModel()
            {
                AviationCode = x.c.AviationCode,
                Name = x.c.Name,
                ImageAviation = x.c.ImageAviation
            }).ToListAsync();
        }

        public async Task<ApiResult<PagedResult<AviationViewModel>>> GetAviationsPaging(GetAviationsPagingRequest request)
        {
            var query = from a in _context.Aviations
                        select new { a };

            if (!string.IsNullOrEmpty(request.Keyword))
            {
                query = query.Where(x => x.a.Name.Contains(request.Keyword)
                 || x.a.AviationCode.ToString().Contains(request.Keyword));
            }

            //3. Paging
            int totalRow = await query.CountAsync();

            var data = await query.Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(x => new AviationViewModel()
                {
                    AviationCode = x.a.AviationCode,
                    Name = x.a.Name,
                    ImageAviation = _storageService.GetFileUrl(x.a.ImageAviation)
                }).ToListAsync();

            //4. Select and projection
            var pagedResult = new PagedResult<AviationViewModel>()
            {
                TotalRecords = totalRow,
                PageIndex = request.PageIndex,
                PageSize = request.PageSize,
                Items = data
            };
            return new ApiSuccessResult<PagedResult<AviationViewModel>>(pagedResult);
        }

        public async Task<AviationViewModel> GetById(string aviationCode)
        {
            var aviation = await _context.Aviations.FirstOrDefaultAsync(x => x.AviationCode == aviationCode);
            if (aviation == null) throw new ReservationFlightException(string.Format(
                    Constants.ERR_NOT_EXIST,
                    aviationCode));
            else
            {
                var aviationViewModel = new AviationViewModel
                {
                    AviationCode = aviation.AviationCode,
                    Name = aviation.Name,
                    ImageAviation = aviation.ImageAviation != null 
                        ? _storageService.GetFileUrl(aviation.ImageAviation) 
                            : _storageService.GetFileUrl(Constants.DEFAUT_IMAGE_FILE),
                };
                return aviationViewModel;
            }       
        }

        public async Task<int> Update(AviationUpdateRequest request)
        {
            var aviation = _context.Aviations.Where(x => x.AviationCode == request.AviationCode).FirstOrDefault();
            if (aviation == null) throw new ReservationFlightException(
                string.Format(
                    Constants.ERR_NOT_EXIST,
                    request.Name));

            aviation.AviationCode = request.AviationCode;
            aviation.Name = request.Name;

            var oldImage = aviation.ImageAviation;

            if (request.ImageAviation != null)
            {                
                aviation.ImageAviation = await this.SaveFile(request.ImageAviation);
                if ((string.IsNullOrEmpty(oldImage) == false) && (oldImage.Equals(Constants.DEFAUT_IMAGE_FILE) == false)) 
                    await _storageService.DeleteFileAsync(oldImage);
            }
            else
            {
                aviation.ImageAviation = Constants.DEFAUT_IMAGE_FILE;
            }

            return await _context.SaveChangesAsync();
        }

        public async Task<int> UpdatePatch(string aviationCode, JsonPatchDocument aviationModel)
        {
            var aviation = _context.Aviations.Where(x => x.AviationCode == aviationCode).FirstOrDefault();
            if (aviation == null) throw new ReservationFlightException(
                string.Format(
                    Constants.ERR_NOT_EXIST,
                    aviationCode));

            aviationModel.ApplyTo(aviation);

            return await _context.SaveChangesAsync();
        }

        private async Task<string> SaveFile(IFormFile file)
        {
            var originalFileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(originalFileName)}";
            await _storageService.SaveFileAsync(file.OpenReadStream(), fileName);
            return fileName;
        }
    }
}