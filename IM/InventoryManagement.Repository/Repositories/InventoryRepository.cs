using InventoryManagement.Core.DTOs;
using InventoryManagement.Core.Models;
using InventoryManagement.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagement.Repository.Repositories
{
    public class InventoryRepository : GenericRepository<Inventory>, IInventoryRepository
    {
        public InventoryRepository(DataContext context) : base(context)
        {

        }



        //public async Task<List<Inventory>> GetInventoryList(int companyId, int page, int pageSize)
        public async Task<List<Inventory>> GetInventoryList(FilteringParameters parameters, int page, int pageSize)
        {
            IQueryable<Inventory> query;

            query = _context.Inventories.AsQueryable();
            if (parameters.CompanyId != null)
            {
                query = query.Where(x => x.CompanyId == parameters.CompanyId).OrderByDescending(x => x.CreatedDate);
            }
            if (parameters.CategoryId != null)
            {
                query = query.Where(x => x.CategoryId == parameters.CategoryId).OrderByDescending(x => x.CreatedDate);
            }
            if (parameters.CategorySubId != null)
            {
                query = query.Where(x => x.CategorySubId == parameters.CategorySubId).OrderByDescending(x => x.CreatedDate);
            }
            if (parameters.BrandId != null)
            {
                query = query.Where(x => x.BrandId == parameters.BrandId).OrderByDescending(x => x.CreatedDate);
            }
            if (parameters.ModelId != null)
            {
                query = query.Where(x => x.ModelId == parameters.ModelId).OrderByDescending(x => x.CreatedDate);
            }
            if (parameters.Name != null)
            {
                query = query.Where(x => x.Name == parameters.Name).OrderByDescending(x => x.CreatedDate);
            }
            if (parameters.Barcode != null)
            {
                query = query.Where(x => x.Barcode == parameters.Barcode).OrderByDescending(x => x.CreatedDate);
            }
            if (parameters.SerialNumber != null)
            {
                query = query.Where(x => x.SerialNumber == parameters.SerialNumber).OrderByDescending(x => x.CreatedDate);
            }
            if (parameters.Mac != null)
            {
                query = query.Where(x => x.Mac == parameters.Mac).OrderByDescending(x => x.CreatedDate);
            }
            if (parameters.Imei != null)
            {
                query = query.Where(x => x.Imei == parameters.Imei).OrderByDescending(x => x.CreatedDate);
            }
            if (parameters.ResponsibleUser != null)
            {
                query = query.Where(x => x.Responsible == parameters.ResponsibleUser).OrderByDescending(x => x.CreatedDate);
            }
            if (parameters.Status != null)
            {
                query = query.Where(x => x.Status == parameters.Status).OrderByDescending(x => x.CreatedDate);
            }

            int totalCount = query.Count();


            var response = await query.Skip((pageSize * (page - 1)))
                .Take(pageSize)
                .Select(x => new Inventory()
                {

                    CompanyId = x.CompanyId,
                    CreatedDate = x.CreatedDate,
                    Barcode = x.Barcode,
                    BrandId = x.BrandId,
                    BusinessCode = x.BusinessCode,
                    CategoryId = x.CategoryId,
                    CategorySubId = x.CategorySubId,
                    Id = x.Id,
                    Imei = x.Imei,
                    InventoryDate = x.InventoryDate,
                    InvoiceDate = x.InvoiceDate,
                    Mac = x.Mac,
                    ModelId = x.ModelId,
                    Name = x.Name,
                    SerialNumber = x.SerialNumber,
                    Status = x.Status,
                    Responsible = x.Responsible,
                    UpdatedDate = x.UpdatedDate,
                    TotalCount = totalCount,
                }).ToListAsync();
            return response;






            //public async Task<List<Inventory>> GetInventoryList(int companyId, int page, int pageSize)
            //IQueryable<Inventory> query;
            //query = _context.Inventories
            //        .Where(x => x.CompanyId == companyId)
            //        .OrderByDescending(x => x.CreatedDate);

            //int totalCount = query.Count();

            //var response = await query.Skip((pageSize * (page - 1)))
            //    .Take(pageSize)
            //    .Select(x => new Inventory()
            //    {
            //        CompanyId = companyId, //parametre olarak companyId gelecek


            //        CreatedDate = x.CreatedDate,
            //        Barcode = x.Barcode,
            //        BrandId = x.BrandId,
            //        BusinessCode = x.BusinessCode,
            //        CategoryId = x.CategoryId,
            //        CategorySubId = x.CategorySubId,
            //        Id = x.Id,
            //        Imei = x.Imei,
            //        InventoryDate = x.InventoryDate,
            //        InvoiceDate = x.InvoiceDate,
            //        Mac = x.Mac,
            //        ModelId = x.ModelId,
            //        Name = x.Name,
            //        SerialNumber = x.SerialNumber,
            //        Status = x.Status,
            //        Responsible = x.Responsible,
            //        TotalCount = totalCount,
            //        UpdatedDate = x.UpdatedDate
            //    }).ToListAsync();
            //return response;
        }
    }
}
