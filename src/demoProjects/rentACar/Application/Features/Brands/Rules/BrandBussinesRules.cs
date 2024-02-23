﻿using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Persistence.Paging;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Brands.Rules;
public class BrandBussinesRules
{
    private readonly IBrandRepository _brandRepository;

    public BrandBussinesRules(IBrandRepository brandRepository)
    {
        _brandRepository = brandRepository;
    }
    public async Task BrandNameCanNotBeDuplicatedWhenInserted(string name)
    {
        IPaginate<Brand> result = await _brandRepository.GetListAsync(b=>b.Name ==  name);
        if(result.Items.Any()) 
        {
            throw new BusinessException("Brand name exist");
        }
    }
    public void BrandShouldExistWhenRequest(Brand brand)
    {
        if (brand == null)
        {
            throw new BusinessException("Requested Brand does not exist");
        }
    }
}
