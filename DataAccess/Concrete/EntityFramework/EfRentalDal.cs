﻿using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfRentalDal : EfEntityRepositoryBase<Rental, RentCarDatabaseContext>, IRentalDal
    {
        public List<RentCarDetailsDto> GetCarRentalDetails()
        {
            using (RentCarDatabaseContext context = new RentCarDatabaseContext())
            {
                var result = from r in context.Rentals
                             join c in context.Cars on r.CarId equals c.CarId
                             join b in context.Brands on c.BrandId equals b.BrandId
                             join cu in context.Customers on r.CustomerId equals cu.CustomerId
                             join u in context.Users on cu.UserId equals u.UserId
                             select new RentCarDetailsDto
                             {
                                 RentalId = r.RentalId,
                                 CarId = b.BrandName,
                                 CustomerId = u.FirstName + " " + u.LastName,
                                 CompanyName= cu.CompanyName,
                                 RentDate = r.RentDate,
                                 ReturnDate=r.ReturnDate
                             };

                return result.ToList();
            }
        }
    }
}
